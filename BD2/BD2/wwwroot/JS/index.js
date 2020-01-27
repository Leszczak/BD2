function onLoad() {
    document.getElementById('inputTable').innerHTML = generateInputTable(null);
    document.getElementById('mainTable').innerHTML = generateTable(null);
    document.getElementById('relatedTable').innerHTML = generateTable(null);
    document.getElementById('selectAtr').innerHTML = generateSelectAtr(null);
}

async function updateTables(select) {
    try {
        let data = await requestGet(select);
        let selectedAtr = document.getElementById('selectAtr').value;
        document.getElementById('inputTable').innerHTML = generateInputTable(dataForms[select], select);
        document.getElementById('mainTable').innerHTML = generateTableWithButtons(data.filter(atrFilter, selectedAtr), select);
    } catch(err) {
        console.log(err);
        document.getElementById('selectAtr').innerHTML = generateSelectAtr(null);
        document.getElementById('inputTable').innerHTML = generateInputTable(null);
        document.getElementById('mainTable').innerHTML = generateTable(null);
    } finally {
        document.getElementById('relatedTable').innerHTML = generateTable(null);
    }
}

async function mainSelectChange(select) {
    document.getElementById('selectAtr').innerHTML = generateSelectAtr(dataForms[select]);
    document.getElementById('inputText').value = '';
    document.getElementById('inputTable').innerHTML = generateInputTable(dataForms[select], select);
    document.getElementById('mainTable').innerHTML = generateTable(null);
    document.getElementById('relatedTable').innerHTML = generateTable(null);
}

function downloadCSV(interfaceName) {
    let text;
    let selectedAtr = document.getElementById('selectAtr').value;
    requestGet(interfaceName).then((data) => {
        text = generateCSV(data.filter(atrFilter, selectedAtr));
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
    let data = Object.create(dataForms[interfaceName]);
    for (atr in dataForms[interfaceName]) {
        if (Array.isArray(dataForms[interfaceName]))
        data[atr] = dataForms[interfaceName][atr].slice();
        data[atr] = dataForms[interfaceName][atr];
    }
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
    for (atr in dataForms[interfaceName]) {
        if (Array.isArray(dataForms[interfaceName]))
        data[atr] = dataForms[interfaceName][atr].slice();
        data[atr] = dataForms[interfaceName][atr];
    }
    data['id'] = parseInt(id);
    console.log(1, data);
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
    console.log(2, data);
    try {
        await requestPut(interfaceName, id, data);
    } catch(err) {
        console.log(err);
    } finally {
        await updateTables(interfaceName);
    }
}

function copy(object) {
    if (null == obj || "object" != typeof obj) return obj;
    var copy = obj.constructor();
    for (var attr in obj) {
        if (obj.hasOwnProperty(attr)) copy[attr] = obj[attr];
    }
    return copy;
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

function generateSelectAtr(data) {
    let htmltxt = '<option value=""></option>';
    if (data != null) {
        htmltxt += `<option value="id">id</option>`;
        for (atr in data)
            htmltxt += `<option value="${atr}">${atr}</option>`;
    }
    
    return htmltxt;
}

function atrFilter(object) {
    if (this == '') return true;
    let inputText = document.getElementById('inputText').value;
    if (typeof object[this] == 'string') 
        return object[this].startsWith(inputText);
    else if (typeof object[this] == 'boolean') {
        if (inputText == 'true' && object[this] || inputText == 'false' && !object[this])
            return true;
        else
            return false;
    } else if (Array.isArray(object[this])) {
        arr = JSON.parse('[' + inputText + ']');
        let t = true;
        arr.forEach((element) => {
            if (!object[this].includes(element))
                t = false;
        })
        return t;
    } else {
        return object[this] == parseInt(inputText);
    }
}