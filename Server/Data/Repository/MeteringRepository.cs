using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using Infrastructure.Contract.Model;
using Infrastructure.Model;
using Infrastructure.Model.Sensors;
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
                        var sv = _context.SensorValues.Add(Mapper.Map<SensorValueDAO>(met.Value));
                        _context.SaveChanges();
                        _context.MeteringSensorRelations.Add(new MeteringSensorValueRelationDAO()
                        {
                            MeteringId = m.Id,
                            SensorValueId = sv.Id,
                            SensorGuid = met.Key
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
            // TODO: combine in one query

            // Get last metering
            MeteringDAO lastMetering = _context.Meterings
                .Include(x => x.SensorValueRelations)
                .OrderByDescending(x => x.Time)
                .FirstOrDefault(x => x.TerminalId == terminalId);

            // Get sensor vals for last metering
            var sensorVals = _context.MeteringSensorRelations
                .Include(x => x.Metering)
                .Include(x => x.SensorValue)
                .Where(x => x.Metering.Id == lastMetering.Id)
                .ToDictionary(
                    k => k.SensorGuid,
                    v => (ISensorValue)DAOHelper.ByteArrayToObject(
                            SensorsRep.GetSensorType(v.SensorGuid),
                            v.SensorValue.Value));

            // Map to Metering model
            Metering res = new Metering()
            {
                TerminalId = lastMetering.TerminalId,
                Latitude = lastMetering.Latitude,
                Longitude = lastMetering.Longitude,
                Time = lastMetering.Time,
                SensorValues = sensorVals
            };
            return res;
        }
    }
}
