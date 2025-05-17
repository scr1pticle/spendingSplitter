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
            CreateMap<Transaction, TransactionReadDTO>();
            CreateMap<Share, ShareReadDTO>();
            CreateMap<ShareCreateDTO, Share>();
        }
    }
}
