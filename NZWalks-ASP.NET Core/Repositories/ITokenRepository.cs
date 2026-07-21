using Microsoft.AspNetCore.Identity;

namespace NZWalks_ASP.NET_Core.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
