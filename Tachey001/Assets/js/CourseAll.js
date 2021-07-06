(function ($) {
    $.fn.toggleDisabled = function () {
        return this.each(function () {
            this.disabled = !this.disabled;
        });
    };
})(jQuery);

// 猜你想學
$("#learning").on('click', function () {
    $.ajax({
        url: "GuessYouLike",
        cache: false,
        type: "get",
        success: function (result) {
            $("#proposalAttachments").html(result);
        }
    });
});

// 熱門排序
$("#hot").on('click', function () {
    $.ajax({
        url: "/Courses/AllHot",
        cache: false,
        type: "get",
        success: function (result) {
            $("#proposalAttachments").html(result);
        }
    });
});

// 搜尋
$(".search-btn").on('click', function () {
    $.ajax({
        url: "Search",
        cache: false,
        type: "get",
        data: { search: $("site-search").val() },
        success: function (result) {
            $("#proposalAttachments").html(result);
        }
    });
});

// 最新(預設)
$("#new").on('click', function () {
    $("#btnGroupDrop1").val(this.text().toString());
    $.ajax({
        url: "All",
        cache: false,
        type: "get",
        success: function (result) {
            $("#proposalAttachments").html(result);
        }
    });
});

// 最多人數
$("#pn").on('click', function () {
    $("#btnGroupDrop1").val(this.text().toString());
    $.ajax({
        url: "Orderbypn",
        cache: false,
        type: "get",
        success: function (result) {
            $("#proposalAttachments").html(result);
        }
    });
});

// 最長課時
$("#ct").on('click', function () {
    $("#btnGroupDrop1").val(this.text().toString());
    $.ajax({
        url: "Orderbyct",
        cache: false,
        type: "get",
        success: function (result) {
            $("#proposalAttachments").html(result);
        }
    });
});

// 最高評價
$("#cs").on('click', function () {
    $("#btnGroupDrop1").val(this.text().toString());
    $.ajax({
        url: "Orderbycs",
        cache: false,
        type: "get",
        success: function (result) {
            $("#proposalAttachments").html(result);
        }
    });
});