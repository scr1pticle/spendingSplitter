using api.DTOs;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _sv;
        public MembersController(IMemberService sv){
           _sv = sv;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MemberReadDTO>> GetMember(int id)
        {
            var member = await _sv.GetMemberAsync(id);
            if(member == null) return NotFound();
            return member;
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMember(int id)
        {
            if(!await _sv.RemoveMemberAsync(id)) return NotFound();
            return NoContent();
        }
    }
}