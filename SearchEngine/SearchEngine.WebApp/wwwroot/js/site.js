$(document).ready(function () {

    $("#btnResult").click(function () {
        let value = $("#inputWord").val();
        console.log(value);
        $("#partial").load('/Search/Results', {word:value});
    });
    //TODO: check response and throw error popup

});
