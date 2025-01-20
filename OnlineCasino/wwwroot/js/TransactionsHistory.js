
$(document).ready(function () {
    $('#TransactionsTable').DataTable({
        ajax: {
            url: '/TransactionsForCurrentUser',
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
                data: 'amount',
                render: function (data, type, row) {
                    return `<span class="text-dark" style="font-weight: 600; text-transform: capitalize;">${data}</span>`;
                }
            },
            {
                data: 'status',
                render: function (data, type, row) {
                    let statusClass = '';

                    if (data.toLowerCase() === 'success') {
                        statusClass = 'text-success';
                    } else if (data.toLowerCase() === 'rejected') {
                        statusClass = 'text-danger';
                    }

                    return `<span class="${statusClass}" style="font-weight: 700; text-transform: capitalize;">${data}</span>`;
                }
            },
            {
                data: 'createdAt',
                render: function (data, type, row) {
                    const date = new Date(data);

                    const day = String(date.getDate()).padStart(2, '0');
                    const month = String(date.getMonth() + 1).padStart(2, '0');
                    const year = date.getFullYear();

                    return `<span class="text-primary" style="font-weight: 600; text-transform: capitalize;">${day}-${month}-${year}</span>`;
                }
            },
        ],
        pageLength: 10,
        ordering: true,
        searching: true
    });
});