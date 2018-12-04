
$.cookie.json = true;
$.cookie.defaults.path = "/";
function getCartItems() {
    //return [];
    if ($.cookie('productlist')) {
        return $.cookie('productlist').cartItems;
    } else {
        return [];
    }
}
function getCartItems2() {
    //return [];
    if ($.cookie('wishlist')) {
        return $.cookie('wishlist').cartItems;
    } else {
        return [];
    }
}
function saveCartItems(cart_items) {
    var obj = { "cartItems": cart_items };
    $.cookie('productlist', obj);
}

function saveCartItems2(cart_items) {
    var obj = { "cartItems": cart_items };
    $.cookie('wishlist', obj);
}
function emptyCartItems() {

    $.cookie('productlist', null);
}

function emptyCartItems2() {
    var cart_items = getCartItems2();

    $(cart_items).each(function (i, v) {
        if (v) {
            cart_items.splice(i, 1);
        }
    });

    saveCartItems2(cart_items);
}
function addItem(productid, productname, unit, price) {

    var cart_items = getCartItems();

    var is_exist = false;
    $(cart_items).each(function (i, v) {
        if (v && v.productid == productid) {
            is_exist = true;
        }
    });
    if (!is_exist) {
        var new_item = { "productid": productid, "productname": productname, "unit": unit, "price": price.replace(/,/g, ''), "quantity": 1 };
        cart_items.push(new_item);
        saveCartItems(cart_items);
        alert("Thêm giỏ hàng!");
        //loadHeaderCartItems();
    } else {
        alert("Đã có!");
    }
}

function addItem2(productid, productname, unit, price) {

    var cart_items = getCartItems2();

    var is_exist = false;
    $(cart_items).each(function (i, v) {
        if (v && v.productid == productid) {
            is_exist = true;
        }
    });
    if (!is_exist) {
        var new_item = { "productid": productid, "productname": productname, "unit": unit, "price": price.replace(/,/g, '')};
        cart_items.push(new_item);
        saveCartItems2(cart_items);
        alert("Yêu thích!");
        //loadHeaderCartItems();
    } else {
        alert("Existed!");
    }
}

function updateItem(productid, q) {

    var cart_items = getCartItems();
    var t = 0;
    $(cart_items).each(function (i, v) {
        if (v && v.productid == productid) {
            cart_items[i].quantity = q;
            saveCartItems(cart_items);
            t = cart_items[i].price * q;
        }
    });
    //loadHeaderCartItems();
    return t;
}


function removeItem(productid) {

    var cart_items = getCartItems();

    $(cart_items).each(function (i, v) {
        if (v && v.productid == productid) {
            cart_items.splice(i, 1);
        }
    });

    saveCartItems(cart_items);
    //loadHeaderCartItems();
}

function removeItem2(productid) {

    var cart_items = getCartItems();

    $(cart_items).each(function (i, v) {
        if (v && v.productid == productid) {
            cart_items.splice(i, 1);
        }
    });

    saveCartItems2(cart_items);
 
}
function showTotal() {
    var total = 0;
    var cart_items = getCartItems();
    $(cart_items).each(function (i, v) {
        if (v) {
            total += v.price * v.quantity;
        }
    });
    $("#sumtotal").html(total.toLocaleString('en'));
}

function loadCartItems() {

    var cart_items = getCartItems();
    var total = 0;
    $(cart_items).each(function (i, v) {
        var t = v.price * v.quantity;
        total += t;
        $("#carts").append("<tr>"
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

    
    $("#form_checkout").submit(function () {
        var cart_items = getCartItems();
        $("#cartlist").val(JSON.stringify(cart_items));

        $.ajax({
            url: '/Cart/Checkout',
            type: "POST",
            data: $(this).serialize(),
            success: function (response) {
                if (response == "OK") {
                    //tu nghien cuu sau khi ngu day
                    emptyCartItems();
              
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

function loadHeaderCartItems() {

    var cart_items = getCartItems();
    var total = 0;
    $(cart_items).each(function (i, v) {
        var t = v.price * v.quantity;
        total += t;
        $("#carts").append("<tr>"
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


    $(".removeitem").click(function () {
        if (confirm("Are you sure to remove this item?")) {
            var tr = $(this).closest("tr").find("td");
            removeItem(tr.eq(0).html());
            tr.remove();
            showTotal();
        }
    });
}

function loadWishListCartItems() {

    var cart_items = getCartItems2();
    

    $(cart_items).each(function (i, v) {
      
        $("#wishlist2").append("<tr>"
            + "<td>" + v.productname + "</td>"
            + "<td align='center'>" + v.unit + "</td>"
            + "<td align='right'>" + parseFloat(v.price).toLocaleString('en') + "</td>"
            + "<td><button class='addtocart'>Add to cart</button></td>"
            + "<td><button class='removewishlist'>Remove</button></td>"
            + "</tr>");
    });
 
    $(".addtocart").click(function () {
        var is_exist = false;
        $(cart_items).each(function (i, v) {
            if (v && v.productid == productid) {
                is_exist = true;
            }
        });
        if (!is_exist) {
            var new_item = { "productid": productid, "productname": productname, "unit": unit, "price": price.replace(/,/g, ''), "quantity": 1 };
            cart_items.push(new_item);
            saveCartItems(cart_items);
            alert("Added to your cart!");
            //loadHeaderCartItems();
        } else {
            alert("Existed!");
        }
    });
    
   

    $(".removewishlist").click(function () {
        if (confirm("Are you sure to remove this item?")) {
            var tr = $(this).closest("tr").find("td");
            removeItem2(tr.eq(0).html());
            tr.remove();

        }
    });
}

$(document).ready(function () {

    $(".addtowishlist").click(function () {
        var a = $(this);
        addItem2(a.attr("productid"), a.attr("productname"), a.attr("unit"), a.attr("price"));

        return false;
    });
  

    $(".addtocart").click(function () {
        var a = $(this);
        addItem(a.attr("productid"), a.attr("productname"), a.attr("unit"), a.attr("price"));

        return false;
    });



    //loadHeaderCartItems();//tren header moi trang
 
    loadCartItems();
    loadWishListCartItems();
});
