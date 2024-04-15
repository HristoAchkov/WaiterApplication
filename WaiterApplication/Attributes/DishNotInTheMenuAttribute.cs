using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WaiterApplication.Core.Contracts;
using WaiterApplication.Extensions;

namespace WaiterApplication.Attributes
{
    public class DishNotInTheMenuAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            IMenuService? menuService = context.HttpContext.RequestServices.GetService<IMenuService>();

            if (menuService == null)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            if (menuService != null && menuService.DishExistsAsync(context.HttpContext.User.Id()).Result)
            {
                context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
        }
    }
}
