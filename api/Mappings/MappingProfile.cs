using api.DTOs;
using api.Models;
using AutoMapper;

namespace api.Mappings{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Group,  GroupReadDTO>();
            CreateMap<Member, MemberReadDTO>();
            CreateMap<Transaction, TransactionReadDTO>()
                .ForMember(dest => dest.PayerName, opt => opt.MapFrom(src => src.PayerMember.Name));
            CreateMap<Share, ShareReadDTO>()
                .ForMember(dest => dest.MemberName, opt => opt.MapFrom(src => src.Member.Name));
            CreateMap<ShareCreateDTO, Share>();
        }
    }
}
