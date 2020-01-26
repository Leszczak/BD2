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
        document.getElementById('inputTable').innerHTML = generateInputTable(null);
    } finally {
        document.getElementById('relatedTable').innerHTML = generateTable(null);
    }
}

function downloadCSV(interfaceName) {
    let text;
    requestGet(interfaceName).then((data) => {
        text = generateCSV(data);
        element = document.createElement('a');
        element.setAttribute('href', 'data:text/csv;charset=utf-8,' + encodeURIComponent(text));
        element.setAttribute('download', interfaceName + '.csv');
        element.click();
    });
}

async function showBtnClicked(atr, id) {
    try {
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
    var data = dataForms[interfaceName];
    console.log(1, data);
    for (atr in data) {
        if (Array.isArray(data[atr])) {
            let input = document.getElementById(atr).value.split(',');
            for (element in input) {
                data[atr].push(parseInt(input[element]));
            }
        } else {
            if (typeof data[atr] == 'string')
                data[atr] = document.getElementById(atr).value;
            else if (typeof data[atr] == 'boolean') {
                if (document.getElementById(atr).value == 'true')
                    data[atr] = true;
                else
                    data[atr] = false;
            }
            else
                data[atr] = parseInt(document.getElementById(atr).value);
        }
    }
    console.log(2, data);
    try {
        await requestPost(interfaceName, data);
    } catch(err) {
        console.log(err);
    } finally {
        await updateTables(interfaceName);
    }
}

async function putBtnClicked(interfaceName, id) {
    let data = Object.create(dataForms[interfaceName]);
    data['id'] = parseInt(id);
    
    for (atr in data) {
        if (atr != 'id') {
            if (Array.isArray(data[atr])) {
                let input = document.getElementById(atr).value.split(',');
                for (element in input) {
                    data[atr].push(parseInt(input[element]));
                }
            } else {
                if (typeof data[atr] == 'string')
                    data[atr] = document.getElementById(atr).value;
                else if (typeof data[atr] == 'boolean') {
                    if (document.getElementById(atr).value == 'true')
                        data[atr] = true;
                    else
                        data[atr] = false;
                }
                else
                    data[atr] = parseInt(document.getElementById(atr).value);
            }
        }
    }

    try {
        await requestPut(interfaceName, id, data);
    } catch(err) {
        console.log(err);
    } finally {
        await updateTables(interfaceName);
    }
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