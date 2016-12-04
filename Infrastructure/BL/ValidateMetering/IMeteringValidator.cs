using System.Collections.Generic;
using Infrastructure.Contract.Model;

namespace Infrastructure.BL.ValidateMetering
{
    public interface IMeteringValidator
    {
        void Validate(IEnumerable<IMetering> meterings);
    }
}
