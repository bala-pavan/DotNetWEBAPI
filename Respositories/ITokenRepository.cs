using Microsoft.AspNetCore.Identity;

namespace WebSampleApplicationAPI.Respositories
{
    public interface ITokenRepository
    {
       string CreateJWTToken(IdentityUser user,List<string> roles);
    }
}
