using System.Collections.Specialized;

namespace Core.Interfaces;

public interface IConfigService
{
    int CaptchaExpireAfterMintues { get; }

    string CertificateViewUrl { get; }

    int SDADBillExpireAfterDays { get; }

    double JwtExpireAfterMinutes { get; }

    string JwtEncryptionKey { get; }

    string[] ApiKeys { get; }

    string[] AttachmentsImageExtensions { get; }

    string[] AttachmentsFilesExtensions { get; }

    int AttachmentsImageFileSizeInMB { get; }

    int AttachmentsFileSizeInMB { get; }

    string NationalAddressClientIPAddress { get; }

    int NationalAddressOperatorID { get; }

    string NationalAddressLang { get; }

    string AhwaalClientIPAddress { get; }

    int AhwaalOperatorID { get; }

    string AhwaalLang { get; }

    /// <summary>
    /// 966
    /// </summary>
    string OtpPrefix { get; }

    string FirmInfoClientIPAddress { get; }

    int FirmInfoOperatorID { get; }

    string KeepAliveURL { get; }

    int RecordsPageSize { get; }

    string HarLogFolderPath { get; }
    bool IsHealthLogEnabled { get; }

    string HealthEndPointName { get; }

    NameValueCollection LoadConfigValues(string sectionName);

    string[] CrInactiveStatuses { get; }
}
