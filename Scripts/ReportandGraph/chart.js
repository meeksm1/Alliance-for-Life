
google.load("visualization", "1", { packages: ["corechart", "table"] });
google.setOnLoadCallback(drawCharts);

 //fetching the graph data on load
$(document).ready(function () {
    $.ajax({
            url: "/BudgetCosts/GraphDataIndex",
            data: "{}",
            dataType: "json",
            type: "POST",
            contentType: "application/json; chartset=utf-8",
            success: function (responses) {
                drawCharts(responses);
            },
   
            // ajax error display
            error: function () {
                alert("Error loading data! Please try again.");
            }
        });
    });

//fetch data on button change
$(document).ready(function () {
    $('#submit').click(function (e) {
        var yearsearch = $("#YearSearch").val();
        var regionsearch = $("#RegionSearch").val();
        $.ajax({
            url: "/BudgetCosts/GraphDataIndexTest?YearSearch=" + yearsearch + regionsearch,
            data: "{}",
            dataType: "json",
            type: "POST",
            contentType: "application/json; chartset=utf-8",
            success: function (data) {
                drawCharts(data);
            },
            // ajax error display
            error: function () {
                alert("Error loading data! Please try again.");
            }
        });
    });

});
//return month based on the month index value
function monthreturn(value) {
    var month;
    switch (value) {
        case 1: month = "Jan";
            break;
        case 2: month = "Feb";
            break;
        case 3: month = "Mar";
            break;
        case 4: month = "Apr";
            break;
        case 5: month = "May";
            break;
        case 6: month = "Jun";
            break;
        case 7: month = "Jul";
            break;
        case 8: month = "Aug";
            break;
        case 9: month = "Sept";
            break;
        case 10: month = "Oct";
            break;
        case 11: month = "Nov";
            break;
        case 12: month = "Dec";
            break;
    };
    return month;
 }


//draw the chart
function drawCharts(responses) {

    // barchart
    barChart(responses);

    //otherchart
    lineChart(responses);

    //pie chart
    piechart(responses);

    //table view
    drawTable(responses);
}


//barchart
function barChart(responses) {

    //budget summary table
    var budgetsummary = [
        [{ label: 'Month', type: 'string' },
        { label: 'Admin', type: 'number' },
        { label: 'Participation', type: 'number' },
        { label: 'Total', type: 'number' }]
    ];
    //looping through the data 
    for (var i = 0; i < responses.length; i++) {

        //calculating total admin cost
        var admintotal = responses[i].ASalandWages + responses[i].AEmpBenefits + responses[i].AEmpTravel
            + responses[i].AEmpTraining + responses[i].AOfficeRent + responses[i].AOfficeUtilities
            + responses[i].AFacilityIns + responses[i].AOfficeSupplies + responses[i].AEquipment
            + responses[i].AOfficeCommunications + responses[i].AOfficeMaint + responses[i].AConsulting
            + responses[i].SubConPayCost + responses[i].BackgrounCheck + responses[i].Other
            + responses[i].AJanitorServices + responses[i].ADepreciation + responses[i].ATechSupport
            + responses[i].ASecurityServices + responses[i].ATotCosts + responses[i].AdminFee;

        //calculating total participation cost
        var partitotal = responses[i].Trasportation + responses[i].JobTraining + responses[i].TuitionAssistance
            + responses[i].ContractedResidential + responses[i].UtilityAssistance + responses[i].EmergencyShelter
            + responses[i].HousingAssistance + responses[i].Childcare + responses[i].Clothing
            + responses[i].Food + responses[i].Supplies + responses[i].RFO;

      
        //pushing data to the table
        var month = monthreturn(responses[i].Month);

        //adding to the table
        budgetsummary.push([month, admintotal, partitotal, admintotal + partitotal]);
    }

    var barData = new google.visualization.arrayToDataTable(budgetsummary);


    //bar chart options 
    // set bar chart options
    var barOptions = {
        focusTarget: 'category',
        backgroundColor: 'transparent',
        colors: ['forestgreen', 'tomato', 'cornflowerblue'],
        fontName: 'Open Sans',
        chartArea: {
            left: 50,
            top: 10,
            width: '100%',
            height: '70%'
        },
        bar: {
            groupWidth: '80%'
        },
        hAxis: {
            textStyle: {
                fontSize: 11
            }
        },
        vAxis: {
            minValue: 0,
            maxValue: 500,
            baselineColor: '#DDD',
            gridlines: {
                color: '#DDD',
                count: 4
            },
            textStyle: {
                fontSize: 11
            }
        },
        legend: {
            position: 'bottom',
            textStyle: {
                fontSize: 12
            }
        },
        animation: {
            duration: 1200,
            easing: 'out',
            startup: true
        }
    };

    // draw bar chart twice so it animates
    var barChart = new google.visualization.ColumnChart(document.getElementById('bar-chart'));
    //barChart.draw(barZeroData, barOptions);
    barChart.draw(barData, barOptions);
}


//linechart
function lineChart(responses) {

    var randomLineData = [
        [{ label: 'Month', type: 'string' },
        { label: 'Admin Cost', type: 'number' },
        { label: 'Participation Cost', type: 'number' }]
    ];

    for (var i = 0; i < responses.length; i++) {

        //calculating total admin cost
        var admintotal = responses[i].ASalandWages + responses[i].AEmpBenefits + responses[i].AEmpTravel
            + responses[i].AEmpTraining + responses[i].AOfficeRent + responses[i].AOfficeUtilities
            + responses[i].AFacilityIns + responses[i].AOfficeSupplies + responses[i].AEquipment
            + responses[i].AOfficeCommunications + responses[i].AOfficeMaint + responses[i].AConsulting
            + responses[i].SubConPayCost + responses[i].BackgrounCheck + responses[i].Other
            + responses[i].AJanitorServices + responses[i].ADepreciation + responses[i].ATechSupport
            + responses[i].ASecurityServices + responses[i].ATotCosts + responses[i].AdminFee;

        //calculating total participation cost
        var partitotal = responses[i].Trasportation + responses[i].JobTraining + responses[i].TuitionAssistance
            + responses[i].ContractedResidential + responses[i].UtilityAssistance + responses[i].EmergencyShelter
            + responses[i].HousingAssistance + responses[i].Childcare + responses[i].Clothing
            + responses[i].Food + responses[i].Supplies + responses[i].RFO;


        //pushing data to the table
        var month = monthreturn(responses[i].Month);

        //adding to the table
        randomLineData.push([month, admintotal, partitotal]);
    }

    var lineData = new google.visualization.arrayToDataTable(randomLineData);

    var lineOptions = {
        backgroundColor: 'transparent',
        colors: ['cornflowerblue', 'tomato'],
        fontName: 'Open Sans',
        focusTarget: 'category',
        chartArea: {
            left: 50,
            top: 10,
            width: '100%',
            height: '70%'
        },
        hAxis: {
            //showTextEvery: 12,
            textStyle: {
                fontSize: 11
            },
            baselineColor: 'transparent',
            gridlines: {
                color: 'transparent'
            }
        },
        vAxis: {
            minValue: 0,
            maxValue: 50000,
            baselineColor: '#DDD',
            gridlines: {
                color: '#DDD',
                count: 4
            },
            textStyle: {
                fontSize: 11
            }
        },
        legend: {
            position: 'bottom',
            textStyle: {
                fontSize: 12
            }
        },
        animation: {
            duration: 1200,
            easing: 'out',
            startup: true
        }
    };

    var lineChart = new google.visualization.LineChart(document.getElementById('line-chart'));
    //lineChart.draw(zeroLineData, lineOptions);
    lineChart.draw(lineData, lineOptions);
}

//piechart
function piechart(responses) {


    var PieChartData = [
        [{ label: 'Cost Type', type: 'string' },
        { label: 'Cost', type: 'number' }]
    ];


    //declare variables for the pie chart
    var admincost = 0;
    var particost = 0;
    var i = 0;
    while (i < responses.length) {

        admincost += admincost;
        particost += particost;

        //adding all the admin costs
        admincost = responses[i].ASalandWages + responses[i].AEmpBenefits + responses[i].AEmpTravel
            + responses[i].AEmpTraining + responses[i].AOfficeRent + responses[i].AOfficeUtilities
            + responses[i].AFacilityIns + responses[i].AOfficeSupplies + responses[i].AEquipment
            + responses[i].AOfficeCommunications + responses[i].AOfficeMaint + responses[i].AConsulting
            + responses[i].SubConPayCost + responses[i].BackgrounCheck + responses[i].Other
            + responses[i].AJanitorServices + responses[i].ADepreciation + responses[i].ATechSupport
            + responses[i].ASecurityServices + responses[i].ATotCosts + responses[i].AdminFee;

        //adding the participation costs
        particost = responses[i].Trasportation + responses[i].JobTraining + responses[i].TuitionAssistance
            + responses[i].ContractedResidential + responses[i].UtilityAssistance + responses[i].EmergencyShelter
            + responses[i].HousingAssistance + responses[i].Childcare + responses[i].Clothing
            + responses[i].Food + responses[i].Supplies + responses[i].RFO;

        //adding the costs saperately
        i++;
    }
    //push admin cost
    //push parti cost
    PieChartData.push(['Admin Cost', admincost]);
    PieChartData.push(['Participation Cost', particost]);
    var pieData = google.visualization.arrayToDataTable(PieChartData);
    var pieOptions = {
        backgroundColor: 'transparent',
        pieHole: 0.4,
        colors: ["cornflowerblue",
            "olivedrab",
            "orange",
            "tomato",
            "crimson",
            "purple",
            "turquoise",
            "forestgreen",
            "navy",
            "gray"],
        pieSliceText: 'value',
        tooltip: {
            text: 'percentage'
        },
        fontName: 'Open Sans',
        chartArea: {
            width: '100%',
            height: '94%'
        },
        legend: {
            textStyle: {
                fontSize: 13
            }
        }
    };

    var pieChart = new google.visualization.PieChart(document.getElementById('pie-chart'));
    pieChart.draw(pieData, pieOptions);
}

//table
function drawTable(responses) {
    var data = new google.visualization.DataTable();
    data.addColumn('string', 'Name');
    data.addColumn('number', 'Admin Cost');
    data.addColumn('number', 'Participation Cost');
    data.addColumn('number', 'State Fee');
    data.addColumn('number', 'Total');

    var firstquaterlyparticost = 0;
    var firstquaterlyadmincost = 0;
    var secondquaterlyparticost = 0;
    var secondquaterlyadmincost = 0;
    var thirdquaterlyparticost = 0;
    var thirdquaterlyadmincost = 0;
    var fourthquaterlyparticost = 0;
    var fourthquaterlyadmincost = 0;

    var i = 0;

    while (i < responses.length) {

        //calculate the first quater price

        if (responses[i].Month <= 3) {
            //increment the cost
            firstquaterlyadmincost += firstquaterlyadmincost;
            firstquaterlyparticost += firstquaterlyparticost;

            firstquaterlyadmincost = responses[i].ASalandWages + responses[i].AEmpBenefits + responses[i].AEmpTravel
                + responses[i].AEmpTraining + responses[i].AOfficeRent + responses[i].AOfficeUtilities
                + responses[i].AFacilityIns + responses[i].AOfficeSupplies + responses[i].AEquipment
                + responses[i].AOfficeCommunications + responses[i].AOfficeMaint + responses[i].AConsulting
                + responses[i].SubConPayCost + responses[i].BackgrounCheck + responses[i].Other
                + responses[i].AJanitorServices + responses[i].ADepreciation + responses[i].ATechSupport
                + responses[i].ASecurityServices + responses[i].ATotCosts + responses[i].AdminFee;

            ////adding the participation costs
            firstquaterlyparticost = responses[i].Trasportation + responses[i].JobTraining + responses[i].TuitionAssistance
                + responses[i].ContractedResidential + responses[i].UtilityAssistance + responses[i].EmergencyShelter
                + responses[i].HousingAssistance + responses[i].Childcare + responses[i].Clothing
                + responses[i].Food + responses[i].Supplies + responses[i].RFO;
        }
        // second quarter
        else if (responses[i].Month <= 6) {

            secondquaterlyadmincost += secondquaterlyadmincost;
            secondquaterlyparticost += secondquaterlyparticost;

            //adding them
            secondquaterlyadmincost = responses[i].ASalandWages + responses[i].AEmpBenefits + responses[i].AEmpTravel
                + responses[i].AEmpTraining + responses[i].AOfficeRent + responses[i].AOfficeUtilities
                + responses[i].AFacilityIns + responses[i].AOfficeSupplies + responses[i].AEquipment
                + responses[i].AOfficeCommunications + responses[i].AOfficeMaint + responses[i].AConsulting
                + responses[i].SubConPayCost + responses[i].BackgrounCheck + responses[i].Other
                + responses[i].AJanitorServices + responses[i].ADepreciation + responses[i].ATechSupport
                + responses[i].ASecurityServices + responses[i].ATotCosts + responses[i].AdminFee;

            ////adding the participation costs
            secondquaterlyparticost = responses[i].Trasportation + responses[i].JobTraining + responses[i].TuitionAssistance
                + responses[i].ContractedResidential + responses[i].UtilityAssistance + responses[i].EmergencyShelter
                + responses[i].HousingAssistance + responses[i].Childcare + responses[i].Clothing
                + responses[i].Food + responses[i].Supplies + responses[i].RFO;

        }
        //third quater
        else if (responses[i].Month <= 9) {
            thirdquaterlyadmincost += thirdquaterlyadmincost;
            thirdquaterlyparticost += thirdquaterlyparticost;

            thirdquaterlyadmincost = responses[i].ASalandWages + responses[i].AEmpBenefits + responses[i].AEmpTravel
                + responses[i].AEmpTraining + responses[i].AOfficeRent + responses[i].AOfficeUtilities
                + responses[i].AFacilityIns + responses[i].AOfficeSupplies + responses[i].AEquipment
                + responses[i].AOfficeCommunications + responses[i].AOfficeMaint + responses[i].AConsulting
                + responses[i].SubConPayCost + responses[i].BackgrounCheck + responses[i].Other
                + responses[i].AJanitorServices + responses[i].ADepreciation + responses[i].ATechSupport
                + responses[i].ASecurityServices + responses[i].ATotCosts + responses[i].AdminFee;

            ////adding the participation costs
            thirdquaterlyparticost = responses[i].Trasportation + responses[i].JobTraining + responses[i].TuitionAssistance
                + responses[i].ContractedResidential + responses[i].UtilityAssistance + responses[i].EmergencyShelter
                + responses[i].HousingAssistance + responses[i].Childcare + responses[i].Clothing
                + responses[i].Food + responses[i].Supplies + responses[i].RFO;

        }
        //fourthquater
        else if (responses[i].Month >= 10) {
            fourthquaterlyadmincost += fourthquaterlyadmincost;
            fourthquaterlyparticost += fourthquaterlyparticost;

            fourthquaterlyadmincost = responses[i].ASalandWages + responses[i].AEmpBenefits + responses[i].AEmpTravel
                + responses[i].AEmpTraining + responses[i].AOfficeRent + responses[i].AOfficeUtilities
                + responses[i].AFacilityIns + responses[i].AOfficeSupplies + responses[i].AEquipment
                + responses[i].AOfficeCommunications + responses[i].AOfficeMaint + responses[i].AConsulting
                + responses[i].SubConPayCost + responses[i].BackgrounCheck + responses[i].Other
                + responses[i].AJanitorServices + responses[i].ADepreciation + responses[i].ATechSupport
                + responses[i].ASecurityServices + responses[i].ATotCosts + responses[i].AdminFee;

            ////adding the participation costs
            fourthquaterlyparticost = responses[i].Trasportation + responses[i].JobTraining + responses[i].TuitionAssistance
                + responses[i].ContractedResidential + responses[i].UtilityAssistance + responses[i].EmergencyShelter
                + responses[i].HousingAssistance + responses[i].Childcare + responses[i].Clothing
                + responses[i].Food + responses[i].Supplies + responses[i].RFO;
        }
        else {}
        i++;
    }

    var totalyearcost = (fourthquaterlyadmincost + fourthquaterlyparticost
        + firstquaterlyparticost + firstquaterlyadmincost
        + secondquaterlyparticost + secondquaterlyadmincost
        + thirdquaterlyparticost + thirdquaterlyadmincost);

    data.addRows([
        ['First Quater', { v: firstquaterlyadmincost, f: '$' + firstquaterlyadmincost.toFixed(2) }, { v: firstquaterlyparticost, f: '$' + firstquaterlyparticost.toFixed(2) }, { v: (firstquaterlyadmincost + firstquaterlyparticost) * .1, f: '$' + ((firstquaterlyadmincost + firstquaterlyparticost) * .1).toFixed(2) }, { v: firstquaterlyadmincost + firstquaterlyparticost, f: '$' + ((firstquaterlyadmincost + firstquaterlyparticost) + (firstquaterlyadmincost + firstquaterlyparticost) * .1).toFixed(2) }],
        ['Second Quater', { v: secondquaterlyadmincost, f: '$' + secondquaterlyadmincost.toFixed(2) }, { v: secondquaterlyparticost, f: '$' + secondquaterlyparticost.toFixed(2) }, { v: (secondquaterlyadmincost + secondquaterlyparticost) * .1, f: '$' + ((secondquaterlyadmincost + secondquaterlyparticost) * .1).toFixed(2) }, { v: secondquaterlyadmincost + secondquaterlyparticost, f: '$' + ((secondquaterlyadmincost + secondquaterlyparticost) + (secondquaterlyadmincost + secondquaterlyparticost) * .1).toFixed(2) }],
        ['Third Quater', { v: thirdquaterlyadmincost, f: '$' + thirdquaterlyadmincost.toFixed(2) }, { v: thirdquaterlyparticost, f: '$' + thirdquaterlyparticost.toFixed(2) }, { v: (thirdquaterlyadmincost + thirdquaterlyparticost) * .1, f: '$' + ((thirdquaterlyadmincost + thirdquaterlyparticost) * .1).toFixed(2) }, { v: thirdquaterlyadmincost + thirdquaterlyparticost, f: '$' + ((thirdquaterlyadmincost + thirdquaterlyparticost) + (thirdquaterlyadmincost + thirdquaterlyparticost) * .1).toFixed(2) }],
        ['Fourth Quater', { v: fourthquaterlyadmincost, f: '$' + fourthquaterlyadmincost.toFixed(2) }, { v: fourthquaterlyparticost, f: '$' + fourthquaterlyparticost.toFixed(2) }, { v: (fourthquaterlyadmincost + fourthquaterlyparticost) * .1, f: '$' + ((fourthquaterlyadmincost + fourthquaterlyparticost) * .1).toFixed(2) }, { v: fourthquaterlyadmincost + fourthquaterlyparticost, f: '$' + ((fourthquaterlyadmincost + fourthquaterlyparticost) + (fourthquaterlyadmincost + fourthquaterlyparticost) * .1).toFixed(2) }],
        ['Total', , , , { v: totalyearcost+totalyearcost * .1, f: '$' + (totalyearcost + totalyearcost*.1).toFixed(2) }]

    ]);

    //calculating total cost
   
 

    var table = new google.visualization.Table(document.getElementById('table_div'));

    table.draw(data, { showRowNumber: true, width: '100%', height: '100%' });
}