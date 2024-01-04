
async function reload() {
    const keys = await caches.keys();
    for (let cch of keys) {
        await caches.delete(cch);
    }
    location.replace(location.origin);
    //window.location.reload(true);
}

async function getVersion() {
    const response = await fetch(`/sample-data/version.txt`);
    const resData = await response.text();
    return resData;
}
