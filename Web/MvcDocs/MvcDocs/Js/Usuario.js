function ValidaPerfil() {
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