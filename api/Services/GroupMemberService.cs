using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.Services{
    public class GroupMemberService : IGroupMemberService
    {
        private readonly SplitterContext _context;

        public GroupMemberService(SplitterContext context)
        {
            _context = context;
        }

        public async Task<Member?> AddMemberAsync(int groupId, MemberDTO memberDTO){
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
            return member;
        }

        public async Task<IEnumerable<MemberResponseDTO>?> GetMembersAsync(int id){
            var group = await _context.Groups.Include(g => g.Members).FirstOrDefaultAsync(g => g.Id == id);
            if(group == null) return null;
            var members = group.Members;
            if(members.Count <= 0) return [];
            return group.Members.Select(member => new MemberResponseDTO
            {
                Id = member.Id,
                Name = member.Name,
                GroupId = member.GroupId
            }).ToList();
        }
    }
}