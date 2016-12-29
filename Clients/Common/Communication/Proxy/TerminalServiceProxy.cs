using System.Collections.Generic;
using System.ServiceModel;
using Infrastructure.Contract.Service;
using Infrastructure.Model;

namespace Common.Communication.Proxy
{
    public class TerminalServiceProxy : 
        ClientBase<ITerminalsService>,
        ITerminalsService
    {
        /// <inheritdoc />
        public List<TerminalStatus> GetCurStatus()
        {
            return Channel.GetCurStatus();
        }

        /// <inheritdoc />
        public bool IsAlive()
        {
            return Channel.IsAlive();
        }
    }
}
