using System.ServiceModel;

namespace Infrastructure.Contract
{
    [ServiceContract]
    public interface IPingAvailable
    {
        /// <summary>
        /// Check if service is alive
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool Ping();
    }
}
