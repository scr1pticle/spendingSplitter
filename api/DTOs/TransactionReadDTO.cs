namespace api.DTOs{
    public class TransactionReadDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int GroupId { get; set; }
        public int PayerMemberId { get; set; }
        public decimal FullAmount { get; set; }
    }
}

