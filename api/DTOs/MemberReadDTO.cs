namespace api.DTOs{
    public class MemberReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Balance { get; set; }
        public bool IsSelf { get; set; }
    }
}

