var cartTitleAmount = $("#cartTitleAmount").get(0)
var cartInnerAmount = $("#cartInnerAmount").get(0)
var cartTotalPrice = $("#cartTotalPrice").get(0)

$(function () {

    var currentId = $("#CartBox").get(0).dataset.memberid
    var url = "/api/MemberAction/GetCartData?MemberId=" + currentId

    if (currentId != undefined) {
        $.get(url, function (response) {
            console.dir(response)

            response.Result.cartpartialViewModels.forEach(item => {
                var cartDiv = document.createElement('div');

                cartDiv.innerHTML = `
                    <a class="dropdown-item text-white my-3" href="/Member/Cart">
                        <div class="media">
                            <img src="${item.TitlePageImageURL}" class="mr-3" style="width:130px; height:70px">
                            <div class="media-body d-flex flex-column justify-content-between overflow-hidden" style="width:300px; height:70px">
                                <div class="m-0">${item.Title}</div>
                                <div class="m-0 d-flex justify-content-between"><span>已開課</span><span class="text-right">NT$${item.OriginalPrice}</span></div>
                            </div>
                        </div>
                    </a>
                `
                $("#CartBox").append(cartDiv)
            })

            cartTitleAmount.innerText = response.Result.cartpartialViewModels.length;
            cartInnerAmount.innerText = response.Result.cartpartialViewModels.length;
            cartTotalPrice.innerText = toCurrency(response.Result.total);
        })
    }
})

/*將數字千分化*/
function toCurrency(num) {
    var parts = num.toString().split('.');
    parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ',');
    return parts.join('.');
}