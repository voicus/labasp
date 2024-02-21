$('#applyFilterButton').on('click', function () {
    applyFilters();
});

function applyFilters() {
    // Gather selected brand IDs
    var selectedBrands = $('#brandDropdown input:checked').map(function () {
        return $(this).val();
    }).get();

    // Get max price value
    var maxPrice = $('#id1').val();

    // Perform filtering based on selected brands and max price
    $('.product-card').each(function () {
        var productBrandId = $(this).data('brand-id');
        var productPrice = $(this).data('price');

        // Check if the product meets the selected criteria
        var brandMatch = selectedBrands.length === 0 || selectedBrands.includes(productBrandId.toString());
        
        var priceMatch = productPrice <= maxPrice;

        if (maxPrice == '10000') {
            priceMatch = true;
        }

        // Show/hide the product card based on the criteria
        $(this).toggle(brandMatch && priceMatch);
    });
}
