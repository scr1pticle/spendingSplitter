using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Models;
using api.Services;

namespace api.Controllers.Groups{
    [Route("api/groups/{id}/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase{
        private readonly IGroupMemberService _sv;
        public MembersController(SplitterContext context, IGroupMemberService groupService)
        {
            _sv = groupService;
        }

        [HttpPost]
        public async Task<ActionResult<Member>> AddMember(int groupId, MemberDTO memberDTO){
            var member = await _sv.AddMemberAsync(groupId, memberDTO);
            if(member == null) return NotFound();

            return CreatedAtAction("GetMember", "Members", new { id = member.Id }, new MemberResponseDTO{
                Id = member.Id,
                Name = member.Name,
                GroupId = member.GroupId
            });
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberResponseDTO>>> GetMembers(int id){
            var members = await _sv.GetMembersAsync(id);
            if(members == null) return NotFound();
            return Ok(members);
        }
    }
}