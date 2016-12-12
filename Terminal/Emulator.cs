using System;
using Infrastructure.Contract.Model;

namespace Terminal
{
    public class Emulator
    {
        private IMetering _cur;

        public Emulator(IMetering start)
        {
            _cur = start;
        }

        public IMetering GetNext()
        {
            return null;
        }
    }
}
