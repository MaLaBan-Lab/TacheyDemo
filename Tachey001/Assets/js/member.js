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
    // �h��T�ӮɡA�ѤU�ﶵ������
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

    // �T�{���s
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

// �u������-��L�ӤH�ߦn
$("#user-like-btn").on('click', function () {
    $('.overlay-likes').css("display", "block");
});

// �����u������
$('.close-button').on('click', function () {
    $('.overlay-jobs').css("display", "none");
    $('.overlay-interest').css("display", "none");
    $('.overlay-likes').css("display", "none");
});

$(".toggle-btn-job").on('click', function () {
    // ���U�u�@�ﶵ
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
    // �ϥ�jQuery ajax post �I�s .Net MVC Controller
    (function ($) {
        $.fn.toggleDisabled = function () {
            return this.each(function () {
                this.disabled = !this.disabled;
            });
        };
    })(jQuery);

    $('.confirm-btn').click(function () {

        //���ī��s �קK�s�I
        $('.confirm-btn').attr('disabled', true);

        let chk_btn_cou = document.querySelectorAll("." + blockname + "-btn-checked");

        chk_btn_cou.forEach(function (val, index) {
            chk_btn += val.innerText + "/";
            console.log(val.innerText);
        });

        var actionUrl = url + "?" + "clickedOption" + "=" + chk_btn;

        $.post(actionUrl, function (data) {
            //console.log(data);
            $('.confirm-btn').attr('disabled', true);
            $('.overlay-' + blockname).css("display", "none");
        })
            .fail(function () {
                console.log("something wrong...");
            })
    });
}


$(".toggle-btn-like").on('click', function () {
    let chk_btn_cou = document.querySelectorAll(".likes-btn-checked");

    // ���U�ߦn�ﶵ
    if ($(this).hasClass("likes-btn")) {
        $(this).addClass('likes-btn-checked');
        $(this).removeClass('likes-btn');
    }
    else {
        $(this).removeClass('likes-btn-checked');
        $(this).addClass('likes-btn');
    }

    // �T�{���s
    chk_btn_cou = document.querySelectorAll(".likes-btn-checked");
    if (chk_btn_cou.length > 0) {
        $('.confirm-btn').prop('disabled', false);
    }
    else {
        $('.confirm-btn').attr('disabled', true);
    }

    confirm_btn("SettingLike", "likes");
});

// input ��ť
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
// select ��ť
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
