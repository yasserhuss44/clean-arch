using System.Security.Claims;
using Core.Constants;

namespace Core.Models;

public sealed class User
{
    public Guid ContactId { get; set; }
    public string Uid { get; set; }
    public string Cn { get; set; }
    public string Email { get; set; }
    public string Mobile { get; set; }

    public string ArabicName { get; set; }
    public string EnglishName { get; set; }
    public string DobHijri { get; set; }
    public string Dob { get; set; }

    public string ArabicFirstName { get; set; }
    public string ArabicFamilyName { get; set; }
    public string EnglishFamilyName { get; set; }
    public string ArabicFatherName { get; set; }
    public string EnglishFatherName { get; set; }
    public string EnglishFirstName { get; set; }
    public string ArabicGrandFatherName { get; set; }
    public string EnglishGrandFatherName { get; set; }

    public Guid? ProfileId { get; set; }
    public string EntityOfficialId { get; set; }
    public Guid? EntityId { get; set; }
    public int EntityTypeId { get; set; }
    public int? ProfileRoleTypeId { get; set; }

    public bool ExternalUser { get; set; } = false;

    public bool IsNewUser { get; set; } = false;

    public long PersonId => Uid.ToLong();

    public long PersonBirthDate
    {
        get
        {
            var birthDateNumeric = Dob!.Replace("-" , "");

            return birthDateNumeric.ToLong();
        }
    }

    public IEnumerable<Claim> Claims()
    {
        var claims = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier, Uid?? " "),
            new Claim(ClaimTypes.Name, Cn ?? " "),

            new Claim(NdpClaimNames.ProfileId, ProfileId.ToString()??" " ),
            new Claim(NdpClaimNames.EntityOfficialId, EntityOfficialId ?? " "),
            new Claim(NdpClaimNames.EntityId, EntityId.ToString() ?? " "),
            new Claim(NdpClaimNames.EntityTypeId, EntityTypeId.ToString() ?? " "),

            new Claim(NdpClaimNames.ProfileRoleTypeId, ProfileRoleTypeId?.ToString() ?? " "),
            new Claim(NdpClaimNames.ExternalUser, ExternalUser.ToString()),

            new Claim(NdpClaimNames.Uid, Uid ?? " "),
            new Claim(NdpClaimNames.ContactId, ContactId.ToString() ?? " "),
            new Claim(NdpClaimNames.Cn, Cn ?? " "),


            new Claim(NdpClaimNames.Email, Email?? " "),
            new Claim(NdpClaimNames.Mobile, Mobile?? " "),
            new Claim(NdpClaimNames.ArabicName, ArabicName?? " "),
            new Claim(NdpClaimNames.EnglishName, EnglishName?? " "),
            new Claim(NdpClaimNames.DobHijri, DobHijri?? " "),
            new Claim(NdpClaimNames.Dob , Dob?? " "),

            new Claim(NdpClaimNames.ArabicFirstName, ArabicFirstName?? " "),
            new Claim(NdpClaimNames.ArabicFamilyName, ArabicFamilyName?? " "),
            new Claim(NdpClaimNames.EnglishFamilyName  , EnglishFamilyName?? " "),
            new Claim(NdpClaimNames.ArabicFatherName, ArabicFatherName?? " "),
            new Claim(NdpClaimNames.EnglishFatherName, EnglishFatherName?? " "),
            new Claim(NdpClaimNames.ArabicGrandFatherName, ArabicGrandFatherName?? " "),
            new Claim(NdpClaimNames.EnglishGrandFatherName, EnglishGrandFatherName?? " "),
        };

        return claims;
    }

    public UserInfo UserInfo => new()
    {
        UserId = Uid ,
        Claims = Claims()
    };
}
