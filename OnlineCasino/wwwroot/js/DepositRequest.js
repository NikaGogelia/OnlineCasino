﻿const Toast = Swal.mixin({
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
    $('#transactionFormDeposit').submit(function (e) {
        e.preventDefault();

        var amount = $('#amount').val();

        var requestData = {
            Amount: parseFloat(amount)
        };

        $.ajax({
            url: '/RegisterDeposit',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(requestData),
            success: function (response) {
                if (response.status === -1) {
                    Toast.fire({
                        icon: "error",
                        title: response.message
                    });
                }

                if (response.status === 0) {
                    Toast.fire({
                        icon: "error",
                        title: response.message
                    });
                }

                if (response.status === 1) {
                    Toast.fire({
                        icon: "success",
                        title: "Deposit Request Was Successful!"
                    });
                    setTimeout(() => window.location.href = response.url, 3000);
                }
            },
            error: function () {
                Toast.fire({
                    icon: "error",
                    title: "Deposit Request Failed!"
                });
            }
        });
    });
});