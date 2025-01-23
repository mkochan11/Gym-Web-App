document.addEventListener('DOMContentLoaded', function () {
    document.getElementById('add-form-btn').addEventListener('click', function () {
        const formCount = document.querySelectorAll('.form-group').length + 1;

        const newFormDiv = document.createElement('div');
        newFormDiv.className = 'form-group border p-3 mb-3';

        newFormDiv.innerHTML = `
            <label for="name${formCount}">Name</label>
            <input type="text" class="form-control mb-2" id="name${formCount}" name="name${formCount}" placeholder="Enter name">

            <label for="description${formCount}">Description</label>
            <textarea class="form-control mb-2" id="description${formCount}" name="description${formCount}" placeholder="Enter description"></textarea>

            <label for="repetitions${formCount}">Repetitions Number</label>
            <input type="number" class="form-control mb-2" id="repetitions${formCount}" name="repetitions${formCount}" placeholder="Enter repetitions number">

            <label for="series${formCount}">Series Number</label>
            <input type="number" class="form-control mb-2" id="series${formCount}" name="series${formCount}" placeholder="Enter series number">

            <label for="rest${formCount}">Rest Time (hh:mm:ss)</label>
            <input type="text" class="form-control mb-2" id="rest${formCount}" name="rest${formCount}" placeholder="Enter rest time (e.g., 00:01:30)">
        `;

        document.getElementById('forms-container').appendChild(newFormDiv);
    });
});
