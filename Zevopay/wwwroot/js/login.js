$(document).ready(function () {

    $('#sign_in').on('click', function (e) {
        e.preventDefault();
        showLoader();
        var email = $('#EmailInput').val();
        var password = $('#PasswordInput').val();
        var formData = {
            Email: email,
            Password: password
        };
        $.ajax({
            url: "/Account/Login",
            type: "post",
            data: formData,
            success: function (d) {
                if (d.resultFlag == 0) {
                    toastr.error(d.message);
                    hideLoader();
                } else {
                    toastr.success(d.message);
                    window.location.href = "/Home/Index";
                }
            },
            error: function (error) {
                hideLoader();
                toastr.error('Login Failed!');
            }
        });
    })
});