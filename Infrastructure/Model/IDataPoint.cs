using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model
{
    public interface IDataPoint
    {
        DateTime Time { get; set; }

        float Latitude { get; set; }
        float Longitude { get; set; }

        float Speed { get; set; }

        bool IsEngineEnable { get; set; }

        float Mileage { get; set; }
    }
}
