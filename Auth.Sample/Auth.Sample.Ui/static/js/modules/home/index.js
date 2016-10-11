require(['jquery', 'util', 'easyui', 'formBind', 'vendor/util/jquery.easyui.combobox.extend'], function ($, util) {
    require(['domReady'], function (doc) {

        //菜单初始化
         

        //点击弹出tab页
        $('.easyui-linkbutton', '.easyui-accordion').on('click', function () {
            var url = $(this).data('url');
            open($(this).text(), url, $(this).linkbutton('options')['iconCls']);
            return false;
        });
        $('#tabs').tabs({
            onContextMenu: function (e, title) {
                e.preventDefault();
                $('#tabsMenu').menu('show', {
                    left:e.pageX,
                    top:e.pageY
                }).data('tabTitle', title);
            }
        });
    });
    function open(text, url, iconCls) {
        if ($('#tabs').tabs('exists', text)) {
            $('#tabs').tabs('select', text);
        } else {
            var id = new Date().getTime();
            $('#tabs').tabs('add', {
                title: text,
                closable: true,
                fit: true,
                content: '<iframe id="{0}" src="{1}" frameBorder=0 scrolling=no width="100%"></iframe>'.format(id, url),
                iconCls: iconCls
            });
            $('#{0}'.format(id)).load(function () {
                $(this).height($(this).parent().height());
            });
        }
    }

    //几个关闭事件的实现
    function CloseTab(menu, type) {
        var curTabTitle = $(menu).data("tabTitle");
        var tabs = $("#tabs");
        if (type === "close") {
            tabs.tabs("close", curTabTitle);
            return;
        }
        var allTabs = tabs.tabs("tabs");
        var closeTabsTitle = [];
        $.each(allTabs, function () {
            var opt = $(this).panel("options");
            if (opt.closable && opt.title != curTabTitle && type === "Other") {
                closeTabsTitle.push(opt.title);
            } else if (opt.closable && type === "All") {
                closeTabsTitle.push(opt.title);
            }
        });
        for (var i = 0; i < closeTabsTitle.length; i++) {
            tabs.tabs("close", closeTabsTitle[i]);
        }
    }

    function setIframeHeight(iframe) {
        if (iframe) {
            var iframeWin = iframe.contentWindow || iframe.contentDocument.parentWindow;
            if (iframeWin.document.body) {
                iframe.height = iframeWin.document.documentElement.scrollHeight || iframeWin.document.body.scrollHeight;
            }
        }
    };

});