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
}