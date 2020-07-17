/* Toggle between showing and hiding the navigation menu links when the user clicks on the hamburger menu / bar icon */
function showMenu() {
    var x = document.getElementById("myLinks");
    if (x.style.display === "block") {
        x.style.display = "none";
    } else {
        x.style.display = "block";
    }
}

function doNavigate() {
        window.location.href = document.getElementById("UrlList").value;
}

function doNavigateMobile() {
    window.location.href = document.getElementById("UrlListMobile").value;
    }