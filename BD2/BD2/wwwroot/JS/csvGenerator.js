function generateCSV(dataJSON) {
    let text = '';

    if (Array.isArray(dataJSON) && dataJSON.length) {
        let keys = Object.keys(dataJSON[0]);
        text += keys.join(',') + '\n';

        dataJSON.forEach(object=> {
            let values = Object.values(object);
            text += values.join(',') + '\n';
        });
    }

    return text
}