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
            var request = context.HttpContext.Request;
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine("- Logged by Ming -");
            Console.WriteLine("-------------------------------------------------------------");
            Console.WriteLine(string.Format($"Action Method {request.Method} {request.Path} executed at {DateTime.Now}"));
            if (request.Method.Equals("POST") && request.Path.Equals("/MingStudent/Create"))
            {
                foreach (var key in request.Form.Keys)
                {
                    Console.WriteLine($"Added {key} with value \"{request.Form[key]}\"");
                }
            }
            else if (request.Method.Equals("POST") &&  request.Path.ToString().Contains("/MingStudent/Edit"))
            {
                foreach (var key in request.Form.Keys)
                {
                    Console.WriteLine($"Update {key} with value \"{request.Form[key]}\"");
                }
            }
        }
    }
}
