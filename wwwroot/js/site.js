// JavaScript for the IAU Best Researcher Award application form

// Wait for the DOM to be fully loaded
document.addEventListener('DOMContentLoaded', function () {
    // Client-side validation
    setupFormValidation();
    
    // Setup dynamic form sections
    setupDynamicFormSections();
    
    // Initialize tooltips if Bootstrap is available
    if (typeof bootstrap !== 'undefined' && bootstrap.Tooltip) {
        var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
        tooltipTriggerList.map(function (tooltipTriggerEl) {
            return new bootstrap.Tooltip(tooltipTriggerEl);
        });
    }
});

// Setup form validation
function setupFormValidation() {
    'use strict';

    // Fetch all forms that need validation
    var forms = document.querySelectorAll('.needs-validation');

    // Loop over them and prevent submission
    Array.prototype.slice.call(forms).forEach(function (form) {
        form.addEventListener('submit', function (event) {
            if (!form.checkValidity()) {
                event.preventDefault();
                event.stopPropagation();
                
                // Find the first invalid element and scroll to it
                const firstInvalid = form.querySelector(':invalid');
                if (firstInvalid) {
                    firstInvalid.scrollIntoView({ behavior: 'smooth', block: 'center' });
                    firstInvalid.focus();
                }
            }

            form.classList.add('was-validated');
        }, false);
    });
}

// Setup dynamic form sections
function setupDynamicFormSections() {
    // Setup all dynamic sections
    setupDynamicSection('addScientificPublication', 'scientificPublicationsContainer', 'ScientificPublications');
    setupDynamicSection('addNatureSciencePublication', 'natureSciencePublicationsContainer', 'NatureSciencePublications');
    setupDynamicSection('addBook', 'booksContainer', 'Books');
    setupDynamicSection('addResearchAbstract', 'researchAbstractsContainer', 'ResearchAbstracts');
    setupDynamicSection('addExcellenceAward', 'excellenceAwardsContainer', 'ExcellenceAwards');
    setupDynamicSection('addResearchGrant', 'researchGrantsContainer', 'ResearchGrants');
    setupDynamicSection('addPatent', 'patentsContainer', 'Patents');
    setupDynamicSection('addResearchPrize', 'researchPrizesContainer', 'ResearchPrizes');
    setupDynamicSection('addStudentSupervision', 'studentSupervisionsContainer', 'StudentSupervisions');
    setupDynamicSection('addOtherExcellence', 'otherExcellencesContainer', 'OtherScientificExcellences');
}

// Helper function to setup a dynamic section
function setupDynamicSection(buttonId, containerId, fieldName) {
    const button = document.getElementById(buttonId);
    if (!button) return; // Skip if button doesn't exist
    
    button.addEventListener('click', function() {
        const container = document.getElementById(containerId);
        if (!container) return; // Skip if container doesn't exist
        
        const entryCount = container.children.length;
        
        if (entryCount < 10) {
            // Clone the first entry
            const template = container.children[0].cloneNode(true);
            
            // Update the heading if it exists
            const heading = template.querySelector('h5');
            if (heading) {
                heading.textContent = heading.textContent.replace(/\d+/, entryCount + 1);
            }
            
            // Clear input values
            template.querySelectorAll('input').forEach(input => {
                input.value = '';
            });
            
            // Reset select elements
            template.querySelectorAll('select').forEach(select => {
                select.selectedIndex = 0;
            });
            
            // Clear textarea elements
            template.querySelectorAll('textarea').forEach(textarea => {
                textarea.value = '';
            });
            
            // Update the indices in the name and id attributes
            updateElementIndices(template, fieldName, entryCount);
            
            // Add the new entry to the container
            container.appendChild(template);
            
            // Remove validation classes from the new entry
            template.querySelectorAll('.is-invalid, .is-valid').forEach(el => {
                el.classList.remove('is-invalid', 'is-valid');
            });
            
            // Focus on the first input in the new entry
            const firstInput = template.querySelector('input, select, textarea');
            if (firstInput) {
                firstInput.focus();
            }
        } else {
            alert('You can add a maximum of 10 entries.');
        }
    });
}

// Helper function to update indices in element attributes
function updateElementIndices(element, fieldName, newIndex) {
    // Update input, select, and textarea elements
    element.querySelectorAll('input, select, textarea').forEach(input => {
        if (input.name) {
            input.name = input.name.replace(/\[\d+\]/, `[${newIndex}]`);
        }
        if (input.id) {
            input.id = input.id.replace(/\_\d+\_/, `_${newIndex}_`);
        }
    });
    
    // Update label elements
    element.querySelectorAll('label').forEach(label => {
        if (label.htmlFor) {
            label.htmlFor = label.htmlFor.replace(/\_\d+\_/, `_${newIndex}_`);
        }
    });
    
    // Update validation spans
    element.querySelectorAll('span[data-valmsg-for]').forEach(span => {
        if (span.dataset.valmsgFor) {
            span.dataset.valmsgFor = span.dataset.valmsgFor.replace(/\[\d+\]/, `[${newIndex}]`);
        }
    });
}
