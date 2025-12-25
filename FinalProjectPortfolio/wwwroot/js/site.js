/*
    My Personal Portfolio JavaScript File
    This is where I've put all the custom JavaScript to make my site interactive.
*/

document.addEventListener('DOMContentLoaded', function () {
    // --- My Theme Toggle Logic ---
    // This script handles switching between light and dark mode and saves my preference.
    const themeToggle = document.getElementById('themeToggle');
    if (themeToggle) {
        themeToggle.addEventListener('click', () => {
            const currentTheme = document.documentElement.getAttribute('data-bs-theme');
            const newTheme = currentTheme === 'dark' ? 'light' : 'dark';
            document.documentElement.setAttribute('data-bs-theme', newTheme);
            localStorage.setItem('theme', newTheme);
        });
    }

    // --- My Contact Form Submission Logic ---
    // This script sends my contact form data to Formspree without reloading the page.
    const contactForm = document.getElementById('contactForm');
    if (contactForm) {
        contactForm.addEventListener('submit', function(e) {
            e.preventDefault(); // I'm stopping the default form submission here.

            const form = e.target;
            const data = new FormData(form);
            const action = form.action;

            fetch(action, {
                method: 'POST',
                body: data,
                headers: {
                    'Accept': 'application/json'
                }
            }).then(response => {
                if (response.ok) {
                    // If the submission is successful, I'll show a thank you message.
                    const successMessage = document.createElement('div');
                    successMessage.className = 'alert alert-success mt-3';
                    successMessage.textContent = 'Thanks for your message! I will get back to you soon.';
                    form.parentNode.insertBefore(successMessage, form.nextSibling);
                    
                    form.reset(); // Then I'll clear the form.
                    form.style.display = 'none'; // And hide it.

                } else {
                    // If there's an error, I'll show an alert.
                    response.json().then(data => {
                        if (Object.hasOwn(data, 'errors')) {
                            alert(data["errors"].map(error => error["message"]).join(", "));
                        } else {
                            alert('Oops! There was a problem submitting your form');
                        }
                    })
                }
            }).catch(error => {
                alert('Oops! There was a problem submitting your form');
            });
        });
    }
    
    // --- My Icon Picker Logic ---
    // This function makes the icon dropdown work on my admin pages.
    activateIconPicker(document);
});

function activateIconPicker(container) {
    const iconDropdown = container.querySelector('#skillIconDropdown');
    if (!iconDropdown) return;

    const iconInput = container.querySelector('#skillIconInput');
    const selectedIconName = container.querySelector('#selectedIconName');
    const selectedIconPreview = container.querySelector('#selectedIconPreview');
    const dropdownItems = container.querySelectorAll('.dropdown-item[data-icon]');

    dropdownItems.forEach(item => {
        item.addEventListener('click', function (e) {
            e.preventDefault();
            
            const iconClass = this.getAttribute('data-icon');
            const iconName = this.textContent;

            // When I click an icon, this updates the hidden input field.
            iconInput.value = iconClass;

            // This updates the button to show the icon I selected.
            selectedIconName.textContent = iconName;
            selectedIconPreview.innerHTML = `<i class="bi ${iconClass}"></i>`;
        });
    });
}
