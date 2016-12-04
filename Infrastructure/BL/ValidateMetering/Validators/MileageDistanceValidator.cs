using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Contract.Model;
using Infrastructure.DTO;
using Infrastructure.DTO.SensorValue;
using System.Device.Location;

namespace Infrastructure.BL.ValidateMetering.Validators
{
    /// <summary>
    /// Distance between meterings and mileage sensor value must be equal
    /// </summary>
    public class MileageDistanceValidator : IMeteringValidator
    {
        private float _eps;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eps">Max error in m</param>
        public MileageDistanceValidator(float eps)
        {
            _eps = eps;
        }

        /// <inheritdoc />
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
                        ((MileageSensorValueDTO)cur.Current.SensorValues[SensorsRep.GetGuid<MileageSensorValueDTO>()]).Mileage;
                    prevMileageVal =
                        ((MileageSensorValueDTO)prev.Current.SensorValues[SensorsRep.GetGuid<MileageSensorValueDTO>()]).Mileage;
                }
                catch (KeyNotFoundException e)
                {
                    throw new ArgumentException("MileageSensorValue must be provided for validation in MileageDistanceValidator");
                }
                var curGeoCoord = new GeoCoordinate(cur.Current.Latitude, cur.Current.Longitude);
                var prevGeoCoord = new GeoCoordinate(prev.Current.Latitude, prev.Current.Longitude);
                if (Math.Abs(curGeoCoord.GetDistanceTo(prevGeoCoord) - Math.Abs(prevMileageVal - curMileageVal) * 1000) > _eps)
                    throw new ArgumentException("Validation in MileageDistanceValidator was failed");
                prev.MoveNext();
            }
        }
    }
}
