﻿$(document).ready(function () {
    fillTransactionData();
});
function fillTransactionData() {
    debugger;
    $.ajax({
        url: "/Member/PayoutTransactionsPartial",
        type: "get",
        success: function (d) {
            $('#Transaction-Model').html('');
            $('#Transaction-Model').html(d);
        },
        error: function (error) {
            toastr.error('Failed to get Transactions!');
        }
    });
}