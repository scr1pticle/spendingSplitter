namespace api.DTOs{
    public class GroupReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Balance { get; set; }
    }
}
