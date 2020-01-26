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
    console.log(`requestPost(${interfaceName}, ${JSON.stringify(data)})`);
    const url = `https://localhost:5001/api/${interfaceName}`;
    const response = await fetch(url, {
        method: 'POST',
        headers: {'Content-Type': 'application/json'},
        body: JSON.stringify(data)});
    if (!response.ok)
        throw Error(response.statusText);
    return data;
}

async function requestPut(interfaceName, id, data) {
    console.log(`requestPut(${interfaceName}, ${id}, ${JSON.stringify(data)})`);
    const url = `https://localhost:5001/api/${interfaceName}/${id}`;
    const response = await fetch(url, {
        method: 'PUT',
        headers: {'Content-Type': 'application/json'},
        body: JSON.stringify(data)});
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