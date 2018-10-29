
$(function () {

    InitAction();

    $(".rcat").click(function () {
        var _this = $(this);
        _this.addClass('active');
        $('.rcat').not(_this).each(function () {
            $(this).removeClass('active');
        });
        var catid = _this.attr("catid");
        $("#lstProducts").load("/Admin/Products/Category?cat=" + catid, function (response, status, xhr) {
            if (status == "error") {
                $("#lstProducts").html(xhr.statusText);
            }
            else { InitAction(); }
        });
        
    });
    $(".rcat").eq(0).click();
});

function InitAction() {
    $("a[baction]").on("click", function (e) {
        cur_href = this.href;
        $("#dialogContent").load(cur_href, function () {
            $("#dialog").modal({
                backdrop: "static",
                keyboard: true
            }, "show");
            bindForm(this);
        });

        return false;
    });
}

function bindForm(dialog) {

    $('form', dialog).submit(function () {

        var formData = new FormData($(this)[0]);
        var files = $('input[type=file]')[0];
        if (files != null) {
            formData.append('pic');
        }
        $.ajax({
            
            url: this.action,
            type: this.method,
            data: formData,
            //data: $(this).serialize(),
            contentType: false,
            processData: false,
            success: function (result) {
                if (result == "OK") {
                    $('#dialog').modal('hide');
                    loadBox(_cur_menu);
                } else {//error
                    $('#dialogContent').html(result);
                    bindForm(dialog);
                }
            }

        });

        return false;
    });
}

