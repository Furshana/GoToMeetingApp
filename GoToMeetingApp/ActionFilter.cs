using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace GoToMeetingApp
{
    public class ActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("before execution");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("After Execution");
        }
    }
}
