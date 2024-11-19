// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    document.querySelectorAll("[data-bs-togglee='popover']").forEach(popover => {
        new bootstrap.Popover(popover)
    })
});

var IIS_Root = "";

var com = {
    UUID: function () {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            const r = Math.random() * 16 | 0;
            const v = c === 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    },
    GenPageId: function () {
        return com.UUID().replace(/-/g, '');
    },
    LoadView: function (opt) {
        $.ajax({
            url: IIS_Root + opt.url,
            type: "GET",
            success: function (d) {
                if (opt.success) {
                    opt.success(d);
                }
            }
        });
    },
    SetIISRoot: function (root) {
        if (root && root != "" && root.length > 0) {
            IIS_Root = "/" + root;
        }
    },
    RemoveLoginInfo: function () {
        $.removeCookie("tz1989_LoginToken", { path: '/' });
    },
    SetLoginInfo: function (loginInfo) {
        $.cookie("tz1989_LoginToken", JSON.stringify(loginInfo), { expires: 1, path: '/' })
    },
    GetLoginInfo: function () {
        var loginInfoStr = $.cookie("tz1989_LoginToken");
        if (loginInfoStr) {
            try {
                var loginInfo = JSON.parse(loginInfoStr);
                return loginInfo;
            } catch (e) {
                return null;
            }

        }
        return null;
    },
    GetLoginToken: function () {
        var loginInfo = com.GetLoginInfo();
        if (loginInfo) {
            return loginInfo.f_Token;
        }
        return null;
    },
    GetLoginAccount: function () {
        var loginInfo = com.GetLoginInfo();
        if (loginInfo) {
            return loginInfo.f_Account;
        }
        return null;
    },
    CheckLoginStatus: function () {
        var token = com.GetLoginToken();
        console.log(token);
        if (token) {
            apiClient.auth.checkToken(token, function (d) {
                if (!d) {
                    console.log(d);
                    alert("账号过期，请重新登录");
                    location.href = `${IIS_Root}/login`;
                }
            }, function (err) {
                alert("账号过期，请重新登录");
                location.href = `${IIS_Root}/login`;
            })
        } else {
            alert("账号过期，请重新登录");
            location.href = `${IIS_Root}/login`;
        }
    },
    fmtDateTime: function (t) {
        var date = util.ConverToDate(t);
        return fmtDateTime(date);
    }
};

var util = {
    GetCurrentTime: function () {
        var date = new Date();
        return fmtDateTime(date);
    },
    GetTimeAddDays: function (day) {
        var date = new Date();
        var ts = date.getTime() + (day * 1000 * 60 * 60 * 24);
        date = new Date(ts);
        return fmtDateTime(date);
    },
    GetTimeAddHours: function (hour) {
        var date = new Date();
        var ts = date.getTime() + (1000 * 60 * 60 * hour);
        date = new Date(ts);
        return fmtDateTime(date);
    },
    SetTimeAddHours: function (time, hour) {
        var date = util.ConverToDate(time);
        var ts = date.getTime() + (1000 * 60 * 60 * hour);
        date = new Date(ts);
        return fmtDateTime(date);
    },
    GetTimeAddSeconds: function (second) {
        var date = new Date();
        var ts = date.getTime() + (1000 * second);
        date = new Date(ts);
        return fmtDateTime(date);
    },
    SetTimeAddSeconds: function (time, second) {
        var date = util.ConverToDate(time);
        var ts = date.getTime() + (1000 * second);
        date = new Date(ts);
        return fmtDateTime(date);
    },
    DiffTime: function (time1, time2, t) {
        var date1 = util.ConverToDate(time1);
        var date2 = util.ConverToDate(time2);
        var ts1 = date1.getTime();
        var ts2 = date2.getTime();
        var ts = ts2 - ts1;
        var diff = ts / 1000;
        switch (t) {
            case "s":
                break;
            case "m":
                diff = diff / 60;
                break;
            case "h":
                diff = diff / 60 / 60;
                break;
            case "d":
                diff = diff / 60 / 60 / 24;
                break;
        }
        return diff;
    },
    ConverToDate: function (dateStr) {
        var str = dateStr.replace('T', " ").replace(/-/g, "/");
        return new Date(str);
    }
};
var fmtDateTime = function (date) {
    var year = date.getFullYear();
    var month = date.getMonth() + 1;
    var day = date.getDate();
    var hour = date.getHours();
    var minute = date.getMinutes();
    var second = date.getSeconds();
    var str = year + "-" + (month < 10 ? ("0" + month) : month) + "-" + (day < 10 ? ("0" + day) : day) + " " + (hour < 10 ? ("0" + hour) : hour) + ":" + (minute < 10 ? ("0" + minute) : minute) + ":" + (second < 10 ? ("0" + second) : second);
    return str;
}
var fmtDate = function (date) {
    var year = date.getFullYear();
    var month = date.getMonth() + 1;
    var day = date.getDate();
    var str = year + "-" + (month < 10 ? ("0" + month) : month) + "-" + (day < 10 ? ("0" + day) : day);
    return str;
}

var copyToClipboard = function (txt) {
    if (window.clipboardData) {
        window.clipboardData.clearData();
        clipboardData.setData("Text", txt);
        alert("复制成功！");
    } else if (navigator.userAgent.indexOf("Opera") != -1) {
        window.location = txt;
    } else if (window.netscape) {
        try {
            netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
        } catch (e) {
            alert("被浏览器拒绝！\n请在浏览器地址栏输入'about:config'并回车\n然后将 'signed.applets.codebase_principal_support'设置为'true'");
        }
        var clip = Components.classes['@mozilla.org/widget/clipboard;1'].createInstance(Components.interfaces.nsIClipboard);
        if (!clip)
            return;
        var trans = Components.classes['@mozilla.org/widget/transferable;1'].createInstance(Components.interfaces.nsITransferable);
        if (!trans)
            return;
        trans.addDataFlavor("text/unicode");
        var str = new Object();
        var len = new Object();
        var str = Components.classes["@mozilla.org/supports-string;1"].createInstance(Components.interfaces.nsISupportsString);
        var copytext = txt;
        str.data = copytext;
        trans.setTransferData("text/unicode", str, copytext.length * 2);
        var clipid = Components.interfaces.nsIClipboard;
        if (!clip)
            return false;
        clip.setData(trans, null, clipid.kGlobalClipboard);
        alert("复制成功！");
    }
}