using System.Security.Cryptography.Pkcs;

namespace api.Models;

public class TransactionDTO
{
    public string? Title { get; set; }
    public int GroupId { get; set; }
    public int PayerMemberId { get; set; }
    public decimal FullAmount { get; set; }
}