namespace api.DTOs
{
    public class TransactionCreateDTO
    {
        public string? Title { get; set; }
        public int PayerMemberId { get; set; }
        public decimal FullAmount { get; set; }
        public string SplitType { get; set; } = null!;
        public ICollection<ShareCreateDTO> Shares { get; set; } = new List<ShareCreateDTO>();
    }
}