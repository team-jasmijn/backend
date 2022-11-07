using EasyIntern_Backend.Attributes.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EasyIntern_Backend.Attributes;

public class IsCompanyAttribute : TypeFilterAttribute
{
    public IsCompanyAttribute() : base(typeof(IsCompanyFilter))
    {
    }
}