using SouthWall;
using SouthWall.Core;
using TZ1989.JWT;

namespace SouthWall
{
    public interface IAuthService
    {
        Task SendCodeToEmail();
        Task<LoginToken> LoginByCode( string code);
        Task<bool> CheckToken(string token);
    }
    public class AuthService : ServiceBase, IAuthService
    {
        public AuthService()
        {
        }
        public async Task SendCodeToEmail()
        {
            string code = Com.GenLoginCode();
            Com.SetLoginCode(code);
            await Com.SendCodeToEmailForLogin(code,Config.AdminEmail);
        }
        public async Task<LoginToken> LoginByCode(string code)
        {
            string _code = Com.GetLoginCode();
            if (code != _code)
            {
                throw new Exception("login failed.");
            }
            Com.RemoveLoginCode();            
            string token = JWTUtil.GenerateToken(Config.AdminEmail, Config.JWTKey, Config.JWTExpired);
            return new LoginToken
            {
                F_Token = token
            };
        }
        public async Task<bool> CheckToken(string token) 
        {
            if (JWTUtil.ValidateToken(token, Config.JWTKey, out string payload)) 
            {
                return payload == Config.AdminEmail;
            }
            return false;
        }
    }
}
