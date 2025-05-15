using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Models;
using System.Collections;
using api.Services;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly SplitterContext _context;
        private readonly IGroupService _groupService;

        public GroupsController(SplitterContext context, IGroupService groupService)
        {
            _context = context;
            _groupService = groupService;
        }

        // GET: api/Groups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Group>>> GetGroups()
        {
            return await _context.Groups.ToListAsync();
        }

        [HttpPost("{groupId}/members")]
        public async Task<ActionResult<Member>> AddMember(int groupId, MemberDTO memberDTO){
            var member = await _groupService.AddMemberAsync(groupId, memberDTO);
            if(member == null) return NotFound();

            return CreatedAtAction("GetMember", "Members", new { id = member.Id }, new MemberResponseDTO{
                Id = member.Id,
                Name = member.Name,
                GroupId = member.GroupId
            });
        }
        [HttpGet("{id}/members")]
        public async Task<ActionResult<IEnumerable<MemberResponseDTO>>> GetMembers(int id){
            var members = await _groupService.GetMembersAsync(id);
            if(members == null) return NotFound();
            return Ok(members);
        }

        // GET: api/Groups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Group>> GetGroup(int id)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group == null) return NotFound();

            return group;
        }

        // PUT: api/Groups/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroup(int id, Group group)
        {
            if (id != group.Id) return BadRequest();

            _context.Entry(group).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/Groups
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GroupDTO>> PostGroup(GroupDTO groupDTO)
        {
            var group = new Group{
                Name = groupDTO.Name
            };
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGroup), new { id = group.Id }, group);
        }

        // DELETE: api/Groups/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group == null) return NotFound();

            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        private bool GroupExists(int id)
        {
            return _context.Groups.Any(e => e.Id == id);
        }
    }
}
