using System.ComponentModel;
using System.Linq.Expressions;
using Data.Enums;
using Data.Models;

namespace Data.Helpers;

public static class DynamicFiltersHelper
{
    /// <summary>
    /// generates a filter that can be put in the database that matches the properties of the student and the company
    /// </summary>
    /// <param name="student">The student with all of its profile settings</param>
    /// <param name="excludeKeys">The excluded keys. Defaults at Description and EducationLevel</param>
    /// <returns>The matching filter used in the Company table</returns>
    public static Expression<Func<User, bool>> GenerateMatchingFilterForCompany(User student, string[] excludeKeys = null)
    {
        if (student.UserType.HasFlag(UserType.Company))
            throw new InvalidEnumArgumentException("Student cannot be a company");

        excludeKeys ??= new[] { "EducationLevel", "Description" };

        Expression<Func<User, bool>> filter = e => (e.UserType == UserType.Company && !e.Flirts.Any(o => o.StudentId == student.Id && o.Status < FlirtStatus.Finished));

        foreach (ProfileSetting setting in student.ProfileSettings)
        {
            filter = filter.And((e => e.ProfileSettings.All(a => a.Key != setting.Key) ||
                                          (e.ProfileSettings.Any(o => excludeKeys.All(s => o.Key != s)
                                                                      && o.Key == setting.Key
                                                                      && o.Value.ToLower().Contains(setting.Value.ToLower())))));
        }

        return filter;

    }
}