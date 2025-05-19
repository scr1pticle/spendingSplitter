namespace api.DTOs{
    public class TransactionReadDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string PayerName { get; set; } = null!;
        public decimal FullAmount { get; set; }
        public ICollection<ShareReadDTO> Shares { get; set; } = null!;
    }
}