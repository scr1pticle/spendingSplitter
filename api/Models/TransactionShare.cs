namespace api.Models
{
    public class TransactionShare
    {
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; } = null!;
        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;
        public double Amount { get; set; }
    }
}