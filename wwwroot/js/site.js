// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Navbar dropdown toggle
document.addEventListener('DOMContentLoaded', function () {
    const dropdowns = document.querySelectorAll('.nav-dropdown .dropdown-toggle');
    dropdowns.forEach(function (button) {
        button.addEventListener('click', function (e) {
            e.preventDefault();
            const menu = this.nextElementSibling;
            const isExpanded = this.getAttribute('aria-expanded') === 'true';
            this.setAttribute('aria-expanded', String(!isExpanded));
            if (menu) {
                menu.classList.toggle('show');
            }
        });
    });

    // Close dropdown when clicking outside
    document.addEventListener('click', function (e) {
        if (!e.target.closest('.nav-dropdown')) {
            dropdowns.forEach(function (button) {
                button.setAttribute('aria-expanded', 'false');
                const menu = button.nextElementSibling;
                if (menu) {
                    menu.classList.remove('show');
                }
            });
        }
    });
});
