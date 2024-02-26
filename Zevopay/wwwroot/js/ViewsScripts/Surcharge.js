
$(document).ready(function () {
    $("#Surcharge_Submit_btn").on("click", function (e) {

        var surchargeAmount = $("#SurchargeAmount").val();
        var txnType = $("#TransactionType").val();
        var rangeFrom = $("#RangeFrom").val();
        var rangeTo = $("#RangeTo").val();
        var isFlat = $("#IsFlat").val();
        var formData = {
            surchargeAmount: surchargeAmount,
            TransactionType: txnType,
            RangeFrom: rangeFrom,
            RangeTo: rangeTo,
            IsFlat: isFlat
        }

        if (surchargeAmount == '') {
            toastr.error('please enter surchargeAmount!');
        } else if (txnType == '') {
            toastr.error('please select TransactionType!');
        } else if (rangeFrom == '' || rangeFrom == 0) {
            if (rangeFrom == '') {
                toastr.error('please enter amount!');
            } else {
                toastr.error('RangeFrom should be grater then 0!');
            }
        } else if (rangeTo == '' || rangeTo <= rangeFrom) {
            if (rangeTo == '') {

                toastr.error('please enter RangeTo!');
            } else {

                toastr.error('RangeTo should be grater then RangeFrom!');
            }
        } else {
            $.ajax({
                url: "/Admin/Surcharge",
                type: "post",
                data: formData,
                success: function (result) {
                    if (result.resultFlag == 1) {
                        toastr.success(result.message);
                        window.location.href = "/Admin/SurchargeList";
                    }
                    toastr.error(result.message);
                },
                error: function (error) {
                    toastr.error("error");
                }
            });
        }
    })
})