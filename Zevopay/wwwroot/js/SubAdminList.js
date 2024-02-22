$(document).ready(function () {
    fillData();

    $(document).on('click', '.updateStatus', function (e) {
        var status = false;
        var active = $(this).val();
        if (active == 'Active') {
            status = true;
        }
        var id = $(this).attr('data-SubadminId');

        var formData = {
            Status: status,
            Id: id
        };

        $.ajax({
            url: "/Account/UpdateSubAdminStatus",
            type: "get",
            data: formData,
            success: function (d) {
                fillData();
            },
            error: function (error) {
                //toastr.error('Failed to get subadminList!');
            }
        });

    });
});
   
function fillData() {
    $.ajax({
        url: "/Account/SubAdminListPrtial",
        type: "get",
        success: function (d) {
            $('#SubAdmins-Div').html('');
            $('#SubAdmins-Div').html(d);
            //toastr.info(d.message);
        },
        error: function (error) {
            //toastr.error('Failed to get subadminList!');
        }
    });
}