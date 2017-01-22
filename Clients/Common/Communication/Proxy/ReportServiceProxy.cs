﻿using System.ServiceModel;
using System.Threading.Tasks;
using Infrastructure.Contract.Service;
using Infrastructure.Model.Dto.Reports;

namespace Common.Communication.Proxy
{
    public class ReportServiceProxy :
        ClientBase<IReportService>,
        IReportService
    {
        /// <inheritdoc />
        public Task<ReportDto> BuildReport(ReportSettingsDto settings)
        {
            return Channel.BuildReport(settings);
        }

        /// <inheritdoc />
        public bool Ping()
        {
            return Channel.Ping();
        }
    }
}
