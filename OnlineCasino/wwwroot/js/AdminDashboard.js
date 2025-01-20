
$(document).ready(function () {
    $('#DashboardTable').on('click', '#approveRequest', function () {
        var id = $(this).data('id');

        Swal.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#198754",
            cancelButtonColor: "#6e7881",
            confirmButtonText: "Yes, Approve Transaction!"
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: '/ApproveWithdrawRequest',
                    method: 'POST',
                    contentType: 'application/json',
                    data: JSON.stringify(id),
                    success: function (res) {
                        console.log(res)
                    },
                });
                Swal.fire({
                    title: "Approved!",
                    text: `Withdraw with id ${id} was Approved`,
                    icon: "success"
                });
            }
        });
    });

    $('#DashboardTable').on('click', '#rejectRequest', function () {
        var id = $(this).data('id');

        Swal.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#dc3545",
            cancelButtonColor: "#6e7881",
            confirmButtonText: "Yes, Reject Transaction!"
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: '/RejectWithdrawRequest',
                    method: 'PUT',
                    contentType: 'application/json',
                    data: JSON.stringify(id),
                    success: function () {
                        table.ajax.reload();
                    },
                });
                Swal.fire({
                    title: "Rejected!",
                    text: `Withdraw with id ${id} was rejected`,
                    icon: "success"
                });
            }
        });

    });

    const table = $('#DashboardTable').DataTable({
        ajax: {
            url: '/TransactionRequests',
            type: 'GET',
            dataSrc: ''
        },
        columns: [
            {
                data: 'id',
                render: function (data, type, row) {
                    return `<span class="text-dark" style="font-weight: 600; text-transform: capitalize;">${data}</span>`;
                }
            },
            {
                data: 'userName',
                render: function (data, type, row) {
                    return `<span class="text-secondary" style="font-weight: 600;">${data}</span>`;
                }
            },
            {
                data: 'transactionType',
                render: function (data, type, row) {
                    return `<span class="text-primary" style="font-weight: 600; text-transform: capitalize;">${data}</span>`;
                }
            },
            {
                data: 'amount',
                render: function (data, type, row) {
                    return `<span class="text-dark" style="font-weight: 600; text-transform: capitalize;">${data}</span>`;
                }
            },
            {
                data: 'status',
                render: function (data, type, row) {
                    let statusClass = '';

                    if (data === 'pending') {
                        statusClass = 'text-warning';
                    } else if (data === 'approved') {
                        statusClass = 'text-success';
                    } else if (data === 'rejected') {
                        statusClass = 'text-danger';
                    }

                    return `<span class="${statusClass}" style="font-weight: 700; text-transform: capitalize;">${data}</span>`;
                }
            },
            {
                data: null,
                render: (data, type, row) => {
                    if (row.transactionType == 'withdraw') {
                        if (row.status == 'pending') {
                            return `<button id="approveRequest" type="button" class="btn btn-success me-1" data-id="${row.id}">Approve</button>
                            <button id="rejectRequest" type="button" class="btn btn-danger" data-id="${row.id}">Reject</button>`
                        } else {
                            return ''
                        }
                    } else {
                        return ''
                    }
                },
            }
        ],
        pageLength: 10,
        ordering: true,
        searching: true
    });
});