﻿$(document).ready(function () {
    $('#AddOrUpdate-SubAdmin').on('click', function (e) {
        e.preventDefault();
        var id = $('#subAdminId').val();
        var fName = $('#firstName').val();
        var lName = $('#lastName').val();
        var email = $('#email').val();
        var password = $('#password').val();
        var address = $('#address').val();
        var roleId = $('#RoleId').val();
        var phone = $('#PhoneNumber').val();
        var userName = $('#UserName').val();

        if (fName == '') {
            toastr.error(`FirstName is required!`);
        } else if (lName == '') {
            toastr.error(`LastName is required!`);
        } else if (userName == '') {
            toastr.error(`userName is required!`);
        } else if (password == '') {
            if (id == '') {
                toastr.error(`Password is required!`);
            }
        } else if (email == '') {
            toastr.error(`Email is required!`);
        } else if (address == '') {
            toastr.error(`Address is required!`);
        } else if (phone == '') {
            toastr.error(`Phone is required!`);
        } else if (roleId == '') {
            if (id == '') {
                toastr.error(`Please select Role!`);
            }
        }
        else {
            var formData = {
                FirstName: fName,
                UserName: userName,
                LastName: lName,
                ApplicationRoleId: roleId,
                Address: address,
                Email: email,
                Password: password,
                PhoneNumber: phone,
                Id: id
            };

            $.ajax({
                url: "/Account/SubAdmin",
                type: "post",
                data: formData,
                success: function (d) {
                    if (d.message != '') {

                        if (d.resultFlag == 1) {
                            toastr.success(d.message);
                            window.location.href = "/Account/SubAdminList";
                        } else {

                            toastr.error(d.message);
                        }
                    }
                },
                error: function (error) {
                    toastr.error('Error while adding subAdmin');
                }
            });
        }
    })
});