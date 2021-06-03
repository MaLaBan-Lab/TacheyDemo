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
      let emptyText = document.getElementById("emptyText");

      emptyText.innerText = item.innerText;
    } else {
      item.classList.remove("change-btn-checked");
      item.classList.add("change-btn");
    }
  });
}
