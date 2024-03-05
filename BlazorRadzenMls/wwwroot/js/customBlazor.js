addLoaderCss();

function addLoaderCss() {
    const num = Math.floor((Math.random() * 6) + 1);
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

function getLocation(prop) {
    switch (prop) {
        case 'hash':
            return location.hash;
        case 'host':
            return location.host;
        case 'hostname':
            return location.hostname;
        case 'href':
            return location.href;
        case 'origin':
            return location.origin;
        case 'pathname':
            return location.pathname;
        case 'port':
            return location.port;
        case 'protocol':
            return location.protocol;
        case 'search':
            return location.search;
        default:
            return location;
    }
}

async function getIp() {
    const response = await fetch('https://jsonip.com/');
    const data = await response.json();
    return data.ip;
}

function scrollToTop() {
    document.documentElement.scrollTop = 0;
}
