using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace StudentAppMvc.Filter
{
    public class AuthenticationFilterAttribute : ActionFilterAttribute
    {
        public AuthenticationFilterAttribute()
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var identity = context.HttpContext.User.Identity;
            if (identity == null || !identity.IsAuthenticated)
            {
                //throw new Exception("login is required");
                context.Result = new RedirectResult("Identity/Account/Login");
            }
        }
    }
}
