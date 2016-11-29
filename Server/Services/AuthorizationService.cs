using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFServer.Services
{
    class AuthorizationService : IAuthorizationService
    {
        /// <summary>
        /// For each terminal id stores last Login time
        /// </summary>
        private Dictionary<string, DateTime> _sessions = new Dictionary<string, DateTime>();

        public bool IsLogged(string terminalId)
        {
            return _sessions.ContainsKey(terminalId);
        }

        public void Login(string terminalId)
        {
            _sessions[terminalId] = DateTime.Now;
        }
    }
}
