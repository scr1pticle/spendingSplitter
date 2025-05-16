using api.Models;

namespace api.Services{
    public interface IGroupService{
        Task<IEnumerable<Group>> GetGroupsAsync();
        Task<Group?> GetGroupAsync(int id);
        Task<GroupDTO> PostGroupAsync(GroupDTO groupDTO);
        Task<bool> DeleteGroupAsync(int id);
        Task<bool> GroupExistsAsync(int id);

    }
}