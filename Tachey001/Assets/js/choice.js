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
    //check_intervals_btn_disable();
});

$(".toggle-btn-interest-first").on('click', function () {
    interest_btn_ckeck();
    // 開啟或關閉的子選項區塊
    if (!$('.toggle-first').hasClass("bshow")) {
        $('.toggle-first').slideToggle('fast').addClass("bshow");
    }
    //else {
    //    $('.toggle-first').slideToggle('fast').removeClass("bshow");
    //}
    if ($('.toggle-second').hasClass("bshow")) {
        $('.toggle-second').slideToggle('fast').removeClass("bshow");
    }
    if ($('.toggle-third').hasClass("bshow")) {
        $('.toggle-third').slideToggle('fast').removeClass("bshow");
    }

    // 清除上一個被選的主選項
    Array.from(document.getElementsByClassName("selected")).forEach(
        function (element, index, array) {
            $(element).removeClass("selected");
        }
    );
    // 渲染這一個被選的主選項
    $(this).addClass("selected");


    // 隱藏所有子標題與子選項
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

    // 顯示被點選的主選項他的子選項
    $('#title_' + $(this).get(0).id).addClass("btn-show"); // 子標題
    $('.dat_' + $(this).get(0).id).addClass("show-inline"); // 子選項
});

$(".toggle-btn-interest-second").on('click', function () {
    interest_btn_ckeck();
    // 開啟或關閉的子選項區塊
    if ($('.toggle-first').hasClass("bshow")) {
        $('.toggle-first').slideToggle('fast').removeClass("bshow");
    }
    if (!$('.toggle-second').hasClass("bshow")) {
        $('.toggle-second').slideToggle('fast').addClass("bshow");
    }
    //else {
    //    $('.toggle-second').slideToggle('fast').removeClass("bshow");
    //}
    if ($('.toggle-third').hasClass("bshow")) {
        $('.toggle-third').slideToggle('fast').removeClass("bshow");
    }

    // 清除上一個被選的主選項
    Array.from(document.getElementsByClassName("selected")).forEach(
        function (element, index, array) {
            $(element).removeClass("selected");
        }
    );
    // 渲染這一個被選的主選項
    $(this).addClass("selected");


    // 隱藏所有子標題與子選項
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

    // 顯示被點選的主選項他的子選項
    $('#title_' + $(this).get(0).id).addClass("btn-show"); // 子標題
    $('.dat_' + $(this).get(0).id).addClass("show-inline"); // 子選項
});

$(".toggle-btn-interest-third").on('click', function () {
    interest_btn_ckeck();
    // 開啟或關閉的子選項區塊
    if ($('.toggle-first').hasClass("bshow")) {
        $('.toggle-first').slideToggle('fast').removeClass("bshow");
    }
    if ($('.toggle-second').hasClass("bshow")) {
        $('.toggle-second').slideToggle('fast').removeClass("bshow");
    }
    if (!$('.toggle-third').hasClass("bshow")) {
        $('.toggle-third').slideToggle('fast').addClass("bshow");
    }

    // 清除上一個被選的主選項
    Array.from(document.getElementsByClassName("selected")).forEach(
        function (element, index, array) {
            $(element).removeClass("selected");
        }
    );
    // 渲染這一個被選的主選項
    $(this).addClass("selected");


    // 隱藏所有子標題與子選項
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

    // 顯示被點選的主選項他的子選項
    $('#title_' + $(this).get(0).id).addClass("btn-show"); // 子標題
    $('.dat_' + $(this).get(0).id).addClass("show-inline"); // 子選項
});

// 子選項
$('.toggle-btn-interval').on('click', function () {
    if ($(this).hasClass("intervals-btn")) { // 尚未被選擇
        // 變為選擇
        $(this).addClass('intervals-btn-checked');
        $(this).removeClass('intervals-btn');

        // 清除上一個被選的主選項
        Array.from(document.getElementsByClassName("selected")).forEach(
            function (element, index, array) {
                $(element).removeClass("selected"); // 只是被點選
            }
        );

        // 被點選的主選項改為已被選擇樣式
        $.each($(this).attr('class').split(' '), function (index, value) {
            if (value.includes('dat_')) {
                //console.log($('#' + value.split('_').pop()).get(0).id);
                $('#' + value.split('_').pop()).addClass("kind-btn-choiced"); // 已被選擇樣式
            }
        });
    }
    else { // 已被選擇
        // 取消選擇
        $(this).removeClass('intervals-btn-checked');
        $(this).addClass('intervals-btn');

        // 改變主選項樣式
        $.each($(this).attr('class').split(' '), function (index, value) {
            if (value.includes('dat_')) { // 找特定class name
                let chk_btn_same = document.querySelectorAll("." + value + ".intervals-btn-checked"); // 已選擇的子選項
                if (chk_btn_same.length <= 0) { // 如果沒有其他子選項被點選
                    //console.log($('#' + value.split('_').pop()).get(0).id);
                    $('#' + value.split('_').pop()).removeClass("kind-btn-choiced"); // 主選項改成為未被選擇樣式
                }
                
            }
        });
    }
    //check_intervals_btn_disable();
    // 確認按鈕
    chk_btn_cou = document.querySelectorAll(".intervals-btn-checked");
    if (chk_btn_cou.length > 0) {
        $('.confirm-btn').prop('disabled', false);
    }
    else {
        $('.confirm-btn').attr('disabled', true);
    }
});

confirm_btn_intervals("SettingInterval", "intervals");

// 確認鍵
function confirm_btn_intervals(url, blockname) {
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

        // 抓取已選擇的子選項
        let chk_btn_cou = document.querySelectorAll("." + blockname + "-btn-checked");

        chk_btn_cou.forEach(function (val, index) {
            chk_btn += val.innerText + "/";
            //console.log(val.innerText);
        });

        let actionUrl = url + "?" + "clickedOption" + "=" + chk_btn;
        show_block_intervals();

        $.post(actionUrl, function (data) {
            $('.confirm-btn').attr('disabled', true);
        })
        .fail(function (e) {
            console.log("something wrong...");
        })
    });
}

show_block_intervals();

// 顯示已選擇的子選項
function show_block_intervals() {
    let proliferate = '';
    chk_btn_cou = document.querySelectorAll(".intervals-btn-checked"); // 已選擇的子選項
    if (chk_btn_cou.length > 0) {
        chk_btn_cou.forEach(element => proliferate += "<span class=\"choiced-card marg-b-10 marg-r-10 inline-block text-center\" role=\"button\">" + element.innerText + "</span>");
        //console.log(proliferate);
        $('#show-block').html(proliferate);
    }
}