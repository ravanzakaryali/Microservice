using Microsoft.AspNetCore.Identity;

namespace AuthService.API.DataAccessLayer.Entites
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
