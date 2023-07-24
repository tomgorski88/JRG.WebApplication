// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(document).ready(function () {
    var table = $('#myTable').DataTable({
        "pageLength": 15,
        dom: 'Bfrtip',
        buttons: [
            {
                text: 'Wszyscy',
                action: function (e, dt, node, conf) {
                    table.search('');
                    table.columns().search('').draw();
                }
            },
            {
                text: 'Zmiana 1',
                action: function (e, dt, node, conf) {
                    table.columns(3).search("Zmiana 1").draw();
                }
            },
            {
                text: 'Zmiana 2',
                action: function (e, dt, node, conf) {
                    table.columns(3).search("Zmiana 2").draw();
                }
            },
            {
                text: 'Zmiana 3',
                action: function (e, dt, node, conf) {
                    table.columns(3).search("Zmiana 3").draw();
                }
            }
        ]
    });

    table.buttons(1, null).container().appendTo(
        table.table().container()
    );
});



$(document).ready(function () {
    var table = $('#myTable2').DataTable({
        "pageLength": 15,
        dom: 'Bfrtip'
    })
});