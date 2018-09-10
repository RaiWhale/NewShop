$(document).ready(function () {
    $("a.btnmodal").click(function () {

        var btn = $(this);

        $("#myContent").load(btn.attr("href"), function () {

            $("#myModal").modal({ backdrop: "static", keyboard: false }, "show");
        });
        return false;
    });
});