namespace api.Models;

public class MemberResponseDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int GroupId { get; set; }
}