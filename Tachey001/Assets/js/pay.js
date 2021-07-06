



let rd1 = document.getElementById("flexRadioDefault1");
let r1 = document.getElementById("r1");
let rd2 = document.getElementById("flexRadioDefault2");
let r2 = document.getElementById("r2");

rd1.addEventListener("click", function () {
    r1.innerHTML = '<span>6900Hp折抵Nt69</span> <a href="#" id="pay-cancel1">X取消</a>' ;
    r2.innerHTML = "";

    let cancel1 = document.getElementById("pay-cancel1");
    cancel1.addEventListener("click", function () {
        window.location.href = '/Pay/check';
    });
   
});
rd2.addEventListener("click", function () {
    r1.innerHTML = "";
    r2.innerHTML = '<a href="#" id="pay-cancel2">X取消</a>  <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#discpuntCoupon">使用我的折扣券</button >';
    let cancel2 = document.getElementById("pay-cancel2");
    cancel2.addEventListener("click", function () {
        window.location.href = '/Pay/check';
    });
  
});
var value = null;

var btns_1 = document.querySelectorAll('.discountcard');
        btns_1.forEach((item,index)=>{
            item.addEventListener('click',function(e){
                btns_1.forEach(Object=>Object.setAttribute('style',''))
                //把每一個btn1的style清空
                let _btn = e.path[0].closest('div')
                item.setAttribute('style', 'border:3px solid red')
                console.log(e)  
              
                value = item.getAttribute("value");
                
                console.warn(value);
            })
        })

var btn_use = document.querySelector(".btn-use");
btn_use.addEventListener("click", function () {
    window.location.href = '/Pay/check?ticketId=' + value;


})

let cancel2 = document.getElementById("pay-cancel2");
if (cancel2 != null) {
    cancel2.addEventListener("click", function () {
        window.location.href = '/Pay/check';
    });
}