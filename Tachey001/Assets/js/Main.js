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

//POST Score
function postScore(CId) {
    var data = new FormData($(`#ScorePost`)[0]);

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
            $('#ScoreContainer').prepend(response)
            $('#scoreBtn').attr('disabled', true)

            var val = parseInt($("input[name='PostCourseScore.Score']:checked").val());
            var sco = parseInt($('#scoreAvg').get(0).textContent)
            var avg = Math.ceil((sco + val) / 2)
            $('#scoreAvg').get(0).textContent = avg
            $('#scorePeople').get(0).textContent++

            $('#scoreStar').children().each((index, item) => {
                if (index < avg) {
                    item.innerHTML = `
                        <i class="
                                        fas
                                        fa-star
                                        star-yellow
                                        position-absolute
                                        star-display
                                      "></i>
                    `
                }
            })

            $('#exampleModal').modal('hide')
        },
        error: function (err) {
            console.log(err.ErrMsg)
        }
    })
}

//POST Ques
function postQues(CId) {
    var data = new FormData($(`#QuesPost`)[0]);

    $.ajax({
        type: "POST",
        url: `/Courses/CreateQuestion?CourseId=${CId}`,
        data: data,
        processData: false,
        contentType: false,
        success: function (response) {
            if ($('#emptyQues') != undefined) {
                $('#emptyQues').remove();
            }
            $('#QuesContainer').append(response)

            $('#QuesModal').modal('hide')
        },
        error: function (err) {
            console.log(err.ErrMsg)
        }
    })
}

//POST Ans
function postAns(CId, QId) {
    var data = new FormData($(`#AnsPost-${QId}`)[0]);

    $.ajax({
        type: "POST",
        url: `/Courses/CreateAnswer?CourseId=${CId}&QuestionId=${QId}`,
        data: data,
        processData: false,
        contentType: false,
        success: function (response) {
            $(`#AnsContainer-${QId}`).append(response)

            $(`#ques-${QId}-count`).get(0).textContent++;

        },
        error: function (err) {
            console.log(err.ErrMsg)
        }
    })
}