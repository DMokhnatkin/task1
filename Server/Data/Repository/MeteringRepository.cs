using System;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using Infrastructure.Contract.Model;
using Infrastructure.Model;
using Infrastructure.Model.DynamicProperties;
using Infrastructure.Model.DynamicProperties.Specialized;
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

        public IMetering GetLastMetering(string terminalId)
        {
            // Get last metering
            MeteringDAO lastMetering = _context.Meterings
                .Include(x => x.SensorValueRelations)
                .OrderByDescending(x => x.Time)
                .FirstOrDefault(x => x.TerminalId == terminalId);


            // Map to Metering model
            Metering res = new Metering()
            {
                TerminalId = lastMetering.TerminalId,
                Latitude = lastMetering.Latitude,
                Longitude = lastMetering.Longitude,
                Time = lastMetering.Time,
            };

            // Get sensor vals for last metering
            var sensorVals = _context.MeteringSensorRelations
                .Include(x => x.Metering)
                .Include(x => x.SensorValue)
                .Where(x => x.Metering.Id == lastMetering.Id)
                .ToList();
            foreach (var sv in sensorVals)
            {
                Property prop = DynamicPropertyManagers.Sensors.GetProperty(sv.PropertyName);
                res.SensorValues.SetValue(prop, DAOHelper.ByteArrayToObject(prop.TypeOfValue, sv.SensorValue.Value));
            }
            return res;
        }
    }
}
