// 
$("#intro-textarea-pen").on('click', function () {
    if ($('#intro-p').html().toString() != "簡單介紹一下自己吧！") {
        $('#intro-textarea-content').val($('#intro-p').html().toString());
    }
    $('#intro-textarea').css("display", "block");
    $('.about-content-intro').css("display", "none");
});

// 
$("#expert-textarea-pen").on('click', function () {
    if ($('#expert-p').text().toString() != "有什麼擅長的事情嗎？") {
        $('#expert-textarea-content').val($('#expert-p').text().toString());
    }
    $('#expert-textarea').css("display", "block");
    $('.about-content-expert').css("display", "none");
});

// 
$("#like-textarea-pen").on('click', function () {
    if ($('#like-p').text().toString() != "平常喜歡做什麼呢？") {
        $('#like-textarea-content').val($('#like-p').text().toString());
    }
    $('#like-textarea').css("display", "block");
    $('.about-content-like').css("display", "none");
});

// 
$(".textarea-cancel-btn").on('click', function () {
    $('#' + $(this).parent().parent().attr('id')).css("display", "none");
    $('.about-content-' + $(this).parent().parent().attr('id').split('-')[0]).css("display", "block");
});

$(".textarea-submit-btn").on('click', function () {
   // if ($("#desc").val() == "") {
   //     $("#desc").focus();
   //     alert("請務必填入回應內容"); return false;
   // }
   // // 移除空格確定是否空值
   // if (!$.trim($("#desc").val())) {
   //..
   // }
    // 存入資料庫
    $('#' + $(this).parent().parent().attr('id')).css("display", "none");
    // 顯示修改過的內容
    $('.about-content-' + $(this).parent().parent().attr('id').split('-')[0]).css("display", "block");
});

// 
$("#website-textarea-pen").on('click', function () {
    $('.styles-overlay').css("display", "block");
});

// 
$(".styles-close-button").on('click', function () {
    $('.styles-overlay').css("display", "none");
});

$(".style-cancel-btn").on('click', function () {
    $('.styles-overlay').css("display", "none");
});

$(".style-submit-btn").on('click', function () {
    // 存入資料庫
    $('.styles-overlay').css("display", "none");
    // 顯示修改過的內容
});

// 使用者姓名
$("#name-textarea-pen").on('click', function () {
    let h3 = document.getElementsByTagName('h3');
    $('.name-input-textarea').value(h3.text());
    $('.username-input-block').css("display", "block");
    $('#name-textarea-pen').parent().css("display", "none");
});

$(".name-cancel").on('click', function () {
    $('.username-input-block').css("display", "none");
    $('#name-textarea-pen').parent().css("display", "block");
});

$(".name-submit").on('click', function () {
    // 存入資料庫
    $('.username-input-block').css("display", "none");
    $('#name-textarea-pen').parent().css("display", "block");
    // 顯示修改過的內容
});

// 
$("#text-textarea-pen").on('click', function () {
    $('.text-context').css("display", "none");
    $('.text-secondary').css("display", "block");
});

$(".text-cancel-btn").on('click', function () {
    $('.text-context').css("display", "block");
    $('.text-secondary').css("display", "none");
});

$(".text-submit-btn").on('click', function () {
    // 存入資料庫
    $('.text-context').css("display", "block");
    $('.text-secondary').css("display", "none");
    // 顯示修改過的內容
});

// 
function uploadTheme(e) {
    var file = e.files[0];
    if (!file) {
        return;
    }

    //檔案上傳
    //...
    //檔案上傳

    //上傳後將檔案清除
    e.value = '';
}

// 頭貼上傳按鈕
$(".click-container").on('click', function () {
    // 具備不具備上傳功能
    console.log($(".cancel-btn-photo").children());
    if ($(".cancel-btn-photo").children().hasClass("fa-cloud-upload-alt")) {
        photo_file.click();
        $(".user-img").attr("src", "/Assets/img/吉祥物.jpg"); // 使用者上傳圖片
    }
    else {
        $(".user-img").attr("src", "/Assets/img/photo.jpg"); // 預設圖片
    }
    $(".cancel-btn-photo").children().toggleClass("fas fa-cloud-upload-alt fas fa-times");
});
