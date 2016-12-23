using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contract.Model
{
    public interface ISensorValue
    {
        //Int64 MeteringId { get; set; }
        object GetValue { get; }
    }
}
