$(document).ready(function () {
    $("#MoneyTransfer_Submit_btn").on("click", function (e) {

        var paymentMode = $("#PaymentMode").val();
        var accountNumber = $("#AccountNumber").val();
        var iFSCCode = $("#IFSCCode").val();
        var fullName = $("#FullName").val();
        var amount = $("#Amount").val();

        var formData = {
            PaymentMode: paymentMode,
            AccountNumber: accountNumber,
            IFSCCode: iFSCCode,
            FullName: fullName,
            Amount: amount

        }

        if (paymentMode == '') {
            toastr.error('please enter paymentMode!');

        } else if (accountNumber == '') {
            toastr.error('please enter accountNumber!');

        } else if (iFSCCode == '') {
            toastr.error('please enter IFSCCode!');

        }else if (fullName == '') {
            toastr.error('please enter fullName!');

        } else if (amount == '' || amount == 0) {
            toastr.error('please enter amount!');

        } else {
            $.ajax({
                url: "/Member/MoneyTransferSave",
                type: "post",
                data: formData,
                success: function (result) {
                    if (result.resultFlag == 1) {
                        toastr.success(result.message);

                    }
                    else {
                        toastr.error(result.message);
                    }

                },
                error: function (error) {
                    toastr.error("error");
                }
            });
        }
    })

})