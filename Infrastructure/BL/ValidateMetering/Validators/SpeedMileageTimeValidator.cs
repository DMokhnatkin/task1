using System;
using System.Collections.Generic;
using System.Device.Location;
using Infrastructure.Contract.Model;
using Infrastructure.Contract.Model.SensorValue;
using Infrastructure.DTO;
using Infrastructure.DTO.SensorValue;

namespace Infrastructure.BL.ValidateMetering.Validators
{
    public class SpeedMileageTimeValidator : IMeteringValidator
    {
        private float _eps;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eps">Max error in m</param>
        public SpeedMileageTimeValidator(float eps)
        {
            _eps = eps;
        }

        public void Validate(IEnumerable<IMetering> meterings)
        {
            var prev = meterings.GetEnumerator();
            if (!prev.MoveNext()) return;
            var cur = meterings.GetEnumerator();
            if (!cur.MoveNext()) return;
            while (cur.MoveNext())
            {
                float curMileageVal, prevMileageVal;

                try
                {
                    curMileageVal =
                        ((IMileageSensorValue)cur.Current.SensorValues[SensorsRep.GetGuid<IMileageSensorValue>()]).MileageKm;
                    prevMileageVal =
                        ((IMileageSensorValue)prev.Current.SensorValues[SensorsRep.GetGuid<IMileageSensorValue>()]).MileageKm;
                }
                catch (KeyNotFoundException e)
                {
                    throw new ArgumentException(String.Format("MileageSensorValue must be provided for validation in {0}", typeof(SpeedMileageTimeValidator)));
                }

                float curSpeedVal, prevSpeedVal;

                try
                {
                    curSpeedVal =
                        ((ISpeedSensorValue)cur.Current.SensorValues[SensorsRep.GetGuid<ISpeedSensorValue>()]).SpeedKmh;
                    prevSpeedVal =
                        ((ISpeedSensorValue)prev.Current.SensorValues[SensorsRep.GetGuid<ISpeedSensorValue>()]).SpeedKmh;
                }
                catch (KeyNotFoundException e)
                {
                    throw new ArgumentException(String.Format("SpeedSensorValue must be provided for validation in {0}", typeof(SpeedMileageTimeValidator)));
                }

                var curTime = cur.Current.Time;
                var prevTime = prev.Current.Time;
                if (Math.Abs((curTime - prevTime).TotalHours * (0.5f * (curSpeedVal + prevSpeedVal)) - (Math.Abs(curMileageVal - prevMileageVal))) > (_eps / 1000.0f))
                    throw new ArgumentException("Validation in SpeedMileageTimeValidator was failed");
                prev.MoveNext();
            }
        }
    }
}
