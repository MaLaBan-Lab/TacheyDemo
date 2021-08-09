$(function () {
    $('[data-toggle="tooltip"]').tooltip();
});

//var progress = $.connection.progressHub;

//// Create a function that the hub can call back to display messages.
//progress.client.AddProgress = function (message, percentage) {
//    $(`.toast`).toast('show')
//    $('#ProgressMessage').get(0).innerText = percentage;
//    $('#ProgressPercent').width(percentage);

//    if (percentage == "100%") {
//        $(`.toast`).toast('hide')
//        $('#ProgressMessage').get(0).innerText = '0%';
//        $('#ProgressPercent').width('0%');
//    }
//};

//$.connection.hub.start().done(function () {
//    var connectionId = $.connection.hub.id;
//});

var CourseId = $("#course_CourseID").val();

let titleContent = [
    {
        h2: "歡迎加入 Tachey 好老師的行列",
        h4: "跟著 Tachey 一步一步完成提案吧！",
    },
    {
        h2: "好的標題和敘述很重要！",
        h4: "提供的資訊越詳細，越能提高學生上課的意願！",
    },
    {
        h2: "哪些學生適合這堂課？",
        h4: "課程提供越多資訊，越會提高學生上課的意願喔！",
    },
    {
        h2: "預計的單元架構與作業",
        h4: "安排單元架構，讓學生清楚看見學習過程的每一步，並設計作業練習",
    },
    {
        h2: "來定個吸引人的價格吧！",
        h4: "價格低一點，學生多一點！",
    },
    {
        h2: "加上更詳細的課程內容！",
        h4: "圖片加上文字，發揮你的創意！",
    },
    {
        h2: "用募資影片簡介課程內容！",
        h4: "影片包含主題、適合族群和課程目標",
    },
    {
        h2: "自訂專屬的行銷網址",
        h4: "以此網址行銷宣傳可獲得較高的分潤喔！",
    },
    {
        h2: "老師開課身份確認",
        h4: "",
    },
    {
        h2: "恭喜你成功地完成了提案編輯囉！",
        h4: "在送出審核前做最後的檢查與預覽吧",
    },
];

//更換Title文字
function changeStep(num) {
    let titleContentH2 = document.getElementById("titleContentH2");
    let titleContentH4 = document.getElementById("titleContentH4");

    titleContentH2.innerText = titleContent[num].h2;
    titleContentH4.innerText = titleContent[num].h4;
}

//更換進度條
$('.stepBtn').click(function () {
    var val = $(this).get(0).innerText
    $("#progress-bar").attr({ style: `width:${val * 11}%` })

    $('.stepBtn').each(function (index) {
        if (index <= val) {
            $(this).addClass("bg-hahow-green")
            $(this).removeClass("bg-hahow-orange")
        } else {
            $(this).addClass("bg-hahow-orange")
            $(this).removeClass("bg-hahow-green")
        }
    })

    changeStep(val)
    
})

//更換步驟
function changePage(val) {
    $('.tab-pane').each(function (index) {
        if (index == val) {
            $(this).addClass("active")
        } else {
            $(this).removeClass("active")
        }
    })

    $('.tab-btn').each(function (index) {
        if (index == val) {
            $(this).addClass("active")
        } else {
            $(this).removeClass("active")
        }
    })

    $("#progress-bar").attr({ style: `width:${val * 11}%` })

    $('.stepBtn').each(function (index) {
        if (index <= val) {
            $(this).addClass("bg-hahow-green")
            $(this).removeClass("bg-hahow-orange")
        } else {
            $(this).addClass("bg-hahow-orange")
            $(this).removeClass("bg-hahow-green")
        }
    })

    changeStep(val)
    $("html,body").animate(
        {
            scrollTop: 0,
        },
        600
    );
}

//課程章節單元
function enableDragSort(listClass) {
    const sortableLists = document.getElementsByClassName(listClass);
    Array.prototype.map.call(sortableLists, (list) => { enableDragList(list) });
    sortUpdate()
}

function enableDragList(list) {
    Array.prototype.map.call(list.children, (item) => { enableDragItem(item) });
    sortUpdate()
}

function enableDragItem(item) {
    item.setAttribute('draggable', true)
    item.ondrag = handleDrag;
    item.ondragend = handleDrop;
    sortUpdate()
}

function handleDrag(item) {
    const selectedItem = item.target,
        list = selectedItem.parentNode,
        x = event.clientX,
        y = event.clientY;

    selectedItem.classList.add('drag-sort-active');
    let swapItem = document.elementFromPoint(x, y) === null ? selectedItem : document.elementFromPoint(x, y);

    if (list === swapItem.parentNode) {
        swapItem = swapItem !== selectedItem.nextSibling ? swapItem : swapItem.nextSibling;
        list.insertBefore(selectedItem, swapItem);
    }
    sortUpdate()
}

function handleDrop(item) {
    item.target.classList.remove('drag-sort-active');
    sortUpdate()
}

(() => { enableDragSort('drag-sort-enable') })();

function newChapter() {
    var chapter = document.getElementById("chapter")
    var newDiv = document.createElement("div")
    var count = chapter.childElementCount

    newDiv.classList.add('col-11')
    newDiv.classList.add('border')
    newDiv.classList.add('my-1')
    newDiv.classList.add('py-2')
    newDiv.classList.add('chapterBox')
    newDiv.innerHTML = `
                            <h4>
                              <i class="fas fa-align-justify mr-2"></i>
                              <span class="chapterName">章節${count + 1}</span>
                              <input type="text" class="w-75 chapterInput" name="${count + 1}">
                              <i class="far fa-window-close" onclick="deleteChapter()"></i>
                            </h4>
                            <hr>
                            <h5 class="text-right">
                              <span>-新增單元</span>
                              <i class="far fa-plus-square" onclick="newUnit()"></i>
                            </h5>
                            <div class="drag-sort-enable unitBox">
                              <h5 class="text-right">
                                <i class="fas fa-align-justify mr-2"></i>
                                <span>單元1</span>
                                <input type="text" class="w-75" name="${count + 1}-1">
                                <i class="far fa-window-close mx-1" onclick="deleteUnit()"></i>
                              </h5>
                            </div>
                          `

    chapter.appendChild(newDiv)
    enableDragSort('drag-sort-enable')
    sortUpdate()
}

function newUnit() {
    var unit = event.target.offsetParent.lastElementChild
    var targetCh = event.target.offsetParent.id
    var count = unit.childElementCount

    var h5 = document.createElement('h5')
    h5.classList.add('text-right')
    h5.innerHTML = `
                            <i class="fas fa-align-justify mr-2"></i>
                            <span>單元${count + 1}</span>
                            <input type="text" class="w-75" name="${count + 1}">
                            <i class="far fa-window-close mx-1" onclick="deleteUnit()"></i>
                          `
    unit.appendChild(h5)
    enableDragSort('drag-sort-enable')
    sortUpdate()
}

function deleteChapter() {
    var chapter = document.getElementById("chapter")
    var target = event.target.offsetParent

    chapter.removeChild(target)
    sortUpdate()
}

function deleteUnit() {
    var unit = event.target.offsetParent.lastElementChild
    var target = event.target.parentElement

    unit.removeChild(target)
    sortUpdate()
}

function sortUpdate() {
    var chapterArr = document.querySelectorAll('.chapterName')
    var chapterInput = document.querySelectorAll('.chapterInput')
    var chapterBox = document.querySelectorAll('.chapterBox')
    var unitBox = document.querySelectorAll('.unitBox')

    chapterArr.forEach((item, index) => {
        item.innerText = `章節${index + 1}`
    })

    unitBox.forEach(x => {
        var unitArr = x.querySelectorAll('span')
        unitArr.forEach((item, index) => {
            item.innerText = `單元${index + 1}`
        })
    })

    chapterInput.forEach((item, index) => {
        item.name = (index + 1)
    })

    chapterBox.forEach((item, index) => {
        item.querySelector('.unitBox').querySelectorAll('input').forEach((x, count) => {
            x.name = `${index + 1}`
        })
    })
}

//POST Step Fun
function postStep(num) {
    var intro = $(".ck-content").get(0).innerHTML;
    $("* [name='course.Introduction']").html(intro);
    var data = new FormData($(`#Step${num}Form`)[0]);

    $.ajax({
        type: "POST",
        url: `/Courses/Step?id=${num}`,
        data: data,
        processData: false,
        contentType: false,
        success: function (response) {
            if (num == 7) {
                var val = $("#course_CustomUrl").val();
                $("#basic-url").text(`你可以經由下方網址宣傳課程： https://localhost:44394/Course/cm/${val}`)
            }
            changePage(num + 1)
            StepCheckUpdate(CourseId)
            PostToast(num, "儲存成功")
            if (num == 3) {
                upVideo(CourseId)
            }
        },
        error: function (err) {
            console.log(err.ErrMsg)
        }
    })
}

function upVideo(CourseId) {
    $.ajax({
        url: `/Courses/Update_Step_Video?CourseID=${CourseId}`,
        type: "get",
        success: function (result) {
            $("#step6").html(result);

            //步驟6專用
            //上傳預覽影片
            $("#previewVideo").change(function () {
                var CourseId = $("#previewVideo").get(0).dataset.courseid;

                var result = document.getElementById("previewVideo").files[0]
                var data = new FormData();
                data.append("PreviewVideoUpload", result);

                FilePostToast(6);
                $.ajax({
                    type: "Post",
                    url: `/Courses/CourseVideoUpload?CourseId=${CourseId}`,
                    data: data,
                    contentType: false,
                    processData: false,
                    success: function (response) {
                        DelToast(6);
                    }
                })

                readVideoURL(this)
            })

            //步驟6專用
            //上傳課程影片
            $(".courseVideoPost").change(function () {
                var CourseId = $(this).get(0).dataset.courseid;
                var UnitId = $(this).get(0).dataset.unitid;

                var result = $(this).get(0).files[0]
                var data = new FormData();
                data.append(`${UnitId}`, result);

                FilePostToast(UnitId);
                $.ajax({
                    type: "Post",
                    url: `/Courses/MainCourseVideoUpload?CourseId=${CourseId}&UnitId=${UnitId}`,
                    data: data,
                    contentType: false,
                    processData: false,
                    success: function (response) {
                        DelToast(UnitId);
                    }
                })

                readCourseVideoURL(this, UnitId)
            })
        }
    })
}


function PostToast(Num, Msg) {
    $('#PostMsgBox').append(`
        <div class="toast hide" role="alert" aria-live="assertive" aria-atomic="true" data-autohide="false" id="toast${Num}">
            <div class="toast-header">
                <strong class="mr-auto">提示訊息</strong>
                <button type="button" class="ml-2 mb-1 close" data-dismiss="toast" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="toast-body">
                <h5><p class="mx-5 text-success"><i class="fas fa-check-circle"></i> ${Msg}</p></h5>
            </div>
        </div>
    `)

    $(`#toast${Num}`).toast('show')
    setTimeout(function () {
        $(`#toast${Num}`).remove();
    }, 2500);
}

function FilePostToast(Num) {
    $('#PostMsgBox').append(`
        <div class="toast hide" role="alert" aria-live="assertive" aria-atomic="true" data-autohide="false" id="toast${Num}">
            <div class="toast-header">
                <strong class="mr-auto">提示訊息</strong>
                <button type="button" class="ml-2 mb-1 close" data-dismiss="toast" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div class="toast-body d-flex mx-5" id="toast-body-${Num}">
                <p class="mr-1">檔案上傳中 ... </p>
                <div class="spinner-border text-secondary" role="status">
                    <span class="sr-only">Loading...</span>
                </div>
            </div>
        </div>
    `)

    $(`#toast${Num}`).toast('show')
}

function DelToast(Num) {
    $(`#toast-body-${Num}`).html(`<h5><p class="text-success"><i class="fas fa-check-circle"></i> 檔案上傳成功</p></h5>`)
    setTimeout(function () {
        $(`#toast${Num}`).animate().remove();
    }, 2500);
}

//更新步驟9
function StepCheckUpdate(CourseId) {
    return (
        $.ajax({
            type: "Get",
            url: `/Courses/StepCheck?CourseId=${CourseId}`,
            success: function (res) {
                $('#StepCheck-Container').html(res);
            }
        })
    )
}

//步驟1專用
//上傳Title圖片
$("#TitlePageImage").change(function () {
    var CourseId = $("#TitlePageImage").get(0).dataset.courseid;

    var result = document.getElementById("TitlePageImage").files[0]
    var data = new FormData();
    data.append("TitlePageImageUpload", result);

    FilePostToast(1);
    $.ajax({
        type: "Post",
        url: `/Courses/CoursePhotoUpload?CourseId=${CourseId}`,
        data: data,
        contentType: false,
        processData: false,
        success: function (response) {
            DelToast(1);
        }
    })

    //當檔案改變後，做一些事 
    readURL(this);
});

//上傳預覽
function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $("#targetCourseImg").attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
}

//步驟6專用
//上傳預覽影片
$("#previewVideo").change(function () {
    var CourseId = $("#previewVideo").get(0).dataset.courseid;

    var result = document.getElementById("previewVideo").files[0]
    var data = new FormData();
    data.append("PreviewVideoUpload", result);

    FilePostToast(6);
    $.ajax({
        type: "Post",
        url: `/Courses/CourseVideoUpload?CourseId=${CourseId}`,
        data: data,
        contentType: false,
        processData: false,
        success: function (response) {
            DelToast(6);
        }
    })

    readVideoURL(this)
})

function readVideoURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $("#previewVideoPlayBox").attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
}

//步驟6專用
//上傳課程影片
$(".courseVideoPost").change(function () {
    var CourseId = $(this).get(0).dataset.courseid;
    var UnitId = $(this).get(0).dataset.unitid;

    var result = $(this).get(0).files[0]
    var data = new FormData();
    data.append(`${UnitId}`, result);

    FilePostToast(UnitId);
    $.ajax({
        type: "Post",
        url: `/Courses/MainCourseVideoUpload?CourseId=${CourseId}&UnitId=${UnitId}`,
        data: data,
        contentType: false,
        processData: false,
        success: function (response) {
            DelToast(UnitId);
        }
    })

    readCourseVideoURL(this, UnitId)
})

function readCourseVideoURL(input, unitId) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(`#courseVideoPlayBox-${unitId}`).attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
}

//步驟7專用
//判斷客製網址是否重複
$("#course_CustomUrl").change(function () {
    var urlBtn = $("#url-btn")
    var inputText = $(this).val();
    var msgBox = $("#msg-box")
    var CourseId = $("#msg-box").get(0).dataset.courseid;

    $.ajax({
        type: "Get",
        url: `/api/MemberAction/CheckUrl?Url=${inputText}&CourseId=${CourseId}`,
        success: function (res) {
            if (res.Status == 1) {
                msgBox.addClass("text-danger")
                msgBox.removeClass("text-success")
                msgBox.get(0).innerHTML = `<i class="fas fa-exclamation-triangle"></i> ${res.ErrMsg}`
                urlBtn.attr("disabled", true)
            } else {
                msgBox.addClass("text-success")
                msgBox.removeClass("text-danger")
                msgBox.get(0).innerHTML = `<i class="fas fa-check-circle"></i> ${res.ErrMsg}`
                urlBtn.removeAttr("disabled")
            }
        }
    })
})
