using Data.Models;

namespace Data.Helpers;

public class BCryptHelper
{
    public static void ConfigureUserPassword(User user, string password)
    {
        user.Salt = BCrypt.Net.BCrypt.GenerateSalt();
        user.Hash = BCrypt.Net.BCrypt.HashPassword(password + user.Salt);
    }
}