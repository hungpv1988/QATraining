using Microsoft.AspNetCore.Mvc.Filters;

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
                Console.WriteLine("- Logged action -");
                Console.WriteLine("-------------------------------------------------------------");
                Console.WriteLine(string.Format($"Action Method {request.Method} {request.Path} executed at {DateTime.Now}"));
                if (request.Method.Equals("POST") && request.Path.ToString().IndexOf("Create") >= 0)
                {
                    foreach (var key in request.Form.Keys)
                    {
                        Console.WriteLine($"Added {key} with value \"{request.Form[key]}\"");
                    }
                }
                else if (request.Method.Equals("POST") && request.Path.ToString().IndexOf("Edit") >= 0)
                {
                    foreach (var key in request.Form.Keys)
                    {
                        Console.WriteLine($"Update {key} with value \"{request.Form[key]}\"");
                    }
                }
            }
        }
    }
