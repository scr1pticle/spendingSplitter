using api.DTOs;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.Groups
{
    [Route("api/groups/{id}/Transactions")]
    [ApiController]
    public class GroupTransactionsController : ControllerBase
    {
        private readonly ITransactionService _sv;
        public GroupTransactionsController(ITransactionService sv)
        {
            _sv = sv;
        }

        [HttpPost]
        public async Task<ActionResult<TransactionReadDTO>> AddTransaction(int id, TransactionCreateDTO createDTO)
        {
            var transaction = await _sv.AddTransactionAsync(id, createDTO);
            if(transaction == null) return NotFound();

            return CreatedAtAction("GetTransaction", "Transactions", new { id = transaction.Id }, transaction);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionReadDTO>>> GetTransactions(int id)
        {
            var transactions = await _sv.GetTransactionsAsync(id);
            if(transactions == null) return NotFound();
            return Ok(transactions);
        }
    }
}