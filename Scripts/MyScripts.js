$(function addAdministrationTotals() {
    $('.numAdd').change(function () {

        // Loop through all input's and re-calculate the total
        var total = 0;
        $('.numAdd').each(function () {
            total += +$(this).val();
        });

        // Update the total
        $("#ATotCosts").val(total.toFixed(2));
    });
});


$(function addBudgetFormTotals() {
    $('.numAdd2').change(function () {

        // Loop through all input's and re-calculate the total
        var total = 0;
        $('.numAdd2').each(function () {
            total += +$(this).val();
        });

        // Update the total
        $("#BTotal").val(total.toFixed(2));
        var maxtot = (parseFloat($("#ATotCosts").val()) + parseFloat($("#AdminFee").val()) + parseFloat($("#BTotal").val())).toFixed(2)
        $("#Maxtot").val(maxtot)
    });
});


$(function addParticipationServicesForm() {
    $('.numAdd3').change(function () {

        // Loop through all input's and re-calculate the total
        var total = 0;
        $('.numAdd3').each(function () {
            total += +$(this).val();
        });

        // Update the total
        $("#PTotals").val(total.toFixed(2));
    });
});

$(function calcInvoiceGrandTotals() {
    $('.numAdd4').change(function () {

        // Loop through all input's and re-calculate the total
        var total = 0;
        $('.numAdd4').each(function () {
            total += +$(this).val();
        });

        // Update the total
        $("#GrandTotal").val(total.toFixed(2));
        $("#LessManagementFee").val((total * .03).toFixed(2));
        $("#DepositAmount").val(((total - (total * .03)).toFixed(2)));
    });
});

$(function calcInvoiceAllocatedFunds() {

    $('.numAdd5').change(function () {

        var q = $("#BeginningAllocation").val();
        var p = $("#AdjustedAllocation").val();
        var result = "";

        if (q !== "" && p !== "" && $.isNumeric(q) && $.isNumeric(p)) {
            result = parseFloat(q) + parseFloat(p);
        }
        jQuery("#BalanceRemaining").val(result);
    });
});