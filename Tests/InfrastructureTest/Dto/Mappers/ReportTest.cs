using Infrastructure.Model.Dto.Reports;
using Infrastructure.Model.DynamicProperties.Specialized;
using Infrastructure.Model.Reports;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InfrastructureTest.Dto.Mappers
{
    [TestClass]
    public class ReportTest
    {
        /// <summary>
        /// Test Report to ReportDto wrap
        /// </summary>
        [TestMethod]
        public void TestBoToDto()
        {
            Report z = new Report();
            z.ReportSettings.TerminalId = "test";

            z.Values.SetValue(DynamicPropertyManagers.Reports.AvgSpeed, 23.0f);
            z.Values.SetValue(DynamicPropertyManagers.Reports.Mileage, 45.0f);

            var dto = ReportDto.Wrap(z);
            Assert.IsTrue(dto.Values.ContainsKey(DynamicPropertyManagers.Reports.AvgSpeed.Name));
            Assert.IsTrue(dto.Values.ContainsKey(DynamicPropertyManagers.Reports.Mileage.Name));
            Assert.AreEqual(z.ReportSettings.TerminalId, dto.ReportSettings.TerminalId);
        }

        /// <summary>
        /// Test ReportDto to Report Unwrap
        /// </summary>
        [TestMethod]
        public void TestDtoToBo()
        {
            ReportDto dto = new ReportDto();
            dto.ReportSettings.TerminalId = "test";
            dto.Values.Add(DynamicPropertyManagers.Reports.AvgSpeed.Name, 123.0f);
            dto.Values.Add(DynamicPropertyManagers.Reports.Mileage.Name, 23.0f);

            var bo = ReportDto.Unwrap(dto);
            Assert.AreEqual(dto.ReportSettings.TerminalId, bo.ReportSettings.TerminalId);
            Assert.IsTrue(bo.Values.ContainsValue(DynamicPropertyManagers.Reports.AvgSpeed));
            Assert.IsTrue(bo.Values.ContainsValue(DynamicPropertyManagers.Reports.Mileage));
        }
    }
}
