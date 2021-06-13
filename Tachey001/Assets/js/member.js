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

// 彈跳視窗-其他個人喜好
$("#user-like-btn").on('click', function () {
    $('.overlay-likes').css("display", "block");
});

// 關閉彈跳視窗
$('.close-button').on('click', function () {
    $('.overlay-jobs').css("display", "none");
    $('.overlay-interest').css("display", "none");
    $('.overlay-likes').css("display", "none");
});

$(".jobs-btn").on('click', function () {
    let chk_btn_cou = document.querySelectorAll(".jobs-btn-checked");

    // 按下工作選項
    if ($(this).hasClass("jobs-btn")) {
        $(this).addClass('jobs-btn-checked');
        $(this).removeClass('jobs-btn');   
    }
    else {
        $(this).removeClass('jobs-btn-checked');
        $(this).addClass('jobs-btn');
    }

    // 多於三個時，剩下選項不給選
    chk_btn_cou = document.querySelectorAll(".jobs-btn-checked");
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
});

$(".likes-btn").on('click', function () {
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
});

// input 監聽
//$(".mail-input").bind("input propertychange", function (event) {
//    // console.log($(".mail-input").val())
//    if ($(".mail-input").val().indexof("@") >= 0) {

//    }
//});

// select 監聽
$('.bday-year').change(() => {
    if ($('.bday-year').val() != '' && $('.bday-month').val() != '' && $('.bday-day').val() != '') {
        $('.close-click-btn').prop('disabled', false);
    }
    else {
        $('.close-click-btn').attr('disabled', true);
    }
});

$('.bday-month').change(() => {
    if ($('.bday-year').val() != '' && $('.bday-month').val() != '' && $('.bday-day').val() != '') {
        $('.close-click-btn').prop('disabled', false);
    }
    else {
        $('.close-click-btn').attr('disabled', true);
    }
});

$('.bday-day').change(() => {
    if ($('.bday-year').val() != '' && $('.bday-month').val() != '' && $('.bday-day').val() != '') {
        $('.close-click-btn').prop('disabled', false);
    }
    else {
        $('.close-click-btn').attr('disabled', true);
    }
});

$('.form-check-inline').change(() => {
    //alert('123');
    //var obj = document.getElementsByName("status_1");
    //var selected = [];
    //for (var i = 0; i < obj.length; i++) {
    //    if (obj[i].checked) {
    //        selected.push(obj[i].value);
    //    }
    //}
    //alert("您勾選的項目 : " + selected.join());

    //$("[name='status_1']:checked").val() 
});
