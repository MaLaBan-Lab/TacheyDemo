
var btn = document.getElementById("morecourse");
var CardArea = document.getElementById("CardArea");
var Changetext = document.getElementById("Changetext");
var time = 8;

btn.addEventListener("click", function () {
    console.log(time)
    for ( i = 0; i < 4; i++)
    {
        var url = "/Home/GetHomePageCard?start=" + (time + i);
        $.get(url, function (response) {
            if (response != "") {
                CardArea.innerHTML += response


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
            }
            else {
                
                Changetext.innerHTML = '<h3 class="text-danger text-center fs-18">無法顯示更多課程更多</h3>'
                i = 4;
            }
            /*console.log(response);*/
        });
    }
    
    time += 4;
})


