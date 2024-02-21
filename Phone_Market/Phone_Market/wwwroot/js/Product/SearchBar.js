function handleKeyPress(event) {
   
    if (event.keyCode === 13) {
        search();
    }
}

function search() {
    let searchInput = document.getElementById("searchBar");
    filter = searchInput.value.toUpperCase();
    if (filter != "") {
        location.href = `/Home/Index?filter=${filter}`;
    }
}