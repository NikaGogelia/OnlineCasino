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
    $('#transactionFormWithdraw').submit(function (e) {
        e.preventDefault();

        var amount = $('#amount').val();

        var requestData = {
            Amount: parseFloat(amount)
        };

        $.ajax({
            url: '/RegisterWithdraw',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(requestData),
            success: function (response) {
                if (response.status === 1) {
                    Toast.fire({
                        icon: "success",
                        title: response.message
                    });
                } else {
                    Toast.fire({
                        icon: "error",
                        title: response.message
                    });
                }
            },
            error: function () {
                Toast.fire({
                    icon: "error",
                    title: "Withdraw Request Failed!"
                });
            }
        });
    });
});