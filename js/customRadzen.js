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

function isOsDarkTheme() {
    const isDarkMode = window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches;
    const isLightMode = window.matchMedia && window.matchMedia('(prefers-color-scheme: light)').matches;
    if (isDarkMode == true)
        return true;
    if (isLightMode == true)
        return false;
    return null;
}

function removeBodyContentPadding() {
    let bodyDiv = document.getElementById('myRadzenBody');
    bodyDiv.style.padding = 0;
}
function changeBodyContentPadding() {
    let bodyDiv = document.getElementById('myRadzenBody');
    if (bodyDiv.classList.contains("my-rz-body")) {
        bodyDiv.classList.remove('my-rz-body');
        return true;
    } else {
        bodyDiv.classList.add('my-rz-body');
        return false;
    }
}

function changeSidebarToggleStyle(id, fontSize, fontWeight) {
    let tggBtn = document.getElementById(id);
    let i = tggBtn.getElementsByTagName('i')[0];
    if (fontSize != null) {
        i.style.fontSize = fontSize;
    }
    if (fontWeight != null) {
        i.style.fontWeight = fontWeight;
    }
}
