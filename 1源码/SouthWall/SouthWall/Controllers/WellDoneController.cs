using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SouthWall.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WellDoneController : TZControllerBase
    {
        public WellDoneController(
             IAuthService authService,
             ITimesService timesService,
             IVideosService videosService
            ) :
            base(authService, timesService, videosService)
        {
        }
        #region Auth   
        #region Auth_Login_ByCode
        [HttpPost("auth/loginbycode")]
        public Task<TZResponse> Auth_Login_ByCode([FromBody] Auth_Login_ByCode_Args args)
        {
            return RunAction(CheckAuthType.None, async () =>
            {
                var data = await _AuthService.LoginByCode(args.F_Code);
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
                await _AuthService.SendCodeToEmail();
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
                var isok = await _AuthService.CheckToken(args.F_Token);
                return Success("操作成功", isok);
            });
        }
        public class AuthCheckTokenArgs
        {
            public string F_Token { get; set; }
        }
        #endregion
        #endregion Auth

        #region Times
        #region Times_List
        [HttpPost("times/list")]
        public Task<TZResponse> Times_List([FromBody] Times_List_Args args)
        {
            return RunAction(CheckAuthType.User, async () =>
            {
                var list = await _TimesService.GetList(null);
                return Success("获取成功", list.Select(t => new
                {
                    t.F_Id,
                    t.F_Content,
                    t.F_Imgs
                }).ToList());
            });
        }
        public class Times_List_Args
        {
        }
        #endregion Times_List
        #region Times_Save
        [HttpPost("times/save")]
        public Task<TZResponse> Times_Save([FromBody] Times_Save_Args args)
        {
            return RunAction(CheckAuthType.User, async () =>
            {
                await _TimesService.Save(new TimesEntity
                {
                    F_Id = args.F_Id,
                    F_Content = args.F_Content,
                    F_Imgs = args.F_Imgs
                });
                return Success("保存成功");
            });
        }
        public class Times_Save_Args
        {
            public string? F_Id { get; set; }
            public string? F_Content { get; set; }
            public string? F_Imgs { get; set; }
        }
        #endregion Times_Save
        #region Times_Delete
        [HttpPost("times/delete")]
        public Task<TZResponse> Times_Delete([FromBody] Times_Delete_Args args)
        {
            return RunAction(CheckAuthType.User, async () =>
            {
                await _TimesService.Delete(args.F_Id);
                return Success("删除成功");
            });
        }
        public class Times_Delete_Args
        {
            public string? F_Id { get; set; }
        }
        #endregion Times_Delete
        #region Times_Get
        [HttpPost("times/get")]
        public Task<TZResponse> Times_Get([FromBody] Times_Get_Args args)
        {
            return RunAction(CheckAuthType.User, async () =>
            {
               var entity=  await _TimesService.GetById(args.F_Id);
                return Success("获取成功", new
                {
                    entity.F_Id,
                    entity.F_Content,
                    F_ImgSrcList =entity.E_ImgSrcList
                });
            });
        }
        public class Times_Get_Args
        {
            public string? F_Id { get; set; }
        }
        #endregion Times_Get
        #endregion

        #region Videos
        #region Videos_List
        [HttpPost("videos/list")]
        public Task<TZResponse> Videos_List([FromBody] Videos_List_Args args)
        {
            return RunAction(CheckAuthType.User, async () =>
            {
                var list = await _VideosService.GetList(null);
                return Success("获取成功", list.Select(t => new
                {
                    t.F_Id,
                    t.F_Title,
                    t.F_ConverImg,
                    t.F_VideoUrl,
                    t.F_VideoCode,
                    t.F_CreateTime
                }).ToList());
            });
        }
        public class Videos_List_Args
        {
        }
        #endregion Videos_List
        #region Videos_Save
        [HttpPost("videos/save")]
        public Task<TZResponse> Videos_Save([FromBody] Videos_Save_Args args)
        {
            return RunAction(CheckAuthType.User, async () =>
            {
                await _VideosService.Save(new VideosEntity
                {
                    F_Id = args.F_Id,
                     F_ConverImg=args.F_ConverImg,
                      F_Title=args.F_Title,
                       F_VideoUrl = args.F_VideoUrl,
                        F_VideoCode=args.F_VideoCode
                });
                return Success("保存成功");
            });
        }
        public class Videos_Save_Args
        {
            public string? F_Id { get; set; }
            public string? F_ConverImg { get; set; }
            public string? F_Title { get; set; }
            public string? F_VideoUrl {  get; set; }
            public string? F_VideoCode { get; set; }
        }
        #endregion Videos_Save
        #region Videos_Delete
        [HttpPost("videos/delete")]
        public Task<TZResponse> Videos_Delete([FromBody] Videos_Delete_Args args)
        {
            return RunAction(CheckAuthType.User, async () =>
            {
                await _VideosService.Delete(args.F_Id);
                return Success("删除成功");
            });
        }
        public class Videos_Delete_Args
        {
            public string? F_Id { get; set; }
        }
        #endregion Videos_Delete
        #region Videos_Get
        [HttpPost("videos/get")]
        public Task<TZResponse> Videos_Get([FromBody] Videos_Get_Args args)
        {
            return RunAction(CheckAuthType.User, async () =>
            {
                var entity = await _VideosService.GetById(args.F_Id);
                return Success("获取成功", new
                {
                    entity.F_Id,
                    entity.F_ConverImg,
                    entity.F_Title,
                    entity.F_VideoUrl,
                    entity.F_VideoCode
                });
            });
        }
        public class Videos_Get_Args
        {
            public string? F_Id { get; set; }
        }
        #endregion Videos_Get
        #endregion

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