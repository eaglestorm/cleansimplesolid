using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using CleanConnect.Common.Model.Errors;
using ServiceBase.Dto;

namespace ServiceBase
{
    public class ApiProfile: Profile
    {
        public ApiProfile()
        {
            CreateMap<ErrorCode, ErrorDto>().ForMember(x=>x.Description,opt=>opt.MapFrom(src=>src.GetInternalDescription()));
            CreateMap<Task, TaskDto>();
        }
    }
}