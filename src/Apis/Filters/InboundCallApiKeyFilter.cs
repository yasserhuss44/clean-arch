using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters;

public class InboundCallApiKeyAttribute : TypeFilterAttribute
{
    public InboundCallApiKeyAttribute(params ApiKeyPrefixes[] prefixes) : base(typeof(InboundCallApiKeyFilter))
        => Arguments = new object[] { prefixes };

    private class InboundCallApiKeyFilter : IActionFilter
    {
        private readonly ApiKeyPrefixes[] prefixes;

        public InboundCallApiKeyFilter(
            ApiKeyPrefixes[] prefixes)
            => this.prefixes = prefixes;

        public void OnActionExecuting(
            ActionExecutingContext context)
        {
            if(context.HttpContext
                .Request
                .Headers
                .TryGetValue(SecurityConstants.ApiKeyHeaderName , out var extractedApiKey)
                .IsFalsy())
                throw new ForbiddenAccessException();

            var apiKey = extractedApiKey.ToString();

            var hasNoPrefixMatched = prefixes.HasNo(prefix => apiKey.StartsWith(prefix.ToString()));

            if(hasNoPrefixMatched)
                throw new ForbiddenAccessException();
        }

        void IActionFilter.OnActionExecuted(
            ActionExecutedContext context)
        {
        }
    }
}

public enum ApiKeyPrefixes
{
    //DX,
    CRM,
    K2
}
