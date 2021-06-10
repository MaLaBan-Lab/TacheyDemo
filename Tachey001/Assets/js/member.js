$(function () {
    $('a[href*="#"]:not([href="#"])').click(function () {
        var target = $(this.hash);

        $('html,body').animate({
            scrollTop: target.offset().top
        }, 1000);

        return false;
    });
});

$("#user-pseudo-btn").on('click', function () {
    $('.overlay-jobs').css("display", "block");
});

$("#user-interest-btn").on('click', function () {
    $('.overlay-interest').css("display", "block");
});

$("#user-like-btn").on('click', function () {
    $('.overlay-likes').css("display", "block");
});

$('.close-button').on('click', function () {
    $('.overlay-jobs').css("display", "none");
    $('.overlay-interest').css("display", "none");
    $('.overlay-likes').css("display", "none");
});

$(".jobs-btn").on('click', function () {
    $('.jobs-btn').toggleClass('toggle');
});
