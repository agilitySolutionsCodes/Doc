//Account Login 
function validateEmailAuthenticate(emailAdressL) {
    var objEmailL = document.getElementById("Login");
    regexL = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

    if (!regexL.test(objEmailL.value)) {
        objEmailL.setCustomValidity("Informe um E-mail válido");
        objEmailL.validity.customError;
        return false;
    }
    else {
        checkEmailExistsAuthenticate(objEmailL.value);
    }
}
function checkEmailExistsAuthenticate(emailAdressL) {
    $.ajax({
        type: "POST",
        url: "Account/CheckEmailExist/",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        data: '{"cEmail":"' + emailAdressL + '"}',
        success: callBackCheckEmailAuthenticate
    });
}
function callBackCheckEmailAuthenticate(data) {
    var objEmailAuthenticate = document.getElementById("Login");
    if (!data) {
        objEmailAuthenticate.setCustomValidity("Ops esse e-mail não foi localizado.");
        return false;
    }
    else {
        objEmailAuthenticate.setCustomValidity("");
        return true;
    }
}

//Account Register
function validateEmailRegister(emailAdressR) {
    var objEmailR = document.getElementById("Email");
    regexR = /^[a-zA-Z0-9][a-zA-Z0-9\._-]+@([a-zA-Z0-9\._-]+\.)[a-zA-Z-0-9]{2}/;

    if (!regexR.test(emailAdressR)) {
        objEmailR.setCustomValidity("Informe um E-mail válido");
        return false;
    }
    else {
        checkEmailExists(objEmailR.value);
    }
}
function checkEmailExistsRegister(emailAdressR) {
    $.ajax({
        type: "POST",
        url: "Account/CheckEmailExist/",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        data: '{"cEmail":"' + emailAdressR + '"}',
        success: callBackCheckEmail
    });
}
function callBackCheckEmailRegister(data) {
    var objEmailRegister = document.getElementById("Login");
    if (data) {
        objEmailRegister.setCustomValidity("Ops esse e-mail já existe.");
        return false;
    }
    else {
        objEmailRegister.setCustomValidity("");
        return true;
    }
}
function validateProfile() {
    var obj = document.getElementById("Profile");
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

