function onLoad() {
    document.getElementById('inputTable').innerHTML = generateInputTable(null);
    document.getElementById('mainTable').innerHTML = generateTable(null);
    document.getElementById('relatedTable').innerHTML = generateTable(null);
}

async function updateTables(select) {
    try {
        let data = await requestGet(select);
        document.getElementById('mainTable').innerHTML = generateTableWithButtons(data, select);
        document.getElementById('inputTable').innerHTML = generateInputTable(dataForms[select], select);
    } catch(err) {
        console.log(err);
        document.getElementById('mainTable').innerHTML = generateTable(null);
    } finally {
        document.getElementById('relatedTable').innerHTML = generateTable(null);
    }
}

async function showBtnClicked(atr, id) {
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
        document.getElementById('relatedTable').innerHTML = generateTable(dataArr);
    } catch(err) {
        console.log(err);
        document.getElementById('relatedTable').innerHTML = generateTable(null);
    }
}

async function postBtnClicked(interfaceName) {
    let data = dataForms[interfaceName];

    for (atr in data) {
        if (Array.isArray(data[atr])) {
            let input = document.getElementById(atr).value.split(',');
            console.log(input);
            for (element in input) {
                data[atr].push(parseInt(input[element]));
            }
        } else {
            data[atr] = document.getElementById(atr).value;
        }
    }

    try {
        console.log(interfaceName);
        console.log(data);
        await requestPost(interfaceName, data);
        await updateTables(interfaceName);
    } catch(err) {
        console.log(err);
    } finally {
        await updateTables(interfaceName);
    }
}

async function putBtnClicked(interfaceName, id) {
    
}

async function deleteBtnClicked(interfaceName, id) {
    try {
        await requestDelete(interfaceName, id);
    } catch(err) {
        console.log(err);
    } finally {
        await updateTables(interfaceName);
    }
}