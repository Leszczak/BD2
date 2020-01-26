async function requestGet(interfaceName) {
    console.log(`requestGet(${interfaceName})`);
    const url = `https://localhost:5001/api/${interfaceName}`;
    const response = await fetch(url, {method: 'GET'});
    if (!response.ok)
        throw Error(response.statusText);
    const data = await response.json();
    console.log(data);
    return data;
}

async function requestPost(interfaceName, data) {
    console.log(`requestPost(${interfaceName})`);
    const url = `https://localhost:5001/api/${interfaceName}`;
    const response = await fetch(url, {method: 'POST'});
    if (!response.ok)
        throw Error(response.statusText);
    return data;
}

async function requestDelete(interfaceName, id) {
    console.log(`requestDelete(${interfaceName}, ${id})`);
    const url = `https://localhost:5001/api/${interfaceName}/${id}`;
    const response = await fetch(url, {method: 'DELETE'});
    if (!response.ok)
        throw Error(response.statusText);
    return data;
}