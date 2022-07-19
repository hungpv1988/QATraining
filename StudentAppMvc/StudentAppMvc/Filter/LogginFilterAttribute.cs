using Microsoft.AspNetCore.Mvc.Filters;

namespace StudentAppMvc.Filter
{
    public class LogginFilterAttribute : ActionFilterAttribute
    {
        public LogginFilterAttribute()
        { 
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var request = context.HttpContext.Request;
            if(request.Method.Equals("POST") && request.Path.ToString().Contains("/ThtrStudent"))
            {
                var identity = context.HttpContext.User.Identity;
                if (identity == null || !identity.IsAuthenticated)
                {
                    throw new Exception("login is required");
                }
            }
           
        }
    }
}
