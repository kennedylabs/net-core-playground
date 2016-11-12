using AccountsWebsite.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;
using System.Threading.Tasks;

namespace AccountsWebsite.Infrastructure.Filters
{
    public class ApiConventionsFilterAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(
            ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
            else
            {
                var executedContext = await next.Invoke();

                var verb = context.HttpContext.Request.Method;
                var resultValue = (executedContext.Result as ObjectResult)?.Value;
                var exceptionResult = (executedContext.Exception as ActionResultException).GetResult();

                if (exceptionResult != null)
                {
                    context.Result = exceptionResult;
                    executedContext.Exception = null;
                }
                else if (verb == "PUT" && resultValue != null)
                {
                    context.Result = new NoContentResult();
                }
                else if (verb == "POST" && resultValue != null)
                {
                    var key = GetKey(resultValue);

                    if (key != null)
                    {
                        var location = context.HttpContext.Request.Path.Add(key.ToString());
                        context.Result = new CreatedResult(location, resultValue);
                    }
                }
            }
        }

        protected virtual object GetKey(object entity)
        {
            return entity.GetType()
                .GetTypeInfo()
                .GetProperty("Id")?
                .GetMethod?
                .Invoke(entity, null);
        }
    }
}
