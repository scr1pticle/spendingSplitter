using Microsoft.AspNetCore.Mvc;
using api.Services;
using api.DTOs;

namespace api.Controllers.Groups
{
    [Route("api/groups/{id}/Members")]
    [ApiController]
    public class GroupMembersController : ControllerBase
    {
        private readonly IMemberService _sv;
        public GroupMembersController(IMemberService sv)
        {
            _sv = sv;
        }

        [HttpPost]
        public async Task<ActionResult<MemberReadDTO>> AddMember(int id, MemberCreateDTO memberDTO){
            var member = await _sv.AddMemberAsync(id, memberDTO);
            if(member == null) return NotFound();

            return CreatedAtAction("GetMember", "Members", new { id = member.Id }, member);
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberReadDTO>>> GetMembers(int id){
            var members = await _sv.GetMembersAsync(id);
            if(members == null) return NotFound();
            return Ok(members);
        }
    }
}