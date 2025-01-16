var api_url = `/api/welldone`;
var apiClient = {
    ipinfos: {
        save: function (F_IP, F_Country, F_Province, F_City, F_Lat, F_Lon, succ, err) {
            apiPostAction({
                action: "ipinfos/save",
                data: {
                    F_IP, F_Country, F_Province, F_City, F_Lat, F_Lon
                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        }
    },
    datas: {
        get: function (F_Id, succ, err) {
            apiPostAction({
                action: "datas/get",
                data: {
                    F_Id
                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        }
    },
    times: {
        get: function (F_Id, succ, err) {
            apiPostAction({
                action: "times/get",
                data: {
                    F_Id
                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        }
    },
    videos: {
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
    },
    audios: {
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
    },
    articles: {
        get: function (F_Id, succ, err) {
            apiPostAction({
                action: "articles/get",
                data: {
                    F_Id
                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        }
    },
    messages: {
        save: function (F_UserName, F_Content, F_UserEmail, F_ShiJuId,F_ShiJuN,succ, err) {
            apiPostAction({
                action: "messages/save",
                data: {
                    F_Content, F_UserName, F_UserEmail, F_ShiJuId, F_ShiJuN
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
        }
    },
    websites: {
        get: function (F_Id, succ, err) {
            apiPostAction({
                action: "websites/get",
                data: {
                    F_Id
                },
                succ: function (d) { if (succ) succ(d) },
                err: function (ex) { if (err) err(ex) }
            });
        }
    },
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