using api.DTOs;
using api.Models;

namespace api.Services{
    public interface IGroupService{
        Task<IEnumerable<GroupReadDTO>> GetGroupsAsync();
        Task<GroupReadDTO?> GetGroupAsync(int id);
        Task<GroupReadDTO> PostGroupAsync(GroupCreateDTO groupDTO);
        Task<bool> DeleteGroupAsync(int id);
    }
}