document.addEventListener('DOMContentLoaded', function () {
    var id = $('#designationId').val();

    if (id) {
        fetchJobDetails(id);
    }

    function fetchJobDetails(id) {
        fetch(`/your-endpoint/GetDetailsById?id=${id}`)
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    populateForm(data.data);
                } else {
                    alert('Error: ' + data.message);
                }
            })
            .catch(error => {
                console.error('Error:', error);
            });
    }

    function populateForm(data) {
        document.getElementById('description').value = data.description;

        populateSection('responsibilities', data.responsibilities);
        populateSection('qualifications', data.qualifications);
        populateSection('benefits', data.benefits);
        populateSection('other-information', data.otherInformation);
    }

    function populateSection(sectionId, items) {
        const section = document.getElementById(sectionId);
        items.forEach(item => {
            addInputField(section, item);
        });
    }

    function addInputField(section, value) {
        const inputGroup = document.createElement('div');
        inputGroup.className = 'input-group mb-2';

        const input = document.createElement('input');
        input.type = 'text';
        input.className = 'form-control border-2';
        input.value = value;

        inputGroup.appendChild(input);
        section.appendChild(inputGroup);
    }

    document.querySelector('.add-responsibility').addEventListener('click', function () {
        addInputField(document.getElementById('responsibilities'), '');
    });

    document.querySelector('.add-qualification').addEventListener('click', function () {
        addInputField(document.getElementById('qualifications'), '');
    });

    document.querySelector('.add-benefit').addEventListener('click', function () {
        addInputField(document.getElementById('benefits'), '');
    });

    document.querySelector('.add-other-information').addEventListener('click', function () {
        addInputField(document.getElementById('other-information'), '');
    });
});

document.querySelector('form').addEventListener('submit', function (e) {
    e.preventDefault();

    const formData = {
        description: document.getElementById('description').value,
        responsibilities: collectSectionData('responsibilities'),
        qualifications: collectSectionData('qualifications'),
        benefits: collectSectionData('benefits'),
        otherInformation: collectSectionData('other-information')
    };

    // Send formData to the server via AJAX
    fetch('/your-endpoint', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(formData)
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                alert('Job description saved successfully!');
            } else {
                alert('Error saving job description: ' + data.message);
            }
        })
        .catch(error => {
            console.error('Error:', error);
        });
});

function collectSectionData(sectionId) {
    const section = document.getElementById(sectionId);
    return Array.from(section.getElementsByTagName('input')).map(input => input.value);
}
