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

    console.dir(chapterInput)
    chapterInput.forEach((item, index) => {
        item.name = (index + 1)
    })

    chapterBox.forEach((item, index) => {
        item.querySelector('.unitBox').querySelectorAll('input').forEach((x, count) => {
            x.name = `${index + 1}`
        })
    })
}


//上傳Title圖片
$("#TitlePageImage").change(function () {
    var CourseId = $("#TitlePageImage").get(0).dataset.courseid;

    var result = document.getElementById("TitlePageImage").files[0]
    var data = new FormData();
    data.append("TitlePageImageUpload", result);

    $.ajax({

        type: "Post",
        url: `/Courses/CoursePhotoUpload?CourseId=${CourseId}`,
        data: data,
        contentType: false,
        processData: false,
        success: function (response) {
            alert("上傳網址 : " + response.ErrMsg);
        }
    })

    //當檔案改變後，做一些事 
    readURL(this);   // this代表<input id="imgInp">
});

function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $("#targetCourseImg").attr('src', e.target.result);
        }
        reader.readAsDataURL(input.files[0]);
    }
}