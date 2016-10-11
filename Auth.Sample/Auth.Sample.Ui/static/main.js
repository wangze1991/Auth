require.config({
    baseUrl: '../../static/js/',
    shim: {
        'easyui': {
            deps: ['jquery'],
            exports: 'easyui'
        },
        'edatagrid': {
            deps: ['easyui'],
            exports: 'edatagrid'
        }
    },
    paths: {
        'jquery':'vendor/jquery/jquery-1.10.2.min',
        'easyui':'vendor/easyui/jquery.easyui.min',
        'local':'vendor/easyui/easyui-lang-zh_CN',
        'edatagrid':'vendor/easyui/jquery.edatagrid',
        'domReady':'vendor/require/domReady',
        'formBind':'vendor/jquery/plugin/jquery.FormBind',
        'eMessager':'vendor/util/jquery.easyui.messager',
        'util':'vendor/util/util',
        'eUtil':'vendor/util/jquery.easyui.util',
        'laypage':'vendor/laypage/laypage',
        'template':'vendor/artTemplate/template'
    }
});