using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contract.Model
{
    public interface ISensorValue
    {
        Guid SensorId { get; set; }
        object Value { get; set; }
    }
}
