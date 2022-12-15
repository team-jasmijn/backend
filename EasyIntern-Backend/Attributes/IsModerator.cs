using EasyIntern_Backend.Attributes.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EasyIntern_Backend.Attributes;

public class IsModeratorAttribute : TypeFilterAttribute
{
    public IsModeratorAttribute() : base(typeof(IsModeratorFilter))
    {
    }
}