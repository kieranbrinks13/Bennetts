// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

(function () {
    'use strict'

    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    var forms = document.querySelectorAll('.needs-validation')

    // Loop over them and prevent submission
    Array.prototype.slice.call(forms)
        .forEach(function (form) {
            form.addEventListener('submit', function (event) {
                if (!form.checkValidity()) {
                    event.preventDefault()
                    event.stopPropagation()
                }

                form.classList.add('was-validated')
            }, false)
        })
})()

$(document).ready(function () {
    var table = $('.dt-table').DataTable();

    $('.remove-user').on('click', function () {
        if (confirm('Are you sure you want to delete this user?')) {
            var element = $(this);
            var userId = $(this).data('userid');
            $.ajax({
                url: "/Home/DeleteUser",
                data: { id: userId },
                method: "POST",
                success: function (data) {
                    table.row(element.parents('tr')).remove().draw();
                }
            });
        }
    });
});
