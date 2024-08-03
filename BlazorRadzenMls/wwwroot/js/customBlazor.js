const cssRoot = document.querySelector(':root');
cssRoot.style.setProperty('--windowHeight', window.innerHeight + 'px');
addEventListener("resize", (event) => {
    windowHeight = window.innerHeight;
    cssRoot.style.setProperty('--windowHeight', window.innerHeight + 'px');
});
let windowHeight = 0;
let headerHeight = 0;
let footerHeight = 0;
function addHeaderFooterHeight(htmlId, isHeader) {
    const el = document.getElementById(htmlId);
    setHeaderFooterHeight(el, isHeader);
    const resizeObserver = new ResizeObserver(entries => {
        setHeaderFooterHeight(el, isHeader);
        ///for (let entry of entries) {
        ///    const { width, height } = entry.contentRect;
        ///    console.log(`Element resized to ${width}px x ${height}px`);
        ///}
    });
    resizeObserver.observe(el);
}
function setHeaderFooterHeight(el, isHeader) {
    if (isHeader) {
        headerHeight = el.clientHeight;//offsetHeight
        cssRoot.style.setProperty('--headerHeight', headerHeight + 'px');
    } else {
        footerHeight = el.clientHeight;
        cssRoot.style.setProperty('--footerHeight', footerHeight + 'px');
    }
    cssRoot.style.setProperty('--heightHeaderFooter', (headerHeight + footerHeight + 1) + 'px');
    cssRoot.style.setProperty('--heightHeaderFooterRdzn', (headerHeight + footerHeight + 33) + 'px');
}


addLoaderCss();
function addLoaderCss() {
    const num = Math.floor((Math.random() * 6) + 1);
    const linkLoader = document.createElement('link');
    linkLoader.rel = 'stylesheet';
    linkLoader.href = `css/loaders/load${num}.css`;
    document.head.appendChild(linkLoader);
}

function scrollToTop() {
    document.documentElement.scrollTop = 0;
}

async function reload() {
    const keys = await caches.keys();
    for (let cch of keys) {
        await caches.delete(cch);
    }
    location.replace(location.href);//origin
    ///window.location.reload(true);
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

async function getIp() {
    const response = await fetch('https://jsonip.com/');
    const data = await response.json();
    return data.ip;
}

function pdfToIframe(base64String, iframeId) {
    const byteArray = Uint8Array.from(atob(base64String), c => c.charCodeAt(0));
    const blob = new Blob([byteArray], { type: 'application/pdf' });

    const viewer = document.getElementById(iframeId);
    const objectUrl = URL.createObjectURL(blob);

    viewer.setAttribute('src', objectUrl);
    viewer.onload = () => URL.revokeObjectURL(objectUrl);
}

let menuClicked = false;
function showMenuPanel(htmlId, className, cssDisplay) {
    hideMenuPanel(htmlId, className);
    const div = document.getElementById(htmlId);
    if (div != null) {
        let isDisplayVisible = div.style.display == cssDisplay;
        div.style.display = isDisplayVisible ? 'none' : cssDisplay;
        menuClicked = true;
        return !isDisplayVisible;
    }
    return false;
}
function hideMenuPanel(skipId, className) {
    if (!menuClicked) {
        const panels = document.getElementsByClassName(className);
        for (const el of panels) {
            if (el.id != skipId)
                el.style.display = 'none';
        }
    }
    menuClicked = false;
}

function isMobileDevice() {
    // Check the user agent string for common mobile device keywords
    const mobileKeywords = ['Android', 'webOS', 'iPhone', 'iPad', 'iPod', 'BlackBerry', 'Windows Phone'];
    const userAgent = navigator.userAgent;
    for (let i = 0; i < mobileKeywords.length; i++) {
        if (userAgent.indexOf(mobileKeywords[i]) !== -1) {
            return true;
        }
    }
    return false;
}
