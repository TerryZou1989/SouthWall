using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SouthWall.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WellDoneController : TZControllerBase
    {
        public WellDoneController(IAuthService authService) :
            base(authService)
        {
        }
        #region Auth   
        #region Auth_Login_ByCode
        [HttpPost("auth/loginbycode")]
        public Task<TZResponse> Auth_Login_ByCode([FromBody] Auth_Login_ByCode_Args args)
        {
            return RunAction(CheckAuthType.None, async () =>
            {
                var data = await AuthService.LoginByCode(args.F_Code);
                return Success("登录成功", data);
            });
        }
        public class Auth_Login_ByCode_Args
        {
            public string F_Code { get; set; }
        }
        #endregion        

        #region Auth_SendLoginCode
        [HttpPost("auth/sendlogincode")]
        public Task<TZResponse> Auth_SendLoginCode([FromBody] Auth_SendLoginCode_Args args)
        {
            return RunAction(CheckAuthType.None, async () =>
            {
                await AuthService.SendCodeToEmail();
                return Success("发送成功");
            });
        }
        public class Auth_SendLoginCode_Args
        {
        }
        #endregion Auth_SendLoginCode

        #region Auth_CheckToken
        [HttpPost("auth/checktoken")]
        public Task<TZResponse> Auth_CheckToken([FromBody] AuthCheckTokenArgs args)
        {
            return RunAction(CheckAuthType.None, async () =>
            {
                var isok = await AuthService.CheckToken(args.F_Token);
                return Success("操作成功", isok);
            });
        }
        public class AuthCheckTokenArgs
        {
            public string F_Token { get; set; }
        }
        #endregion
        #endregion Auth

        public class List_Page_Args_Base : List_Args_Base
        {
            public int Page { get; set; }
            public int PageSize { get; set; }
        }
        public class List_Args_Base
        {
            public string? SearchKey { get; set; }
        }
    }
}