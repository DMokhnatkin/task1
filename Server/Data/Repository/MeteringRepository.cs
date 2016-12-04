using System;
using AutoMapper;
using Infrastructure.Contract.Model;
using Infrastructure.DTO;
using Server.Data.DAO;

namespace Server.Data.Repository
{
    class MeteringRepository
    {
        ServerDbContext _context = new ServerDbContext();

        public void SaveMetering(IMetering metering)
        {
            var m = _context.Meterings.Add(Mapper.Map<MeteringDAO>(metering));
            _context.SaveChanges();
            foreach (var met in metering.SensorValues)
            {
                var sv = _context.SensorValues.Add(Mapper.Map<SensorValueDAO>(met.Value));
                _context.MeteringSensorRelations.Add(new MeteringSensorValueRelationDAO()
                {
                    MeteringId = m.Id,
                    SensorValueId = sv.Id,
                    SensorGuid = met.Key
                });
                _context.SaveChanges();
            }
        }
    }
}
