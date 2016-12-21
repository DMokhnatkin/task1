using System;
using Infrastructure.Contract.Model;

namespace Infrastructure.Model
{
    public class TerminalStatus
    {
        public string TerminalId { get; set; }

        public IMetering LastMetering { get; set; }
    }
}
