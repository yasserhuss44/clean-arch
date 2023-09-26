
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters;


public class ValidateCaptchaAttribute : TypeFilterAttribute
{
    public ValidateCaptchaAttribute() : base(typeof(ValidateCaptchaFilter))
    { }

    private class ValidateCaptchaFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new NotImplementedException();
        }

        //private readonly ICaptchaService captchaService;

        //public ValidateCaptchaFilter(ICaptchaService captchaService) => this.captchaService = captchaService;

        //public void OnActionExecuting(ActionExecutingContext context)
        //{
        //    var captchaValidationRequestDto = context.ActionArguments
        //                                             .SingleOrDefault(p => p.Value is CaptchaValidationRequestDto).Value
        //                                             as CaptchaValidationRequestDto;

        //    captchaService.ValidateCaptcha(captchaValidationRequestDto);
        //}

        void IActionFilter.OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
