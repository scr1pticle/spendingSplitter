using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Services;
using api.DTOs;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _sv;

        public GroupsController(IGroupService sv){
            _sv = sv;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupReadDTO>>> GetGroups()
        {
            var groups = await _sv.GetGroupsAsync();
            return Ok(groups);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GroupReadDTO>> GetGroup(int id)
        {
            var group = await _sv.GetGroupAsync(id);
            if (group == null) return NotFound();

            return group;
        }

        [HttpPost]
        public async Task<ActionResult<GroupReadDTO>> PostGroup(GroupCreateDTO createDTO)
        {
            var readDTO = await _sv.PostGroupAsync(createDTO);

            return CreatedAtAction(nameof(GetGroup), new { id = readDTO.Id }, readDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            if(!await _sv.DeleteGroupAsync(id)) return NotFound();
            return NoContent();
        }
    }
}
