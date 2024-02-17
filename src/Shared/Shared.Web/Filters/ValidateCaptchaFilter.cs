//using Microsoft.AspNetCore.Mvc.Filters;
//using Shared.Application.Captcha;
//using Shared.Application.Captcha.DTOs;

//namespace Shared.Web.Filters;
//public class ValidateCaptchaAttribute :
//    TypeFilterAttribute
//{
//    public ValidateCaptchaAttribute()
//        : base(typeof(ValidateCaptchaFilter)) { }

//    private class ValidateCaptchaFilter :
//        IActionFilter
//    {
//        private readonly ICaptchaService captchaService;

//        public ValidateCaptchaFilter(
//            ICaptchaService captchaService)
//            => this.captchaService = captchaService;

//        public void OnActionExecuting(
//            ActionExecutingContext context)
//        {
//            var captchaValidationRequestDto = context.ActionArguments
//                                                     .SingleOrDefault(p => p.Value is CaptchaValidationRequestDto).Value
//                                                     as CaptchaValidationRequestDto;

//            captchaService.ValidateCaptcha(captchaValidationRequestDto);
//        }

//        void IActionFilter.OnActionExecuted(ActionExecutedContext context)
//        {
//        }
//    }
//}