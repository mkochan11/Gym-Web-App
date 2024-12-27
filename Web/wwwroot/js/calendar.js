function openCreateTrainingModal(day) {
    if (!day) return; // If the day is null, do nothing
    const trainingDateInput = document.getElementById("trainingDate");

    // Set the selected date in the form
    const today = new Date();
    const year = today.getFullYear();
    const month = ("0" + (today.getMonth() + 1)).slice(-2); // Add leading zero to month
    const date = ("0" + day).slice(-2); // Add leading zero to date
    trainingDateInput.value = `${year}-${month}-${date}`;

    // Open the modal
    const modal = new bootstrap.Modal(document.getElementById("createTrainingModal"));
    modal.show();
}

document.addEventListener('DOMContentLoaded', function () {
    // Now we know the DOM is fully loaded and elements are available to add event listeners
    document.querySelectorAll('[data-bs-toggle="modal"][data-bs-target="#createTrainingModal"]').forEach(button => {
        button.addEventListener('click', function () {
            const isCyclicCheckbox = document.getElementById('IsCyclic');
            const repeatabilityOptions = document.getElementById('RepeatabilityOptions');

            // Get the date passed in the button's data-day attribute and set it in the form
            const day = this.getAttribute('data-day');
            const formattedDate = new Date(day).toLocaleDateString('pl-PL', {
                day: '2-digit',
                month: '2-digit',
                year: 'numeric'
            });
            document.getElementById('modalDate').textContent = formattedDate;
            document.getElementById('TrainingDate').value = day;  // Set the Date field in the form

            // Reset the IsCyclic checkbox and handle repeatability options visibility
            if (isCyclicCheckbox.checked) {
                repeatabilityOptions.style.display = 'block'; // If checked, show repeatability options
            } else {
                repeatabilityOptions.style.display = 'none';  // If not checked, hide repeatability options
            }

            // Reset any previous values in the form (if necessary)
            // Optionally clear other form fields or validation errors
        });
    });

    // Handle the toggle of repeatability options based on the IsCyclic checkbox state
    document.getElementById('IsCyclic').addEventListener('change', function () {
        const repeatabilityOptions = document.getElementById('RepeatabilityOptions');
        if (this.checked) {
            repeatabilityOptions.style.display = 'block';  // Show repeatability options
        } else {
            repeatabilityOptions.style.display = 'none';   // Hide repeatability options
        }
    });
});


