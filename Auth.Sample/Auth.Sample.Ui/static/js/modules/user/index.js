require(['jquery', 'util', 'easyui', 'eMessager', 'eUtil', 'template',], function ($, util, easyui, eMessager, eUtil, template) {
    var exporter = {};
    exporter.opts = {
        grid: $('#grid'),
        roleGrid: $('#role-grid'),
        url: '/User/GetUserList',
        roleUrl: '/Role/GetRoleList',
        form: 'ff',
        formAddUrl: '',
        formEditUrl: '',
        tmp: 'edit_template'
    };

    exporter.init = function () {
        exporter.grid.init();
        exporter.button.init();
        exporter.roleGrid.init();
        exporter.tabs.init();
    }

    //**grid**
    exporter.grid = {};
    exporter.grid.init = function () {
        exporter.opts.grid.datagrid({
            url: exporter.opts.url,
            rownumbers: true,
            border: false,
            singleSelect: true,
            rownumbers: false,
            nowrap: false,
            striped: false,
            columns: this.getColumn(),
            idField:'Id',
            frozenColumns: [[
                { field: 'Id', checkbox: true }
            ]],
            onSelect: function (index, row) {
                exporter.roleGrid.unSelectAll();
                var id = row['Id'];
                eUtil.ajax({
                    url: '/Role/GetRolesByUserId',
                    type: 'post',
                    data: { userId: id },
                    done: function (data) {
                        if (data && data.isSuccess) {
                            if (!data.data) return;
                            $.each(data.data, function (i, item) {
                                exporter.roleGrid.selectRecord(item);
                            });
                        } else {
                            eMessager.warning(data.msg || '出错了');
                        }

                    }
                });
            }
        });
    }
    exporter.grid.getColumn = function () {
        return [[
            { field: 'UserName', title: '用户名', width: 100 },
            {
                field: 'IsOpen', title: '启用', width: 80, formatter: function (value, row, index) {
                    if (value === true) return '是';
                    return '否';
                }
            }
        ]];
    }
    exporter.grid.reload = function () {
        exporter.opts.grid.datagrid('reload');
    }
    exporter.grid.getSelection = function () {
        var selection = exporter.opts.grid.datagrid('getSelections');
        if (!selection || selection.length == 0) throw '请选择一个用户';
        if (selection.length > 1) throw '只能选择一个用户';
        return selection[0];
    }

    //**grid end**


    //*role grid begin*
    exporter.roleGrid = {};
    exporter.roleGrid.init = function () {
        exporter.opts.roleGrid.datagrid({
            url: exporter.opts.roleUrl,
            rownumbers: true,
            border: true,
            singleSelect: false,
            rownumbers: true,
            nowrap: true,
            striped: true,
            checkOnSelect: true,
            selectOnCheck: true,
            columns: this.columns,
            idField:'Id',
            frozenColumns: [[
                { field: 'Id', checkbox: true }
            ]]
        });
    }
    exporter.roleGrid.columns = [[
        { field: 'Name', title: '角色名称', width: 120 },
        { field: 'Sort', title: '排序', width: 80 },
        { field: 'IsDisabledName', title: '启用', width: 80 }
    ]];

    //exporter.roleGrid.reloadByUserId = function (userId) {
    //    var obj=()
    //}

    exporter.roleGrid.reload = function (obj) {
        obj = obj || {};
        exporter.opts.roleGrid.datagrid('reload', obj);
    }
    exporter.roleGrid.getSelection = function () {
        var selection = exporter.opts.roleGrid.datagrid('getSelections');
        if (!selection || selection.length == 0) throw '请选择角色';
        return selection;
    }
    exporter.roleGrid.selectRow = function (index) {
        return exporter.opts.roleGrid.datagrid('selectRow', index);
    }
    exporter.roleGrid.selectRecord = function (idField) {
        return exporter.opts.roleGrid.datagrid('selectRecord', idField);
    }
    exporter.roleGrid.unSelectAll = function () {
        exporter.opts.roleGrid.datagrid('unselectAll');
    }
    //*role grid end*
    //button begin
    exporter.button = {};
    exporter.button.init = function () {
        var self = this;
        this.add();
        $('#btn-user-role-save').on('click', self.userRoleSave);
        $('[data-handle="edit"]').on('click', self.edit);

    };
    exporter.button.add = function () {
        $('[data-handle="add"]').on('click', function () {
            var html = template(exporter.opts.tmp, {});
            eUtil.showWindow({
                width: 580,
                height: 300,
                title: '新增',
                content: html,
                onBeforeOpen: function (body, win) {
                    win.$('#' + exporter.opts.form).form({
                        url: '/User/SaveUser',
                        onSubmit: function () {
                            var isValid = win.$(this).form('validate');
                            return isValid;
                        },
                        success: function (data) {
                            data = $.parseJSON(data);
                            if (data && data.isSuccess) {
                                body.close();
                                eMessager.info('保存成功', '提示');
                                exporter.grid.reload();
                            } else {
                                eMessager.warning('保存失败', '提示');
                            }
                        }
                    });
                },
                buttons: [{
                    text: '确认', iconCls: 'icon-ok', handler: function (body) {
                        window.top.$('#' + exporter.opts.form).submit();
                    }
                }, {
                    text: '取消', iconCls: 'icon-cancel', handler: function (body) {
                        body.close();
                    }
                }]

            });
        });

    };
    exporter.button.edit = function () {
            try{
                var html = ''
                    , selection = exporter.grid.getSelection();
                html = template(exporter.opts.tmp, selection);

                //eUtil.ajax({
                //    url: '/User/GetUser',
                //    data: {},
                //    done: function (data) {
                //        if (data && data.isSuccess) {
                //            var html = template(exporter.opts.tmp,data.data);
                //        } else {
                //            eMessager.warning('当前这条数据不存在');
                //            exporter.grid.reload();
                //        }
     
                //    },
                //    async:false
                //});
                eUtil.showWindow({
                    width: 580,
                    height: 300,
                    title: '编辑',
                    content: html,
                    onBeforeOpen: function (body, win) {
                        win.$('#IsOpen').combobox('selectValue', selection['IsOpen']);
                        win.$('#Sex').combobox('select', selection['Sex']);
                        win.$('#' + exporter.opts.form).form({
                            url: '/User/SaveUser',
                            onSubmit: function () {
                                var isValid = win.$(this).form('validate');
                                return isValid;
                            },
                            success: function (data) {
                                data = $.parseJSON(data);
                                if (data && data.isSuccess) {
                                    body.close();
                                    eMessager.info('保存成功', '提示');
                                    exporter.grid.reload();
                                } else {
                                    eMessager.warning('保存失败', '提示');
                                }
                            }
                        });
                 
      
                    },
                    buttons: [{
                        text: '确认', iconCls: 'icon-ok', handler: function (body) {
                            window.top.$('#' + exporter.opts.form).submit();
                        }
                    }, {
                        text: '取消', iconCls: 'icon-cancel', handler: function (body) {
                            body.close();
                        }
                    }]
                });
            }
        catch (ex) {
            throw ex;
               // eMessager.warning(ex);
            }
          
    
    };
    /*
    exporter.button.saveOrUpdate = function (type) {
        var selection = getSelection(),
            title = type === 'add' ? '新增' : '编辑';
        url = type === 'add' ? opt.formAddUrl : opt.formEditUrl;
        if (!selection) return;
        var html = template(opt.tmp, { type: type, id: selection['id'] });
        eUtil.showWindow({
            width: 580,
            height: 300,
            title: title,
            content: html,
            onBeforeOpen: function (body, win) {
                if (type === 'edit') {
                    eUtil.ajax({
                        url: '/Department/Get',
                        data: { id: selection['id'] },
                        done: function (data) {
                            win.$('#' + opt.form).formBind(data);
                        }

                    });
                }
                win.$('#' + opt.form).form({
                    url: url,
                    onSubmit: function () {
                        var isValid = win.$(this).form('validate');
                        return isValid;
                    },
                    success: function (data) {
                        data = $.parseJSON(data);
                        if (data && data.isSuccess) {
                            body.close();
                            eMessager.info('保存成功', '提示');
                            if (type === 'edit') {
                                var parent = opt.tt.tree('getParent', selection.target);
                                if (parent) {
                                    opt.tt.tree('reload', parent.target);
                                } else {
                                    opt.tt.tree('reload');
                                }

                            } else {
                                opt.tt.tree('reload', selection.target);
                            }
                        } else {
                            eMessager.warning('保存失败', '提示');
                        }
                    }
                });

            },
            buttons: [{
                text: '确认', iconCls: 'icon-ok', handler: function (body) {
                    window.top.$('#' + opt.form).submit();
                }
            }, {
                text: '取消', iconCls: 'icon-cancel', handler: function (body) {
                    body.close();
                }
            }]
        });
    }
    */
    exporter.button.userRoleSave = function () {
        try {
            var userId = exporter.grid.getSelection()['Id'];
            var roles = function () {
                var object = exporter.roleGrid.getSelection();
                var result = [];
                $.each(object, function (i, item) {
                    result.push(item['Id']);
                });
                return result;
            }();
            eUtil.ajax({
                url: '/Role/SaveRoleUser',
                data: {userId:userId,roles:roles},
                done: function (data) {
                    eMessager.info('保存成功');
                }
            });
        } catch (ex) {
            eMessager.warning(ex);
        }

    }
    //button end

    //tab begin

    exporter.tabs = {
        id: '#tt',
        init: function () {
            $(this.id).tabs({
                fit: true,
                plain: true
            });
        }
    };



    //tab end




    exporter.init();
});
