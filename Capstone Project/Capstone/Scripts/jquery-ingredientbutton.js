
$(document).ready(function () {
    $("input:text:visible:first").focus();
    //pull in number of ingredients already added
    var testNumber = $("#test").attr("value");
    //when the Add Field button is clicked
    $("#add").click(function (e) {
        //Append a new row of code to the "#newItems" div
        testNumber++;
        $("#newItems").append('<div class="extra-ingred"><div id="items" class="col-sm-5 added-ingred"><input class="form-control" id="Ingredients_' + testNumber + '_" name="Ingredients[' + testNumber + ']" placeholder="Ingredient" type="text" value="" /></div><div id="items" class="col-sm-5 added-ingred"><input class="form-control" id="Amount_' + testNumber + '_" name="Amount[' + testNumber + ']" placeholder="Amount" type="text" value="" /></div ><input type="button" id="button" class="delete btn btn-default col-sm-1 ingred-delete" value="Delete"></div>');
    });

    $(document).on("click", ".delete", function hideIngredient(e) {
        $(this).parent().hide();
        $(this).parent().children().children().addClass("toEdit");
        $(".toEdit").attr("value", "**delete**");
        return false;
    });

});

