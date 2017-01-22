using System;
using Common.Communication.Proxy;
using Infrastructure.Contract.Service;
using Infrastructure.Model.Dto.Reports;

namespace Common.Communication.ProxyWrappers
{
    public class ReportServiceProxyWrapper : BaseProxyWrapper, IReportService
    {
        /// <inheritdoc />
        public ReportServiceProxyWrapper(ReportServiceProxy proxy) : base(proxy)
        {
        }

        /// <inheritdoc />
        public ReportDto BuildReport(ReportSettingsDto settings)
        {
            try
            {
                return ((ReportServiceProxy)_proxy).BuildReport(settings);
            }
            catch (Exception)
            {
                OnFault();
                return null;
            }
        }

        /// <inheritdoc />
        public bool Ping()
        {
            try
            {
                return ((ReportServiceProxy)_proxy).Ping();
            }
            catch (Exception)
            {
                OnFault();
                return false;
            }
        }
    }
}
