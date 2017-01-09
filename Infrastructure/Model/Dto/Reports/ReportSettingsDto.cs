using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using AutoMapper;
using Infrastructure.Model.DynamicProperties.Specialized;
using Infrastructure.Model.DynamicProperties.Specialized.Properties;
using Infrastructure.Model.Reports;

namespace Infrastructure.Model.Dto.Reports
{
    [DataContract]
    public class ReportSettingsDto
    {
        [DataMember]
        public string TerminalId { get; set; }

        [DataMember]
        public DateTime StartDateTime { get; set; }

        [DataMember]
        public DateTime EndDateTime { get; set; }

        [DataMember]
        public List<string> Properties { get; set; } = new List<string>();

        private static readonly IMapper MapperInstance;

        public static ReportSettingsDto Wrap(ReportSettings reportSettings)
        {
            return MapperInstance.Map<ReportSettingsDto>(reportSettings);
        }

        public static ReportSettings Unwrap(ReportSettingsDto reportSettingsDto)
        {
            return MapperInstance.Map<ReportSettings>(reportSettingsDto);
        }

        static ReportSettingsDto()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ReportSettings, ReportSettingsDto>()
                    // Map HashSet<ReportProperty> to List<string> 
                    .ForMember(
                        dto => dto.Properties, 
                        opt => opt.MapFrom(x => x.Properties.Select(t => t.Name).ToList()));
                cfg.CreateMap<ReportSettingsDto, ReportSettings>()
                    // Map List<string> to HashSet<ReportProperty>
                    .ForMember(
                        bo => bo.Properties,
                        opt => opt.MapFrom(
                            x => new HashSet<ReportProperty>(
                                x.Properties.Select(t => DynamicPropertyManagers.Reports.GetProperty(t)))));
            });
            MapperInstance = config.CreateMapper();
        }
    }
}
