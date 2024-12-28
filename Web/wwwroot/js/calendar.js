function openCreateTrainingModal(day) {
    if (!day) return;
    const trainingDateInput = document.getElementById("trainingDate");

    const today = new Date();
    const year = today.getFullYear();
    const month = ("0" + (today.getMonth() + 1)).slice(-2);
    const date = ("0" + day).slice(-2);
    trainingDateInput.value = `${year}-${month}-${date}`;

    const modal = new bootstrap.Modal(document.getElementById("createTrainingModal"));
    modal.show();
}

document.addEventListener('DOMContentLoaded', function () {
    // Now we know the DOM is fully loaded and elements are available to add event listeners
    document.querySelectorAll('[data-bs-toggle="modal"][data-bs-target="#createTrainingModal"]').forEach(button => {
        button.addEventListener('click', function () {
            const isCyclicCheckbox = document.getElementById('IsCyclic');
            const repeatabilityOptions = document.getElementById('RepeatabilityOptions');

            const day = this.getAttribute('data-day');
            const formattedDate = new Date(day).toLocaleDateString('pl-PL', {
                day: '2-digit',
                month: '2-digit',
                year: 'numeric'
            });
            document.getElementById('modalDate').textContent = formattedDate;
            document.getElementById('TrainingDate').value = day;

            if (isCyclicCheckbox.checked) {
                repeatabilityOptions.style.display = 'block';
            } else {
                repeatabilityOptions.style.display = 'none'; 
            }
        });
    });

    // Handle the toggle of repeatability options based on the IsCyclic checkbox state
    document.getElementById('IsCyclic').addEventListener('change', function () {
        const repeatabilityOptions = document.getElementById('RepeatabilityOptions');
        if (this.checked) {
            repeatabilityOptions.style.display = 'block'; 
        } else {
            repeatabilityOptions.style.display = 'none';
        }
    });
});


