function set() {
    var myCookie = getCookie("userName");
    if(myCookie == null){
        val = window.location.href.split('/')[5];
        document.cookie = "userName=" + val;
    }

    else {
        alert("already set");
    }
}