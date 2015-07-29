$(function () {
    $("#example1").dataTable({
        "order": [[1, "asc"]],
        "bPaginate": true,
        "bLengthChange": true,
        "bFilter": true,
        "bSort": true,
        "bInfo": true,
        "bAutoWidth": true

    });
    $('#myChatTable').dataTable({
        "order": [[2, "asc"]],
        "bPaginate": true,
        "bLengthChange": true,
        "bFilter": true,
        "bSort": true,
        "bInfo": true,
        "bAutoWidth": true
    });

});


    





