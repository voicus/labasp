function addFavouriteButton(productId, userId) {
    let a = document.createElement("a");
    let img = document.createElement("img");
    let div = document.getElementById(productId);
    let addFunction = () => {
        let div = document.getElementById(productId);
        div.removeChild(div.children[0]);
        let a = document.createElement("a");
        let img = document.createElement("img");
        img.title = "Remove from favourites";
        img.src = "../img/full-fire.png";
        $.ajax({
            url: '/Product/AddFavouriteProduct',
            type: 'POST',
            data: { productId: productId, userId: userId }
        });
        a.appendChild(img);
        a.addEventListener("click", removeFunction);
        div.appendChild(a);
    };
    let removeFunction = () => {
        let div = document.getElementById(productId);
        div.removeChild(div.children[0]);
        let a = document.createElement("a");
        let img = document.createElement("img");
        img.title = "Add to favourites";
        img.src = "../img/empty-fire.png";
        $.ajax({
            url: '/Product/RemoveFavouriteProduct',
            type: 'POST',
            data: { productId: productId, userId: userId }
        });
        a.appendChild(img);
        img.addEventListener("mouseout", function () {
            img.addEventListener("mouseover", function () {
                img.src = "../img/full-fire.png";
            });
            img.src = "../img/empty-fire.png";
        });
        a.addEventListener("click", addFunction);
        div.appendChild(a);

    };
    fetch(`/Product/IsFavouriteProduct?userId=${userId}&productId=${productId}`,
        {
            method: "GET"
        }).then(response => response.json())
        .then(isFavourite => {
            if (isFavourite == true) {
                img.src = "../img/full-fire.png";
                a.addEventListener("click", removeFunction);

            }
            else {
                img.src = "../img/empty-fire.png";
                a.addEventListener("click", addFunction);
                img.addEventListener("mouseover", function () {
                    img.src = "../img/full-fire.png";
                });
                img.addEventListener("mouseout", function () {
                    img.src = "../img/empty-fire.png";
                });
            }
            a.appendChild(img);
            div.appendChild(a);
        })
}