namespace api.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int GroupId { get; set; }
        public Group Group { get; set; } = null!;
        public int PayerMemberId { get; set; }
        public Member PayerMember { get; set; } = null!;
        public double FullAmount { get; set; }

    }
}