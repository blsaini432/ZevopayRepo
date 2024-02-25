$(document).ready(function () {
    fillTransactionData();
});
function fillTransactionData() {
    $.ajax({
        url: "/Admin/WalletTransactionsPartial",
        type: "get",
        success: function (d) {
            $('#Transaction-Div').html('');
            $('#Transaction-Div').html(d);
            //toastr.info(d.message);
        },
        error: function (error) {
            //toastr.error('Failed to get subadminList!');
        }
    });
}