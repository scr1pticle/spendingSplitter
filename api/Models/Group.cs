namespace api.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Member> Members { get; } = new List<Member>();
    }
}