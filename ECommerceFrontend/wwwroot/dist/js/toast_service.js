/*
 * Author: Dennis Kamau
 * Date: 15 Sep 2021
 * Description:
 *      
 **/

function showSwalToast(message, type) {
    var Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000
    });

    Toast.fire({
        icon: type,
        title: "&nbsp;&nbsp;" + message
    })
}
