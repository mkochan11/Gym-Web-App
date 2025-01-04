document.addEventListener('DOMContentLoaded', function () {
    const employeeSelect = document.getElementById('employeeSelect');
    const employeePositionInput = document.getElementById('employeePosition');
    const submitButton = document.querySelector('form button[type="submit"]');

    if (employeeSelect) {
        employeeSelect.addEventListener('change', function () {
            const selectedOption = this.options[this.selectedIndex];
            const position = selectedOption.getAttribute('data-position');
            employeePositionInput.value = position || '';
        });
    }

    if (submitButton) {
        submitButton.addEventListener('click', function (event) {
            if (!employeeSelect || !employeeSelect.value) {
                event.preventDefault();
                alert('Proszę wybrać pracownika przed utworzeniem raportu.');
            }
        });
    }
});
