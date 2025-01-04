document.addEventListener('DOMContentLoaded', function () {
    document.getElementById('fromDate').addEventListener('change', function () {
        var fromDate = this.value;
        var toDateInput = document.getElementById('toDate');

        toDateInput.setAttribute('min', fromDate);
    });
});