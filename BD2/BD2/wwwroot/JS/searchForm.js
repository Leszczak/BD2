async function selectChange(select) {
    try {
        let data = await requestGet(select);
        document.getElementById('table').innerHTML = generateTable(data);
    } catch(err) {
        console.log(err);
        document.getElementById('table').innerHTML = generateTable(null);
    }
}

function buttonClicked(atr, id) {
    console.log(atr);
    console.log(id);
}