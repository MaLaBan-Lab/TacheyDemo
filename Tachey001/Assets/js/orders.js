let changeBtn = document
  .querySelector(".checked-btn")
  .querySelectorAll("button");

changeBtn.forEach((item) => {
  item.addEventListener("click", changeIndex);
});

function changeIndex(e) {
    changeBtn.forEach((item) => {
        if (item.classList.contains(e.target.id)) {
            item.classList.add("change-btn-checked");
            item.classList.remove("change-btn");
        } else {
            item.classList.remove("change-btn-checked");
            item.classList.add("change-btn");
        }
  });
}


let od_success = document.querySelector("#od-success");
let od_pay = document.querySelector("#od-pay");
let od_error = document.querySelector("#od-error");
let od_record = document.querySelector("#od-record");


od_success.addEventListener("click", function () {
    
})

