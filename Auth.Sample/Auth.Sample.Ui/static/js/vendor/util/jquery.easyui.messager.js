define(['easyui'], function () {
    var exports = {};
    exports._alert = function (icon, msg, title, fn) {
        var defautOpt = {
            icon: 'info',
            title: '',
            fn: function () { },
            msg: ''
        };
        switch (icon) {
            case 'info': defautOpt.title = '提示'; break;
            case 'warning': defautOpt.title = '警告'; break;
            case 'error': defautOpt.title = '错误'; break;
            case 'question': defautOpt.title = '问题'; break;
            default: break;
        }
        var opt = $.extend({}, defautOpt, { title: title, fn: fn, msg: msg, icon: icon });
        $.messager.alert(opt.title, opt.msg, opt.icon, opt.fn);
    }
    exports.info = function (msg, title, fn) {
        exports._alert('info', msg, title, fn);
    }
    exports.warning = function (msg, title, fn) {
        exports._alert('warning', msg, title, fn);
    }
    exports.error = function (msg, title, fn) {
        exports._alert('error', msg, title, fn);
    }
    exports.question = function (msg, title, fn) {
        exports._alert('question', msg, title, fn);
    }
    //确认提交
    exports.confirm = function (option) {
        var defaultOpt = {
            title: '确认',
            msg: '',
            fn: function (r) { }
        },
        opt = $.extend(defaultOpt,option);
        $.messager.confirm(opt.title,opt.msg,opt.fn);

    };
    return exports;
});