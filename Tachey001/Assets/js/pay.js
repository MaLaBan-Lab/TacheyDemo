


var send = document.getElementById("send");

let money1 = document.getElementById("money1");
let money2 = document.getElementById("money2");
let totalprice = money1.getAttribute("value");
let rd1 = document.getElementById("flexRadioDefault1");
let r1 = document.getElementById("r1");
let rd2 = document.getElementById("flexRadioDefault2");
let r2 = document.getElementById("r2");
let point = null;
let check1 = r1.getAttribute("check");
let check2 = r2.getAttribute("check");
rd1.addEventListener("click", function () {
    point = r1.getAttribute("value");
    r1.innerHTML = '<span>' + point + 'Hp折抵Nt' + point / 10 + '</span> <a href="#" id="pay-cancel1" class="btn btn-primary">X取消</a>';
    r2.innerHTML = "";
   /* money1.innerHTML = "NT" + (totalprice - point / 10).toLocaleString('zh-TW', { style: 'currency', currency: 'TWD', minimumFractionDigits: 0});*/
    money2.innerHTML = "NT" + (totalprice - point / 10).toLocaleString('zh-TW', { style: 'currency', currency: 'TWD', minimumFractionDigits: 0 });
    let cancel1 = document.getElementById("pay-cancel1");
    cancel1.addEventListener("click", function () {
        window.location.href = '/Pay/check';
    });
    r1.setAttribute('check', 'check');
    r2.setAttribute('check', 'null')
    //window.location.href = '/Pay/check?usepoint=' + "use";
    var url = "/Pay/check?usepoint=use"
    $.get(url, function () {
        send.setAttribute("sendvalue", "use")
    })
   
});
rd2.addEventListener("click", function () {
    r1.innerHTML = "";
    r2.innerHTML = '<a href="#" id="pay-cancel2" class="btn btn-primary">X取消</a>  <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#discpuntCoupon">使用我的折扣券</button >';
    let cancel2 = document.getElementById("pay-cancel2");
    money1.innerHTML = "NT" + (totalprice / 1).toLocaleString('zh-TW', { style: 'currency', currency: 'TWD', minimumFractionDigits: 0 });
    money2.innerHTML = "NT" + (totalprice / 1).toLocaleString('zh-TW', { style: 'currency', currency: 'TWD', minimumFractionDigits: 0 });
    cancel2.addEventListener("click", function () {
        r2.setAttribute('check', 'null');
        rd2.checked = false;    
        window.location.href = '/Pay/check';
    });
    r1.setAttribute('check', 'null');
    r2.setAttribute('check', 'check');
});
var value = null;
var dicount = 0;
var btns_1 = document.querySelectorAll('.discountcard');
        btns_1.forEach((item,index)=>{
            item.addEventListener('click',function(e){
                btns_1.forEach(Object=>Object.setAttribute('style',''))
                //把每一個btn1的style清空
                let _btn = e.path[0].closest('div')
                item.setAttribute('style', 'border:3px solid red')
                console.log(e)  
              
                value = item.getAttribute("value");
                discount = item.getAttribute("discount")
                console.warn(value);
            })
        })

var btn_use = document.querySelector(".btn-use");
btn_use.addEventListener("click", function () {

    var close = document.getElementById("pay-close")
    close.click();
 /*   money1.innerHTML = "NT$" + Math.round(totalprice * discount).toLocaleString('zh-TW', { style: 'currency', currency: 'TWD', minimumFractionDigits: 0 });*/
    money2.innerHTML = "NT" + Math.round(totalprice * discount).toLocaleString('zh-TW', { style: 'currency', currency: 'TWD', minimumFractionDigits: 0 });
    var url;
    if (value == null) {
        url = "/Pay/check"
    } else {
        url = "/Pay/check?ticketId=" + value
    }
    $.get(url, function () {
        send.setAttribute("sendvalue", value)
    });

})

let cancel2 = document.getElementById("pay-cancel2");
if (cancel2 != null) {
    cancel2.addEventListener("click", function () {
        window.location.href = '/Pay/check';
    });
}

if (check2 != "null")
    rd2.checked = true;


send.addEventListener("click", function () {
    var sendvalue = send.getAttribute("sendvalue");
    if (sendvalue == null && value == null) {
        var url = "/Pay/create";
        $.get(url, function () {
            var url = "https://localhost:44394/AioCheckOut.aspx";
            window.location.href = url;
        });
    }
    else if (sendvalue == "use") {
        var url = "/Pay/create?usepoint=" + "use"
        $.get(url, function () {
            var url = "https://localhost:44394/AioCheckOut.aspx";
            window.location.href = url;
        });
    }
    else {
        var url = "/Pay/create?ticketId=" + value;
        $.get(url, function () {
            var url = "https://localhost:44394/AioCheckOut.aspx";
            window.location.href = url;
        });
    }
        

});






