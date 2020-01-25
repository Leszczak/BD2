function generateTableWithLinks(dataJSON, interfaceName) {
    let htmltxt = '<tr>';

    if (Array.isArray(dataJSON) && dataJSON.length) {
        for (atr in dataJSON[0]) {
            if (!isForeignId(atr))
                htmltxt += '<th>' + atr + '</th>';
        }
        
        htmltxt += '<th>more</th></tr>';
    
        dataJSON.forEach(object => {
            htmltxt += '<tr>';
    
            for (atr in object) {
                if (!isForeignId(atr))
                    htmltxt += '<th>' + object[atr] + '</th>';
            }
            
            htmltxt += `<th><a href="${interfaceName}.html?id=${object['id']}">more...</a></th>`;
            
            htmltxt += '</tr>';
        });    
    } else {
        htmltxt = '<th>empty</th></tr>';
    }

    return htmltxt
}

function generateTableWithButtons(dataJSON, interfaceName) {
    let htmltxt = '<tr>';

    if (Array.isArray(dataJSON) && dataJSON.length) {
        for (atr in dataJSON[0]) {
            if (isForeignId(atr))
                htmltxt += '<th>' + sliceId(atr) + '</th>'
            else
                htmltxt += '<th>' + atr + '</th>';
        }
        
        htmltxt += '<th>delete</th>';

        dataJSON.forEach(object => {
            htmltxt += '<tr>';
    
            for (atr in object) {
                if (isForeignId(atr)) {
                    if (object[atr] != '' && object[atr] != -1) {
                        if (object[atr] != null)
                            htmltxt += `<th><button name="${sliceId(atr)}" value="${object[atr]}" 
                            onclick="showBtnClicked(this.name, this.value)">show</button></th>`;
                        else
                            htmltxt += '<th>null</th>'
                    }
                    else
                        htmltxt += '<th>empty</th>'
                } else
                    htmltxt += '<th>' + object[atr] + '</th>';
            }
            htmltxt += `<th><button name="${interfaceName}" value="${object['id']}" 
                        onclick="deleteBtnClicked(this.name, this.value)">delete</button></th>`;
            htmltxt += '</tr>';
        });    
    } else {
        htmltxt = '<th>empty</th></tr>';
    }

    return htmltxt
}

function generateTable(dataJSON) {
    let htmltxt = '<tr>';
    if (Array.isArray(dataJSON) && dataJSON.length) {
        for (atr in dataJSON[0]) {
            if (!isForeignId(atr))
                htmltxt += '<th>' + atr + '</th>';
        }
    
        dataJSON.forEach(object => {
            htmltxt += '<tr>';
    
            for (atr in object) {
                if (!isForeignId(atr))
                    htmltxt += '<th>' + object[atr] + '</th>';
            }
            
            htmltxt += '</tr>';
        });    
    } else {
        htmltxt = '<th>empty</th></tr>';
    }

    return htmltxt
}

function isId(atr) {
    return atr.toLowerCase().endsWith('id') || atr.toLowerCase().endsWith('ids')
}

function isForeignId(atr) {
    return isId(atr) && atr.length > 2;
}

function sliceId(foreignId) {
    return foreignId.slice(0, foreignId.lastIndexOf('Id'));
}