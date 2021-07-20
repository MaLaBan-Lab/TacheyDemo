
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


