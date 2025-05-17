using Microsoft.EntityFrameworkCore;
using api.Models;
using api.DTOs;
using AutoMapper;

namespace api.Services{
    public class MemberService : IMemberService
    {
        private readonly SplitterContext _context;
        private readonly IMapper _mapper;

        public MemberService(SplitterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MemberReadDTO?> AddMemberAsync(int groupId, MemberCreateDTO memberDTO){
            var group = await _context.Groups.FindAsync(groupId);
            if(group == null) return null;
            Member member = new()
            {
                Group = group,
                Name = memberDTO.Name
            };
            group.Members.Add(member);
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
            return _mapper.Map<MemberReadDTO>(member);
        }

        public async Task<IEnumerable<MemberReadDTO>?> GetMembersAsync(int id){
            var group = await _context.Groups.Include(g => g.Members).FirstOrDefaultAsync(g => g.Id == id);
            if(group == null) return null;
            var members = group.Members;
            if(members.Count <= 0) return [];
            return _mapper.Map<IEnumerable<Member>, IEnumerable<MemberReadDTO>>(members);
        }

        public async Task<MemberReadDTO?> GetMemberAsync(int id){
            var member = await _context.Members.FindAsync(id);
            if(member == null) return null;
            return _mapper.Map<MemberReadDTO>(member);
        }

        public async Task<bool> RemoveMemberAsync(int id){
            var member = await _context.Members.FindAsync(id);
            if(member == null) return false;
            _context.Members.Remove(member);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}