$(function () {
    $(".submit-on-checked").change(function (ev) {
        var that = $(this);
        if (that.attr("type") == "radio" && !that.is(":checked")) {
            return;
        }
        $("#search-form").submit();
    })
})