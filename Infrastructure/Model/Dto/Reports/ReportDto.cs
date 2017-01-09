using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using AutoMapper;
using Infrastructure.Contract;
using Infrastructure.Model.DynamicProperties;
using Infrastructure.Model.DynamicProperties.Specialized;
using Infrastructure.Model.Reports;

namespace Infrastructure.Model.Dto.Reports
{
    [DataContract]
    public class ReportDto
    {
        [DataMember]
        public ReportSettingsDto ReportSettings { get; set; } = new ReportSettingsDto();

        [DataMember]
        public Dictionary<string, object> Values = new Dictionary<string, object>();

        private static readonly IMapper MapperInstance;

        public static ReportDto Wrap(Report report)
        {
            return MapperInstance.Map<ReportDto>(report);
        }

        public static Report Unwrap(ReportDto reportDto)
        {
            return MapperInstance.Map<Report>(reportDto);
        }

        static ReportDto()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Report, ReportDto>()
                    // Map inner ReportSettings to ReportSettingsDto
                    .ForMember(
                        dto => dto.ReportSettings,
                        opt => opt.MapFrom(x => ReportSettingsDto.Wrap(x.ReportSettings)))
                    // Map PropertyCollection to Dictionary<string, object>
                    .ForMember(
                        dto => dto.Values,
                        opt => opt.MapFrom(
                            x => x.Values.ToDictionary(
                                t => t.Key.Name,
                                t => t.Value)));
                cfg.CreateMap<ReportDto, Report>()
                    // Map ReportSettingsDto to ReportSettings
                    .ForMember(
                        bo => bo.ReportSettings,
                        opt => opt.MapFrom(x => ReportSettingsDto.Unwrap(x.ReportSettings)))
                    // Map Dictionary<string, object> to PropertyCollection
                    .ForMember(
                        bo => bo.Values,
                        opt => opt.MapFrom(
                            x => new PropertiesCollection(
                                x.Values.Select(selector => new KeyValuePair<Property, object>(
                                    DynamicPropertyManagers.Reports.GetProperty(selector.Key), 
                                    selector.Value)))));
            });
            MapperInstance = config.CreateMapper();
        }
    }
}
