addLoaderCss();

function addLoaderCss() {
    const num = Math.floor((Math.random() * 8) + 1);
    const linkLoader = document.createElement('link');
    linkLoader.rel = 'stylesheet';
    linkLoader.href = `css/loaders/load${num}.css`;
    document.head.appendChild(linkLoader);
}

async function reload() {
    const keys = await caches.keys();
    for (let cch of keys) {
        await caches.delete(cch);
    }
    location.replace(location.href);//origin
    //window.location.reload(true);
}

async function fetchJson(path) {
    const response = await fetch(path);
    const resData = await response.json();
    return resData;
}
async function fetchText(path) {
    const response = await fetch(path);
    const resData = await response.text();
    return resData;
}

//function getTest() {
//    return location.href;
//}
