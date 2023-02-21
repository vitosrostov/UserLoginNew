using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
namespace UserLoginNew
{
    public interface IGetToken
    {
        string Test { get; set; } 
        string TokenGet(string username, string role);
    }
}
