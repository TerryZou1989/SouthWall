var api_url = `/api/welldone`;
var apiClient = {
    auth: {
        sendLoginCode: function (succ, err) {
            apiPostAction({
                action: "auth/sendlogincode",
                data: {
                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
        loginByCode: function (F_Code, succ, err) {
            apiPostAction({
                action: "auth/loginbycode",
                data: {
                    F_Code
                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
        checkToken: function (F_Token, succ, err) {
            apiPostAction({
                action: "auth/checktoken",
                data: {
                    F_Token
                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        }
    },
    datas: {
        save: function (F_Id, F_Title, F_Description,F_Content, F_Type, F_CoverImg, F_Imgs, F_Url, F_VideoUrl, F_AudioUrl, F_IFrameCode, succ, err) {
            apiPostAction({
                action: "datas/save",
                data: {
                    F_Id, F_Title, F_Description, F_Content, F_Type, F_CoverImg, F_Imgs, F_Url, F_VideoUrl, F_AudioUrl, F_IFrameCode
                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
        delete: function (F_Id, succ, err) {
            apiPostAction({
                action: "datas/delete",
                data: {
                    F_Id
                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
        get: function (F_Id, succ, err) {
            apiPostAction({
                action: "datas/get",
                data: {
                    F_Id
                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
        list: function (succ, err) {
            apiPostAction({
                action: "datas/list",
                data: {

                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
    },
    times: {
        save: function (F_Id,F_Title, F_Content,F_Url, F_Imgs, F_Video, succ, err) {
            apiPostAction({
                action: "times/save",
                data: {
                    F_Id, F_Title, F_Content, F_Url, F_Imgs, F_Video
                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
        delete: function (F_Id, succ, err) {
            apiPostAction({
                action: "times/delete",
                data: {
                    F_Id
                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
        get: function (F_Id, succ, err) {
            apiPostAction({
                action: "times/get",
                data: {
                    F_Id
                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
        list: function (succ, err) {
            apiPostAction({
                action: "times/list",
                data: {

                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
    },
    videos: {
        save: function (F_Id, F_Title, F_VideoUrl, F_VideoCode, F_CoverImg, succ, err) {
            apiPostAction({
                action: "videos/save",
                data: {
                    F_Id, F_Title, F_VideoUrl, F_VideoCode, F_CoverImg
                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
        delete: function (F_Id, succ, err) {
            apiPostAction({
                action: "videos/delete",
                data: {
                    F_Id
                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
        get: function (F_Id, succ, err) {
            apiPostAction({
                action: "videos/get",
                data: {
                    F_Id
                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
        list: function (succ, err) {
            apiPostAction({
                action: "videos/list",
                data: {

                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
    },
    audios: {
        save: function (F_Id, F_Title, F_Description, F_AudioUrl, F_CoverImg, succ, err) {
            apiPostAction({
                action: "audios/save",
                data: {
                    F_Id, F_Title, F_Description, F_AudioUrl, F_CoverImg
                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
        delete: function (F_Id, succ, err) {
            apiPostAction({
                action: "audios/delete",
                data: {
                    F_Id
                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
        get: function (F_Id, succ, err) {
            apiPostAction({
                action: "audios/get",
                data: {
                    F_Id
                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
        list: function (succ, err) {
            apiPostAction({
                action: "audios/list",
                data: {

                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
    },
    articles: {
        save: function (F_Id, F_Title, F_Content, F_ArticleUrl, F_CoverImg, succ, err) {
            apiPostAction({
                action: "articles/save",
                data: {
                    F_Id, F_Title, F_Content, F_ArticleUrl, F_CoverImg
                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
        delete: function (F_Id, succ, err) {
            apiPostAction({
                action: "articles/delete",
                data: {
                    F_Id
                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
        get: function (F_Id, succ, err) {
            apiPostAction({
                action: "articles/get",
                data: {
                    F_Id
                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
        list: function (succ, err) {
            apiPostAction({
                action: "articles/list",
                data: {

                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
    },
    messages: {
        save: function (F_Content, F_UserName,F_UserEmail,succ, err) {
            apiPostAction({
                action: "messages/save",
                data: {
                    F_Content, F_UserName, F_UserEmail
                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
        delete: function (F_Id, succ, err) {
            apiPostAction({
                action: "messages/delete",
                data: {
                    F_Id
                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
        get: function (F_Id, succ, err) {
            apiPostAction({
                action: "messages/get",
                data: {
                    F_Id
                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
        list: function (succ, err) {
            apiPostAction({
                action: "messages/list",
                data: {

                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
        reply: function (F_Id,F_Content, succ, err) {
            apiPostAction({
                action: "messages/reply",
                data: {
                    F_Id, F_Content
                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
    },
    websites: {
        save: function (F_Id, F_Title, F_Description, F_Url, F_CoverImg, succ, err) {
            apiPostAction({
                action: "websites/save",
                data: {
                    F_Id, F_Title, F_Description, F_Url, F_CoverImg
                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
        delete: function (F_Id, succ, err) {
            apiPostAction({
                action: "websites/delete",
                data: {
                    F_Id
                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
        get: function (F_Id, succ, err) {
            apiPostAction({
                action: "websites/get",
                data: {
                    F_Id
                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
        list: function (succ, err) {
            apiPostAction({
                action: "websites/list",
                data: {

                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
    },
    shijus: {
        save: function (F_Id,F_P, F_N, succ, err) {
            apiPostAction({
                action: "shijus/save",
                data: {
                    F_Id, F_P, F_N
                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
        delete: function (F_Id, succ, err) {
            apiPostAction({
                action: "shijus/delete",
                data: {
                    F_Id
                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
        get: function (F_Id, succ, err) {
            apiPostAction({
                action: "shijus/get",
                data: {
                    F_Id
                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
        list: function (succ, err) {
            apiPostAction({
                action: "shijus/list",
                data: {

                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
    },
    touxiangs: {
        save: function (F_Id, F_ImgSrc, succ, err) {
            apiPostAction({
                action: "touxiangs/save",
                data: {
                    F_Id, F_ImgSrc
                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
        delete: function (F_Id, succ, err) {
            apiPostAction({
                action: "touxiangs/delete",
                data: {
                    F_Id
                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
        get: function (F_Id, succ, err) {
            apiPostAction({
                action: "touxiangs/get",
                data: {
                    F_Id
                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
        list: function (succ, err) {
            apiPostAction({
                action: "touxiangs/list",
                data: {

                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
    },
    statistics: {
        count_requestandip: function (F_StartTime, F_EndTime,succ, err) {
            apiPostAction({
                action: "statistics/count/requestandip",
                data: {
                    F_StartTime, F_EndTime
                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
        count_iprequest: function (F_Date, succ, err) {
            apiPostAction({
                action: "statistics/count/iprequest",
                data: {
                    F_Date
                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
        count_countryrequest: function (succ, err) {
            apiPostAction({
                action: "statistics/count/countryrequest",
                data: {
                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        },
        count_provincerequest: function (F_Country,succ, err) {
            apiPostAction({
                action: "statistics/count/provincerequest",
                data: {
                    F_Country
                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        }
    }
}
var apiPostAction = function (opt) {
    postAction(api_url, opt);
}

var postAction = function (url, opt) {
    apiPost({
        url: url + "/" + opt.action + "?token=" + com.GetLoginToken(),
        data: opt.data,
        success: function (d) {
            if (d && d.code == 200) {
                if (opt.succ != undefined) {
                    opt.succ(d.data);
                }
            }
            else if (d && d.code != 200) {
                if (opt.err != undefined) {
                    opt.err({ message: d.message });
                }
            } else {
                if (opt.err != undefined) {
                    opt.err({ message: "request error." });
                }
            }
        }, error: function (ex) {
            if (opt.err != undefined) {
                opt.err(ex);
            }
        }
    })
}
var apiPost = function (opt) {
    $.ajax({
        type: "POST",
        url: opt.url,
        data: JSON.stringify(opt.data),
        dataType: "JSON",
        contentType: "application/json; charset=utf-8",
        success: function (d) {
            if (d.code == 403) {
                com.RemoveLoginInfo();
                com.CheckLoginStatus();
            }
            opt.success(d);
        }, error: function (ex) {
            opt.error(ex);
        }
    });
}
var download = function (url) {
    open(url, "_blank");
}
var watchTime = function () {
    var scope = this;
    scope.ts1 = 0;
    scope.ts2 = 0;
    scope.start = function () {
        scope.ts1 = new Date().getTime();
    }

    scope.end = function () {
        scope.ts2 = new Date().getTime();
    }

    scope.getTS = function () {
        return scope.ts2 - scope.ts1;
    }
    return scope;
}