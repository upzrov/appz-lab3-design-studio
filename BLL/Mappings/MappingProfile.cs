using AutoMapper;
using BLL.DTOs;
using DAL.Models;

namespace BLL.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DesignService, DesignServiceDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(src =>
                    src.DesignService != null ? src.DesignService.Name : string.Empty));
            CreateMap<OrderDTO, Order>()
                .ForMember(dest => dest.DesignService, opt => opt.Ignore());
        }
    }
}