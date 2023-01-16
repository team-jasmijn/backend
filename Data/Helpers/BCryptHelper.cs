using Data.Models;

namespace Data.Helpers;

public class BCryptHelper
{
    public static void ConfigureUserPassword(User user, string password)
    {
        user.Salt = BCrypt.Net.BCrypt.GenerateSalt();
        user.Hash = BCrypt.Net.BCrypt.HashPassword(password + user.Salt);
    }

    public static bool ValidatePassword(User user, string password)
    {
        if (user == null) return false;

        return BCrypt.Net.BCrypt.Verify(password + user.Salt, user.Hash);
    }
}