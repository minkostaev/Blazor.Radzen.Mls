
function getRadzenTheme() {
    let links = document.head.getElementsByTagName('link');
    for (let li of links) {
        if (li.href.includes('_content/Radzen.Blazor/css/')) {
            let css = li.href.split('/').slice(-1)[0];
            return css.replace('-base.css', '');
        }
    }
}

function setRadzenTheme(val) {
    let links = document.head.getElementsByTagName('link');
    for (let li of links) {
        if (li.href.includes('_content/Radzen.Blazor/css/')) {
            let css = li.href.split('/').slice(-1)[0];
            let newLink = li.href.replace(css, val + '-base.css');
            li.href = newLink;
        }
    }
}

async function getVersion() {
    const response = await fetch(`/sample-data/ver.txt`);
    const resData = await response.text();
    return resData;
}

async function reload() {
    const keys = await caches.keys();
    for (let cch of keys) {
        await caches.delete(cch);
    }
    location.replace(location.origin);
    //window.location.reload(true);
}
