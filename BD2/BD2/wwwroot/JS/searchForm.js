async function selectChange(select) {
    try {
        let data = await requestGet(select);
        document.getElementById('mainTable').innerHTML = generateTableWithButtons(data);
    } catch(err) {
        console.log(err);
        document.getElementById('mainTable').innerHTML = generateTable(null);
    }
}

async function buttonClicked(atr, id) {
    console.log(atr);
    console.log(id);
    try {
        let data = await requestGet(atr+`s/${id}`);
        if (Array.isArray(data)) {
            document.getElementById('detailsTable').innerHTML = generateTable(data);
        } else {
            let dataArr = [];
            dataArr.push(data);
            document.getElementById('detailsTable').innerHTML = generateTable(dataArr);
        }
    } catch(err) {
        console.log(err);
        document.getElementById('detailsTable').innerHTML = generateTable(null);
    }
}