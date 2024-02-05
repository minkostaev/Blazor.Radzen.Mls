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
        console.log(cch);// temp to check what is deleted
        await caches.delete(cch);
    }
    location.replace(location.href);//origin
    //window.location.reload(true);
}

async function getVersion() {
    const response = await fetch(`/data/version.txt`);
    const resData = await response.text();
    return resData;
}
