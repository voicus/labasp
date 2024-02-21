function AddToCart() {


    function AddProductToCart(productId) {
        $.ajax({
            url: '/Product/AddToCart',
            type: 'POST',
            data: { productId: productId },
            success: function (result) {
                ShowNotification('Product added to cart');
            },
            error: function (xhr, status, error) {
                console.error('error adding product to cart:', error);
            }
        });
    }

    function ShowNotification(message) {


        var notification = $('#notification');
        notification.css('background-color', 'green');


        notification.text(message);
        notification.removeClass("hidden");
        setTimeout(function () {
            notification.addClass("hidden");
        }, 3000);
    }




    var productId = $('#productIdHidden').val();
    var isUserAuthenticated = $('#isUserAuthenticatedHidden').val();



    if (isUserAuthenticated === '') {
        window.location.href = '/Account/Login';
    }

    else { AddProductToCart(productId); }

}
