/**绑定参数
*/
define(["jquery"], function ($) {
    $.fn.formBind = function (params) {
        var me = this;
        return me.each(function () {
            var self = this;
            new FormBind({ 'form': $(self), 'params': params }).bind();
        });
    }
    function FormBind(opt) {
        this.form = opt.form;//表单
        this.params = opt.params;//json返回值
    }
    FormBind.prototype.bind = function () {
        var self = this;
        $('input', self.form).each(function () {
            var val = self.params[this.name];
            if (this.type == "text" || this.type == "hidden") {
                $(this).val(val);
            }
            else if (this.type == "checkbox") {
                $(this).attr("checked", val == "True" || val == true);
            }
            else if ((this.type == "radio")) {
                $("input[name=" + this.name + "][value=" + val + "]").attr("checked", true);
            }
        });
        $('textarea,label', self.form).each(function () {
            $(this).val(self.params[this.name]);
        });
    }
});
