namespace Core.Exceptions.Constants;

public enum ExceptionCodes
{
    CrmConnectionFailed = 1001,
    CrmOperationFailed = 1002,

    IntegrationConnectionFailed = 2001,
    Integration_OtpNotGenerated = 2002,
    Integration_OtpNotvalid = 2003,
    Integration_NationalAddress_Retrieval = 2004,
    Integration_Ahwaal_Retrieval = 2005,
    Integration_FirmInfo_Retrieval = 2006,

    NotFound = 3001,
    CaptchaNotValid = 3002,
    CaptchaGenerationError = 3003,
    QrGenerationError = 3004,
    AlreadyExist = 3005,

    FluentValidation = 4001,
    ModelStateValidation = 4002,
    XssViolation = 4003,

    Unhandled = 5001,
    Unhealthy = 5003,
    Forbidden = 6001,
    InvalidCredentials = 6002,
    DisabledRoute = 6003,
    ConstraintViolation = 6004,

    Questionnaire_NotEligible = 7002,
    SDAD_BillUnpaid = 7003,
    Disabled = 7006
}
