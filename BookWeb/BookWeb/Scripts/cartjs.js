$(document).ready(function () {
    showCount();
    $('body').on('click', '.btnAddToCart', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var quantity = 1;
        var txtQuantity = $('#quantity_value').text();
        if (txtQuantity != '') {
            quantity = parseInt(txtQuantity);
        }
        $.ajax({
            url: '/cart/add',
            type: 'POST',
            data: { id: id, quantity: quantity },
            success: function (rs) {
                    $('#checkout_items').html(rs.Count);
                    alert(rs.msg);
                    showCount();
            }
        });
    })

    $('body').on('click', '.btnDelete', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
            $.ajax({
                url: '/cart/delete',
                type: 'POST',
                data: { id: id },
                success: function (rs) {
                    showCount();
                    $('#trow_' + id).remove();
                }
            });
    })

    $('body').on('click', '.btnClear', function (e) {
        e.preventDefault();
        $.ajax({
            url: '/cart/clear',
            type: 'POST',
            success: function (rs) {
                showCount();
            }
        });
    })
});

function showCount() {
    $.ajax({
        url: '/cart/count',
        type: 'GET',
        success: function (rs) {      
          $('#checkout_items').html(rs.Count);
        }
    });
}