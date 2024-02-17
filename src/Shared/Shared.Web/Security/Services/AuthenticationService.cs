//using Shared.Application.Contacts;
//using Shared.Application.CustomerProfiles;

//namespace Shared.Web.Security.Services;

//public sealed class AuthenticationService :
//    IAuthenticationService, 
//    IScopedService
//{
//    private readonly ISecurityService securityService;
//    private readonly ITokenService tokenService;
//    private readonly IContactService contactService;
//    private readonly ICustomerProfileService customerProfileService;

//    public AuthenticationService(
//        ITokenService tokenService ,
//        ISecurityService securityService ,
//        IContactService contactService ,
//        ICustomerProfileService customerProfileService)
//    {
//        this.tokenService = tokenService;
//        this.securityService = securityService;
//        this.contactService = contactService;
//        this.customerProfileService = customerProfileService;
//    }

//    public async Task<UserLoginResult> AuthenticateAsync(
//        DxUser dxUser ,
//        CancellationToken cancellationToken)
//    {
//        var user = securityService.GetUser(dxUser);

//        user = await contactService.CreateOrUpdateContactUponLogin(
//            dxUser ,
//            user ,
//            cancellationToken);

//        if(user.IsNewUser && user.ExternalUser.IsFalsy())
//        {
//            user.ProfileId = await customerProfileService.CreateIndividualProfile(
//                user.ContactId ,
//                cancellationToken);

//            user.ProfileRoleTypeId = RoleTypes.Individual.ToInt();

//            user.EntityTypeId = EntityTypes.Individual.ToInt();

//            user.EntityOfficialId = user.Uid;
//        }

//        var securityToken = tokenService.GetToken(user);

//        return new UserLoginResult
//        {
//            Token = securityToken
//        };
//    }
//}