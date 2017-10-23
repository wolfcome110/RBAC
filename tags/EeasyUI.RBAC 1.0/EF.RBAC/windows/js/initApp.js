$(function () {

    $('body').app({
        loadUrl: {
            //远程数据加载路径
            app: '/Desktop/Desktop', //app数据
            startMenu: '/windows/json/startMenu.js', //开始菜单数据
            widget: '/windows/json/widget.js'
        },
        onTaskBlankContextMenu: onTaskBlankContextMenu,
        onAppContextMenu: onAppContextMenu,
        onWallContextMenu: onWallContextMenu,
        onStartMenuClick: onStartMenuClick,
        onBeforeOpenApp: function (appOpt) {
            //appOpt.width = 500;
            //appOpt.height = 600;
            //console.log(appOpt);
            //return appOpt;
            return { width: 950, height: 680 }

        }
    });

    function onStartMenuClick(item) {
        var data = $(item.target).data("data");
        $('body').app("createwindow", data);
    }

    var appListMenuData = [
        {
            "text": "打开"
        },
        {
            "text": "关闭"
        },
        {
            "text": "关闭其他"
        },
        {
            "text": "关闭所有"
        },
        {
            "text": "任务管理器"
        },
        {
            "text": "属性"
        }
    ];

    var appListMenu = $('body').app('createMenu', { data: appListMenuData });

    function onTaskBlankContextMenu(e, appid) {
        if (appid) {
            console.log(appid);
            appListMenu.menu('findItem', '任务管理器').target.style.display = 'none';
            appListMenu.menu('findItem', '属性').target.style.display = 'none';
            appListMenu.menu('findItem', '打开').target.style.display = 'block';
            appListMenu.menu('findItem', '关闭').target.style.display = 'block';
            appListMenu.menu('findItem', '关闭其他').target.style.display = 'block';
            appListMenu.menu('findItem', '关闭所有').target.style.display = 'block';
        } else {
            appListMenu.menu('findItem', '任务管理器').target.style.display = 'block';
            appListMenu.menu('findItem', '属性').target.style.display = 'block';
            appListMenu.menu('findItem', '打开').target.style.display = 'none';
            appListMenu.menu('findItem', '关闭').target.style.display = 'none';
            appListMenu.menu('findItem', '关闭其他').target.style.display = 'none';
            appListMenu.menu('findItem', '关闭所有').target.style.display = 'none';
        }

        appListMenu.menu('show', {
            left: e.pageX,
            top: e.pageY
        });
        e.preventDefault();
    }


    var wallMenuData = [
        {
            "text": "属性",
            "href": "http://www.sucaijiayuan.com"
        },
        '-',
        {
            "text": "关于",
            "href": "http://www.sucaijiayuan.com"
        }
    ];
    var appMenuData = [
        {
            "text": "打开"
        },
        '-',
        {
            "text": "属性"
        }
    ];

    var wallMenu = $('body').app('createMenu', { data: wallMenuData, opt: { onClick: onStartMenuClick } });
    var appMenu = $('body').app('createMenu', { data: appMenuData, opt: { onClick: onAppContextMenuClick } });

    var APPID;
    function onAppContextMenu(e, appid) {
        appMenu.menu('show', {
            left: e.pageX,
            top: e.pageY
        });
        APPID = appid;
    }

    function onAppContextMenuClick(item) {
        if (item.text == '打开') {
            $("li[app_id='" + APPID + "']").dblclick();
        }
    }

    function onWallContextMenu(e) {
        wallMenu.menu('show', {
            left: e.pageX,
            top: e.pageY
        });
    }
});
