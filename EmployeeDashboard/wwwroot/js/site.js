// Employee Dashboard JavaScript

$(document).ready(function () {
    // Auto-submit form on filter change (optional enhancement)
    $('#department, #status').on('change', function () {
        // Uncomment to enable auto-submit
        // $('#filterForm').submit();
    });

    // Add smooth scrolling for pagination links
    $('.pagination a').on('click', function () {
        $('html, body').animate({
            scrollTop: $('.dashboard-container').offset().top
        }, 500);
    });

    // Add loading state to search button
    $('#filterForm').on('submit', function () {
        const submitBtn = $(this).find('button[type="submit"]');
        submitBtn.prop('disabled', true);
        submitBtn.html('<span class="loading"></span> Searching...');
    });

    // Tooltip initialization (if Bootstrap tooltips are needed)
    if (typeof bootstrap !== 'undefined') {
        var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
        var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
            return new bootstrap.Tooltip(tooltipTriggerEl);
        });
    }

    // Table row click enhancement
    $('.table tbody tr').on('click', function () {
        const viewLink = $(this).find('a[href*="Details"]');
        if (viewLink.length) {
            window.location.href = viewLink.attr('href');
        }
    });

    // Add cursor pointer to clickable rows
    $('.table tbody tr').css('cursor', 'pointer');

    // Search input debounce (optional enhancement for real-time search)
    let searchTimeout;
    $('#search').on('input', function () {
        clearTimeout(searchTimeout);
        // Uncomment to enable real-time search with debounce
        // searchTimeout = setTimeout(function() {
        //     $('#filterForm').submit();
        // }, 500);
    });
});

// Export function for statistics refresh (if needed for AJAX updates)
function refreshStatistics() {
    $.ajax({
        url: '/Employee/GetStatistics',
        method: 'GET',
        success: function (data) {
            // Update statistics cards
            $('.stat-card-primary .stat-content h3').text(data.totalEmployees);
            $('.stat-card-success .stat-content h3').text(data.activeEmployees);
            $('.stat-card-info .stat-content h3').text(data.departmentsCount);
            $('.stat-card-warning .stat-content h3').text('$' + Math.round(data.averageSalary).toLocaleString());
        },
        error: function () {
            console.error('Failed to refresh statistics');
        }
    });
}
