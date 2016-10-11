/// <reference path="../../vendor/require/require.js" />
/// <reference path="../../vendor/util/util.js" />
/// <reference path="../../vendor/easyui/easyui-lang-zh_CN.js" />
require(['jquery', 'easyui', 'util'], function ($,_easyui,util) {
    require(['domReady'], function (dom) {
    });

    function EasyUiList(opt) {
        var defaultOpt = {
            add:'',
            del: '',
            search: '',
            reload: '',
            url:'',
            grid:$('#grid')
        };
        this.opt = $.extentd({}, defaultopt, opt);
    }

    var proto = EasyUiList.prototype;

    proto._init = function () { };

    proto_gridInit = function () {


    };


});