$("#filter-unit").on('click', function () {
  $("#unit-dropdown").addClass("open");
});

$("#main").on('click', function () {
  $("#feedbacks").removeClass("active");
  $("#discussions").removeClass("active");
  $("#assignments").removeClass("active");
  $("#main").addClass("active");
  $(".feedbacks").css("display", "none");
  $(".discussions").css("display", "none");
  $(".assignments").css("display", "none");
  $(".main").css("display", "block");
});

$("#feedbacks").on('click', function () {
  $("#main").removeClass("active");
  $("#discussions").removeClass("active");
  $("#assignments").removeClass("active");
  $("#feedbacks").addClass("active");
  $(".main").css("display", "none");
  $(".discussions").css("display", "none");
  $(".assignments").css("display", "none");
  $(".feedbacks").css("display", "block");
});

$("#discussions").on('click', function () {
  $("#main").removeClass("active");
  $("#feedbacks").removeClass("active");
  $("#assignments").removeClass("active");
  $("#discussions").addClass("active");
  $(".main").css("display", "none");
  $(".feedbacks").css("display", "none");
  $(".assignments").css("display", "none");
  $(".discussions").css("display", "block");
});

$("#assignments").on('click', function () {
  $("#main").removeClass("active");
  $("#feedbacks").removeClass("active");
  $("#discussions").removeClass("active");
  $("#assignments").addClass("active");
  $(".main").css("display", "none");
  $(".feedbacks").css("display", "none");
  $(".discussions").css("display", "none");
  $(".assignments").css("display", "block");
});