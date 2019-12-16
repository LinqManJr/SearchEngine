$(document).ready(function () {

    $("#btnResult").click(function () {
        let value = $("#inputWord").val();
        console.log(value);
        $("#partial").load('/Search/Results', {word:value});
    });

    $(function () {
        $.ajaxSetup({ cache: false });
        $(".linkDetails").click(function (e) {
            e.preventDefault();
            $.get(this.href, function (data) {
                $('#dialogContent').html(data);
                $('#modDialog').modal('show');
            });
        });
    })

});
