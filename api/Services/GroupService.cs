using Microsoft.EntityFrameworkCore;
using api.Models;
using api.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace api.Services{
    public class GroupService : IGroupService
    {
        private readonly SplitterContext _context;
        private readonly IMapper _mapper;

        public GroupService(SplitterContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<GroupReadDTO>> GetGroupsAsync(){
            return await _context.Groups
                .Select(g => new GroupReadDTO {
                    Id   = g.Id,
                    Name = g.Name,
                    Balance = g.Members
                        .Where(m => m.IsSelf)
                        .Select(m =>
                            m.Shares.Sum(s => s.Amount)
                            - m.Transactions.Sum(t => t.FullAmount)
                        )
                .FirstOrDefault()
            })
            .ToListAsync();
        }

        public async Task<GroupReadDTO?> GetGroupAsync(int id){
            var group = await _context.Groups.FindAsync(id);
            if(group == null) return null;
            return _mapper.Map<GroupReadDTO>(group);
        }

        public async Task<GroupReadDTO> PostGroupAsync(GroupCreateDTO createDTO){
            var group = new Group{
                Name = createDTO.Name
            };
            var member = new Member{
                Name = "You",
                IsSelf = true
            };
            group.Members.Add(member);
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            var readDTO  = _mapper.Map<GroupReadDTO>(group);

            return readDTO;
        }

        public async Task<bool> DeleteGroupAsync(int id){
            var group = await _context.Groups.FindAsync(id);
            if (group == null) return false;

            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}