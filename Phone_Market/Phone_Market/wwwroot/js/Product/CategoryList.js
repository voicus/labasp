function CategoryList() {
    $(document).ready(function () {
        $.ajax({
            url: '/Category/GetCategories',
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                var dropdown = $('#categoryDropdown');
                dropdown.empty();
                dropdown.append($('<option></option>').val('').text('Select a color'));
                $.each(data, (index, brand) => {
                    dropdown.append($('<option></option>').val(brand.id).text(brand.name));
                });
            },
            error: function () {
                console.error('Eroare la încărcarea țărilor.');
            }
        });
    });
}