namespace api.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set;} = null!;
        public int GroupId { get; set; }
        public Group Group { get; set; } = null!;
        public ICollection<Share> Shares { get; set; } = new List<Share>();
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
        public bool IsSelf { get; set; }
    }
}