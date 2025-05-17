using Microsoft.AspNetCore.Mvc;
using api.Services;
using api.DTOs;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _sv;

        public TransactionsController(ITransactionService sv)
        {
            _sv = sv;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionReadDTO>> GetTransaction(int id)
        {
            var transaction = await _sv.GetTransactionAsync(id);
            if(transaction == null) return NotFound();
            return Ok(transaction);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            if(!await _sv.DeleteTransactionAsync(id)) return NotFound();

            return NoContent();
        }
    }
}
