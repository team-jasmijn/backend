using EasyIntern_Backend.Attributes.Filters;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace EasyIntern_Backend.Attributes;

public class IsStudentAttribute : TypeFilterAttribute
{
    public IsStudentAttribute() : base(typeof(IsStudentFilter))
    {
    }
}