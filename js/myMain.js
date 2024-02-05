addLoaderCss();
gitHub('/Blazor.Radzen.Mls/');// pass your repo name here

function gitHub(addAfterDomain) {// GitHub method to help with publish
    let base = document.head.getElementsByTagName('base')[0];
    if (base.href.includes('.github.io')) {
        base.href = addAfterDomain;// github domain fix
    }
}

function addLoaderCss() {
    const num = Math.floor((Math.random() * 8) + 1);
    const linkLoader = document.createElement('link');
    linkLoader.rel = 'stylesheet';
    linkLoader.href = `css/loaders/load${num}.css`;
    document.head.appendChild(linkLoader);
}

async function reload() {
    const keys = await caches.keys();
    const i = keys.length;// for debug (delete)
    for (let cch of keys) {
        await caches.delete(cch);
    }
    location.replace(location.href);//origin
    //window.location.reload(true);
    console.log(i);// for debug (delete)
}

async function getVersion() {
    const response = await fetch(`/data/version.txt`);
    const resData = await response.text();
    return resData;
}
