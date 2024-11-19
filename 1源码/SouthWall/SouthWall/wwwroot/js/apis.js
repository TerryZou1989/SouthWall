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