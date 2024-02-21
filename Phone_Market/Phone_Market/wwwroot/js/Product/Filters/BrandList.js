//function BrandList() {
//    $(document).ready(function () {
//        $.ajax({
//            url: '/Brand/GetBrands',
//            type: 'GET',
//            dataType: 'json',
//            success: function (data) {
//                var dropdown = $('#brandDropdown');
//                dropdown.empty();
//                $.each(data, (index, brand) => {
//                    dropdown.append($('<option></option>').val(brand.id).text(brand.name));
//                });
//            },
//            error: function () {
//                console.error('Erorr.');
//            }
//        });
//    });
//}

function BrandList() {
    $(document).ready(function () {
        $.ajax({
            url: '/Brand/GetBrands',
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                var dropdown = $('#brandDropdown');
                dropdown.empty();

                // Add checkboxes
                $.each(data, (index, brand) => {
                    var checkbox = $('<input>').attr({
                        type: 'checkbox',
                        id: 'brandCheckbox_' + brand.id,
                        value: brand.id
                    });
                    var label = $('<label>').attr('for', 'brandCheckbox_' + brand.id).text(brand.name);
                    var listItem = $('<li>').append(checkbox).append(label);

                    dropdown.append(listItem);
                });
            },
            error: function () {
                console.error('Error.');
            }
        });
    });
}
