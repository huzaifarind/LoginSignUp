using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

public class AuthorizeLoggedInAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var username = context.HttpContext.Session.GetString("Username");

        if (string.IsNullOrEmpty(username))
        {
            context.Result = new RedirectToActionResult("Login", "Login", null);
        }

        base.OnActionExecuting(context);
    }
}
