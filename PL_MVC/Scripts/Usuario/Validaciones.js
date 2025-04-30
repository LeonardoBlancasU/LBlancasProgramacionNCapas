//$(document).ready(function () {

//    DesactivarEnter(event);

//});
function DesactivarEnter(event) {
    if (event.charCode == 13) {
        event.preventDefault();
    }
}
function ValidarSoloLetras(event, LabelId) { 
    var noEnter = DesactivarEnter(event);
    var letra = event.key;
    var regularExpression = /^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]$/;
    
    if (regularExpression.test(letra)) {
        $("#" + labelId).text("");
        return true;
    }
    else {
        $("#" + LabelId).text("Solo se permiten letras")
        return false;
    }
}

function ValidarSoloNumeros(event, LabelId) {
    var noEnter = DesactivarEnter(event);
    var numero = event.key;
    var regularExpression = /^[0-9]+$/;
    
    if (regularExpression.test(numero)) {
        $("#" + LabelId).text("")
        return true;
    }
    else {
        $("#" + LabelId).text("Solo se permiten numeros")
        return false;
    }
}

function ValidarNumeroLetrasGuionBajo(event, LabelId) {
    var username = event.key;
    var regularExpression = /^[a-zA-Z0-9_]+$/;
    if (regularExpression.test(username)) {
        $("#" + LabelId).text("")
        return true;
    }
    else {
        $("#" + LabelId).text("Solo se permiten letras, numeros y guion bajo")
        return false;
    }
}

function ValidarSexo(event) {
    var noEnter = DesactivarEnter(event);
    $("#lblErrorSexo").text("")
    var letra = event.key.toUpperCase();
    var input = document.getElementById("txtSexo").value;
    if (input.length < 1) {
        if (letra === 'M' || letra === 'F') {
            event.target.value = letra;
            return false;
        } else {
            $("#lblErrorSexo").text("Solo se permiten M o F")
            return false;
        }
    }
    else {
        return false;
    }
}

function ValidarPrimerLetraMayuscula(event, inputId, labelId) {
    var input = $("#" + inputId).val();
    var caracter = event.key;
    var regularExpression = /^[A-Z]/;
    if (input.length < 1) {
        if (!regularExpression.test(caracter)) {
            $("#" + labelId).text("Debe comenzar con mayúscula");
            return false;
        } else {
            return true;
        }
    } else {
        return true;
    }
}

function ConfirmarTextbox(inputId, TextBoxId, labelId) {
    $("#" + TextBoxId).css({ 'border': 'dark', 'color': 'dark' });
    var input = $("#" + inputId).val();
    var textBox = $("#" + TextBoxId).val();
    if (input !== textBox) {
        $("#" + labelId).text("No coinciden");
        $("#" + TextBoxId).css({ 'border': '3px solid red', 'color': 'red' });
        return false;
    } else {
        $("#" + labelId).text("");
        $("#" + TextBoxId).css({ 'border': '3px solid green', 'color': 'green'});
        return true;
    }
}

//function ActivarTextbox(inputId, TextBoxId) {
//    var input = $("#"+ inputId).val();
//    var confirmInput = $("#" + TextBoxId);
//    if (input.length > 0) {
//        confirmInput.show();
//        confirmInput.prop('disabled', false);
//    } else {
//        confirmInput.hide();
//        confirmInput.prop('disabled', true);
//    }
//    return true;
//}

function ValidarNumerosTelefonicos(event) {
    var noEnter = DesactivarEnter(event);
    var input = event.target;
    var value = input.value.replace(/\D/g, '');

    if (value.length <= 2) {
        input.value = value;
    } else if (value.length <= 6) {
        input.value = `${value.slice(0, 2)}-${value.slice(2)}`;
    } else if (value.length < 10) {
        input.value = `${value.slice(0, 2)}-${value.slice(2, 6)}-${value.slice(6, 10)}`;
    } else {
        return false;
    }
    
    return true;
}

function ValidarPassword(inputId, labelId, TextboxId, long) {
    var input = $("#" + inputId).val();
    var confirm = $("#" + TextboxId);
    var repetidos = /(\d)\1/;
    var secuenciaAscendente = /(?:0(?=1)|1(?=2)|2(?=3)|3(?=4)|4(?=5)|5(?=6)|6(?=7)|7(?=8)|8(?=9)){3,}/;
    var secuenciaDescendente = /(?:9(?=8)|8(?=7)|7(?=6)|6(?=5)|5(?=4)|4(?=3)|3(?=2)|2(?=1)|1(?=0)){3,}/;
    if (!/[A-Z]/.test(input)) {
        $("#" + labelId).text("Debe tener al menos una Mayuscula");
        confirm.hide();
        confirm.prop('disabled', true);
        $("#" + inputId).css({ 'border': '3px solid red', 'color': 'red' });
    }
    else if (repetidos.test(input) || secuenciaAscendente.test(input) || secuenciaDescendente.test(input)) {
        $("#" + labelId).text("No debe tener numeros consecutivos o repetidos");
        confirm.hide();
        confirm.prop('disabled', true);
        $("#" + inputId).css({ 'border': '3px solid red', 'color': 'red' });
    }
    else if (!/[a-z]/.test(input)) {
        $("#" + labelId).text("Debe tener al menos una minuscula");
        confirm.hide();
        confirm.prop('disabled', true);
        $("#" + inputId).css({ 'border': '3px solid red', 'color': 'red' });
    }
    else if (!/[@$!%*?&]/.test(input)) {
        $("#" + labelId).text("Debe tener al menos un Caracter Especial (@$!%*?&)");
        confirm.hide();
        confirm.prop('disabled', true);
        $("#" + inputId).css({ 'border': '3px solid red', 'color': 'red' });
    } 
    else if (!/\d/.test(input)) {
        $("#" + labelId).text("Debe tener al menos un numero");
        confirm.hide();
        confirm.prop('disabled', true);
        $("#" + inputId).css({ 'border': '3px solid red', 'color': 'red' });
    } 
    else if (input.length < long) {
        $("#" + labelId).text(`Debe tener al menos ${long} caracteres`);
        confirm.hide();
        confirm.prop('disabled', true);
        $("#" + inputId).css({ 'border': '3px solid red', 'color': 'red' });
    } 
    else {   
        confirm.show();
        confirm.prop('disabled', false);
        $("#" + inputId).css({ 'border': '3px solid green', 'color': 'green' });
        $("#" + labelId).text("");
    }
}

function ValidarUserName(event, labelId, inputId, long) {
    $("#" + inputId).css({ 'border': 'dark', 'color': 'dark' });
    var letra = event.key;
    if (letra == "Enter") {
        event.preventDefault();
        return false;
    }
    else {
        input = $("#" + inputId).val();
        if (input.length < 1) {
            if (!/^[A-Z]/.test(letra)) {
                $("#" + labelId).text("La Primer Letra debe ser Mayuscula");
                $("#" + inputId).css({ 'border': '3px solid red', 'color': 'red' });
                return false;
                } else {
                return true;
                }
        } else {
            input = input + letra;
            if (!/\d/.test(input)) {
                $("#" + labelId).text("Debe tener al menos un numero");
                $("#" + inputId).css({ 'border': '3px solid red', 'color': 'red' });
            } else {
                if (input.length < long) {
                    $("#" + labelId).text(`Debe tener al menos ${long} Caracteres`);
                    $("#" + inputId).css({ 'border': '3px solid red', 'color': 'red' });
                } else {
                    $("#" + labelId).text("");
                    $("#" + inputId).css({ 'border': '3px solid green', 'color': 'green' });
                }
            }
            return true;
        }
    }
}
function AgregarGuion(event, inputId) {
    input = $("#" + inputId).val();
    var guion = "-";
    event.target.value = input + guion;
    return true;
}
function ValidarEmail(inputId, TextBoxId, labelId) {
    $("#" + inputId).css({ 'border': 'dark', 'color': 'dark' });
    var input = $("#" + inputId).val();
    var expression = /[a-zA-Z0-9.*%±]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,}/;
    var confirmEmail = $("#" + TextBoxId);
    if (expression.test(input)) {
        confirmEmail.show();
        confirmEmail.prop('disabled', false);
        $("#" + labelId).text("");
        $("#" + inputId).css({ 'border': '3px solid green', 'color': 'green' });
        return true;
    }
    else {
        confirmEmail.hide();
        confirmEmail.prop('disabled', true);
        $("#" + inputId).css({ 'border': '3px solid red', 'color': 'red' });
        $("#" + labelId).text("Email no valido");
        return false
    }
}

function ValidarCurp(inputId, labelId) {
    $("#" + inputId).css({ 'border': 'dark', 'color': 'dark' });
    var input = $("#" + inputId).val().toUpperCase();
    var regex = /^[A-Z]{4}[0-9]{6}[HM][A-Z]{5}[0-9A-Z]\d$/;
    if (regex.test(input)) {
        $("#" + inputId).css({ 'border': '3px solid green', 'color': 'green' });
        $("#" + labelId).text("");
        return true;
    }
    else {
        $("#" + inputId).css({ 'border': '3px solid red', 'color': 'red' });
        $("#" + labelId).text("CURP no valido");
        return false;
    }
}