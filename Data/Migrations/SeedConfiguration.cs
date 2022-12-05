using Data.Enums;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Migrations;

public static class SeedConfiguration
{
    private static readonly DateTime BaseDate = new DateTime(2022, 12, 1);
    private static int _profileSettingOptionInded = 0;

    private static void AddProfileSettingOption(this ModelBuilder builder, string profileSettingOption)
    {
        _profileSettingOptionInded--;
        builder.Entity<ProfileSettingOption>().HasData(new ProfileSettingOption()
        {
            Key = profileSettingOption,
            Id = _profileSettingOptionInded,
            CreateDate = BaseDate
        });
    }

    public static void Seed(ModelBuilder builder)
    {
        builder.Entity<User>().HasData(new User()
            {
                Email = "artimmerman@landstede.nl",
                CreateDate = BaseDate,
                Hash = "$2a$11$A1PAL2tcek6yMqg8VVVzauteuOFGnD1S4DoqlPP/Hf9ulBwfJTS8y", // 'testen' as password
                Salt = "$2a$11$FIhli04K3CDnTp4ObcYIb.",
                UserType = UserType.Moderator,
                TimeZoneId = "Africa/Abidjan",
                Id = -1,
                Name = "Arjan Timmerman"
            }
        );

        builder.AddProfileSettingOption("Description");
        builder.AddProfileSettingOption("Education");
        builder.AddProfileSettingOption("Goals");
        builder.AddProfileSettingOption("Experience");
        builder.AddProfileSettingOption("School");
        builder.AddProfileSettingOption("EducationLevel");
    }
}