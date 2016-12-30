using System;
using System.Net.Mime;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Contract;

namespace Common.Communication
{
    /// <summary>
    /// Use this base class if you need Connected and Fault events for proxy.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseProxy<T> : ClientBase<T> 
        where T : class, IPingAvailable
    {
        public int SleepTime { get; set; } = 1000;

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
            Fault?.Invoke();
        }

        protected virtual void OnConnected()
        {
            Connected?.Invoke();
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
                        var z = Channel.Ping();
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
