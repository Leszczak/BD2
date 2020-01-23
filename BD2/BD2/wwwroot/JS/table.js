function showTable(dataJSON, entityName) {
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
            
            htmltxt += `<th><a href="${entityName}.html?id=${object['id']}">more...</a></th>`;
            
            htmltxt += '</tr>';
        });    
    } else {
        htmltxt = '<th>empty</th></tr>';
    }

    document.getElementById('table').innerHTML = htmltxt;
}

function isId(atr) {
    return atr.toLowerCase().endsWith('id') || atr.toLowerCase().endsWith('ids')
}

function isForeignId(atr) {
    return isId(atr) && atr.length > 2;
}