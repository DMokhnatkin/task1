using System.Collections.Generic;

namespace Server.Data.Repository
{
    internal interface ITerminalsRepository
    {
        IEnumerable<string> GetTerminalIds();
    }
}
