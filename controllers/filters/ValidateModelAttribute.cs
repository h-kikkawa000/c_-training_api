using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace jp.co.matisse.web.education.controllers.filters
{
    /// <summary>
    /// Modelバリデーションカスタムフィルター属性クラス
    /// </summary>
    /*
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var modelState = actionContext.ModelState;
            if (modelState.IsValid == false)
            {
                actionContext.Response = actionContext.Request
                            .CreateErrorResponse(HttpStatusCode.BadRequest, modelState);
            }
            base.OnActionExecuting(actionContext);
        }
    }

    */
    public class ValidateModelAttribute :ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }

}
