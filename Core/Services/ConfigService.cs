using Core.Constants;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Specialized;

namespace Core.Services;

public sealed class ConfigService : IConfigService, ISingltonService
{
    private readonly IConfiguration configuration;

    public ConfigService(IConfiguration configuration)
        => this.configuration = configuration;

    public int CaptchaExpireAfterMintues => configuration.GetValue<int>(ConfigKeys.CaptchaExpireAfterMintues);

    public bool IsHealthLogEnabled => configuration.GetValue<bool>(ConfigKeys.IsHealthLogEnabled);

    public string HealthEndPointName => configuration.GetValue<string>(ConfigKeys.HealthEndointName);

    public string CertificateViewUrl => configuration.GetValue<string>(ConfigKeys.CertificateViewUrl);

    public int SDADBillExpireAfterDays => configuration.GetValue<int>(ConfigKeys.SDADBillExpireAfterDays);

    public double JwtExpireAfterMinutes => configuration.GetValue<double>(ConfigKeys.JwtExpireAfterMinutes);

    public string JwtEncryptionKey => configuration.GetValue<string>(ConfigKeys.JwtEncryptionKey);

    public string[] ApiKeys => configuration.GetSection(ConfigKeys.ApiKeys).Get<string[]>();

    public string[] AttachmentsImageExtensions => configuration.GetSection(ConfigKeys.AttachmentsImageExtensions).Get<string[]>();

    public string[] AttachmentsFilesExtensions => configuration.GetSection(ConfigKeys.AttachmentsFilesExtensions).Get<string[]>();

    public int AttachmentsImageFileSizeInMB => configuration.GetSection(ConfigKeys.AttachmentsImageFileSizeInMB).Get<int>();

    public int AttachmentsFileSizeInMB => configuration.GetSection(ConfigKeys.AttachmentsFileSizeInMB).Get<int>();

    public string NationalAddressClientIPAddress => configuration.GetValue<string>(ConfigKeys.NationalAddressClientIPAddress);

    public int NationalAddressOperatorID => configuration.GetValue<int>(ConfigKeys.NationalAddressOperatorID);

    public string NationalAddressLang => configuration.GetValue<string>(ConfigKeys.NationalAddressLang);

    public string AhwaalClientIPAddress => configuration.GetValue<string>(ConfigKeys.AhwaalClientIPAddress);

    public int AhwaalOperatorID => configuration.GetValue<int>(ConfigKeys.AhwaalOperatorID);

    public string AhwaalLang => configuration.GetValue<string>(ConfigKeys.AhwaalLang);

    public string OtpPrefix => configuration.GetValue<string>(ConfigKeys.OtpPrefix);

    public string FirmInfoClientIPAddress => configuration.GetValue<string>(ConfigKeys.AhwaalClientIPAddress);

    public int FirmInfoOperatorID => configuration.GetValue<int>(ConfigKeys.AhwaalOperatorID);

    public int RecordsPageSize => configuration.GetValue<int>(ConfigKeys.RecordsPageSize , 20);

    public string KeepAliveURL => configuration.GetValue<string>(ConfigKeys.KeepAliveURL);

    public string HarLogFolderPath => configuration.GetValue<string>(ConfigKeys.HarLogFolderPath);

    public string[] CrInactiveStatuses => configuration.GetSection(ConfigKeys.CrInactiveStatuses).Get<string[]>();

    public NameValueCollection LoadConfigValues(string sectionName)
    {
        var configurationSection = configuration.GetSection(sectionName)
                                                .GetChildren()
                                                .ToList();

        var result = new NameValueCollection();

        foreach(var item in configurationSection)
        {
            result.Add(item.Key , item.Value);
        }

        return result;
    }
}
