using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SouthWall.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WellDoneController : TZControllerBase
    {
        public WellDoneController(
             IAuthService authService,
             IRequestLogsService requestLogsService,
             IDatasService datasService,
             ITimesService timesService,
             IVideosService videosService,
             IAudiosService audioService,
             IArticlesService articlesService,
             IMessagesService messagesService,
             IWebSitesService webSitesService,
             IShiJusService shiJusService,
             ITouXiangsService touXiangsService,
                IStatisticsServiceService statisticsServiceService
            ) :
            base(authService,
                requestLogsService,
                datasService,
                timesService,
                videosService,
                audioService,
                articlesService,
                messagesService,
                webSitesService,
                shiJusService,
                touXiangsService,
                statisticsServiceService)
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

        #region Datas
        #region Datas_List
        [HttpPost("datas/list")]
        public Task<TZResponse> Datas_List([FromBody] Datas_List_Args args)
        {
            return RunAction(CheckAuthType.User, async () =>
            {
                var list = await _DatasService.GetList(new DatasEntity
                {
                    F_Type = args.F_Type
                });
                return Success("获取成功", list.Select(t => new
                {
                    t.F_Id,
                    t.F_Type,
                    t.F_Title,
                    t.F_Description,
                    t.F_Content,
                    t.F_CoverImg,
                    t.F_Imgs,
                    t.F_Url,
                    t.F_VideoUrl,
                    t.F_AudioUrl,
                    t.F_IFrameCode,
                    t.F_CreateTime
                }).ToList());
            });
        }
        public class Datas_List_Args
        {
            public int? F_Type { get; set; }
        }
        #endregion Datas_List
        #region Datas_Save
        [HttpPost("datas/save")]
        public Task<TZResponse> Datas_Save([FromBody] Datas_Save_Args args)
        {
            return RunAction(CheckAuthType.User, async () =>
            {
                await _DatasService.Save(new DatasEntity
                {
                    F_Id = args.F_Id,
                    F_Title = args.F_Title,
                    F_Description = args.F_Description,
                    F_Content = args.F_Content,
                    F_Type = args.F_Type,
                    F_CoverImg = args.F_CoverImg,
                    F_Imgs = args.F_Imgs,
                    F_VideoUrl = args.F_VideoUrl,
                    F_AudioUrl = args.F_AudioUrl,
                    F_IFrameCode = args.F_IFrameCode,
                    F_Url = args.F_Url
                });
                switch (args.F_Type)
                {
                    case 1:
                        await this.SaveTimes(TimesType.Article, new DatasEntity
                        {
                            F_Title = args.F_Title,
                            F_Imgs = args.F_CoverImg,
                            F_Content = args.F_Content,
                            F_Url = args.F_Url
                        });
                        break;
                    case 2:
                        await this.SaveTimes(TimesType.Video, new DatasEntity
                        {
                            F_Title = args.F_Title,
                            F_Imgs = args.F_CoverImg,
                            F_Url = args.F_Url
                        });
                        break;
                    case 3:
                        await this.SaveTimes(TimesType.Audio, new DatasEntity
                        {
                            F_Title = args.F_Title,
                            F_Imgs = args.F_CoverImg,
                            F_Url = args.F_Url
                        });
                        break;
                    case 4:
                        await this.SaveTimes(TimesType.WebSite, new DatasEntity
                        {
                            F_Title = args.F_Title,
                            F_Imgs = args.F_CoverImg,
                            F_Url = args.F_Url
                        });
                        break;
                }
                return Success("保存成功");
            });
        }
        public class Datas_Save_Args
        {
            public string? F_Id { get; set; }
            public string? F_Title { get; set; }
            public string? F_Description { get; set; }
            public string? F_Content { get; set; }
            public int? F_Type { get; set; }
            public string? F_Url { get; set; }
            public string? F_Imgs { get; set; }
            public string? F_CoverImg { get; set; }
            public string? F_VideoUrl { get; set; }
            public string? F_IFrameCode { get; set; }
            public string? F_AudioUrl { get; set; }
        }
        #endregion Datas_Save
        #region Datas_Delete
        [HttpPost("datas/delete")]
        public Task<TZResponse> Datas_Delete([FromBody] Videos_Delete_Args args)
        {
            return RunAction(CheckAuthType.User, async () =>
            {
                await _DatasService.Delete(args.F_Id);
                return Success("删除成功");
            });
        }
        public class Datas_Delete_Args
        {
            public string? F_Id { get; set; }
        }
        #endregion Datas_Delete
        #region Datas_Get
        [HttpPost("datas/get")]
        public Task<TZResponse> Datas_Get([FromBody] Datas_Get_Args args)
        {
            return RunAction(CheckAuthType.None, async () =>
            {
                var entity = await _DatasService.GetById(args.F_Id);
                return Success("获取成功", new
                {
                    entity.F_Id,
                    entity.F_Type,
                    entity.F_Title,
                    entity.F_Description,
                    entity.F_Content,
                    entity.F_Url,
                    entity.F_CoverImg,
                    entity.F_Imgs,
                    entity.F_VideoUrl,
                    entity.F_AudioUrl,
                    entity.F_IFrameCode,
                    entity.F_CreateTime,
                    F_ImgSrcList = entity.E_ImgSrcList
                });
            });
        }
        public class Datas_Get_Args
        {
            public string? F_Id { get; set; }
        }
        #endregion Datas_Get
        #endregion

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
                    t.F_Title,
                    t.F_Content,
                    t.F_Url,
                    t.F_Imgs,
                    t.F_Video
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
                    F_Title = args.F_Title,
                    F_Content = args.F_Content,
                    F_Url = args.F_Url,
                    F_Imgs = args.F_Imgs,
                    F_Video = args.F_Video
                });
                return Success("保存成功");
            });
        }
        public class Times_Save_Args
        {
            public string? F_Id { get; set; }
            public string? F_Title { get; set; }
            public string? F_Content { get; set; }
            public string? F_Url { get; set; }
            public string? F_Imgs { get; set; }
            public string? F_Video { get; set; }
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
            return RunAction(CheckAuthType.None, async () =>
            {
                var entity = await _TimesService.GetById(args.F_Id);
                return Success("获取成功", new
                {
                    entity.F_Id,
                    entity.F_Title,
                    entity.F_Content,
                    entity.F_Url,
                    entity.F_Video,
                    F_ImgSrcList = entity.E_ImgSrcList
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
                    t.F_CoverImg,
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
                    F_CoverImg = args.F_CoverImg,
                    F_Title = args.F_Title,
                    F_VideoUrl = args.F_VideoUrl,
                    F_VideoCode = args.F_VideoCode
                });
                if (string.IsNullOrEmpty(args.F_Id))
                {
                    await this.SaveTimes(TimesType.Video, new DatasEntity
                    {
                        F_Title = args.F_Title,
                        F_Imgs = args.F_CoverImg,
                        F_Url = args.F_VideoUrl
                    });
                }
                return Success("保存成功");
            });
        }
        public class Videos_Save_Args
        {
            public string? F_Id { get; set; }
            public string? F_CoverImg { get; set; }
            public string? F_Title { get; set; }
            public string? F_VideoUrl { get; set; }
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
            return RunAction(CheckAuthType.None, async () =>
            {
                var entity = await _VideosService.GetById(args.F_Id);
                return Success("获取成功", new
                {
                    entity.F_Id,
                    entity.F_CoverImg,
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

        #region Audios
        #region Audios_List
        [HttpPost("audios/list")]
        public Task<TZResponse> Audios_List([FromBody] Audios_List_Args args)
        {
            return RunAction(CheckAuthType.User, async () =>
            {
                var list = await _AudiosService.GetList(null);
                return Success("获取成功", list.Select(t => new
                {
                    t.F_Id,
                    t.F_Title,
                    t.F_CoverImg,
                    t.F_AudioUrl,
                    t.F_Description,
                    t.F_CreateTime
                }).ToList());
            });
        }
        public class Audios_List_Args
        {
        }
        #endregion Audios_List
        #region Audios_Save
        [HttpPost("audios/save")]
        public Task<TZResponse> Audios_Save([FromBody] Audios_Save_Args args)
        {
            return RunAction(CheckAuthType.User, async () =>
            {
                await _AudiosService.Save(new AudiosEntity
                {
                    F_Id = args.F_Id,
                    F_CoverImg = args.F_CoverImg,
                    F_Title = args.F_Title,
                    F_AudioUrl = args.F_AudioUrl,
                    F_Description = args.F_Description
                });
                if (string.IsNullOrEmpty(args.F_Id))
                {
                    await this.SaveTimes(TimesType.Audio, new DatasEntity
                    {
                        F_Title = args.F_Title,
                        F_Imgs = args.F_CoverImg,
                        F_Url = args.F_AudioUrl
                    });
                }
                return Success("保存成功");
            });
        }
        public class Audios_Save_Args
        {
            public string? F_Id { get; set; }
            public string? F_CoverImg { get; set; }
            public string? F_Title { get; set; }
            public string? F_AudioUrl { get; set; }
            public string? F_Description { get; set; }
        }
        #endregion Audios_Save
        #region Audios_Delete
        [HttpPost("audios/delete")]
        public Task<TZResponse> Audios_Delete([FromBody] Audios_Delete_Args args)
        {
            return RunAction(CheckAuthType.User, async () =>
            {
                await _AudiosService.Delete(args.F_Id);
                return Success("删除成功");
            });
        }
        public class Audios_Delete_Args
        {
            public string? F_Id { get; set; }
        }
        #endregion Audios_Delete
        #region Audios_Get
        [HttpPost("audios/get")]
        public Task<TZResponse> Audios_Get([FromBody] Audios_Get_Args args)
        {
            return RunAction(CheckAuthType.None, async () =>
            {
                var entity = await _AudiosService.GetById(args.F_Id);
                return Success("获取成功", new
                {
                    entity.F_Id,
                    entity.F_CoverImg,
                    entity.F_Title,
                    entity.F_Description,
                    entity.F_AudioUrl
                });
            });
        }
        public class Audios_Get_Args
        {
            public string? F_Id { get; set; }
        }
        #endregion Audios_Get
        #endregion

        #region Articles
        #region Articles_List
        [HttpPost("articles/list")]
        public Task<TZResponse> Articles_List([FromBody] Articles_List_Args args)
        {
            return RunAction(CheckAuthType.User, async () =>
            {
                var list = await _ArticlesService.GetList(null);
                return Success("获取成功", list.Select(t => new
                {
                    t.F_Id,
                    t.F_Title,
                    t.F_CoverImg,
                    t.F_Content,
                    t.F_ArticleUrl,
                    t.F_CreateTime
                }).ToList());
            });
        }
        public class Articles_List_Args
        {
        }
        #endregion Articles_List
        #region Articles_Save
        [HttpPost("articles/save")]
        public Task<TZResponse> Articles_Save([FromBody] Articles_Save_Args args)
        {
            return RunAction(CheckAuthType.User, async () =>
            {
                await _ArticlesService.Save(new ArticlesEntity
                {
                    F_Id = args.F_Id,
                    F_CoverImg = args.F_CoverImg,
                    F_Title = args.F_Title,
                    F_Content = args.F_Content,
                    F_ArticleUrl = args.F_ArticleUrl
                });
                if (string.IsNullOrEmpty(args.F_Id))
                {
                    await this.SaveTimes(TimesType.Article, new DatasEntity
                    {
                        F_Title = args.F_Title,
                        F_Imgs = args.F_CoverImg,
                        F_Content = args.F_Content,
                        F_Url = args.F_ArticleUrl
                    });
                }
                return Success("保存成功");
            });
        }
        public class Articles_Save_Args
        {
            public string? F_Id { get; set; }
            public string? F_CoverImg { get; set; }
            public string? F_Title { get; set; }
            public string? F_Content { get; set; }
            public string? F_ArticleUrl { get; set; }
        }
        #endregion Articles_Save
        #region Articles_Delete
        [HttpPost("articles/delete")]
        public Task<TZResponse> Articles_Delete([FromBody] Articles_Delete_Args args)
        {
            return RunAction(CheckAuthType.User, async () =>
            {
                await _ArticlesService.Delete(args.F_Id);
                return Success("删除成功");
            });
        }
        public class Articles_Delete_Args
        {
            public string? F_Id { get; set; }
        }
        #endregion Articles_Delete
        #region Articles_Get
        [HttpPost("articles/get")]
        public Task<TZResponse> Articles_Get([FromBody] Articles_Get_Args args)
        {
            return RunAction(CheckAuthType.None, async () =>
            {
                var entity = await _ArticlesService.GetById(args.F_Id);
                return Success("获取成功", new
                {
                    entity.F_Id,
                    entity.F_CoverImg,
                    entity.F_Title,
                    entity.F_Content,
                    entity.F_ArticleUrl
                });
            });
        }
        public class Articles_Get_Args
        {
            public string? F_Id { get; set; }
        }
        #endregion Articles_Get
        #endregion

        #region Messages
        #region Messages_List
        [HttpPost("messages/list")]
        public Task<TZResponse> Messages_List([FromBody] Messages_List_Args args)
        {
            return RunAction(CheckAuthType.User, async () =>
            {
                var list = await _MessagesService.GetList(null);
                return Success("获取成功", list.Select(t => new
                {
                    t.F_Id,
                    t.F_Content,
                    t.F_UserName,
                    t.F_CreateTime
                }).ToList());
            });
        }
        public class Messages_List_Args
        {
        }
        #endregion Messages_List
        #region Messages_Save
        [HttpPost("messages/save")]
        public Task<TZResponse> Messages_Save([FromBody] Messages_Save_Args args)
        {
            return RunAction(CheckAuthType.None, async () =>
            {
                var shiju = await _ShiJusService.GetById(args.F_ShiJuId);
                if (shiju != null && shiju.F_N == args.F_ShiJuN)
                {
                    await _MessagesService.Save(new MessagesEntity
                    {
                        F_Id = args.F_Id,
                        F_Content = args.F_Content,
                        F_UserName = args.F_UserName,
                        F_UserEmail = args.F_UserEmail
                    });
                    return Success("保存成功");
                }
                else
                {
                    return Error500("诗句的下句不对哦，你可以百度一下。");
                }

            });
        }
        public class Messages_Save_Args
        {
            public string? F_Id { get; set; }
            public string? F_UserName { get; set; }
            public string? F_UserEmail { get; set; }
            public string? F_Content { get; set; }
            public string? F_ShiJuId { get; set; }
            public string? F_ShiJuN { get; set; }
        }
        #endregion Messages_Save
        #region Messages_Delete
        [HttpPost("messages/delete")]
        public Task<TZResponse> Messages_Delete([FromBody] Messages_Delete_Args args)
        {
            return RunAction(CheckAuthType.User, async () =>
            {
                await _MessagesService.Delete(args.F_Id);
                return Success("删除成功");
            });
        }
        public class Messages_Delete_Args
        {
            public string? F_Id { get; set; }
        }
        #endregion Messages_Delete
        #region Messages_Get
        [HttpPost("messages/get")]
        public Task<TZResponse> Messages_Get([FromBody] Messages_Get_Args args)
        {
            return RunAction(CheckAuthType.User, async () =>
            {
                var entity = await _MessagesService.GetById(args.F_Id);
                return Success("获取成功", new
                {
                    entity.F_Id,
                    entity.F_UserName,
                    entity.F_UserEmail,
                    entity.F_Content,
                });
            });
        }
        public class Messages_Get_Args
        {
            public string? F_Id { get; set; }
        }
        #endregion Messages_Get
        #region Messages_Reply
        [HttpPost("messages/reply")]
        public Task<TZResponse> Messages_Reply([FromBody] Messages_Reply_Args args)
        {
            return RunAction(CheckAuthType.User, async () =>
            {
                await _MessagesService.Reply(args.F_Id, args.F_Content);
                return Success("回复成功");
            });
        }
        public class Messages_Reply_Args
        {
            public string? F_Id { get; set; }
            public string? F_Content { get; set; }
        }
        #endregion Messages_Get
        #endregion

        #region WebSites
        #region WebSites_List
        [HttpPost("websites/list")]
        public Task<TZResponse> WebSites_List([FromBody] WebSites_List_Args args)
        {
            return RunAction(CheckAuthType.User, async () =>
            {
                var list = await _WebSitesService.GetList(null);
                return Success("获取成功", list.Select(t => new
                {
                    t.F_Id,
                    t.F_Title,
                    t.F_CoverImg,
                    t.F_Description,
                    t.F_Url,
                    t.F_CreateTime
                }).ToList());
            });
        }
        public class WebSites_List_Args
        {
        }
        #endregion WebSites_List
        #region WebSites_Save
        [HttpPost("websites/save")]
        public Task<TZResponse> WebSites_Save([FromBody] WebSites_Save_Args args)
        {
            return RunAction(CheckAuthType.User, async () =>
            {
                await _WebSitesService.Save(new WebSitesEntity
                {
                    F_Id = args.F_Id,
                    F_CoverImg = args.F_CoverImg,
                    F_Title = args.F_Title,
                    F_Description = args.F_Description,
                    F_Url = args.F_Url
                });
                if (string.IsNullOrEmpty(args.F_Id))
                {
                    await this.SaveTimes(TimesType.WebSite, new DatasEntity
                    {
                        F_Title = args.F_Title,
                        F_Imgs = args.F_CoverImg,
                        F_Content = args.F_Description,
                        F_Url = args.F_Url
                    });
                }
                return Success("保存成功");
            });
        }
        public class WebSites_Save_Args
        {
            public string? F_Id { get; set; }
            public string? F_CoverImg { get; set; }
            public string? F_Title { get; set; }
            public string? F_Description { get; set; }
            public string? F_Url { get; set; }
        }
        #endregion WebSites_Save
        #region WebSites_Delete
        [HttpPost("websites/delete")]
        public Task<TZResponse> WebSites_Delete([FromBody] WebSites_Delete_Args args)
        {
            return RunAction(CheckAuthType.User, async () =>
            {
                await _WebSitesService.Delete(args.F_Id);
                return Success("删除成功");
            });
        }
        public class WebSites_Delete_Args
        {
            public string? F_Id { get; set; }
        }
        #endregion WebSites_Delete
        #region WebSites_Get
        [HttpPost("websites/get")]
        public Task<TZResponse> WebSites_Get([FromBody] WebSites_Get_Args args)
        {
            return RunAction(CheckAuthType.None, async () =>
            {
                var entity = await _WebSitesService.GetById(args.F_Id);
                return Success("获取成功", new
                {
                    entity.F_Id,
                    entity.F_CoverImg,
                    entity.F_Title,
                    entity.F_Description,
                    entity.F_Url
                });
            });
        }
        public class WebSites_Get_Args
        {
            public string? F_Id { get; set; }
        }
        #endregion WebSites_Get
        #endregion

        #region ShiJus
        #region ShiJus_List
        [HttpPost("shijus/list")]
        public Task<TZResponse> ShiJus_List([FromBody] ShiJus_List_Args args)
        {
            return RunAction(CheckAuthType.User, async () =>
            {
                var list = await _ShiJusService.GetList(null);
                return Success("获取成功", list.Select(t => new
                {
                    t.F_Id,
                    t.F_P,
                    t.F_N,
                    t.F_CreateTime
                }).ToList());
            });
        }
        public class ShiJus_List_Args
        {
        }
        #endregion ShiJus_List
        #region ShiJus_Save
        [HttpPost("shijus/save")]
        public Task<TZResponse> ShiJus_Save([FromBody] ShiJus_Save_Args args)
        {
            return RunAction(CheckAuthType.User, async () =>
            {
                await _ShiJusService.Save(new ShiJusEntity
                {
                    F_Id = args.F_Id,
                    F_N = args.F_N,
                    F_P = args.F_P
                });
                return Success("保存成功");
            });
        }
        public class ShiJus_Save_Args
        {
            public string? F_Id { get; set; }
            public string? F_N { get; set; }
            public string? F_P { get; set; }
        }
        #endregion ShiJus_Save
        #region ShiJus_Delete
        [HttpPost("shijus/delete")]
        public Task<TZResponse> ShiJus_Delete([FromBody] ShiJus_Delete_Args args)
        {
            return RunAction(CheckAuthType.User, async () =>
            {
                await _ShiJusService.Delete(args.F_Id);
                return Success("删除成功");
            });
        }
        public class ShiJus_Delete_Args
        {
            public string? F_Id { get; set; }
        }
        #endregion ShiJus_Delete
        #region ShiJus_Get
        [HttpPost("shijus/get")]
        public Task<TZResponse> ShiJus_Get([FromBody] ShiJus_Get_Args args)
        {
            return RunAction(CheckAuthType.User, async () =>
            {
                var entity = await _ShiJusService.GetById(args.F_Id);
                return Success("获取成功", new
                {
                    entity.F_Id,
                    entity.F_N,
                    entity.F_P,
                });

            });
        }
        public class ShiJus_Get_Args
        {
            public string? F_Id { get; set; }
        }
        #endregion ShiJus_Get
        #endregion

        #region TouXiangs
        #region TouXiangs_List
        [HttpPost("touxiangs/list")]
        public Task<TZResponse> TouXiangs_List([FromBody] TouXiangs_List_Args args)
        {
            return RunAction(CheckAuthType.User, async () =>
            {
                var list = await _TouXiangsService.GetList(null);
                return Success("获取成功", list.Select(t => new
                {
                    t.F_Id,
                    t.F_ImgSrc,
                    t.F_CreateTime
                }).ToList());
            });
        }
        public class TouXiangs_List_Args
        {
        }
        #endregion TouXiangs_List
        #region TouXiangs_Save
        [HttpPost("touxiangs/save")]
        public Task<TZResponse> TouXiangs_Save([FromBody] TouXiangs_Save_Args args)
        {
            return RunAction(CheckAuthType.User, async () =>
            {
                await _TouXiangsService.Save(new TouXiangsEntity
                {
                    F_Id = args.F_Id,
                    F_ImgSrc = args.F_ImgSrc,
                });
                return Success("保存成功");
            });
        }
        public class TouXiangs_Save_Args
        {
            public string? F_Id { get; set; }
            public string? F_ImgSrc { get; set; }
        }
        #endregion TouXiangs_Save
        #region TouXiangs_Delete
        [HttpPost("touxiangs/delete")]
        public Task<TZResponse> TouXiangs_Delete([FromBody] TouXiangs_Delete_Args args)
        {
            return RunAction(CheckAuthType.User, async () =>
            {
                await _TouXiangsService.Delete(args.F_Id);
                return Success("删除成功");
            });
        }
        public class TouXiangs_Delete_Args
        {
            public string? F_Id { get; set; }
        }
        #endregion TouXiangs_Delete
        #region TouXiangs_Get
        [HttpPost("touxiangs/get")]
        public Task<TZResponse> TouXiangs_Get([FromBody] TouXiangs_Get_Args args)
        {
            return RunAction(CheckAuthType.User, async () =>
            {
                var entity = await _TouXiangsService.GetById(args.F_Id);
                return Success("获取成功", new
                {
                    entity.F_Id,
                    entity.F_ImgSrc
                });

            });
        }
        public class TouXiangs_Get_Args
        {
            public string? F_Id { get; set; }
        }
        #endregion TouXiangs_Get
        #endregion

        #region Statistics
        #region Statistics_Count_RequestAndIP
        [HttpPost("statistics/count/requestandip")]
        public Task<TZResponse> Statistics_Count_RequestAndIP([FromBody] Statistics_Count_RequestAndIP_Args args)
        {
            return RunAction(CheckAuthType.User, async () =>
            {
                DateTime start=DateTime.Now.AddDays(-30);
                if(!string.IsNullOrEmpty(args.F_StartTime)&&DateTime.TryParse(args.F_StartTime,out DateTime st))
                {
                    start =st;
                }
                DateTime end = DateTime.Now;
                if (!string.IsNullOrEmpty(args.F_EndTime) && DateTime.TryParse(args.F_EndTime, out DateTime et))
                {
                    end = et;
                }
                var list = await _StatisticsServiceService.StatisticalRequestAndIPCount(start,end);
                return Success("获取成功", list);
            });
        }
        public class Statistics_Count_RequestAndIP_Args
        {
            public string? F_StartTime { get; set; }
            public string? F_EndTime { get; set; }
        }
        #endregion Statistics_Count_RequestAndIP
        #region Statistics_Count_IPRequest
        [HttpPost("statistics/count/iprequest")]
        public Task<TZResponse> Statistics_Count_IPRequest([FromBody] Statistics_Count_IPRequest_Args args)
        {
            return RunAction(CheckAuthType.User, async () =>
            {
                DateTime date = DateTime.Now;
                if (!string.IsNullOrEmpty(args.F_Date) && DateTime.TryParse(args.F_Date, out DateTime t))
                {
                    date = t;
                }
                var list = await _StatisticsServiceService.StatisticalIPRequestCount(date);
                return Success("获取成功", list);
            });
        }
        public class Statistics_Count_IPRequest_Args
        {
            public string? F_Date { get; set; }
        }
        #endregion Statistics_Count_IPRequest
        #region Statistics_Count_CountryRequest
        [HttpPost("statistics/count/countryrequest")]
        public Task<TZResponse> Statistics_Count_CountryRequest([FromBody] Statistics_Count_IPRequest_Args args)
        {
            return RunAction(CheckAuthType.User, async () =>
            {
                var list = await _StatisticsServiceService.StatisticalCountryRequestCount();
                return Success("获取成功", list);
            });
        }
        public class Statistics_Count_CountryRequest_Args
        {
        }
        #endregion Statistics_Count_IPRequest
        #region Statistics_Count_ProvinceRequest
        [HttpPost("statistics/count/provincerequest")]
        public Task<TZResponse> Statistics_Count_ProvinceRequest([FromBody] Statistics_Count_ProvinceRequest_Args args)
        {
            return RunAction(CheckAuthType.User, async () =>
            {
                if (!string.IsNullOrEmpty(args.F_Country))
                {
                    args.F_Country= "China";    
                }
                var list = await _StatisticsServiceService.StatisticalProvicneRequestCount(args.F_Country);
                return Success("获取成功", list);
            });
        }
        public class Statistics_Count_ProvinceRequest_Args
        {
            public string? F_Country { get; set; }
        }
        #endregion Statistics_Count_IPRequest
        #endregion Statistics

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