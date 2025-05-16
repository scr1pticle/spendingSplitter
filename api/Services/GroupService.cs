using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.Services{
    public class GroupService : IGroupService
    {
        private readonly SplitterContext _context;

        public GroupService(SplitterContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Group>> GetGroupsAsync(){
            return await _context.Groups.ToListAsync();
        }

        public async Task<Group?> GetGroupAsync(int id){
            return await _context.Groups.FindAsync(id);
        }

        public async Task<Group> PostGroupAsync(GroupDTO groupDTO){
            var group = new Group{
                Name = groupDTO.Name
            };
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            return group;
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