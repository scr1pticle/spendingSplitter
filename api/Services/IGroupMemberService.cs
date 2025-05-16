using Microsoft.AspNetCore.Mvc;
using api.Models;

namespace api.Services{
    public interface IGroupMemberService{
        Task<Member?> AddMemberAsync(int groupId, MemberDTO memberDTO);
        Task<IEnumerable<MemberResponseDTO>?> GetMembersAsync(int id);
    }
}
