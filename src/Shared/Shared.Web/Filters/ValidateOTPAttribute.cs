//using Microsoft.AspNetCore.Mvc.Filters;
//using Shared.Application.Otp;
//using Shared.Application.Otp.DTOs;

//namespace Shared.Web.Filters;

//public class ValidateOTPAttribute :
//    TypeFilterAttribute
//{
//    public ValidateOTPAttribute()
//        : base(typeof(ValidateOTPFilter)) { }

//    private class ValidateOTPFilter :
//        IAsyncActionFilter
//    {
//        private readonly IOtpService otpService;

//        public ValidateOTPFilter(
//            IOtpService otpService)
//            => this.otpService = otpService;

//        public async Task OnActionExecutionAsync(
//            ActionExecutingContext context ,
//            ActionExecutionDelegate next)
//        {
//            var otpValidationRequestDto = context.ActionArguments
//                                                 .SingleOrDefault(p => p.Value is OtpValidationRequestDto)
//                                                 .Value as OtpValidationRequestDto;

//            await otpService.ValidateOtp(
//                otpValidationRequestDto ,
//                default);
            
//            await next();
//        }
//    }
//}