function generateCSV(dataJSON) {
    let text = '';

    if (Array.isArray(dataJSON) && dataJSON.length) {
        let keys = Object.keys(dataJSON[0]);
        text += keys.join(',') + '\n';

        dataJSON.forEach(object=> {
            let values = Object.values(object);
            text += values.join(',') + '\n';
        });
        // for (atr in dataJSON[0]) {
        //     text += atr + ',';
        // }
    
        // dataJSON.forEach(object => {
        //     text += '<tr>';
    
        //     for (atr in object) {
        //         text += '<th>' + object[atr] + '</th>';
        //     }
            
        //     text += '</tr>';
        // });    
    }

    return text
}