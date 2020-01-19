document.getElementById('inputSubmit').addEventListener('click', sendRequest);

async function sendRequest() {
    const url = 'https://localhost:5001/api/' + document.getElementById('inputApiTxt').value;
    const method = document.getElementById('inputMethodCmb').value;
    const response = await fetch(url, {
        method: method,
    });
    console.log('request send');
    const data = await response.json();
    return data;
}
