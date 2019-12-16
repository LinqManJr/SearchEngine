$(document).ready(function () {

    $("#btnResult").click(function () {
        let value = $("#inputWord").val();        
        $("#partial").load('/Search/Results', {word:value});
    });

    $("#btnDbResult").click(function () {
        let value = $("#dropDownWord :selected").text();        
        $("#partial").load('/SearchDb/FilterByWord', { word: value });
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
