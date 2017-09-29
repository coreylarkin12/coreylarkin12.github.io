$(document).ready(function () {
    $(".recipe-add").click(function (e) {
        //Append a new row of code to the ".breakfast-div" div

        var x = $(".hiddenMeal select").clone();
        $(this).parent().append(x);
    });
});