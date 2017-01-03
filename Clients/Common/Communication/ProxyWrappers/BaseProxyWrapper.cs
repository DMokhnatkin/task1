using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Contract;

namespace Common.Communication.ProxyWrappers
{
    public class BaseProxyWrapper
    {
        protected readonly IPingAvailable _proxy;

        public int SleepTime { get; set; } = 1000;

        public bool IsActive { get; private set; } = false;

        /// <summary>
        /// Raised when proxy can't call some method on server
        /// </summary>
        public event Action Fault;

        /// <summary>
        /// Raised when proxy is connected to server (Ping method returned true)
        /// </summary>
        public event Action Connected;

        protected virtual void OnFault()
        {
            IsActive = false;
            Fault?.Invoke();
        }

        protected virtual void OnConnected()
        {
            IsActive = true;
            Connected?.Invoke();
        }

        public BaseProxyWrapper(IPingAvailable proxy)
        {
            _proxy = proxy;
        }

        /// <summary>
        /// Start attempts to connect to server (when success Connected event will be raised)
        /// </summary>
        public virtual async void StartPing()
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        var z = _proxy.Ping();
                        if (z)
                        {
                            OnConnected();
                            return;
                        }
                    }
                    catch (Exception)
                    {
                        OnFault();
                    }
                    Thread.Sleep(SleepTime);
                }
            });
        }
    }
}
