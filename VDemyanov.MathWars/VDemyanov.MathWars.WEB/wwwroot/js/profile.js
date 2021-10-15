function createUserStatChart(stat) {

    const piechart = document.getElementById('doughnut').getContext('2d');

    const data = {
        labels: ["Number of solved tasks", "Number of created tasks"],
        datasets: [{
            label: 'My First Dataset',
            data: [stat.solved, stat.created],
            backgroundColor: [
                '#2ecc71',
                '#f1c40f'
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