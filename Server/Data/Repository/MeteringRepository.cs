using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Contract.Model;
using Infrastructure.Model;
using Infrastructure.Model.DynamicProperties;
using Infrastructure.Model.DynamicProperties.Specialized;
using Infrastructure.Model.DynamicProperties.Specialized.Properties;
using Server.Data.DAO;

namespace Server.Data.Repository
{
    internal class MeteringRepository : IMeteringRepository
    {
        private ServerDbContext _context =
            (ServerDbContext) MyUnityContainer.Instance.Resolve(typeof(ServerDbContext), "db");

        public void SaveMetering(IMetering metering)
        {
            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var m = _context.Meterings.Add(Mapper.Map<MeteringDAO>(metering));
                    _context.SaveChanges();
                    foreach (var met in metering.SensorValues)
                    {
                        var sv = _context.SensorValues.Add(new SensorValueDAO() {Value = DAOHelper.ObjectToByteArray(met.Value)});
                        _context.SaveChanges();
                        _context.MeteringSensorRelations.Add(new MeteringSensorValueRelationDAO()
                        {
                            MeteringId = m.Id,
                            SensorValueId = sv.Id,
                            PropertyName = met.Key.Name
                        });
                        _context.SaveChanges();
                    }
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    throw;
                }
            }
        }

        public async Task<IMetering> GetLastMetering(string terminalId)
        {
            // Get last metering
            MeteringDAO lastMetering = await _context.Meterings
                .Include(x => x.SensorValueRelations)
                .OrderByDescending(x => x.Time)
                .FirstOrDefaultAsync(x => x.TerminalId == terminalId);

            // Map to Metering model
            Metering res = new Metering()
            {
                TerminalId = lastMetering.TerminalId,
                Latitude = lastMetering.Latitude,
                Longitude = lastMetering.Longitude,
                Time = lastMetering.Time,
            };

            // Get sensor vals for last metering
            var sensorVals = await _context.MeteringSensorRelations
                .Include(x => x.Metering)
                .Include(x => x.SensorValue)
                .Where(x => x.Metering.Id == lastMetering.Id)
                .ToListAsync();
            foreach (var sv in sensorVals)
            {
                Property prop = DynamicPropertyManagers.Sensors.GetProperty(sv.PropertyName);
                res.SensorValues.SetValue(prop, DAOHelper.ByteArrayToObject(prop.TypeOfValue, sv.SensorValue.Value));
            }
            return res;
        }

        private IQueryable<MeteringSensorValueRelationDAO> FilterQuery(
            DateTime start,
            DateTime end,
            string terminalId = null, 
            SensorProperty prop = null)
        {
            var res = _context.MeteringSensorRelations
                .Include(x => x.Metering)
                .Where(x => x.Metering.Time < end && x.Metering.Time > start);
            if (prop != null)
                res = res.Where(x => x.PropertyName == prop.Name);
            if (terminalId != null)
                res = res.Where(x => x.Metering.TerminalId == terminalId);
            return res.Include(x => x.SensorValue);
        }

        private async Task<List<MeteringSensorValueRelationDAO>> Filter(string terminalId, SensorProperty prop, DateTime start,
            DateTime end)
        {
            return await FilterQuery(start, end, terminalId, prop).ToListAsync();
        }

        /// <inheritdoc />
        public async Task<object> GetMaxPropertyValue(string terminalId, SensorProperty prop, DateTime start, DateTime end)
        {
            return (await Filter(terminalId, prop, start, end)).Max(selector => DAOHelper.ByteArrayToObject(prop.TypeOfValue, selector.SensorValue.Value));
        }

        public async Task<double> GetAvgPropertyValue(string terminalId, SensorProperty prop, DateTime start, DateTime end)
        {
            var t = FilterQuery(start, end, terminalId, prop);
            if (!t.Any())
                return 0;
            // Try cast to double
            if (typeof(double).IsAssignableFrom(prop.TypeOfValue))
                return
                    (await Filter(terminalId, prop, start, end)).Average(
                        selector => (double) DAOHelper.ByteArrayToObject(prop.TypeOfValue, selector.SensorValue.Value));
            // Try convert to double 
            return (await Filter(terminalId, prop, start, end))
                .Average(selector => Convert.ToDouble(DAOHelper.ByteArrayToObject(prop.TypeOfValue, selector.SensorValue.Value)));
        }

        public async Task<double> GetSumPropertyValue(string terminalId, SensorProperty prop, DateTime start, DateTime end)
        {
            // Try cast to double
            if (typeof(double).IsAssignableFrom(prop.TypeOfValue))
                return
                    (await Filter(terminalId, prop, start, end)).Sum(
                        selector => (double)DAOHelper.ByteArrayToObject(prop.TypeOfValue, selector.SensorValue.Value));
            // Try convert to double 
            return (await Filter(terminalId, prop, start, end))
                .Sum(selector => Convert.ToDouble(DAOHelper.ByteArrayToObject(prop.TypeOfValue, selector.SensorValue.Value)));
        }

        public async Task<double> GetLastFirstDifferencePropertyValue(string terminalId, SensorProperty prop, DateTime start, DateTime end)
        {
            var fltr = FilterQuery(start, end, terminalId, prop);
            if (!fltr.Any())
                return 0;
            var first = fltr.OrderBy(x => x.Metering.Time).First();
            var last = fltr.OrderByDescending(x => x.Metering.Time).First();

            // Try cast to double
            if (typeof(double).IsAssignableFrom(prop.TypeOfValue))
                return
                    (double) DAOHelper.ByteArrayToObject(prop.TypeOfValue, last.SensorValue.Value) -
                    (double) DAOHelper.ByteArrayToObject(prop.TypeOfValue, first.SensorValue.Value);
            // Try convert to double
            return
                Convert.ToDouble(DAOHelper.ByteArrayToObject(prop.TypeOfValue, last.SensorValue.Value)) -
                Convert.ToDouble(DAOHelper.ByteArrayToObject(prop.TypeOfValue, first.SensorValue.Value));
        }

        public async Task<List<Metering>> GetMeterings(string terminalId, DateTime start, DateTime end, SensorProperty prop)
        {
            return (await FilterQuery(start, end, terminalId, prop)
                .OrderBy(x => x.Metering.Time)
                .ToListAsync())
                .Select(x => new Metering()
                {
                    TerminalId = x.Metering.TerminalId,
                    Longitude = x.Metering.Longitude,
                    Latitude = x.Metering.Latitude,
                    Time = x.Metering.Time,
                    SensorValues = new PropertiesCollection(new []{new KeyValuePair<Property, object>(
                        DynamicPropertyManagers.Sensors.GetProperty(prop.Name), 
                        DAOHelper.ByteArrayToObject(prop.TypeOfValue, x.SensorValue.Value)), })
                })
                .ToList();
        }
    }
}
