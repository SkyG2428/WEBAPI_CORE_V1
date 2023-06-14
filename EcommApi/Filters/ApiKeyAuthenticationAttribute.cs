using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EcommApi.Filters
{
    [AttributeUsage( validOn:AttributeTargets.Class  | AttributeTargets.Method)]
    public class ApiKeyAuthenticationAttribute : Attribute, IAsyncActionFilter
    {
        private const string myApiKey = "ApiKey";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            
            if(!context.HttpContext.Request.Headers.TryGetValue(myApiKey,out var potentialKey))
            {
                context.Result=new UnauthorizedResult();
                return;
            }
            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>(); //Api key get karne ke liye
            var apiKey = configuration.GetValue<string>(key: "ApiKey");
            if(!apiKey.Equals(potentialKey))
            {
                    context.Result=new UnauthorizedResult();
                return;
            }
            await next();
        }
    }
}
