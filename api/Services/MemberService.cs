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
                Name = memberDTO.Name,
                IsSelf = false
            };
            group.Members.Add(member);
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
            return _mapper.Map<MemberReadDTO>(member);
        }

        public async Task<IEnumerable<MemberReadDTO>?> GetMembersAsync(int id){
            var members = await _context.Members
            .Where(m => m.GroupId == id)
            .Select(m => new MemberReadDTO{
                Id = m.Id,
                Name = m.Name,
                IsSelf = m.IsSelf,
                Balance = 
                    m.Shares.Sum(s => s.Amount)
                    - m.Transactions
                        .Where(t => t.PayerMemberId == m.Id)
                        .Sum(t => t.FullAmount)
            })
            .ToListAsync();
            if(members.Count <= 0) return [];
            return members;
        }

        public async Task<MemberReadDTO?> GetMemberAsync(int id)
        {
            return await _context.Members
                .Where(m => m.Id == id)
                .Select(m => new MemberReadDTO {
                    Id     = m.Id,
                    Name   = m.Name,
                    IsSelf = m.IsSelf,
                    Balance =
                        m.Shares.Sum(s => s.Amount)
                        - m.Transactions
                            .Where(t => t.PayerMemberId == m.Id)
                            .Sum(t => t.FullAmount)
            })
            .SingleOrDefaultAsync();
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