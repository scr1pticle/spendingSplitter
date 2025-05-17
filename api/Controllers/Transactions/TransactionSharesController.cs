using Microsoft.AspNetCore.Mvc;
using api.Services;
using api.DTOs;

namespace api.Controllers.Groups.Transactions
{
    [Route("api/Transactions/{id}/Shares")]
    [ApiController]
    public class TransactionSharesController : ControllerBase
    {
        private readonly IShareService _sv;
        public TransactionSharesController(IShareService sv)
        {
            _sv = sv;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShareReadDTO>>> GetShares(int id){
            var shares = await _sv.GetSharesAsync(id);
            if(shares == null) return NotFound();
            return Ok(shares);
        }
        [HttpPost]
        public async Task<ActionResult<ShareReadDTO>> PostShare(int id, ShareCreateDTO createDTO){
            var shareReadDTO = await _sv.PostShareAsync(id, createDTO);
            if(shareReadDTO == null) return NotFound();

            return CreatedAtAction("GetShare", "Shares", new {id = shareReadDTO.Id}, shareReadDTO);
        }
    }
}