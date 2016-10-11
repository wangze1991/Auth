require(['jquery','util'], function ($,util) {
    var exporter = {};
    




    exporter.event = {};

    exporter.event.init = function () {
        $('[type="submit"]').on('click', this.submit);

    }

    exporter.event.submit = function () {
        util.ajax({
            url: '/Account/Login',
            data: $('form').serialize()
        }, function (data) {
            if (data.isSuccess) {
                window.location.href = (data.url || '/');
            } else {
                alert(data.msg);
            }
        });
        return false;
    }

    exporter.init = function () {
       exporter.event.init();

    };
    $(function () {
        exporter.init();
    })

})