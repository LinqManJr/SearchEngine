$(document).ready(function () {

    $("#btnResult").click(function () {
        let value = $("#inputWord").val();
        console.log(value);
        $("#partial").load('/Search/Results', {word:value});
    });

    $("#linkDetails").click(function () {   
        alert("fg");
        $("#dialogData").load('SearchDb/Details/1');
        $("#modal").modal('show');
    });

});
