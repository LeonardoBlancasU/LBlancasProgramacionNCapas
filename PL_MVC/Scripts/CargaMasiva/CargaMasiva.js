$(document).ready(function () {
    $('input[name="rdobtnCargaMasiva"]').on('change', function () {
        $("#inputCargaMasiva").val('').prop('disabled', false);
        //$("#btnValidar").prop('disabled', true);
        //$("#btnCargar").prop('disabled', true);
    });
});

function ValidarArchivo(event) {
    var archivo = event.target.files[0];
    var radio = $('input[name="rdobtnCargaMasiva"]:checked').val();
    $("#btnValidar").prop('disabled', true);

    if (radio == 'TXT') {
        var extension = archivo.name.split('.').pop().toLowerCase();
        var extensionPermitida = ["txt"];

        if (extensionPermitida.includes(extension)) {
            $("#btnValidar").prop('disabled', false);
        }
        else {
            alert("Archivo no Valido. Solo se permite TXT")
            event.target.value = "";
        }
    }
    if (radio == 'EXCEL') {
        var extension = archivo.name.split('.').pop().toLowerCase();
        var extensionPermitida = ["xls", "xlsx"];

        if (extensionPermitida.includes(extension)) {
            $("#btnValidar").prop('disabled', false);
        }
        else {
            alert("Archivo no Valido. Solo se permite EXCEL")
            event.target.value = "";
        }
    }
}