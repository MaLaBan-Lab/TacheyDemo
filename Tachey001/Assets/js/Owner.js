$('.ToastBtn').on('click', function () {
    $(`#toast-${$(this).get(0).dataset.courseid}`).toast('show')
})

$(".bookmarkCheck").on('click', function () {
    var currDom = $(this);

    var url = "/api/MemberAction/Owner?MemberId=" + $(this).get(0).dataset.memberid + "&CourseId=" + $(this).get(0).dataset.courseid

    $.get(url, function (res) {
        if (currDom.hasClass("backgroundBookmarkChecked")) {
            $(`#text-${currDom.get(0).dataset.courseid}`).text("取消收藏")
            currDom.removeClass("backgroundBookmarkChecked")
            currDom.addClass("backgroundBookmarkMove")
        } else {
            $(`#text-${currDom.get(0).dataset.courseid}`).text("加入收藏")
            currDom.addClass("backgroundBookmarkChecked")
            currDom.removeClass("backgroundBookmarkMove")
        }
    })
})