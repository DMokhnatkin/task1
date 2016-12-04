using System.Collections.Generic;
using Infrastructure.Contract.Model;

namespace Infrastructure.BL.ValidateMetering
{
    public class MeteringValidateManager
    {
        private IEnumerable<IMeteringValidator> _validators;

        public MeteringValidateManager(IEnumerable<IMeteringValidator> validators)
        {
            _validators = validators;
        }

        public void Validate(IEnumerable<IMetering> meterings)
        {
            foreach (var validator in _validators)
            {
                validator.Validate(meterings);
            }
        }
    }
}
