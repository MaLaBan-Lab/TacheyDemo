$('.QLike').on("click", function () {
    var currentI = $(this);
    var currentN = currentI.parent().next();

    var url = "/api/MemberAction/Like?MemberId=" + $(this).get(0).dataset.memberid + "&CourseId=" + $(this).get(0).dataset.courseid + "&QuestionId=" + $(this).get(0).dataset.questionid;

    if (currentI.hasClass("fas")) {
        // 紅心時，移除紅心變白心
        currentI.removeClass('fas')
        currentI.removeClass('text-danger')
        currentI.addClass('far')
        currentI.addClass('text-white')

        //收回讚 值-1
        var result = currentN.get(0).innerText
        currentN.get(0).innerText = (parseInt(result)-1)
    } else {
        // 白心時，移除白心變紅心
        currentI.removeClass('far')
        currentI.removeClass('text-white')
        currentI.addClass('fas')
        currentI.addClass('text-danger')

        //按讚 值+1
        var result = currentN.get(0).innerText
        currentN.get(0).innerText = (parseInt(result)+1)
    }

    $.get(url, function (res) {

    })
})