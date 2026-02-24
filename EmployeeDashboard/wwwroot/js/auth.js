// Authentication Page JavaScript

$(document).ready(function () {
    // Auto-focus first input
    $('.auth-form input[type="text"], .auth-form input[type="email"]').first().focus();

    // Form validation enhancement
    $('.auth-form').on('submit', function () {
        const submitBtn = $(this).find('button[type="submit"]');
        submitBtn.prop('disabled', true);
        submitBtn.html('<i class="fas fa-spinner fa-spin"></i> Processing...');
    });

    // Real-time validation feedback
    $('.form-control').on('blur', function () {
        if ($(this).val().trim() === '') {
            $(this).addClass('is-invalid');
        } else {
            $(this).removeClass('is-invalid');
        }
    });

    // Password strength indicator (optional enhancement)
    $('#passwordInput').on('input', function () {
        const password = $(this).val();
        // Add password strength logic if needed
    });
});
