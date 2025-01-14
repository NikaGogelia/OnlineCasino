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
    $('#transactionForm').submit(function (e) {
        e.preventDefault();

        var amount = $('#amount').val();
        var transactionType = $(this).data('transaction-type');

        var requestData = {
            TransactionType: transactionType,
            Amount: parseFloat(amount)
        };

        $.ajax({
            url: '/RegisterTransaction',
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(requestData),
            success: function (response) {
                Toast.fire({
                    icon: "success",
                    title: "Trasnaction Was Successful!"
                });
                setTimeout(() => window.location.href = '/Home/Index', 3000);
            },
            error: function (error) {
                Toast.fire({
                    icon: "error",
                    title: "Trasnaction Was Failed!"
                });
            }
        });
    });
});