using Microsoft.AspNetCore.Identity;

namespace CoreProjectAPI.Repositories.Interface;

public interface ITokenRepository
{
    string CreateJwtToken(IdentityUser user, List<string> roles);
}