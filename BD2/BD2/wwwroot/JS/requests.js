async function requestGet(interfaceName) {
    console.log('requestGet(' + interfaceName + ')');
    const url = 'https://localhost:5001/api/' + interfaceName;
    const response = await fetch(url, {method: 'GET'});
    if (!response.ok)
        throw Error(response.statusText);
    const data = await response.json();
    console.log(data);
    return data;
}