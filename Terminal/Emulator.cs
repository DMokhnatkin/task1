using System;
using Infrastructure.Contract.Model;
using Infrastructure.Contract.Model.SensorValue;
using Terminal.Model;
using Terminal.Model.SensorValue;

namespace Terminal
{
    class Emulator
    {
        private Mettering _cur;

        public Emulator(Mettering start)
        {
            _cur = start;
        }

        public Mettering GetNext(TimeSpan time)
        {
            var z = new Mettering();
            z.Time = new DateTime();
            z.Latitude = 56.2f;
            z.Longitude = 36.2f;
            z.AddSensorValue<IEngineSensorValue>(new EngineSensorValue() { IsTurnedOn = true });
            z.AddSensorValue<IMileageSensorValue>(new MileageSensorValue() { MileageKm = 80 });
            z.AddSensorValue<ISpeedSensorValue>(new SpeedSensorValue() { SpeedKmh = 60 });
            return z;
        }
    }
}
