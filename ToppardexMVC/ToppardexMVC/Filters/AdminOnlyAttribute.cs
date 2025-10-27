using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class AdminOnlyAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var rol = context.HttpContext.Session.GetString("Rol");
        if (rol != "Admin")
        {
            context.Result = new RedirectToActionResult("AccesoDenegado", "Home", null);
        }

        base.OnActionExecuting(context);
    }
}
