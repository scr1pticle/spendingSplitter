namespace api.Models
{
    public class Share
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; } = null!;
        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;
        public decimal Amount { get; set; }
    }
}