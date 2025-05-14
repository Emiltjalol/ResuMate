using Microsoft.AspNetCore.Identity;
using ResuMate.Shared.Models;

namespace ResuMate.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public string County { get; set; } = string.Empty;
    

}

