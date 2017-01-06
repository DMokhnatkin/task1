using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common.Communication.Proxy;
using Infrastructure.Contract.Service;
using Infrastructure.Model;
using Infrastructure.Model.Dto;

namespace Common.Communication.ProxyWrappers
{
    public class TerminalServiceProxyWrapper : BaseProxyWrapper, ITerminalsService
    {
        public TerminalServiceProxyWrapper(TerminalServiceProxy proxy) : base(proxy)
        {
        }

        /// <inheritdoc />
        public bool Ping()
        {
            try
            {
                return ((TerminalServiceProxy)_proxy).Ping();
            }
            catch (Exception)
            {
                OnFault();
                return false;
            }
        }

        /// <inheritdoc />
        public List<TerminalStatusDto> GetCurStatus()
        {
            try
            {
                return ((TerminalServiceProxy)_proxy).GetCurStatus();
            }
            catch (Exception e)
            {
                OnFault();
                return new List<TerminalStatusDto>();
            }
        }
    }
}
