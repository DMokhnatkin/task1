
using Infrastructure.Contract.Model;

namespace Server.Data.Repository
{
    internal interface IMeteringRepository
    {
        void SaveMetering(IMetering metering);

        IMetering GetLastMetering(string terminalId);
    }
}
