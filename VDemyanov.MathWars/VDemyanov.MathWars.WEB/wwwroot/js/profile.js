function createUserStatChart(stat) {

    //let providerDisplayNameArr = stat.map(item => item.providerDisplayName);
    //let userCountArr = stat.map(item => item.userCount);

    const piechart = document.getElementById('doughnut').getContext('2d');

    const data = {
        labels: ["Number of comleted tasks", "Number of created tasks"],
        datasets: [{
            label: 'My First Dataset',
            data: [3, 4],
            backgroundColor: [
                'rgb(255, 99, 132)',
                'rgb(54, 162, 235)'
            ],
            hoverOffset: 4
        }]
    };

    const config = {
        type: 'doughnut',
        data: data,
    };

    var mypiechart = new Chart(piechart, config);
}