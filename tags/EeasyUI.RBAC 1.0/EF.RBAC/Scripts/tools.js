var tools = {
    waiting: function (options) {
        function UUID() {
            function S4() {
                return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
            }
            return "UUID-" + (S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4());
        }

        var defaults = {
            title: "",
            modal: true,
            width: 200,
            doSize:true,
            content: "<span><img src=\"/images/loading.gif\" style=\"vertical-align: sub;\" /> 请稍后...</span>"
        };
        var opts = $.extend({}, defaults, options);
        var id = UUID();
        $("<div/>").attr("id", id).css({ "background": "#F2F2F2", "border-width": "0px", "line-height": "30px", "text-align": "center" }).appendTo(top.$(window.document.body));
        var obj = top.$(window.document.body).find("div[id="+id+"]").window(opts);
        return obj;
    },
    close: function (obj) {
        top.$(obj).window("close");
    }
};
//$.extend();