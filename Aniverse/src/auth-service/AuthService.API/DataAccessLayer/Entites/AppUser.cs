using Microsoft.AspNetCore.Identity;

namespace AuthService.API.DataAccessLayer.Entites
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
