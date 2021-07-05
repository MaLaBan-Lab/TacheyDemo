(function ($) {
    $.fn.toggleDisabled = function () {
        return this.each(function () {
            this.disabled = !this.disabled;
        });
    };
})(jQuery);

$("#learning").on('click', function () {
    let actionUrl = "GuessYouLike";

    $.post(actionUrl, function (data) {
        console.log("123");
    })
    .fail(function (e) {
        console.log("something wrong...");
    })
});

$("#hot").on('click', function () {
    let actionUrl = "AllHot";

    $.post(actionUrl, function (data) {
        console.log("123");
    })
    .fail(function (e) {
        console.log("something wrong...");
    })
});

$(".search-btn").on('click', function () {
    let actionUrl = "Search";

    $.post(actionUrl, function (data) {
        console.log("123");
    })
    .fail(function (e) {
        console.log("something wrong...");
    })
});