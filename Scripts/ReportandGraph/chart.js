
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

    //excel output
    drawToolbar(responses)
}

//calculating admin total
function admincosts(responses) {
    var admintotal = responses.ASalandWages + responses.AEmpBenefits + responses.AEmpTravel
        + responses.AEmpTraining + responses.AOfficeRent + responses.AOfficeUtilities
        + responses.AFacilityIns + responses.AOfficeSupplies + responses.AEquipment
        + responses.AOfficeCommunications + responses.AOfficeMaint + responses.AConsulting
        + responses.SubConPayCost + responses.BackgrounCheck + responses.Other
        + responses.AJanitorServices + responses.ADepreciation + responses.ATechSupport
        + responses.ASecurityServices + responses.ATotCosts + responses.AdminFee;
    return admintotal;
}

//calculating participation costs
function particosts(responses) {
    var partitotal = responses.Trasportation + responses.JobTraining + responses.TuitionAssistance
        + responses.ContractedResidential + responses.UtilityAssistance + responses.EmergencyShelter
        + responses.HousingAssistance + responses.Childcare + responses.Clothing
        + responses.Food + responses.Supplies + responses.RFO;
    return partitotal;
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

        //getting admin total
        var admintotal = admincosts(responses[i]);
        //getting participation total
        var partitotal = particosts(responses[i]);
      
        //getting month
        var month = monthreturn(responses[i].Month);

        //adding to the table
        budgetsummary.push([month, admintotal, partitotal, admintotal + partitotal]);
    }

    var barData = new google.visualization.arrayToDataTable(budgetsummary);


    //bar chart options 
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

        //getting admin cost total
        var admintotal = admincosts(responses[i]);

        //getting particiapation cost total
        var partitotal = particosts(responses[i]);
           
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


        //getting admin cost total
        admincost = admincosts(responses[i]);

        //getting particiapation cost total
        particost  = particosts(responses[i]);

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

    //quarterly adding costs
    var firstquarterlyparticost = 0;
    var firstquarterlyadmincost = 0;
    var secondquarterlyparticost = 0;
    var secondquarterlyadmincost = 0;
    var thirdquarterlyparticost = 0;
    var thirdquarterlyadmincost = 0;
    var fourthquarterlyparticost = 0;
    var fourthquarterlyadmincost = 0;

    var i = 0;

    while (i < responses.length) {

        //calculate the first quater price

        if (responses[i].Month <= 3) {
            //increment the cost
            firstquarterlyadmincost += firstquarterlyadmincost;
            firstquarterlyparticost += firstquarterlyparticost;

            //getting admin and participation cost
            firstquarterlyadmincost = admincosts(responses[i]);
            firstquarterlyparticost = particosts(responses[i]);
        }
        // second quarter
        else if (responses[i].Month <= 6) {
             //increment the cost
            secondquarterlyadmincost += secondquarterlyadmincost;
            secondquarterlyparticost += secondquarterlyparticost;

           //getting admin and participation cost
            secondquarterlyadmincost = admincosts(responses[i]);
            secondquarterlyparticost = particosts(responses[i]);

        }
        //third quater
        else if (responses[i].Month <= 9) {
             //increment the cost
            thirdquarterlyadmincost += thirdquarterlyadmincost;
            thirdquarterlyparticost += thirdquarterlyparticost;

             //getting admin and participation cost
            thirdquarterlyadmincost = admincosts(responses[i]);
            thirdquarterlyparticost = particosts(responses[i]);

        }
        //fourthquater
        else if (responses[i].Month >= 10) {
             //increment the cost
            fourthquarterlyadmincost += fourthquarterlyadmincost;
            fourthquarterlyparticost += fourthquarterlyparticost;

             //getting admin and participation cost
            fourthquarterlyadmincost = admincosts(responses[i]);
            fourthquarterlyparticost = particosts(responses[i]);
        }
       
        i++;
    }

    var totalyearcost = (fourthquarterlyadmincost + fourthquarterlyparticost
        + firstquarterlyparticost + firstquarterlyadmincost
        + secondquarterlyparticost + secondquarterlyadmincost
        + thirdquarterlyparticost + thirdquarterlyadmincost);

    data.addRows([
        ['First Quarter', { v: firstquarterlyadmincost, f: '$' + firstquarterlyadmincost.toFixed(2) }, { v: firstquarterlyparticost, f: '$' + firstquarterlyparticost.toFixed(2) }, { v: (firstquarterlyadmincost + firstquarterlyparticost) * .1, f: '$' + ((firstquarterlyadmincost + firstquarterlyparticost) * .1).toFixed(2) }, { v: firstquarterlyadmincost + firstquarterlyparticost, f: '$' + ((firstquarterlyadmincost + firstquarterlyparticost) + (firstquarterlyadmincost + firstquarterlyparticost) * .1).toFixed(2) }],
        ['Second Quarter', { v: secondquarterlyadmincost, f: '$' + secondquarterlyadmincost.toFixed(2) }, { v: secondquarterlyparticost, f: '$' + secondquarterlyparticost.toFixed(2) }, { v: (secondquarterlyadmincost + secondquarterlyparticost) * .1, f: '$' + ((secondquarterlyadmincost + secondquarterlyparticost) * .1).toFixed(2) }, { v: secondquarterlyadmincost + secondquarterlyparticost, f: '$' + ((secondquarterlyadmincost + secondquarterlyparticost) + (secondquarterlyadmincost + secondquarterlyparticost) * .1).toFixed(2) }],
        ['Third Quarter', { v: thirdquarterlyadmincost, f: '$' + thirdquarterlyadmincost.toFixed(2) }, { v: thirdquarterlyparticost, f: '$' + thirdquarterlyparticost.toFixed(2) }, { v: (thirdquarterlyadmincost + thirdquarterlyparticost) * .1, f: '$' + ((thirdquarterlyadmincost + thirdquarterlyparticost) * .1).toFixed(2) }, { v: thirdquarterlyadmincost + thirdquarterlyparticost, f: '$' + ((thirdquarterlyadmincost + thirdquarterlyparticost) + (thirdquarterlyadmincost + thirdquarterlyparticost) * .1).toFixed(2) }],
        ['Fourth Quarter', { v: fourthquarterlyadmincost, f: '$' + fourthquarterlyadmincost.toFixed(2) }, { v: fourthquarterlyparticost, f: '$' + fourthquarterlyparticost.toFixed(2) }, { v: (fourthquarterlyadmincost + fourthquarterlyparticost) * .1, f: '$' + ((fourthquarterlyadmincost + fourthquarterlyparticost) * .1).toFixed(2) }, { v: fourthquarterlyadmincost + fourthquarterlyparticost, f: '$' + ((fourthquarterlyadmincost + fourthquarterlyparticost) + (fourthquarterlyadmincost + fourthquarterlyparticost) * .1).toFixed(2) }],
        //['Total', , , , { v: totalyearcost+totalyearcost * .1, f: '$' + (totalyearcost + totalyearcost*.1).toFixed(2) }]

    ]);

    //calculating total cost

    var table = new google.visualization.Table(document.getElementById('table_div'));

    table.draw(data, { showRowNumber: true, width: '100%', height: '100%' });
}

function drawToolbar(responses) {

    var data = new google.visualization.DataTable();

    // Declare columns
    data.addColumn('string', 'Employee Name');
    data.addColumn('datetime', 'Hire Date');

    // Add data.
    data.addRows([
        ['Mike', { v: new Date(2008, 1, 28), f: 'February 28, 2008' }], // Example of specifying actual and formatted values.
        ['Bob', new Date(2007, 5, 1)],                              // More typically this would be done using a
        ['Alice', new Date(2006, 7, 16)],                           // formatter.
        ['Frank', new Date(2007, 11, 28)],
        ['Floyd', new Date(2005, 3, 13)],
        ['Fritz', new Date(2011, 6, 1)]
    ]);

    var csv = google.visualization.dataTableToCsv(data);
    console.log(csv);

    var components = [
        {
            type: 'html', datasource: csv },
        { type: 'csv', datasource: data }
        
    ];

    var container = document.getElementById('toolbar_div');
    google.visualization.drawToolbar(container, components);
}
