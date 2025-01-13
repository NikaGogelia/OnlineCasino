const currencySymbols = {
    "USD": "$",
    "EUR": "€",
    "GEL": "₾"
};

function fetchBalance() {
    $.ajax({
        url: '/balance',
        method: 'GET',
        dataType: 'json',
        success: function (data) {
            const currencySymbol = currencySymbols[data.currencyName] || data.currencyName;
            $('#Balance').text(`Balance: ${data.currentBallance}${currencySymbol}`);
        },
        error: function (_, _, error) {
            console.error('Error fetching balance:', error);
        }
    });
}

$(document).ready(function () {
    fetchBalance();
    setInterval(fetchBalance, 30000);
});