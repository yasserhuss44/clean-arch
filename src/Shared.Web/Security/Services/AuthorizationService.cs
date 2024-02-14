using IAuthorizationService = Core.Interfaces.IAuthorizationService;

namespace Shared.Web.Security.Services;

public sealed class AuthorizationService :
    IAuthorizationService,
    IScopedService
{
    private readonly ISecurityService securityService; 
    private readonly ILogger<AuthorizationService> logger;

    public AuthorizationService(
        ISecurityService securityService, 
        ILogger<AuthorizationService> logger)
    {
        this.securityService = securityService; 
        this.logger = logger;
    }

    public async Task<bool> IsUserAuthorized(
        int requestTypeId,
        int accessLevel, 
        CancellationToken cancellationToken = default)
    {
        try
        {

            //var rules = await GetAuthMatrixRulesAsync(
            //    requestTypeId,
            //    accessLevel,
            //    cancellationToken);

            //if (rules.IsNullOrEmpty())
            //    return false;

            //List<int> currentEntityProfilesRoles = null;

            //if (rules.Any(r => r.MasterRole.NotEmpty()))
            //{
            //    currentEntityProfilesRoles = await GetCurrentEntityRoles(cancellationToken);
            //}

            //foreach (var rule in rules)
            //{
            //    if (IsMasterRoleExist(currentEntityProfilesRoles, rule))
            //        continue;

            //    if (IsValidExecuteAction(accessLevel, rule))
            //        continue;

            //    rule.IsRuleFulfilled = true;
            //}

            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(
                "\r\r#### AUTHORIZE USER ERROR ####\r{ex}\r\r",
                ex.ToString());
        }

        return false;
    }
     
}