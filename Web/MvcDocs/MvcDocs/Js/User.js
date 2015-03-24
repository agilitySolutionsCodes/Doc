//Account Login 
function ValidateEmail(emailAdress) {
    var objEmail = document.getElementById("Login");
    regex = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (regex.test(objEmail.value)) {
        objEmail.setCustomValidity("");
        return true;
    }
    else {
        objEmail.setCustomValidity("Informe um E-mail válido");
        return false;
    }
}

function ValidateProfile() {
    var obj = document.getElementById("Perfil");
    var v = obj.value;
    if (v == 0) {
        obj.setCustomValidity("Selecione um Perfil");
        return false;
    }
    else {
        obj.setCustomValidity("");
        return true;
    }
    return;
}

function AlertHey() {
    alert("Hey");
}