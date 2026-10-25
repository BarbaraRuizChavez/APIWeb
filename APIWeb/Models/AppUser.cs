namespace APIWeb.Models;

public static class Roles
{
    public const string Admin = "Admin";
    public const string Supplier = "Supplier";
    public const string Customer = "Customer";
}

public partial class AppUser
{
    public int UserId { get; set; }
    public string Email { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string Role { get; set; } = null!;
    public string? PasswordHash { get; set; }
    public string? GoogleSubject { get; set; }
    public int? SupplierId { get; set; }
    public string? CustomerId { get; set; }
    public DateTime CreatedAt { get; set; }
}
