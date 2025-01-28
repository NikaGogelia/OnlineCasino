const Toast = Swal.mixin({
    toast: true,
    position: "bottom-end",
    showConfirmButton: false,
    timer: 3000,
    timerProgressBar: true,
    didOpen: (toast) => {
        toast.onmouseenter = Swal.stopTimer;
        toast.onmouseleave = Swal.resumeTimer;
    }
});

$(document).ready(function () {
    $('#generate-public-token').on('click', function () {
        $.ajax({
            url: '/CreatePublicToken',
            method: 'POST',
            success: function (response) {
                if (response.status === 1) {
                    Toast.fire({
                        icon: "success",
                        title: response.message
                    });
                }

                if (response.status === 0) {
                    Toast.fire({
                        icon: "error",
                        title: response.message
                    });
                }
            },
            error: function () {
                Toast.fire({
                    icon: "error",
                    title: "Token Generation Failed!"
                });
            }
        });
    });
});