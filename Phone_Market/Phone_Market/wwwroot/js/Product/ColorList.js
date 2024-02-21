function ColorList() {
    $(document).ready(function () {
        $.ajax({
            url: '/Color/GetColors',
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                var dropdown = $('#colorMultiSelectDropdown');
                dropdown.empty();
                dropdown.append($('<option></option>').val('').text('Select a category'));
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