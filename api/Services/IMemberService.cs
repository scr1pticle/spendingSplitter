using api.DTOs;

namespace api.Services{
    public interface IMemberService{
        Task<MemberReadDTO?> AddMemberAsync(int groupId, MemberCreateDTO memberDTO);
        Task<IEnumerable<MemberReadDTO>?> GetMembersAsync(int id);
        Task<MemberReadDTO?> GetMemberAsync(int id);
        Task<bool> RemoveMemberAsync(int id);
    }
}
