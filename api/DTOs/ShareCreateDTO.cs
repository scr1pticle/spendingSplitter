namespace api.DTOs
{
    public class ShareCreateDTO
    {
        public int MemberId { get; set; }
        public int TransactionId { get; set; }
        public decimal Amount { get; set; }
    }
}