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
    location.replace(location.href);///origin
    ///window.location.reload(true);
}

function getLocation() {
    let result = {};
    result.hash = location.hash;
    result.host = location.host;
    result.hostname = location.hostname;
    result.href = location.href;
    result.origin = location.origin;
    result.pathname = location.pathname;
    result.port = location.port;
    result.protocol = location.protocol;
    result.search = location.search;
    return result;
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

let currentIp;
async function getIp(forceRefresh) {
    // to do https://api.ipify.org
    if (currentIp == null || forceRefresh) {
        currentIp = await fetchText('https://api.ipify.org/');
    }
    return currentIp;
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
    for (const element of mobileKeywords) {
        if (userAgent.indexOf(element) !== -1) {
            return true;
        }
    }
    return false;
}
