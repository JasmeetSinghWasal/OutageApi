public class User
{
    public Guid Id { get; set; }
    public string EntraObjectId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public required string Password { get; set; }
    public string Role { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }

}