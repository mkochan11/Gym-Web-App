function openCreateShiftModal(day) {
    if (!day) return;
    const trainingDateInput = document.getElementById("shiftDate");

    const today = new Date();
    const year = today.getFullYear();
    const month = ("0" + (today.getMonth() + 1)).slice(-2);
    const date = ("0" + day).slice(-2);
    trainingDateInput.value = `${year}-${month}-${date}`;

    const modal = new bootstrap.Modal(document.getElementById("createShiftModal"));
    modal.show();
}

document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('[data-bs-toggle="modal"][data-bs-target="#createShiftModal"]').forEach(button => {
        button.addEventListener('click', function () {
            const day = this.getAttribute('data-day');
            const formattedDate = new Date(day).toLocaleDateString('pl-PL', {
                day: '2-digit',
                month: '2-digit',
                year: 'numeric'
            });
            document.getElementById('modalDate').textContent = formattedDate;
            document.getElementById('shiftDate').value = day;
        });
    });
});