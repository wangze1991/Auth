define(['easyui', 'util', 'eMessager'], function (easyui, util, eMessager) {

    var exporter = {},
        defaultDialogOpt = {
            title: '',
            width: 600,
            height: 400,
            closed: false,
            cahce: false,
            modal: true,
            onClose: function () {
                top.$(this).dialog('destroy');
            },
        };
    exporter.ajax = function (opt) {//修改，删除，更新ajax操作
        var defaultOpt = {
            type: 'post',
            url: '',
            dataType: 'json',
            async: true,
            cache: false,
            data: null,
            done: null,
            successMsg: '操作成功',
            errorMsg: '操作失败'
        };
        var option = $.extend({}, defaultOpt, opt);

        $.ajax(option).done(function (data) {
            if (data && data.isSuccess) {
                if (option.done) {
                    option.done(data);
                } else {
                    eMessager.info(data.msg || option.successMsg);
                }
            } else {
                if (data&&data.hasOwnProperty('isSuccess')) {          
                  var msg = (data.isSuccess === false && data.msg) || option.errorMsg;
                  eMessager.warning(msg);
                } else {
                    option.done(data);
                } 
            }
        }).fail(function () {
            eMessager.warning('网络连接失败,请检查网络');
        });

    }


    ///*当前页面弹出对话框*/
    //$.fn.dialog_page = function (opts) {
    //    var self = this;
    //    var defaults = {
    //        title: '对话框标题',
    //        iconCls: '',
    //        top: 5,
    //        cache: false,
    //        modal: true,
    //        html: '',
    //        url: '',
    //        btnText: '确定',
    //        btnIconCls: 'icon-ok',
    //        onClickButton: function (self) { alert('点击了确定按钮.'); }
    //    }
    //    opts = $.extend(defaults, opts);
    //    var btns = {
    //        buttons: [{
    //            text: opts.btnText, iconCls: opts.btnIconCls, handler: function () { opts.onClickButton(self); }
    //        }, {
    //            text: '取消', iconCls: 'icon-cancel', handler: function () { self.dialog('close'); }
    //        }]
    //    }
    //    opts = $.extend(btns, opts);
    //    //$.parser.parse(self);
    //    self.dialog(opts);
    //    return self;
    //}

    ////扩展easyui-dialog插件  适合iframe页面使用，会在顶层弹出对话框
    ////模板最外层必须为div
    //$.fn.dialog_ext = function (option) {
    //    var j = top.$;
    //    var self = this;
    //    var defaults = {
    //        title: '对话框标题,',
    //        iconCls: '',
    //        width: 640,
    //        height: 430,
    //        cache: false,
    //        modal: true,
    //        content: '',
    //        url: '',
    //        btnText: '确定',
    //        onBeforeOpenEx: function () {
    //            alert('打开之前');
    //        },
    //        onClose: function () {
    //            top.$(this).dialog('destroy');
    //        },
    //        btnIconCls: 'icon-ok',
    //        onBtnSuccess: function (win) { alert('点击了确定按钮.'); },
    //    }
    //    opts = j.extend(defaults, option);
    //    var win = j('<div></div>').appendTo('body');
    //    $(self).appendTo(win);
    //    var btns = {
    //        onBeforeOpen: function () {
    //            opts.onBeforeOpenEx(win);
    //        },
    //        buttons: [{
    //            text: opts.btnText, iconCls: opts.btnIconCls, handler: function () { opts.onBtnSuccess(win); }
    //        }, {
    //            text: '取消', iconCls: 'icon-cancel', handler: function () { win.dialog('destroy'); }
    //        }]
    //    }
    //    opts = j.extend(btns, opts);
    //    j.parser.parse(win);
    //    win.dialog(opts);
    //    return win;
    //}



    exporter.dialog = function (option) {
        var opt = $.extend({}, defaultDialogOpt, option),
            container = top.$("<div id='dialog" + new Date().getTime() + "' class='dialog'></div>");
        top.$('body').append(container);
        container.css('overflow', 'hidden')
                 .dialog(opt);

        return container;
    }

    //iframe弹窗
    exporter.iframeDialog = function (option) {
        var opt = $.extend({}, option),
            iframeId = new Date().getTime();
        opt['content'] = '<iframe  id="frame' + iframeId + '" src="{0}" width="100%" height="100%" frameborder="0" scrolling="no" ></iframe>'.format((opt.url || ''));
        var result = exporter.dialog(opt);
        var api = {
            close: function () {
                result.dialog('close');
            },
            opener: window
        };
        window.top.document.getElementById('frame' + iframeId).contentWindow.api = api;
        return result;
    }
    exporter.showWindow = function (options) {
        function getTop(w, options) {
            var _doc;
            try {
                _doc = w['top'].document;
                _doc.getElementsByTagName;
            } catch (e) {
                return w;
            }

            if (options.locate == 'document' || _doc.getElementsByTagName('frameset').length > 0) {
                return w;
            }

            if (options.locate == 'document.parent' || _doc.getElementsByTagName('frameset').length > 0) {
                return w.parent;
            }

            return w['top'];
        }
        options = options || {};
        var target;
        var winOpts = $.extend({}, {
            iconCls: 'icon-add',
            useiframe: false,
            locate: 'top',
            data: undefined,
            width: '60%',
            height: '60%',
            cache: false,
            minimizable: true,
            maximizable: true,
            collapsible: true,
            modal:true,
            resizable: true,
            loadMsg: $.fn.datagrid.defaults.loadMsg,
            showMask: false,
            onClose: function () { target.dialog('destroy'); }
        }, options);
        var iframe = null;
        if (/^url:/.test(winOpts.content)) {
            var url = winOpts.content.substr(4, winOpts.content.length);
            if (winOpts.useiframe) {
                iframe = $('<iframe>')
                    .attr('height', '100%')
                    .attr('width', '100%')
                    .attr('marginheight', 0)
                    .attr('marginwidth', 0)
                    .attr('frameborder', 0);

                setTimeout(function () {
                    iframe.attr('src', url);
                }, 10);

            } else {
                winOpts.href = url;
            }

            delete winOpts.content;
        }

        var _top = getTop(window, winOpts);
        var selfRefrence = {
            getData: function (name) {
                return winOpts.data ? winOpts.data[name] : null;
            },
            close: function () {
                target.panel('close');
            }
        };
        var warpHandler = function (handler) {
            if (typeof handler == 'function') {
                return function () {
                    handler(selfRefrence);
                };
            }

            if (typeof handler == 'string' && winOpts.useiframe) {
                return function () {
                    iframe[0].contentWindow[handler](selfRefrence);
                }
            }

            if (typeof handler == 'string') {
                return function () {
                    eval(_top[handler])(selfRefrence);
                }
            }
        }
        //包装toolbar中各对象的handler
        if (winOpts.toolbar && $.isArray(winOpts.toolbar)) {
            $.each(winOpts.toolbar, function (i, button) {
                button.handler = warpHandler(button.handler);
            });
        }

        //包装buttons中各对象的handler
        if (winOpts.buttons && $.isArray(winOpts.buttons)) {
            $.each(winOpts.buttons, function (i, button) {
                button.handler = warpHandler(button.handler);
            });
        }
        var onLoadCallback = winOpts.onLoad;
        var onBeforeOpenCallback = winOpts.onBeforeOpen;
        winOpts.onBeforeOpen = function () {
            onBeforeOpenCallback && onBeforeOpenCallback.call(this, selfRefrence, _top);
        }
        winOpts.onLoad = function () {
            onLoadCallback && onLoadCallback.call(this, selfRefrence, _top);
        }
        if (!/^#/.test(winOpts.locate)) {
            if (winOpts.useiframe && iframe) {
                if (winOpts.showMask) {
                    winOpts.onBeforeOpen = function () {
                        var panel =_top.$(this).panel('panel');
                        var header =_top.$(this).panel('header');
                        var body = _top.$(this).panel('body');
                        body.css('position', 'relative');
                        var mask = _top.$("<div class=\"datagrid-mask\" style=\"display:block;\"></div>").appendTo(body);
                        var msg =_top.$("<div class=\"datagrid-mask-msg\" style=\"display:block; left: 50%;\"></div>").html(winOpts.loadMsg).appendTo(body);
                        setTimeout(function () {
                            msg.css("marginLeft", -msg.outerWidth() / 2);
                        }, 5);
                    }
                }
                iframe.bind('load', function () {
                    if (iframe[0].contentWindow) {
                        winOpts.onLoad = onLoadCallback && onLoadCallback.call(this, selfRefrence, iframe[0].contentWindow);
                        if (winOpts.showMask) {
                            target.panel('body').children("div.datagrid-mask-msg").remove();
                           target.panel('body').children("div.datagrid-mask").remove();
                        }
                    }
                });
                target = _top.$('<div>').css({ 'overflow': 'hidden' }).append(iframe).dialog(winOpts);
            }//不是iframe
            else {
                target = _top.$('<div>').css({ 'overflow': 'hidden' }).dialog(winOpts);
            }
        } else {
            var locate = /^#/.test(winOpts.locate) ? winOpts.locate : '#' + winOpts.locate;
            target = $('<div>').css({ 'overflow': 'hidden' }).appendTo(locate).dialog($.extend({}, winOpts, { inline: true }));
            //winOpts.$.parser.parse(target);

        }
        return target;
    }

    return exporter;
});