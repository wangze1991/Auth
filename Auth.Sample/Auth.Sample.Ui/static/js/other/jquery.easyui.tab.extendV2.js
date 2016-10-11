

 /**  
 * @author {CaoGuangHui}  
 */
$.extend($.fn.tabs.methods, {
    /**
     * 加载iframe内容  
     * @param  {jq Object} jq     [description]  
     * @param  {Object} params    params.which:tab的标题或者index;params.iframe:iframe的相关参数  
     * @return {jq Object}        [description]  
     */
    loadTabIframe: function (jq, params) {
        return jq.each(function () {
            var $tab = $(this).tabs('getTab', params.which);
            if ($tab == null) return;

            var $tabBody = $tab.panel('body');

            //销毁已有的iframe   
            var $frame = $('iframe', $tabBody);
            if ($frame.length > 0) {
                try {//跨域会拒绝访问，这里处理掉该异常   
                    $frame[0].contentWindow.document.write('');
                    $frame[0].contentWindow.close();
                } catch (e) {
                    //Do nothing   
                }
                $frame.remove();
                if ($.browser.msie) {
                    CollectGarbage();
                }
            }
            $tabBody.html('');

            $tabBody.css({ 'overflow': 'hidden', 'position': 'relative' });
            var $mask = $('<div style="position:absolute;z-index:2;width:100%;height:100%;background:#ccc;z-index:1000;opacity:0.3;filter:alpha(opacity=30);"><div>').appendTo($tabBody);
            var $maskMessage = $('<div class="mask-message" style="z-index:3;width:auto;height:16px;line-height:16px;position:absolute;top:50%;left:50%;margin-top:-20px;margin-left:-92px;border:2px solid #d4d4d4;padding: 12px 5px 10px 30px;background: #ffffff url(\'../../themes/default/images/loading.gif\') no-repeat scroll 5px center;">' + (params.iframe.message || 'Processing, please wait ...') + '</div>').appendTo($tabBody);
            var $containterMask = $('<div style="position:absolute;width:100%;height:100%;z-index:1;background:#fff;"></div>').appendTo($tabBody);
            var $containter = $('<div style="position:absolute;width:100%;height:100%;z-index:0;"></div>').appendTo($tabBody);

            var iframe = document.createElement("iframe");
            iframe.src = params.iframe.src;
            iframe.frameBorder = params.iframe.frameBorder || 0;
            iframe.height = params.iframe.height || '100%';
            iframe.width = params.iframe.width || '100%';
            if (iframe.attachEvent) {
                iframe.attachEvent("onload", function () {
                    $([$mask[0], $maskMessage[0]]).fadeOut(params.iframe.delay || 'slow', function () {
                        $(this).remove();
                        if ($(this).hasClass('mask-message')) {
                            $containterMask.fadeOut(params.iframe.delay || 'slow', function () {
                                $(this).remove();
                            });
                        }
                    });
                });
            } else {
                iframe.onload = function () {
                    $([$mask[0], $maskMessage[0]]).fadeOut(params.iframe.delay || 'slow', function () {
                        $(this).remove();
                        if ($(this).hasClass('mask-message')) {
                            $containterMask.fadeOut(params.iframe.delay || 'slow', function () {
                                $(this).remove();
                            });
                        }
                    });
                };
            }
            $containter[0].appendChild(iframe);
        });
    },
    /**
     * 增加iframe模式的标签页  
     * @param {[type]} jq     [description]  
     * @param {[type]} params [description]  
     */
    addIframeTab: function (jq, params) {
        return jq.each(function () {
            if (params.tab.href) {
                delete params.tab.href;
            }
            $(this).tabs('add', params.tab);
            $(this).tabs('loadTabIframe', { 'which': params.tab.title, 'iframe': params.iframe });
        });
    },
    /**
     * 更新tab的iframe内容  
     * @param  {jq Object} jq     [description]  
     * @param  {Object} params [description]  
     * @return {jq Object}        [description]  
     */
    updateIframeTab: function (jq, params) {
        return jq.each(function () {
            params.iframe = params.iframe || {};
            if (!params.iframe.src) {
                var $tab = $(this).tabs('getTab', params.which);
                if ($tab == null) return;
                var $tabBody = $tab.panel('body');
                var $iframe = $tabBody.find('iframe');
                if ($iframe.length === 0) return;
                $.extend(params.iframe, { 'src': $iframe.attr('src') });
            }
            $(this).tabs('loadTabIframe', params);
        });
    }
});

/*
http://www.easyui.info/archives/1283.html
$('#tabs').tabs('addIframeTab',{      
    tab:{title:'iframe'+count++,closable:true},      
    iframe:{src:'http://www.loststop.com'}      
});
addIframeTab方法的参数包含以下属性：
名称	参数类型	描述以及默认值
tab	object	该参数是对象，其属性列表同于tabs自带add方法的入参属性列表。
iframe.src	string	目标框架页面的地址，必填项。
iframe.height	string	框架标签iframe的高度，默认值为'100%'。
iframe.width	string	框架标签iframe的宽度，默认值为'100%'。
iframe.frameBorder	number	框架标签iframe的边框宽度，默认值为0。
iframe.message	string	加载中效果显示的消息，默认值为'Processing, please wait ...'

*/