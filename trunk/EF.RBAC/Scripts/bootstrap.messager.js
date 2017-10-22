/*!
 * bootstrap.messager v1.0.1
 *
 * Copyright 2017-2018 bootstrap-messager
 * Licensed under Kermit
 */

(function ($) {

    function S4() {
        return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
    }
    function guid() {
        return (S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4());
    }

    //wating
    var _w = function (title, msg, icon, fn) {
        var id = guid();
        //暂时没用，后期完善
        var _22 = $.extend({ title: "提示", msg: "", icon: "", interval: 2000 }, { title: title, msg: msg, icon: "", time: 2000 });
        var _17 = "";
        var _18 = "";
        switch (icon) {
            case "error":
                _17 = "<div class=\"text-danger\"><span class='glyphicon glyphicon-remove-sign'></span>&nbsp;&nbsp;" + msg + "</div>";
                _18 = "text-danger";
                break;
            case "info":
                _17 = "<div class=\"text-info\"><span class='glyphicon glyphicon-info-sign'></span>&nbsp;&nbsp;" + msg + "</div>";
                _18 = " text-info";
                break;
            case "question":
                _17 = "<div class=\"text-primary\"><span class='glyphicon glyphicon-question-sign'></span>&nbsp;&nbsp;" + msg + "</div>";
                _18 = " text-primary";
                break;
            case "warning":
                _17 = "<div class=\"text-warning\"><span class='glyphicon glyphicon-info-sign'></span>&nbsp;&nbsp;" + msg + "</div>";
                _18 = " text-warning";
                break;
            case "success":
                _17 = "<div class=\"text-success\"><span class='glyphicon glyphicon-ok-sign'></span>&nbsp;&nbsp;" + msg + "</div>";
                _18 = " text-success";
                break;
            case "muted":
                _17 = "<div class=\"text-muted\"><span class='glyphicon glyphicon-info-sign'></span>&nbsp;&nbsp;" + msg + "</div>";
                _18 = " text-muted";
                break;
        }

        var _h = '';
        _h += '<div class="modal fade" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" id="' + id + '">';
        _h += '    <div class="modal-dialog modal-sm">';
        _h += '        <div class="modal-content">';
        _h += '            <div class="modal-header">';
        _h += '                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>';
        _h += '                <h4 class="modal-title' + _18 + '">' + title + '</h4>';
        _h += '            </div>';
        _h += '            <div class="text-center" style="height: 100%; width: 100%; line-height: 4;">';
        _h += '                ' + _17;
        _h += '            </div>';
        _h += '        </div>';
        _h += '    </div>';
        _h += '</div>';

        $("body").append(_h);

        //关闭
        $("#" + id).find("button").on("click", function () {
            $("body").find("div[id='" + id + "']").modal('hide');
            $("body").find("div[id='" + id + "']").remove();
            if (fn) {
                fn();
            }
        });

        $("body").find("div[id='" + id + "']").modal("show");

        //关闭
        setTimeout(function () {
            $("body").find("div[id='" + id + "']").modal('hide');
            $("body").find("div[id='" + id + "']").remove();
        }, 10000);
        return $("#" + id);
    };

    //alert
    var _a = function (title, msg, icon, fn) {
        var id = guid();
        //暂时没用，后期完善
        var _22 = $.extend({ title: "提示", msg: "", icon: "", interval: 2000 }, { title: title, msg: msg, icon: "", time: 2000 });
        var _17 = "";
        var _18 = "";
        switch (icon) {
            case "error":
                _17 = "<div class=\"text-danger\"><span class='glyphicon glyphicon-remove-sign'></span>&nbsp;&nbsp;" + msg + "</div>";
                _18 = "text-danger";
                break;
            case "info":
                _17 = "<div class=\"text-info\"><span class='glyphicon glyphicon-info-sign'></span>&nbsp;&nbsp;" + msg + "</div>";
                _18 = " text-info";
                break;
            case "question":
                _17 = "<div class=\"text-primary\"><span class='glyphicon glyphicon-question-sign'></span>&nbsp;&nbsp;" + msg + "</div>";
                _18 = " text-primary";
                break;
            case "warning":
                _17 = "<div class=\"text-warning\"><span class='glyphicon glyphicon-info-sign'></span>&nbsp;&nbsp;" + msg + "</div>";
                _18 = " text-warning";
                break;
            case "success":
                _17 = "<div class=\"text-success\"><span class='glyphicon glyphicon-ok-sign'></span>&nbsp;&nbsp;" + msg + "</div>";
                _18 = " text-success";
                break;
            case "muted":
                _17 = "<div class=\"text-muted\"><span class='glyphicon glyphicon-info-sign'></span>&nbsp;&nbsp;" + msg + "</div>";
                _18 = " text-muted";
                break;
        }

        var _h = '';
        _h += '<div class="modal fade" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" id="' + id + '">';
        _h += '    <div class="modal-dialog modal-sm">';
        _h += '        <div class="modal-content">';
        _h += '            <div class="modal-header">';
        _h += '                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>';
        _h += '                <h4 class="modal-title' + _18 + '">' + title + '</h4>';
        _h += '            </div>';
        _h += '            <div class="text-center" style="height: 100%; width: 100%; line-height: 4;">';
        _h += '                ' + _17;
        _h += '                 <button type="button" class="btn btn-primary">' + $.messager.defaults.ok + '</button>';
        _h += '            </div>';
        _h += '        </div>';
        _h += '    </div>';
        _h += '</div>';

        $("body").append(_h);

        //关闭
        $("#" + id).find("button").on("click", function () {
            $("body").find("div[id='" + id + "']").modal('hide');
            $("body").find("div[id='" + id + "']").remove();
            $("body").removeClass();
            if (fn) {
                fn();
            }
        });

        $("body").find("div[id='" + id + "']").modal("show");

        //关闭
        var d = setTimeout(function () {
            $("body").find("div[id='" + id + "']").modal('hide');
            $("body").find("div[id='" + id + "']").remove();
            $("body").removeClass();

        }, 10000);
        var o = $("body").find("div[id='" + id + "']");
        return o;
    };

    //show
    var _s = function (options) {

    }

    //confirm
    var _c = function (title, msg, _12) {
        //暂时没用，后期完善
        var _22 = $.extend({ title: "提示", msg: "", icon: "", interval: 2000 }, { title: title, msg: msg, icon: "", time: 2000 });

        var _18 = "";
        _18 = " text-primary";
        var id = guid();
        var _h = '';
        _h += '<div class="modal fade" role="dialog" aria-labelledby="gridSystemModalLabel" id="' + id + '">';
        _h += '    <div class="modal-dialog" role="document" style="width: 450px;">';
        _h += '        <div class="modal-content">';
        _h += '            <div class="modal-header">';
        _h += '                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>';
        _h += '                <h4 class="modal-title' + _18 + '">' + title + '</h4>';
        _h += '            </div>';
        _h += '            <div class="modal-body">';
        _h += '                <div class="text-left" style="height: 100%; width: 100%; line-height: 4;">';
        _h += '                    ' + msg;
        //_h += '                    <div class="text-primary"><span class="glyphicon glyphicon-question-sign"></span>&nbsp;&nbsp;' + msg + '</div>';
        _h += '                </div>';
        _h += '            </div>';
        _h += '            <div class="modal-footer">';
        _h += '                <button type="button" class="btn btn-primary">' + $.messager.defaults.ok + '</button>';
        _h += '                <button type="button" class="btn btn-default" data-dismiss="modal">' + $.messager.defaults.cancel + '</button>';
        _h += '            </div>';
        _h += '        </div>';
        _h += '    </div>';
        _h += '</div>';

        $("body").append(_h);

        //此处后期进行完善，按钮采用动态增加的方式，目前只固定两个
        $("body").find('div[id="' + id + '"] button.btn-primary').on("click", function () {
            _12[$.messager.defaults.ok]();
            $("body").find('div[id="' + id + '"]').remove();
            $("body").removeClass();
        });

        $("body").find('div[id="' + id + '"] button.btn-default').on("click", function () {
            _12[$.messager.defaults.cancel]();
            $("body").find('div[id="' + id + '"]').remove();
            $("body").removeClass();
        });

        //if (_12) {
        //    //var tb = $("<div class=\"messager-button\"></div>").appendTo(win);
        //    for (var _13 in _12) {
        //        $(":button").text(_13).addClass("btn btn-default").on("click", eval(_12[_13])).appendTo('div[id="' + id + '"] div.modal-footer');
        //    }
        //}

        $("body").find("div[id='" + id + "']").modal("show");

        //setTimeout(function () {
        //    $("body").find("div[id='dlgMsg_0001']").modal('hide');
        //    $("body").find("div[id='dlgMsg_0001']").remove();
        //}, 5000);
    }

    $.messager = {};
    $.messager.defaults = { ok: "确定", cancel: "取消" };

    $.messager.wait = function (title, msg, icon, fn) {
        return _w(title, msg, icon, fn);
    }

    //显示警告窗口。参数：
    //title：在头部面板显示的标题文本。
    //msg：显示的消息文本。
    //icon：显示的图标图像。可用值有：error,question,info,warning,success,muted。
    //fn: 在窗口关闭的时候触发该回调函数。
    $.messager.alert = function (title, msg, icon, fn) {
        return _a(title, msg, icon, fn);
    }

    //在屏幕右下角显示一条消息窗口。该选项参数是一个可配置的对象：
    //showType：定义将如何显示该消息。可用值有：null,slide,fade,show。默认：slide。
    //showSpeed：定义窗口显示的过度时间。默认：600毫秒。
    //width：定义消息窗口的宽度。默认：250px。
    //height：定义消息窗口的高度。默认：100px。
    //title：在头部面板显示的标题文本。
    //msg：显示的消息文本。
    //style：定义消息窗体的自定义样式。
    //timeout：如果定义为0，消息窗体将不会自动关闭，除非用户关闭他。如果定义成非0的树，消息窗体将在超时后自动关闭。默认：4秒。
    $.messager.show = function (options) {
        alert("show");
    }



    //显示一个包含“确定”和“取消”按钮的确认消息窗口。参数：
    //title：在头部面板显示的标题文本。
    //msg：显示的消息文本。
    //fn(b): 当用户点击“确定”按钮的时侯将传递一个true值给回调函数，否则传递一个false值。
    $.messager.confirm = function (title, msg, fn) {
        var _1a = "<div class=\"text-primary\"><span class='glyphicon glyphicon-question-sign'></span>&nbsp;&nbsp;" + msg + "</div>";
        var _1b = {};
        _1b[$.messager.defaults.ok] = function () {
            //win.window("close");
            if (fn) {
                fn(true);
                return false;
            }
        };
        _1b[$.messager.defaults.cancel] = function () {
            //win.window("close");
            if (fn) {
                fn(false);
                return false;
            }
        };
        var win = _c(title, _1a, _1b);
        return win;
    }


})(jQuery)