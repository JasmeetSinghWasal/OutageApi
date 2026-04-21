public class Customer
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public Guid CreatdByUserId { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }

}