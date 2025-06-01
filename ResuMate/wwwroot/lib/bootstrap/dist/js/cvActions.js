window.viewPdf = (base64, title) => {
    const newWindow = window.open();
    if (newWindow) {
        newWindow.document.write(`
            <iframe src="data:application/pdf;base64,${base64}"
                    style="width: 100%; height: 100%; border: none;"
                    title="${title}">
            </iframe>
        `);
    }
};

window.downloadPdf = (base64, fileName) => {
    const link = document.createElement('a');
    link.href = 'data:application/pdf;base64,' + base64;
    link.download = fileName;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
};
