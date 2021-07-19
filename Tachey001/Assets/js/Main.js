window.onload = function () {

    let menu = document.querySelector('.menu_wrap');
    let menu_open = document.querySelector('.list-toggle');
    let btnclose = document.querySelector('.close_button');
    let body = document.getElementsByTagName("body")[0];

    menu_open.addEventListener('click', function () {
        $("html,body").animate({
                scrollTop: 0,
            },
            0
        );
        menu.style.display = "block";
        body.style.overflow = "hidden"
    })
    btnclose.addEventListener('click', function () {
        menu.style.display = "none";
        body.style.overflow = "auto"
    })



    let btnunit = document.querySelectorAll('.menu_wrap .list');
    btnunit.forEach(function (btn) {
        btn.addEventListener('click', () => {
            btnunit.forEach(x => x.classList.remove('active'));
            btn.classList.add('active');
        });

    });
}



//POST Step Fun
function postScore(CId) {
    var data = new FormData($(`#ScorePost`)[0]);
    console.dir(data);

    $.ajax({
        type: "POST",
        url: `/Courses/CreateScore?CourseId=${CId}`,
        data: data,
        processData: false,
        contentType: false,
        success: function (response) {
            if ($('#emptyScore') != undefined) {
                $('#emptyScore').remove();
            }
            $('#ScoreContainer').append(response)
            $('#scoreBtn').attr('disabled', true)

            $('#exampleModal').modal('hide')
        },
        error: function (err) {
            console.log(err.ErrMsg)
        }
    })
}