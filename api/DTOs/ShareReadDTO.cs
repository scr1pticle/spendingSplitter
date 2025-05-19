namespace api.DTOs
{
    public class ShareReadDTO
    {
        public int Id { get; set; }
        public string MemberName { get; set; } = null!;
        public decimal Amount { get; set; }
    }
}