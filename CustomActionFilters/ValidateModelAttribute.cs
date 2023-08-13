using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebSampleApplicationAPI.CustomActionFilters
{
    public class ValidateModelAttribute:ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext Context)
        {
            if (Context.ModelState.IsValid == false) 
            {
                Context.Result=new BadRequestResult();
            }
        }
    }
}
