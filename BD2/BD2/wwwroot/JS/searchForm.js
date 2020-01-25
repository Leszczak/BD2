function onLoad() {
    document.getElementById('mainTable').innerHTML = generateTable(null);
    document.getElementById('detailsTable').innerHTML = generateTable(null);
}

async function selectChange(select) {
    try {
        let data = await requestGet(select);
        document.getElementById('mainTable').innerHTML = generateTableWithButtons(data);
    } catch(err) {
        console.log(err);
        document.getElementById('mainTable').innerHTML = generateTable(null);
    } finally {
        document.getElementById('detailsTable').innerHTML = generateTable(null);
    }
}

async function buttonClicked(atr, id) {
    try {
        console.log(atr);
        console.log(id);
        if (id == '') throw Error('empty id');
        let ids = id.split(',');
        let dataArr = [];
        for (element in ids) {
            let data = await requestGet(atr+`s/${ids[element]}`);
            dataArr.push(data);
        }
        document.getElementById('detailsTable').innerHTML = generateTable(dataArr);
    } catch(err) {
        console.log(err);
        document.getElementById('detailsTable').innerHTML = generateTable(null);
    }
}