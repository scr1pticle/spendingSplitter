using api.DTOs;
namespace api.Services
{
    public interface ITransactionService
    {
        Task<TransactionReadDTO?> AddTransactionAsync(int id, TransactionCreateDTO createDTO);
        Task<IEnumerable<TransactionReadDTO>?> GetTransactionsAsync(int id);
        Task<TransactionReadDTO?> GetTransactionAsync(int id);
        Task<bool> DeleteTransactionAsync(int id);
    }
}