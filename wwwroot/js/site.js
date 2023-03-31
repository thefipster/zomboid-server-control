var interop = interop || {};
var syncObj = {};

function readMods() {
    const items = document.querySelectorAll(".mod-item");
    let mods = [];
    for (const mod of items) {
        let modObj = {
            workshopId: mod.id,
            name: mod.dataset.fipModname,
            id: mod.dataset.fipModid
        }

        mods.push(modObj);
        console.log(modObj);
    }

    syncObj.invokeMethodAsync('syncMods', mods);
}

function showLogs() {
    document.getElementById("fip-logs").style.display = "block";
    document.getElementById("fip-mods").style.display = "none";
    document.getElementById("add-btn").disabled = true;
    document.getElementById("restart-btn").disabled = true;
}

function showMods() {
    document.getElementById("fip-logs").style.display = "none";
    document.getElementById("fip-mods").style.display = "block";
    document.getElementById("add-btn").disabled = false;
    document.getElementById("restart-btn").disabled = false;
}

interop.syncInstance = function (dotNetObject) {
    syncObj = dotNetObject;
};

interop.onAfterRender = function () {
    const modList = document.getElementById('fip-mod-list');
    new Sortable(modList, {
        animation: 150,
        onUpdate: function (evt) {
            readMods();
        }
    });
};

interop.showLogs = function () {
    showLogs();
};

interop.showMods = function () {
    showMods();
};
