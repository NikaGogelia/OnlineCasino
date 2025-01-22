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

const BankApiUrl = "https://localhost:7213";

$(document).ready(function () {
    const path = window.location.pathname;

    const id = path.split("/")[3];
    const status = path.split("/")[4];
    const amount = $("#amount").val();

    switch (status) {
        case "Success":
            $('#transactionDepositSuccessForm').submit(function (e) {
                e.preventDefault();

                var requestData = {
                    transactionId: id,
                    amount: parseFloat(amount),
                    status: status.toLowerCase()
                };

                $.ajax({
                    url: `${BankApiUrl}/api/Deposit/CompleteDepositSendToCallback`,
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
                        setTimeout(() => window.location.href = "/", 3000);
                    }
                });
            });
            break;
        case "Rejected":
            $('#transactionRejectDepositBtn').on("click", () => {
                var requestData = {
                    transactionId: id,
                    amount: parseFloat(amount),
                    status: status.toLowerCase()
                };

                $.ajax({
                    url: `${BankApiUrl}/api/Deposit/CompleteDepositSendToCallback`,
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
                        setTimeout(() => window.location.href = "/", 3000);
                    }
                });
            });
            break;
    }
})