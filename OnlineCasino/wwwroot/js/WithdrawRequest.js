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
                Toast.fire({
                    icon: "success",
                    title: "Withdraw Request Was Successful!"
                });
                console.log(response);
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