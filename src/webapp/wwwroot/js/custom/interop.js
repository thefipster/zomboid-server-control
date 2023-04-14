var interop = interop || {};
var syncObj = {};

interop.readMods = function () {
    const items = document.querySelectorAll(".mod-item");
    const mods = [];

    for (const mod of items) {
        const modObj = {
            workshopId: mod.id,
            name: mod.dataset.fipModname,
            id: mod.dataset.fipModid
        }

        mods.push(modObj);
    }

    return mods;
}

interop.syncInstance = function (dotNetObject) {
    syncObj = dotNetObject;
};

interop.setupModsDragAndDrop = function () {
    const modList = document.getElementById('fip-mod-list');

    if (modList)
        new Sortable(modList, {
            animation: 150,
        });
};

interop.alert = function (message) {
    alert(message);
};

interop.focus = function () {
    document.activeElement?.blur();
    const element = document.getElementById('modName');
    if (element) {
        element.focus();
        return;
    }
};

interop.scrollToAnchor = function (elementId, anchorName) {

    const anchor = document.getElementById(anchorName);
    const rectAnchor = anchor.getBoundingClientRect();

    const header = document.getElementById("page-header");
    const footer = document.getElementById("status-bar");

    const topOverlay = header.getBoundingClientRect().height;
    const bottomOverlay = footer.getBoundingClientRect().height;

    const windowHeight = document.documentElement.clientHeight;

    const scrollPosition = window.pageYOffset;

    let scrollHeight = Math.max(
        document.body.scrollHeight, document.documentElement.scrollHeight,
        document.body.offsetHeight, document.documentElement.offsetHeight,
        document.body.clientHeight, document.documentElement.clientHeight
    );

    const visibleTop = scrollPosition + topOverlay;
    const visibleBottom = scrollPosition + windowHeight - bottomOverlay;

    console.log("HIGHLIGHT");
    console.log("header overlay: " + topOverlay);
    console.log("footer overlay: " + bottomOverlay);
    console.log("window height: " + windowHeight);
    console.log("scroll height: " + scrollHeight);
    console.log("scroll pos: " + scrollPosition);
    console.log("visible top: " + visibleTop);
    console.log("visible bottom: " + visibleBottom);
    console.log("anchor top: " + rectAnchor.top);
    console.log("anchor bottom: " + rectAnchor.bottom);

    var scroll = (rectAnchor.top + rectAnchor.bottom) / 2;
    console.log("scroll: " + scroll);
    //if (scroll < 110) {
    //    scroll += 130;
    //}
    //if (scroll > 1200) {
    //    scroll -= 130;
    //}

    console.log("scroll: " + scroll);

    if (scroll < 100 || scroll > 1210) {
        window.scrollBy({ top: scroll });
    }

    anchor.classList.add("breath");
    setTimeout(function () {
        anchor.classList.remove("breath");
    }, 2000);
}

interop.showSuccess = function (elementId) {
    const element = document.getElementById(elementId);
    element.classList.add("fip-success");
    setTimeout(function () {
        element.classList.remove("fip-success");
    }, 2000);
}

interop.showFailure = function (elementId) {
    const element = document.getElementById(elementId);
    element.classList.add("fip-failure");
    setTimeout(function () {
        element.classList.remove("fip-failure");
    }, 2000);
}