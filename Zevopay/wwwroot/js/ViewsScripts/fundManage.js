
$(document).ready(function () {

    $("#fundManage_btn").on("click", function (e) {

        var memberId = $("#memberId").val();
        var factor = $("#factor").val();
        var amount = $("#amount").val().trim();
        var description = $("#description").val();
        var formData = {
            MemberId: memberId,
            Factor: factor,
            Amount: amount,
            Description: description
        }

        if (memberId == '') {
            toastr.error('please select user!');
        } else if (factor == '') {
            toastr.error('please select factor!');
        } else if (amount == '' || amount == 0 || amount == ' ') {
            if (amount == '' || amount == ' ') {
                toastr.error('please enter amount!');
            } else {
                toastr.error('Amount should be grater then 0!');
            }
        } else if (description == '' || description == ' ') {
            toastr.error('please enter description!');
        } else {
            $.ajax({
                url: "/Admin/FundManage",
                type: "post",
                data: formData,
                success: function (result) {
                    if (result.resultFlag == 1) {
                        toastr.success(result.message);
                        window.location.href = "/Home/Index";
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