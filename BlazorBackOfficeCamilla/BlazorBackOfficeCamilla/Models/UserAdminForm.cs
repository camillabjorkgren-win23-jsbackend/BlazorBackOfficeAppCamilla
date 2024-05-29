namespace BlazorBackOfficeCamilla.Models;

public class UserAdminForm
{

    public string UserId { get; set; } = null!;

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public bool IsAdmin { get; set; }
    public bool IsUser { get; set; }
    public bool IsCIO { get; set; }
    public bool IsSuperAdmin { get; set; }

}
