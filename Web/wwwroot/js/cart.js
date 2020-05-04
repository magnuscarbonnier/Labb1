window.onload = function () {
    GetCartAmount();
};
function GetCartAmount() {
    fetch("https://localhost:44344/api/cart/getcartamount")
        .then(response => {
            console.log(response);
            if (response.ok) {
                return response.text();
            }
        }).then(data => {
            var element = document.getElementById("cart-amount");
            if (data != 0) { element.innerHTML = '[' + data + ']'; }
        }

        );
}

    function AddToCart(productId) {
        fetch("https://localhost:44344/api/cart/addtocart?id=" + productId)
            .then(response => {
                console.log(response);
                if (response.ok) {
                    return response.text();
                }
            }).then(data => {
                var element = document.getElementById("cart-amount");
                if (data != 0) {element.innerHTML = '['+data+']';}
                
            }

            );
    }
