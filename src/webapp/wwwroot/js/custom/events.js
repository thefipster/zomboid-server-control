window.onscroll = function () {
    var scrollTop = Math.max(document.body.scrollTop, document.documentElement.scrollTop);

    if (scrollTop > 60) {
        document.getElementById("page-header").classList.add("shrink");
        document.getElementById("fips-app").classList.add("shrink");
    }
    
    if (scrollTop < 1) {
        document.getElementById("page-header").classList.remove("shrink");
        document.getElementById("fips-app").classList.remove("shrink");
    }
};
