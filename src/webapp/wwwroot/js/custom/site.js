var interop = interop || {};
var syncObj = {};
var scrollTimer;

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

    if (mods.length > 0)
        syncObj.invokeMethodAsync('syncMods', mods);
}

interop.syncInstance = function (dotNetObject) {
    syncObj = dotNetObject;
};

interop.onAfterRender = function () {
    const modList = document.getElementById('fip-mod-list');

    if (modList)
        new Sortable(modList, {
            animation: 150,
            onUpdate: function (evt) {
                interop.readMods();
            }
        });
};

interop.showLogs = function () {
    document.getElementById("fip-logs").style.display = "block";
    document.getElementById("fip-mods").style.display = "none";
    document.getElementById("fip-mods-diff").style.display = "none";

    document.getElementById("add-btn").disabled = true;
    document.getElementById("restart-btn").disabled = true;

    scrollTimer = window.setInterval(function () {
        const elem = document.getElementById('fip-log-list');
        elem.scrollTop = elem.scrollHeight;
    }, 1000);
};

interop.showMods = function () {
    document.getElementById("fip-logs").style.display = "none";
    document.getElementById("fip-mods").style.display = "block";
    document.getElementById("fip-mods-diff").style.display = "none";

    document.getElementById("add-btn").disabled = false;
    document.getElementById("restart-btn").disabled = false;

    window.clearInterval(scrollTimer);
};

interop.showModDiff = function () {
    document.getElementById("fip-logs").style.display = "none";
    document.getElementById("fip-mods").style.display = "none";
    document.getElementById("fip-mods-diff").style.display = "block";

    document.getElementById("add-btn").disabled = true;
    document.getElementById("restart-btn").disabled = false;

    window.clearInterval(scrollTimer);
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
    const overflow = document.getElementById(elementId);
    const anchor = document.getElementById(anchorName);

    const rectOverflow = overflow.getBoundingClientRect();
    const rectAnchor = anchor.getBoundingClientRect();

    overflow.scrollTop = rectAnchor.top - rectOverflow.top;

    anchor.classList.add("fip-flash");
    setTimeout(function () {
        anchor.classList.remove("fip-flash");
        anchor.classList.add("fip-flash-out");
        setTimeout(function () {
            anchor.classList.remove("fip-flash-out");
        }, 1000);
    }, 1000);
}