/*!
 * bootstrap.datagrid v1.0.1
 *
 * Copyright 2017-2018 bootstrap-datagrid
 * Licensed under Kermit
 */
var $datagrid = function ($dg, options) {

    var obj = $.extend({}, {
        //contentType: "application/json",
        method: 'post',//数据请求方式
        url: "",//请求数据的地址
        //height: $(window).height() - 300,
        //width: 200,
        striped: true,
        pagination: false,
        pageNumber: 1,
        pageSize: 10,
        pageList: [5, 10, 15],
        uniqueId: "ID",
        sidePagination: "server", //服务端请求
        queryParams: function (params) {
            var temp = {
                pageSize: params.pageSize,
                pageIndex: params.pageNumber,
                //name: $("#txtName").val(),
                sortOrder: params.sortOrder
            };
            params = $.extend(params, { pageIndex: params.pageNumber });
            return params;
        },
        queryParamsType: "",
        sortOrder: "desc",//默认排序方式，降序排列
        columns: [],
        onLoadSuccess: function () {

        },
        onLoadError: function () {
        }
    }, options);

    obj.datagrid = function () {
        $dg.bootstrapTable("destroy");
        $dg.bootstrapTable(obj);
    }

    obj.refresh = function (o) {
        $dg.bootstrapTable('refresh', o);
    }

    obj.destroy = function () {
        $dg.bootstrapTable('destroy');
    }

    obj.hide = function () {
        $dg.bootstrapTable('hide');
    }
    obj.getOptions = function () {
        return $dg.bootstrapTable('getOptions');
    }

    obj.updateRow = function (index, row) {
        $dg.bootstrapTable("updateRow", { index: index, row: row });
    }

    obj.getData = function () {
        return $dg.bootstrapTable("getData", true);
    }

    obj.removeAll = function () {
        //console.log("removeAllremoveAllremoveAll");
        $dg.bootstrapTable('removeAll');
    }
    return obj;
}