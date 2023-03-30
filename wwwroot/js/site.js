var interop = interop || {};
var syncObj = {};

function init() {


    var modList = document.getElementById('fip-mod-list');
    console.log("List Element");
    console.log(modList);
    if (modList) {
        var sortable = new Sortable(modList, {
            handle: '.handle', // handle's class
            animation: 150,
            onUpdate: function (evt) {
                console.log(evt);
                readMods();
            }
        });

        console.log("Sortable Object");
        console.log(sortable);
    }
}

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

window.onload = function () {
    init();
};

interop.syncInstance = function (dotNetObject) {
    syncObj = dotNetObject;
};