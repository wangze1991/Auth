define(['jquery', 'easyui'], function ($) {
    if ($.fn.combobox) {
        $.extend($.fn.combobox.methods, {
            //设置选中值
            selectValue: function (jq, value) {
                return jq.each(function () {
                    if (!(value || value == '0'||value===false)) return;
                    if (!$.isArray(value)) {
                        value = [value];
                    }
                    var self = $(this);
                    var data = self.combobox('getData');
                    if (!data) return;
                    $.each(data, function (i, item) {
                        $.each(value, function (j, v) {
                            if (v.toString() == item['value'])
                            {
                                self.combobox('setValue', item['value']);
                                self.combobox('setText', item['text']);
                            }
                                
                        });
                    });
                });

       
            }
        });
    }
});


/*
(function (factory) {
    if (typeof exports == "object" && typeof module == "object") // CommonJS
        factory(require("jquery"));
    else if (typeof define == "function" && define.amd)// AMD
        define(["jquery", "easyui"], factory);
    else
        factory();
})(function ($) {
    if ($.fn.combobox) {
        $.extend($.fn.combobox.methods, {
            //设置选中值
            selectValue: function (value) {
                if (!(value || value == '0')) return;
                if (!$.isArray(value)) {
                    value = [value];
                }
                var self = $(this);
                var data = self.combobox('getData');
                if (!data) return;
                $.each(data, function (i, item) {
                    $.each(value, function (j, v) {
                        if (v == item['value'])
                            self.combobox('select', item['text']);
                    });
                });
            }
        });
    }

});
*/