using Microsoft.AspNetCore.Authorization;

namespace MultiAuth.Auth;

public class DynamicAuthRequirement(RoleScopeCombination roleScopeCombination) : IAuthorizationRequirement
{
}