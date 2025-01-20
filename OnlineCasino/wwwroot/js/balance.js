const currencySymbols = {
    "USD": "$",
    "EUR": "€",
    "GEL": "₾"
};

function fetchBalance() {
    $.ajax({
        url: '/Balance',
        method: 'GET',
        dataType: 'json',
        success: function (data) {
            const currencySymbol = currencySymbols[data.currencyName] || data.currencyName;
            $('#Balance').text(`Balance: ${data.currentBallance}${currencySymbol}`);
        }
    });
}

$(document).ready(function () {
    fetchBalance();
    setInterval(fetchBalance, 30000);
});