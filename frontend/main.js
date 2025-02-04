window.addEventListener('DOMContentLoaded', (event) => {
    getVisitCount();
});

const functionApiLocal = 'http://localhost:5003/api/GetReusmeCounter';
const functionApiRemote = 'https://resumefuncapp123.azurewebsites.net/api/GetReusmeCounter?code=m3pk6-mZBaJbr1KLli8JY0dAXPCcwVnzvs7G3GOva1vpAzFu-ElM6w%3D%3D';

const getVisitCount = () => {
    debugger;
    let count = 30;

    fetch(functionApiRemote)
        .then(response => {
            return response.json();
        })
        .then(response => {
                // Get the custom header "X-Counter-Data"
                const counterData = response.customData.viewPageCount;
                document.getElementById("counter").innerText = counterData;
        })
        .catch(function(error) {
            console.log(error);
        });
    return count;
};