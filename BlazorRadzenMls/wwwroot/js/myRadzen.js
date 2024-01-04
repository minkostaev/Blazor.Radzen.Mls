
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
