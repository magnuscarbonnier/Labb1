window.onload = function () {
    GetCartAmount();
}

function GetCartAmount() {
    fetch("https://localhost:44344/cart/getcartamount")
        .then(response => {
            console.log(response);
            if (response.ok) {
                return response.text();
            }
        }).then(data => {
            var element = document.getElementById("cart-amount");
            if (data != 0) { element.innerHTML = '[' + data + ']'; }
        });
}

function AddToCart(productId) {
    $.ajax({
        type: "POST",
        url: 'https://localhost:44344/product/addtocart?id=' + productId,

        success: function (result) {
            GetCartAmount();
            ShowAddMessage(productId);
        }
    });
}

function ShowAddMessage(productId) {
    var element = document.getElementById(productId);
    element.innerHTML = ' <i class="fas fa-cart-arrow-down text-success"></i>';
}