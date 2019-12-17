$(document).ready(function () {

    //results ajax
    $("#btnResult").click(function () {
        let value = $("#inputWord").val();        
        $("#partial").load('/Search/Results', {word:value});
    });

    //filter ajax
    $("#btnDbResult").click(function () {
        let value = $("#dropDownWord :selected").text();        
        $("#tableResult").load('/SearchDb/FilterByWord', { word: value });
    });

    //details ajax
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
