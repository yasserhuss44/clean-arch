using AutoMapper;

namespace Shared.Web.Security.Services;

public sealed class SecurityService :
    ISecurityService, 
    IScopedService
{
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly ILogger<SecurityService> logger;
    private readonly IMapper mapper;
    private User _currentUser;
    public User CurrentUser => LoadCurrentUser();

    public SecurityService(
        IHttpContextAccessor httpContextAccessor ,
        ILogger<SecurityService> logger ,
        IMapper mapper)
    {
        this.httpContextAccessor = httpContextAccessor;
        this.logger = logger;
        this.mapper = mapper;
    }

    public User GetUser(
        DxUser dxUser)
    {
        User user;

        try
        {
            user = mapper.Map<User>(dxUser);
        }
        catch(Exception ex)
        {
            logger.LogError(
                "GetUser Error:{Message} \n {StackTrace}" ,
                ex.Message ,
                ex.StackTrace);

            throw new InvalidCredentialsException();
        }

        return user;
    }

    public bool IsCurrentUserHasRoleAndProfileAccess(
        RoleTypes roleType ,
        bool isExternalUser = false)
    {
        var currentRouteProfileId = GetCurrentRouteProfile();

        if(currentRouteProfileId == CurrentUser.ProfileId.ToString()
            && CurrentUser.ProfileRoleTypeId == roleType.ToInt()
            && isExternalUser == CurrentUser.ExternalUser)
            return true;

        return false;
    }

    public bool IsCurrentUserHasRoleAndProfileAccess(
        RoleTypes[] roleTypes ,
        bool isExternalUser = false)
    {
        var currentRouteProfileId = GetCurrentRouteProfile();

        if(isExternalUser != CurrentUser.ExternalUser
            || currentRouteProfileId != CurrentUser.ProfileId.ToString())
            return false;

        return IsCurrentUserHasRole(roleTypes);
    }

    public bool IsCurrentUserHasRole(
        RoleTypes[] roleTypes)
        => roleTypes.HasAny(roleType => CurrentUser.ProfileRoleTypeId == roleType.ToInt());

    private User LoadCurrentUser()
    {
        if(_currentUser.NotNull())
            return _currentUser;       

        var currentUserClaims = httpContextAccessor?.HttpContext?.User?.Claims;

        if(currentUserClaims.IsNullOrEmpty())
            return null;
        
        var user = new User();

        foreach(var claim in currentUserClaims)
        {
            switch(claim.Type)
            {
                case NdpClaimNames.Uid:
                    user.Uid = claim.Value;
                    break;

                case NdpClaimNames.Cn:
                    user.Cn = claim.Value;
                    break;

                case NdpClaimNames.ProfileId:
                    user.ProfileId = claim.Value.ToNullableGuid();
                    break;

                case NdpClaimNames.EntityOfficialId:
                    user.EntityOfficialId = claim.Value;
                    break;

                case NdpClaimNames.EntityId:
                    user.EntityId = claim.Value?.ToNullableGuid();
                    break;

                case NdpClaimNames.EntityTypeId:
                    user.EntityTypeId = claim.Value.ToInt();
                    break;

                case NdpClaimNames.ProfileRoleTypeId:
                    user.ProfileRoleTypeId = claim.Value.IsNullOrEmpty() ? 0 : int.Parse(claim.Value);
                    break;

                case NdpClaimNames.ExternalUser:
                    user.ExternalUser = !claim.Value.IsNullOrEmpty() && bool.Parse(claim.Value);
                    break;

                case NdpClaimNames.Email:
                    user.Email = claim.Value;
                    break;

                case NdpClaimNames.Mobile:
                    user.Mobile = claim.Value;
                    break;

                case NdpClaimNames.ArabicName:
                    user.ArabicName = claim.Value;
                    break;

                case NdpClaimNames.EnglishName:
                    user.EnglishName = claim.Value;
                    break;

                case NdpClaimNames.DobHijri:
                    user.DobHijri = claim.Value;
                    break;

                case NdpClaimNames.Dob:
                    user.Dob = claim.Value;
                    break;

                case NdpClaimNames.ArabicFirstName:
                    user.ArabicFirstName = claim.Value;
                    break;

                case NdpClaimNames.ArabicFamilyName:
                    user.ArabicFamilyName = claim.Value;
                    break;

                case NdpClaimNames.EnglishFamilyName:
                    user.EnglishFamilyName = claim.Value;
                    break;

                case NdpClaimNames.ArabicFatherName:
                    user.ArabicFatherName = claim.Value;
                    break;

                case NdpClaimNames.EnglishFatherName:
                    user.EnglishFatherName = claim.Value;
                    break;

                case NdpClaimNames.ArabicGrandFatherName:
                    user.ArabicGrandFatherName = claim.Value;
                    break;

                case NdpClaimNames.EnglishGrandFatherName:
                    user.EnglishGrandFatherName = claim.Value;
                    break;

                case NdpClaimNames.ContactId:
                    user.ContactId = claim.Value.ToGuid();
                    break;

                case NdpClaimNames.CorrelationId:
                    user.CorrelationId = claim.Value.ToString();
                    break;

                default:
                    break;
            }
        }

        _currentUser = user;

        return user;
    }

    private string GetCurrentRouteProfile()
    {
        var profile = string.Empty;

        var routeValues = httpContextAccessor.HttpContext!.Request.RouteValues;

        if(routeValues.TryGetValue("profileId" , out var profileId))
            profile = profileId!.ToString();

        if(httpContextAccessor.HttpContext!.Request.Method == "POST")
            return CurrentUser!.ProfileId.ToString();

        return profile;
    }
}