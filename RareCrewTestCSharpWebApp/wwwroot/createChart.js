function createAndSaveChart(data) {
    const ctx = document.getElementById("ctx")
    const chart = new Chart(
        ctx,
        {
            type: 'pie',
            data: {
                labels: data.map(item => item.employeeName),
                datasets: [
                    {
                        data: data.map(item => item.procentage)
                    }
                ]
            }
        }
    );
    ctx.style = "display: none;"
    const pngImage = chart.toBase64Image();
    let link = document.createElement('a');
    link.href = pngImage;
    link.download = 'chart.png';
    document.body.appendChild(link);

    link.click();

    document.body.removeChild(link);
}