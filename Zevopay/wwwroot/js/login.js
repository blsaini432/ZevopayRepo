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
        if (email == '' || email == ' ') {
            toastr.error('please enter email!');
            hideLoader();
        } else if (password == '' || password == ' ') {
            toastr.error('please enter password!');
            hideLoader();
        } else {

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
                        var isTwoFactorAuthenticate=false;
                        if (d.resultFlag == 2) {

                            isTwoFactorAuthenticate = false;
                        } else {
                            isTwoFactorAuthenticate = true;

                        }
                        var email = d.data.email;
                        window.location.href = `/Account/TwoFactorAuth?Email=${email}&isTwoFactorAuthenticate=${isTwoFactorAuthenticate}`;
                    } 
                },
                error: function (error) {
                    toastr.error('Login Failed!');
                    hideLoader();
                }
            });
        }
    })

    
});