/*
 * Author: Dennis Kamau
 * Date: 15 Sep 2021
 * Description:
 *      This is a js file for Accounts page
 **/

function setBodyClass(section) {
    if (section === "main") {
        $('body').removeClass("sidebar-mini").addClass("hold-transition layout-top-nav");
    }

    if (section === "admin") {
        $('body').removeClass("hold-transition").removeClass("layout-top-nav").addClass("sidebar-mini layout-navbar-fixed");
    }
}

function removeElement(element_id) {
    $(`#${element_id}`).remove();
}

function updateCart() {
    var count = $("#chart_items").text();
    $("#chart_items").text(Number(count) + 1);
}

function removeItemFromCart(number) {
    var count = $("#chart_items").text();
    $("#chart_items").text(Number(count) - Number(number));
}