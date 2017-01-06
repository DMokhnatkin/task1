using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;

namespace Server.Data.Repository
{
    internal class TerminalsRepository : ITerminalsRepository
    {
        private ServerDbContext _db = MyUnityContainer.Instance.Resolve<ServerDbContext>("db");

        public IEnumerable<string> GetTerminalIds()
        {
            return _db.Meterings.Select(x => x.TerminalId).Distinct();
        }
    }
}
