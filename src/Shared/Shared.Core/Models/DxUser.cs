namespace Core.Models;

public sealed class DxUser
{
    public string UserDN { get; set; }
    public string Uid { get; set; }
    public string Cn { get; set; }
    public string Sn { get; set; }
    public string Gender { get; set; }
    public string Email { get; set; }
    public string NationalId { get; set; }
    public string ArabicName { get; set; }
    public string EnglishFirstName { get; set; }
    public string EnglishName { get; set; }
    public string DobHijri { get; set; }
    public string Dob { get; set; }
    public string ArabicNationality { get; set; }
    public string Nationality { get; set; }
    public string ArabicFirstName { get; set; }
    public string ArabicFamilyName { get; set; }
    public string EnglishFamilyName { get; set; }
    public string ArabicFatherName { get; set; }
    public string EnglishFatherName { get; set; }
    public string ArabicGrandFatherName { get; set; }
    public string EnglishGrandFatherName { get; set; }
    public string CardIssueDateGregorian { get; set; }
    public string CardIssueDateHijri { get; set; }
    public string IqamaExpiryDateHijri { get; set; }
    public string IqamaExpiryDateGregorian { get; set; }
    public string VerisonNumber { get; set; }
    public string Status { get; set; }
    public Guid? ProfileId { get; set; }
    public string Signature { get; set; }
    public bool ExternalUser { get; set; }
}

