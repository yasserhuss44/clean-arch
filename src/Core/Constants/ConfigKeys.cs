namespace Core.Constants;

public static class ConfigKeys
{
    public static readonly string AuthorizationMatrix = "AuthorizationMatrix";

    public static readonly string FeatureFlags = "FeatureFlags";

    public static readonly string CaptchaExpireAfterMintues = "Captcha:ExpireAfterMintues";

    public static readonly string CertificateViewUrl = "Certificate:ViewUrl";

    public static readonly string SDADBillExpireAfterDays = "SDAD:BillExpireAfterDays";

    public static readonly string JwtExpireAfterMinutes = "JWT:ExpireAfterMinutes";

    public static readonly string JwtEncryptionKey = "JWT:EncryptionKey";

    public static readonly string ApiKeys = "ApiKeys";

    public static readonly string AttachmentsImageExtensions = "Attachments:ImageExtensions";

    public static readonly string AttachmentsFilesExtensions = "Attachments:FilesExtensions";

    public static readonly string AttachmentsImageFileSizeInMB = "Attachments:ImageFileSizeInMB";

    public static readonly string AttachmentsFileSizeInMB = "Attachments:FileSizeInMB";

    public static readonly string AllowedCors = "AllowedCors";

    public static readonly string IntegrationsCrmBaseUrl = "Integrations:Crm:BaseUrl";

    public static readonly string IntegrationsExternalBaseUrl = "Integrations:External:BaseUrl";

    public static readonly string IntegrationsInternalBaseUrl = "Integrations:Internal:BaseUrl";

    public static readonly string IntegrationsJobsBaseUrl = "Integrations:Jobs:BaseUrl";

    public static readonly string IntegrationsJobsApiKey = "Integrations:Jobs:XApiKey";

    public static readonly string IntegrationsExternalApiKey = "Integrations:External:XApiKey";

    public static readonly string IntegrationsInternalApiKey = "Integrations:Internal:XApiKey";

    public static readonly string CrmApiKey = "Integrations:Crm:XApiKey";

    public static readonly string NationalAddressClientIPAddress = "Integrations:External:NationalAddress:ClientIPAddress";

    public static readonly string NationalAddressOperatorID = "Integrations:External:NationalAddress:OperatorID";

    public static readonly string NationalAddressLang = "Integrations:External:NationalAddress:Lang";

    public static readonly string AhwaalClientIPAddress = "Integrations:External:Ahwaal:ClientIPAddress";

    public static readonly string AhwaalOperatorID = "Integrations:External:Ahwaal:OperatorID";

    public static readonly string AhwaalLang = "Integrations:External:Ahwaal:Lang";

    public static readonly string OtpPrefix = "Integrations:Internal:Otp:Prefix";

    public static readonly string FirmInfoClientIPAddress = "Integrations:External:FirmInfo:ClientIPAddress";

    public static readonly string FirmInfoOperatorID = "Integrations:External:FirmInfo:OperatorID";

    //public static readonly string LogPath = "LogPath";

    public static readonly string IsAPILogEnabled = "IsAPILogEnabled";

    public static readonly string IsHealthLogEnabled = "IsHealthLogEnabled";
    public static readonly string HealthEndointName = "HealthEndointName";

    public static readonly string KeepAliveURL = "KeepaliveURL";

    public static readonly string RecordsPageSize = "RecordsPageSize";

    public static readonly string HarLogFolderPath = "LogPath";

    public static readonly string ApiHost = "ApiHost";

    public static readonly string CrInactiveStatuses = "Integrations:Internal:CR:InactiveStatuses"; 
}
