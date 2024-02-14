namespace Shared.Web.Security;

//TODO::enhance => write description for every item 
public enum AuthorizationAccessLevels
{
    List = 1,
    View = 2,
    Submit = 3,
    Update = 4,
    Execute = 5,
    AssignDelegate = 6,
    ChangeDelegate = 7,
    EditDelegate = 8,
    ChangeDpo = 9,
    AddExternalEntity = 10,
    CMP_AssignComplianceOfficer=11,
    CMP_AssignChiefDataOfficer = 12,   
}