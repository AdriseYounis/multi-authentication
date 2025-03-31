using Microsoft.AspNetCore.Authorization;

namespace MultiAuth.Auth;

public class DynamicAuthHandler : AuthorizationHandler<DynamicAuthRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DynamicAuthRequirement requirement)
    {
        var tokenIssuer = context.User.FindFirst("iss")?.Value;

        switch (context.Resource)
        {
            case null:
                context.Succeed(requirement);
                return Task.CompletedTask;
            case AuthorizeRoleAttribute:
                Console.WriteLine("Using AzureUser scheme for AuthorizeRoleAttribute.");
                context.Succeed(requirement);
                return Task.CompletedTask;
            case AuthorizeAttribute:
                Console.WriteLine("Using AzureUser scheme for AuthorizeAttribute.");
                context.Succeed(requirement);
                return Task.CompletedTask;
        }

        var dynamicAuthorizeAttribute = context.Resource as DynamicAuthorizeAttribute;
        if (dynamicAuthorizeAttribute != null)
        {
            if (tokenIssuer.Contains("https://login.microsoftonline.com"))
            {
                Console.WriteLine("Using AzureUser scheme based on token issuer.");
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            if (tokenIssuer.Contains("https://signin.company.com"))
            {
                Console.WriteLine("Using SignInUser scheme based on token issuer.");
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
        }

        context.Fail();
        return Task.CompletedTask;
    }
}