$(document).ready(function () {
    $("#addMealPlan").click(function () {
        $("#newPlanName").removeClass("hidden");
        $("#submitNewPlan").removeClass("hidden");
        $("#nevermind").removeClass("hidden");
        $("#addMealPlan").addClass("hidden");
    });
    $("#nevermind").click(function () {
        $("#newPlanName").addClass("hidden");
        $("#submitNewPlan").addClass("hidden");
        $("#addMealPlan").removeClass("hidden");
        $("#nevermind").addClass("hidden");
    });
});