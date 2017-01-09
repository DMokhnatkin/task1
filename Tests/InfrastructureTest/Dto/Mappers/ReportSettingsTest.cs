using System;
using Infrastructure.Model.Dto.Reports;
using Infrastructure.Model.DynamicProperties.Specialized;
using Infrastructure.Model.Reports;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InfrastructureTest.Dto.Mappers
{
    [TestClass]
    public class ReportSettingsTest
    {
        /// <summary>
        /// Test ReportSettings to ReportSettingsDto wrap
        /// </summary>
        [TestMethod]
        public void TestBoToDto()
        {
            ReportSettings bo = new ReportSettings();
            bo.TerminalId = "test";
            bo.StartDateTime = new DateTime(1990,1, 2);
            bo.EndDateTime = new DateTime(1991, 1, 2, 3, 4, 5);
            bo.Properties.Add(DynamicPropertyManagers.Reports.AvgSpeed);
            bo.Properties.Add(DynamicPropertyManagers.Reports.MaxSpeed);

            var dto = ReportSettingsDto.Wrap(bo);
            Assert.AreEqual(bo.TerminalId, dto.TerminalId);
            Assert.AreEqual(bo.StartDateTime, dto.StartDateTime);
            Assert.AreEqual(bo.EndDateTime, dto.EndDateTime);
            Assert.IsTrue(dto.Properties.Contains(DynamicPropertyManagers.Reports.AvgSpeed.Name));
            Assert.IsTrue(dto.Properties.Contains(DynamicPropertyManagers.Reports.MaxSpeed.Name));
        }

        /// <summary>
        /// Test ReportSettingsDto to ReportSettings Unwrap
        /// </summary>
        [TestMethod]
        public void TestDtoToBo()
        {
            ReportSettingsDto dto = new ReportSettingsDto();
            dto.TerminalId = "test";
            dto.StartDateTime = new DateTime(1990, 1, 2, 3, 4, 5);
            dto.EndDateTime = new DateTime(1990, 5, 4, 3, 2, 1);
            dto.Properties.Add(DynamicPropertyManagers.Reports.AvgSpeed.Name);
            dto.Properties.Add(DynamicPropertyManagers.Reports.MaxSpeed.Name);

            var bo = ReportSettingsDto.Unwrap(dto);
            Assert.AreEqual(dto.TerminalId, bo.TerminalId);
            Assert.AreEqual(dto.StartDateTime, bo.StartDateTime);
            Assert.AreEqual(dto.EndDateTime, bo.EndDateTime);
            Assert.IsTrue(dto.Properties.Contains(DynamicPropertyManagers.Reports.AvgSpeed.Name));
            Assert.IsTrue(dto.Properties.Contains(DynamicPropertyManagers.Reports.MaxSpeed.Name));
        }
    }
}
