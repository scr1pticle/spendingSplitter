using api.DTOs;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SharesController : ControllerBase
    {
        private readonly IShareService _sv;
        public SharesController(IShareService sv)
        {
            _sv = sv;
        }

        [HttpGet]
        public async Task<ActionResult<ShareReadDTO>> GetShare(int id){
            var share = await _sv.GetShareAsync(id);
            if(share == null) return NotFound();
            return share;
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteShare(int id){
            if(!await _sv.RemoveShareAsync(id)) return NotFound();
            return NoContent();
        }
    }
}