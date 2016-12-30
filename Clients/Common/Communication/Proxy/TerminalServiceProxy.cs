using System;
using System.Collections.Generic;
using System.ServiceModel;
using Infrastructure.Contract.Service;
using Infrastructure.Model;

namespace Common.Communication.Proxy
{
    public class TerminalServiceProxy :
        BaseProxy<ITerminalsService>,
        ITerminalsService
    {
        /// <inheritdoc />
        public List<TerminalStatus> GetCurStatus()
        {
            try
            {
                return Channel.GetCurStatus();
            }
            catch (Exception)
            {
                OnFault();
                throw;
            }
        }

        /// <inheritdoc />
        public bool Ping()
        {
            try
            {
                return Channel.Ping();
            }
            catch (Exception)
            {
                OnFault();
                throw;
            }
        }
    }
}
