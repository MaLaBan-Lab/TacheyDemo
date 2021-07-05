function check_intervals_btn_disable() {
    // 多於一個時，剩下選項不給選
    let chk_btn_cou = document.querySelectorAll(".intervals-btn-checked");
    if (chk_btn_cou.length > 0) {
        let btn_cou = document.querySelectorAll(".intervals-btn");
        btn_cou.forEach((item) => {
            item.disabled = true;
            item.classList.add('btn-disable');
            item.classList.remove('intervals-btn');
        });
    }
    else {
        let btn_cou = document.querySelectorAll(".btn-disable");
        btn_cou.forEach((item) => {
            item.disabled = false;
            item.classList.remove('btn-disable');
            item.classList.add('intervals-btn');
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

function interest_btn_ckeck() {
    // 按下興趣選項
    if ($(this).hasClass("kind-selected")) {
        $(this).removeClass('kind-selected');
    }
    else {
        $(this).addClass('kind-selected');
    }
}

$("#user-interest-btn").on('click', function () {
    $('.overlay-interest').css("display", "block");
    // 初始化
    $('.toggle-first').removeClass("bshow");
    $('.toggle-first').css("display", "none");
    $('.toggle-second').removeClass("bshow");
    $('.toggle-second').css("display", "none");
    $('.toggle-third').removeClass("bshow");
    $('.toggle-third').css("display", "none");
    check_intervals_btn_disable();
});

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
    check_intervals_btn_disable();
    // 確認按鈕
    chk_btn_cou = document.querySelectorAll(".intervals-btn-checked");
    if (chk_btn_cou.length > 0) {
        $('.confirm-btn').prop('disabled', false);
    }
    else {
        $('.confirm-btn').attr('disabled', true);
    }


    confirm_btn("/Courses/CategoryStep", "intervals");
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
        $(".overlay-" + "interest").css("display", "none");
        // 失效按鈕 避免連點
        $('.confirm-btn').attr('disabled', true);

        let chk_btn_cou = document.querySelectorAll("." + blockname + "-btn-checked");

        chk_btn_cou.forEach(function (val, index) {
            chk_btn = val.innerText;
            console.log(val.innerText);
        });

        let actionUrl = url + "?" + "clickedOption" + "=" + chk_btn + "&" + "CourseId" + "=" + $('.courseid')[0].id;

        $.post(actionUrl, function (data) {
            $('.confirm-btn').attr('disabled', true);
        })
            .fail(function (e) {
                console.log("something wrong...");
            })
    });
}