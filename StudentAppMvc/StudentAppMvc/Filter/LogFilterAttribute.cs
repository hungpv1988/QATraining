using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace StudentAppMvc.Filter
{
    public class LogFilterAttribute : ActionFilterAttribute
    {
        public LogFilterAttribute()
        {
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("- Logged by Ming -");
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine(string.Format($"Action Method {context.HttpContext.Request.Method} {context.HttpContext.Request.Path} executed at {DateTime.Now}"));
            if (context.HttpContext.Request.Method.Equals("POST") && context.HttpContext.Request.Path.Equals("/MingStudent/Create"))
            {
                foreach (var key in context.HttpContext.Request.Form.Keys)
                {
                    Console.WriteLine($"Added {key} with value {context.HttpContext.Request.Form[key]}");
                }
            }
            else if (context.HttpContext.Request.Method.Equals("POST") &&  context.HttpContext.Request.Path.ToString().Contains("/MingStudent/Edit"))
            {
                foreach (var key in context.HttpContext.Request.Form.Keys)
                {
                    Console.WriteLine($"Update {key} with value {context.HttpContext.Request.Form[key]}");
                }
            }
        }
    }
}
