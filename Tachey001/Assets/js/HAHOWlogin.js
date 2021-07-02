var userInput = document.querySelector('#user_input'); //抓到輸入帳號的input
var pwdInput = document.querySelector('#pwd_input'); //抓到輸入密碼的input
var btnInput = document.querySelector('#input_btn'); //抓到button

if (userInput != undefined) {
    userInput.addEventListener('input', btnAllow) //設置監聽事件為input , 觸發方法btnAllow
}
if (pwdInput != undefined) {
    pwdInput.addEventListener('input', btnAllow) //設置監聽事件為input , 觸發方法btnAllow
}

function btnAllow() {
    //當帳號欄位不為空值且密碼欄位不為空值
    if (userInput.value != '' && pwdInput.value != '') {
        //按鈕啟用
        btnInput.disabled = false
    } else {
        //按鈕禁用
        btnInput.disabled = true
    }
}