
$(function () {
    $('a[href*="#"]:not([href="#"])').click(function () {
        var target = $(this.hash);

        $('html,body').animate({
            scrollTop: target.offset().top
        }, 1000);

        return false;
    });
});

function check_job_btn_disable() {
    // 多於三個時，剩下選項不給選
    let chk_btn_cou = document.querySelectorAll(".jobs-btn-checked");
    if (chk_btn_cou.length > 2) {
        let btn_cou = document.querySelectorAll(".jobs-btn");
        btn_cou.forEach((item) => {
            item.disabled = true;
            item.classList.add('btn-disable');
            item.classList.remove('jobs-btn');
        });
    }
    else {
        let btn_cou = document.querySelectorAll(".btn-disable");
        btn_cou.forEach((item) => {
            item.disabled = false;
            item.classList.remove('btn-disable');
            item.classList.add('jobs-btn');
        });
    }

    // 確認按鈕
    if (chk_btn_cou.length > 0) {
        $('.confirm-btn').prop('disabled', false);
    }
    else {
        $('.confirm-btn').attr('disabled', true);
    }
}

$("#user-pseudo-btn").on('click', function () {
    $('.overlay-jobs').css("display", "block");
    check_job_btn_disable();
});

$("#user-interest-btn").on('click', function () {
    $('.overlay-interest').css("display", "block");
});

function interest_btn_ckeck() {
    // 按下興趣選項
    if ($(this).hasClass("kind-selected")) {
        $(this).removeClass('kind-selected');
    }
    else {
        $(this).addClass('kind-selected');
    }
}

$(".toggle-btn-interest-first").on('click', function () {
    interest_btn_ckeck();
    //$('.interval').toggleClass('selecionado', $(this).is(':visible'));
    //$(".interval").slideToggle(1000);
    if (!$('.toggle-first').hasClass("bshow")) {
        $('.toggle-first').slideToggle('fast').addClass("bshow");
    }
    if ($('.toggle-second').hasClass("bshow")) {
        $('.toggle-second').slideToggle('fast').removeClass("bshow");
    }
    if ($('.toggle-third').hasClass("bshow")) {
        $('.toggle-third').slideToggle('fast').removeClass("bshow");
    }
    if ($("btn-show")) {
        Array.from(document.getElementsByClassName("btn-show")).forEach(
            function (element, index, array) {
                $(element).removeClass("btn-show");
            }
        );
        Array.from(document.getElementsByClassName("show-inline")).forEach(
            function (element, index, array) {
                $(element).removeClass("show-inline");
            }
        );
    }
    //forEach(var i in' @ViewBag.interestDetil'[$(this).get(0).id]) {
    //html += '<p>i</p>'
    //}
    //var html = '<p>ThematicName</p>'
    //$('.items-wrap').add
    //check_job_btn_disable();
    //confirm_btn("SettingJob", "jobs");
    $('#title_' + $(this).get(0).id).addClass("btn-show");
    $('.dat_' + $(this).get(0).id).addClass("show-inline");
});

$(".toggle-btn-interest-second").on('click', function () {
    interest_btn_ckeck();
    //$('.interval').toggleClass('selecionado', $(this).is(':visible'));
    //$(".interval").slideToggle(1000);
    if ($('.toggle-first').hasClass("bshow")) {
        $('.toggle-first').slideToggle('fast').removeClass("bshow");
    }
    if (!$('.toggle-second').hasClass("bshow")) {
        $('.toggle-second').slideToggle('fast').addClass("bshow");
    }
    if ($('.toggle-third').hasClass("bshow")) {
        $('.toggle-third').slideToggle('fast').removeClass("bshow");
    }
    
    if ($("btn-show")) {
        Array.from(document.getElementsByClassName("btn-show")).forEach(
            function (element, index, array) {
                $(element).removeClass("btn-show");
            }
        );
        Array.from(document.getElementsByClassName("show-inline")).forEach(
            function (element, index, array) {
                $(element).removeClass("show-inline");
            }
        );
    }
    
    $('#title_' + $(this).get(0).id).addClass("btn-show");
    $('.dat_' + $(this).get(0).id).addClass("show-inline");
    //check_job_btn_disable();
    //confirm_btn("SettingJob", "jobs");
});

$(".toggle-btn-interest-third").on('click', function () {
    interest_btn_ckeck();
    if ($('.toggle-first').hasClass("bshow")) {
        $('.toggle-first').slideToggle('fast').removeClass("bshow");
    }
    if ($('.toggle-second').hasClass("bshow")) {
        $('.toggle-second').slideToggle('fast').removeClass("bshow");
    }
    if (!$('.toggle-third').hasClass("bshow")) {
        $('.toggle-third').slideToggle('fast').addClass("bshow");
    }
    //$('.interval').toggleClass('selecionado', $(this).is(':visible'));
    //$(".interval").slideToggle(1000);
    if ($("btn-show")) {
        Array.from(document.getElementsByClassName("btn-show")).forEach(
            function (element, index, array) {
                $(element).removeClass("btn-show");
            }
        );
        Array.from(document.getElementsByClassName("show-inline")).forEach(
            function (element, index, array) {
                $(element).removeClass("show-inline");
            }
        );
    }

    //check_job_btn_disable();
    //confirm_btn("SettingJob", "jobs");
    $('#title_' + $(this).get(0).id).addClass("btn-show");
    $('.dat_' + $(this).get(0).id).addClass("show-inline");
});

$('.toggle-btn-interval').on('click', function () {
    if ($(this).hasClass("intervals-btn")) {
        $(this).addClass('intervals-btn-checked');
        $(this).removeClass('intervals-btn');
    }
    else {
        $(this).removeClass('intervals-btn-checked');
        $(this).addClass('intervals-btn');
    }

    // 確認按鈕
    chk_btn_cou = document.querySelectorAll(".intervals-btn-checked");
    if (chk_btn_cou.length > 0) {
        $('.confirm-btn').prop('disabled', false);
    }
    else {
        $('.confirm-btn').attr('disabled', true);
    }


    confirm_btn("SettingInterval", "intervals");
});

//if ($(this).hasClass('show-inline')) {
//    confirm_btn("SettingInterval", "intervals");
//}

// 彈跳視窗-其他個人喜好
$("#user-like-btn").on('click', function () {
    $('.overlay-likes').css("display", "block");
    // 確認按鈕
    let chk_btn_cou = document.querySelectorAll(".likes-btn-checked");
    if (chk_btn_cou.length > 0) {
        $('.confirm-btn').prop('disabled', false);
    }
    else {
        $('.confirm-btn').attr('disabled', true);
    }
});

// 關閉彈跳視窗
$('.close-button').on('click', function () {
    $('.overlay-jobs').css("display", "none");
    $('.overlay-interest').css("display", "none");
    $('.overlay-likes').css("display", "none");
});

$(".toggle-btn-job").on('click', function () {
    // 按下工作選項
    if ($(this).hasClass("jobs-btn")) {
        $(this).addClass('jobs-btn-checked');
        $(this).removeClass('jobs-btn');   
    }
    else {
        $(this).removeClass('jobs-btn-checked');
        $(this).addClass('jobs-btn');
    }

    check_job_btn_disable();
    confirm_btn("SettingJob", "jobs");
});

function confirm_btn(url, blockname) {
    let chk_btn = "";
    // 使用jQuery ajax post 呼叫 .Net MVC Controller
    (function ($) {
        $.fn.toggleDisabled = function () {
            return this.each(function () {
                this.disabled = !this.disabled;
            });
        };
    })(jQuery);

    $('.confirm-btn').click(function () {
        // 關閉小視窗
        $(".overlay-" + blockname).css("display", "none");
        // 失效按鈕 避免連點
        $('.confirm-btn').attr('disabled', true);

        let chk_btn_cou = document.querySelectorAll("." + blockname + "-btn-checked");

        chk_btn_cou.forEach(function (val, index) {
            chk_btn += val.innerText + "/";
            console.log(val.innerText);
        });

        let actionUrl = url + "?" + "clickedOption" + "=" + chk_btn;

        $.post(actionUrl, function (data) {
            $('.confirm-btn').attr('disabled', true);
        })
        .fail(function (e) {
            console.log("something wrong...");
        })
    });
}


$(".toggle-btn-like").on('click', function () {
    let chk_btn_cou = document.querySelectorAll(".likes-btn-checked");

    // 按下喜好選項
    if ($(this).hasClass("likes-btn")) {
        $(this).addClass('likes-btn-checked');
        $(this).removeClass('likes-btn');
    }
    else {
        $(this).removeClass('likes-btn-checked');
        $(this).addClass('likes-btn');
    }

    // 確認按鈕
    chk_btn_cou = document.querySelectorAll(".likes-btn-checked");
    if (chk_btn_cou.length > 0) {
        $('.confirm-btn').prop('disabled', false);
    }
    else {
        $('.confirm-btn').attr('disabled', true);
    }

    confirm_btn("SettingLike", "likes");
});

// input 監聽
//$(".mail-input").bind("input propertychange", function (event) {
//    // console.log($(".mail-input").val())
//    if ($(".mail-input").val().indexof("@") >= 0) {

//    }
//});

function close_click_btn_control() {
    if ($('.bday-year').val() != '' && $('.bday-month').val() != '' && $('.bday-day').val() != '' && $('.form-check-inline').find('input[type="radio"]:checked').val() != undefined) {
        $('#close-click-btn').prop('disabled', false);
    }
    else {
        $('#close-click-btn').attr('disabled', true);
    }
}
// select 監聽
$('.bday-year').change(() => {
    close_click_btn_control();
});

$('.bday-month').change(() => {
    close_click_btn_control();
});

$('.bday-day').change(() => {
    close_click_btn_control();
});

$('.form-check-inline').change(() => {
    close_click_btn_control();
});


