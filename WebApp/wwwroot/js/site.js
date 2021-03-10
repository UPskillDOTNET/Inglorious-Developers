// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function confirmCancel(id, isCancel) {
    var confirmCancel = 'confirmCancel_' + id;
    var cancelReservation = 'cancelReservation_' + id;

    if (isCancel) {
        $('#' + cancelReservation).hide();
        $('#' + confirmCancel).show();
    } else {
        $('#' + cancelReservation).show();
        $('#' + confirmCancel).hide();
    }
}

function confirmSublet(id, forSublet) {
    var confirmSublet = 'confirmSublet_' + id;
    var subletReservation = 'subletReservation_' + id;

    if (forSublet) {
        $('#' + subletReservation).hide();
        $('#' + confirmSublet).show();
    } else {
        $('#' + subletReservation).show();
        $('#' + confirmSublet).hide();
    }
}