using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace AuthService.API.Common
{
    public enum Roles
    {
        Admin,
        User
    }
    public class Helper
    {
        private static int RandomNumber(int minValue, int maxValue)
        {
            Random random = new();
            return random.Next(minValue, maxValue);
        }
    }
}
