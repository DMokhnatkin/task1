using System;
using System.Collections.Generic;
using System.ServiceModel;
using Infrastructure.Contract.Service;
using Infrastructure.Model;
using Infrastructure.Model.Dto;

namespace Common.Communication.Proxy
{
    public class TerminalServiceProxy :
        ClientBase<ITerminalsService>,
        ITerminalsService
    {
        /// <inheritdoc />
        public List<TerminalStatusDto> GetCurStatus()
        {
            return Channel.GetCurStatus();
        }

        /// <inheritdoc />
        public bool Ping()
        {
            return Channel.Ping();
        }
    }
}
