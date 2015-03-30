//Account Login 
function validateEmail(emailAdress) {
    var objEmail = document.getElementById("Login");
    regex = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (regex.test(objEmail.value)) {
        

        checkEmailExists(objEmail.value);
    }
    else {
        objEmail.setCustomValidity("Informe um E-mail válido");
        return false;
    }
}

function checkEmailExists(email) {
    $.ajax({
        type: "POST",
        url: "Account/CheckEmailExist/",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        data: '{"cEmail":"' + email + '"}',
        success: callBackCheckEmail
    });
}

function callBackCheckEmail(data) {
    if (!data) {
        objEmail = document.getElementById("Login");
        objEmail.setCustomValidity("Ops esse e-mail não foi localizado.");
    }
}

function validateProfile() {
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

