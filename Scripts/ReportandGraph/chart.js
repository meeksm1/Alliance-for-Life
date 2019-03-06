
    google.load("visualization", "1", {packages: ["corechart"] });
    google.setOnLoadCallback(drawCharts);

    //fetching the graph data
    $(document).ready(function () {
        $.ajax({
            url: "/BudgetCosts/GraphDataIndex",
            data: "{}",
            dataType: "json",
            type: "POST",
            contentType: "application/json; chartset=utf-8",
            success: function (responses) {

                var length = responses.length;

                var budgetsummary = [
                    [{ label: 'Month', type: 'string' },
                    { label: 'Admin', type: 'number' },
                    { label: 'Participation', type: 'number' },
                    { label: 'Total', type: 'number' }]

                ];

                for (var i = 0; i < responses.length; i++) {
                    var admintotal = responses[i].ASalandWages
                        + responses[i].AEmpBenefits
                        + responses[i].AEmpTravel
                        + responses[i].AEmpTraining
                        + responses[i].AOfficeRent
                        + responses[i].AOfficeUtilities
                        + responses[i].AFacilityIns
                        + responses[i].AOfficeSupplies
                        + responses[i].AEquipment
                        + responses[i].AOfficeCommunications
                        + responses[i].AOfficeMaint
                        + responses[i].AConsulting
                        + responses[i].SubConPayCost
                        + responses[i].BackgrounCheck
                        + responses[i].Other
                        + responses[i].AJanitorServices
                        + responses[i].ADepreciation
                        + responses[i].ATechSupport
                        + responses[i].ASecurityServices
                        + responses[i].ATotCosts
                        + responses[i].AdminFee;

                    var partitotal = responses[i].Trasportation
                        + responses[i].JobTraining
                        + responses[i].TuitionAssistance
                        + responses[i].ContractedResidential
                        + responses[i].UtilityAssistance
                        + responses[i].EmergencyShelter
                        + responses[i].HousingAssistance
                        + responses[i].Childcare
                        + responses[i].Clothing
                        + responses[i].Food
                        + responses[i].Supplies
                        + responses[i].RFO;

                    var month;
                    switch (responses[i].Month) {
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

                    budgetsummary.push([month, admintotal, partitotal, admintotal + partitotal]);
                }


                var barData = new google.visualization.arrayToDataTable(budgetsummary);
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

                var data = new google.visualization.DataTable();

                //   Adding columns to data table to insert the chart data

                data.addColumn('string', 'CostType');
                data.addColumn('number', 'Amount');
                ////bind the data to the data table using for loop
                for (var i = 0; i < length; i++) {
                    data.addRow(["Salary/Wages", responses[i].ASalandWages]);

                    data.addRow(["Name", responses[i].Trasportation]);


                }

                // Instantiate and draw our chart, passing in some options
                var chart = new google.visualization.BarChart(document.getElementById('doechart'));
                chart.draw(data,
                    {
                        chartArea: { width: '40%' },
                        legend: 'none',
                        pieHole: 0.6,

                    });
            },

            // ajax error display
            error: function () {
                alert("Error loading data! Please try again.");
            }
        });
    });




    function drawCharts() {

        // BEGIN BAR CHART



        // BEGIN LINE GRAPH

        function randomNumber(base, step) {
            return Math.floor((Math.random() * step) + base);
        }
        function createData(year, start1, start2, step, offset) {
            var ar = [];
            for (var i = 0; i < 12; i++) {
        ar.push([new Date(year, i), randomNumber(start1, step) + offset, randomNumber(start2, step) + offset]);
    }
    return ar;
}
var randomLineData = [
    ['Year', 'Page Views', 'Unique Views']
];
        for (var x = 0; x < 7; x++) {
            var newYear = createData(2007 + x, 10000, 5000, 4000, 800 * Math.pow(x, 2));
            for (var n = 0; n < 12; n++) {
        randomLineData.push(newYear.shift());
    }
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

// BEGIN PIE CHART

// pie chart data
var pieData = google.visualization.arrayToDataTable([
['Country', 'Page Hits'],
['USA', 7242],
['Canada', 4563],
['Mexico', 1345],
['Sweden', 946],
['Germany', 2150]
]);
// pie chart options
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

// draw pie chart
var pieChart = new google.visualization.PieChart(document.getElementById('pie-chart'));
pieChart.draw(pieData, pieOptions);
}
