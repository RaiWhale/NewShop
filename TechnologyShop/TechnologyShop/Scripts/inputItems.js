
$.cookie.json = true;
$.cookie.defaults.path = "/";
function getInputItems() {
    //return [];
    if ($.cookie('productlist')) {
        return $.cookie('productlist').inputItems;
    } else {
        return [];
    }
}

function saveInputItems(input_items) {
    var obj = { "inputItems": input_items };
    $.cookie('productlist', obj);
}

function emptyCartItems() {

    $.cookie('productlist', null);
}

function addItem(productid, productname, unit, price) {

    var input_items = getInputItems();

    var is_exist = false;
    $(input_items).each(function (i, v) {
        if (v && v.productid == productid) {
            is_exist = true;
        }
    });
    if (!is_exist) {
        var new_item = { "productid": productid, "productname": productname, "unit": unit, "price": price.replace(/,/g, ''), "quantity": 1 };
        input_items.push(new_item);
        saveInputItems(input_items);
        alert("Thêm!");

    } else {
        alert("Đã có!");
    }
}

function updateItem(productid, q) {

    var input_items = getInputItems();
    var t = 0;
    $(input_items).each(function (i, v) {
        if (v && v.productid == productid) {
            input_items[i].quantity = q;
            saveInputItems(input_items);
            t = input_items[i].price * q;
        }
    });

    return t;
}

function removeItem(productid) {

    var input_items = getInputItems();

    $(input_items).each(function (i, v) {
        if (v && v.productid == productid) {
            input_items.splice(i, 1);
        }
    });

    saveInputItems(input_items);
}

function showTotal() {
    var total = 0;
    var input_items = getInputItems();
    $(input_items).each(function (i, v) {
        if (v) {
            total += v.price * v.quantity;
        }
    });
    $("#sumtotal").html(total.toLocaleString('en'));
}

function loadInputItems() {

    var input_items = getInputItems();
    var total = 0;
    $(input_items).each(function (i, v) {
        var t = v.price * v.quantity;
        total += t;
        $("#inputs").append("<tr>"
            + "<td align='center'>" + v.productid + "</td>"
            + "<td>" + v.productname + "</td>"
            + "<td align='center'>" + v.unit + "</td>"
            + "<td align='right'>" + parseFloat(v.price).toLocaleString('en') + "</td>"
            + "<td align='right'><input type='number' class='quantity' value='" + v.quantity + "' min='1' max='1000'></td>"
            + "<td align='right'>" + t.toLocaleString('en') + "</td>"
            + "<td><button class='removeitem'>Remove</button></td>"
            + "</tr>");
    });
    $("#sumtotal").html(total.toLocaleString('en'));

    $(".quantity").bind("keyup change click", function () {
        var tr = $(this).closest("tr").find("td");
        var t = updateItem(tr.eq(0).html(), $(this).val());
        tr.eq(5).html(t.toLocaleString('en'));
        showTotal();
    });

    $(".removeitem").click(function () {
        if (confirm("Are you sure to remove this item?")) {
            var tr = $(this).closest("tr").find("td");
            removeItem(tr.eq(0).html());
            tr.remove();
            showTotal();
        }
    });


    $("#form_submit").submit(function () {
        var input_items = getInputItems();
        $("#inputlist").val(JSON.stringify(input_items));

        $.ajax({
            url: '/Cart/Checkout',
            type: "POST",
            data: $(this).serialize(),
            success: function (response) {
                if (response == "OK") {
                    //tu nghien cuu sau khi ngu day
                    emptyInputItems();

                    window.location.href = 'Cart/OrderComplete';
                } else {
                    alert(response);
                }
            },
            error: function () {

            }
        });
        return false;
    });
}

$(document).ready(function () {


    $("#addtoinput").click(function () {
        var a = $(this);
        addItem(a.attr("productid"), a.attr("productname"), a.attr("unit"), a.attr("price"));

        return false;
    });
    loadInputItems();

});