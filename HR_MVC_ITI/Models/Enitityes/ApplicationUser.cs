using Microsoft.AspNetCore.Identity;

namespace HR_MVC_ITI.Models.Enitityes;

public class ApplicationUser : IdentityUser
{

           public string FullName { get; set; } = string.Empty;
           public string Role { get; set; } = "Employee";
 

}
