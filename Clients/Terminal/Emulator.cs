using System;
using Infrastructure.Model;
using Infrastructure.Model.Sensors.Types;

namespace Terminal
{
    static class Emulator
    {
        public static Metering GetRandom(float maxTimeOffsetMs = 10E4f, float maxSpeedKmh = 90f, float maxMileageKm = 500f)
        {
            Random rand= new Random();
            var res = new Metering()
            {
                Latitude = (float) rand.NextDouble()*180 - 90,
                Longitude = (float) rand.NextDouble()*360 - 180,
                Time = DateTime.Now.AddMilliseconds(rand.NextDouble()*maxTimeOffsetMs)
            };
            SensorValuesHelper.AddSensorValue(res, new SpeedSensorValue() { SpeedKmh = (float)rand.NextDouble() * maxSpeedKmh});
            SensorValuesHelper.AddSensorValue(res, new MileageSensorValue() { MileageKm = (float)rand.NextDouble() * maxMileageKm});
            SensorValuesHelper.AddSensorValue(res, new EngineSensorValue() { IsTurnedOn = rand.Next() % 2 == 0 });

            return res;
        }

        public static Metering GetNext(Metering prev, TimeSpan dTime, float speed)
        {
            Metering next = new Metering();

            // Mileage
            float dMil = speed * (float) dTime.TotalHours;

            //Earth’s radius, sphere
            float R = 6378137;

            //offsets in meters
            float q = (float)(new Random()).NextDouble();
            float dn = q * dMil * 1000;
            float de = (1 - q) * dMil * 1000;

            //Coordinate offsets in radians
            float dLat = dn/R;
            float dLon = (float)(de/(R*Math.Cos(Math.PI*prev.Latitude/180)));

            //OffsetPosition, decimal degrees
            float latO = prev.Latitude + dLat*180/(float)Math.PI;
            float lonO = prev.Longitude + dLon*180/(float)Math.PI;

            next.Latitude = latO;
            next.Longitude = lonO;
            next.Time = prev.Time + dTime;

            SensorValuesHelper.AddSensorValue(next, new SpeedSensorValue() { SpeedKmh = speed });
            SensorValuesHelper.AddSensorValue(next, new MileageSensorValue()
            {
                MileageKm = SensorValuesHelper.GetSensorValue<MileageSensorValue>(prev).MileageKm + dMil
            });
            SensorValuesHelper.AddSensorValue(next, new EngineSensorValue() { IsTurnedOn = speed > 0 });
            return next;
        }
    }
}
