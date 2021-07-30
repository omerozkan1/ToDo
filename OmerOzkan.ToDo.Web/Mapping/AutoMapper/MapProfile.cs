using AutoMapper;
using OmerOzkan.ToDo.Dto.Dtos.AppUserDtos;
using OmerOzkan.ToDo.Dto.Dtos.DutyDtos;
using OmerOzkan.ToDo.Dto.Dtos.NotificationDtos;
using OmerOzkan.ToDo.Dto.Dtos.ReportDtos;
using OmerOzkan.ToDo.Dto.Dtos.UrgencyDtos;
using OmerOzkan.ToDo.Entities.Domains;

namespace OmerOzkan.ToDo.Web.Mapping.AutoMapper
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            #region AppUser
            CreateMap<AppUserSignUpDto, AppUser>().ReverseMap();
            CreateMap<AppUserDto, AppUser>().ReverseMap();
            CreateMap<AppUserLoginDto, AppUser>().ReverseMap();
            #endregion

            #region Duty
            CreateMap<DutyAddDto, Duty>().ReverseMap();
            CreateMap<DutyListDto, Duty>().ReverseMap();
            CreateMap<DutyUpdateDto, Duty>().ReverseMap();
            CreateMap<DutyListDto, Duty>().ReverseMap();
            #endregion

            #region Notification
            CreateMap<NotificationListDto, Notification>().ReverseMap();
            #endregion

            #region Report
            CreateMap<ReportAddDto, Report>().ReverseMap();
            CreateMap<ReportUpdateDto, Report>().ReverseMap();
            CreateMap<ReportFileDto, Report>().ReverseMap();
            #endregion

            #region Urgency
            CreateMap<UrgencyAddDto, Urgency>().ReverseMap();
            CreateMap<UrgencyListDto, Urgency>().ReverseMap();
            CreateMap<UrgencyUpdateDto, Urgency>().ReverseMap();
            #endregion
        }
    }
}
