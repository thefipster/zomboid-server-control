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

interop.showMods = function () {
    document.getElementById("fip-mods").style.display = "block";
    document.getElementById("fip-mods-diff").style.display = "none";

    document.getElementById("mods-add-btn").disabled = false;
    document.getElementById("mods-apply-btn").disabled = false;
};

interop.showModDiff = function () {
    document.getElementById("fip-mods").style.display = "none";
    document.getElementById("fip-mods-diff").style.display = "block";

    document.getElementById("mods-add-btn").disabled = true;
    document.getElementById("mods-apply-btn").disabled = false;
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
    //const overflow = document.getElementById(elementId);
    const anchor = document.getElementById(anchorName);
    anchor.scrollIntoView();

    //const rectOverflow = document.getBoundingClientRect();
    //const rectAnchor = anchor.getBoundingClientRect();

    //if (rectAnchor.top < rectOverflow.top) {
    //    document.scrollTop += rectAnchor.top - rectOverflow.top;
    //} else if (rectAnchor.bottom > rectOverflow.bottom) {
    //    document.scrollTop += rectAnchor.bottom - rectOverflow.bottom;
    //}

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