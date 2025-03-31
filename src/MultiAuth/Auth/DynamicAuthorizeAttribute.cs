using Microsoft.AspNetCore.Authorization;

namespace MultiAuth.Auth;

public class DynamicAuthorizeAttribute : AuthorizeAttribute
{
    public DynamicAuthorizeAttribute(string[] roles, string scope)
    {
        Roles = roles;
        Scope = scope;
        AuthenticationSchemes = "AzureUser,SignInUser";
    }

    public new string[] Roles { get; }
    public string Scope { get; }
}