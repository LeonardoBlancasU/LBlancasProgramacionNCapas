function BusquedaAbierta() {
    var tabla = document.getElementById('tablaBusquedaAbierta');
    if (tabla.style.display === 'none' || tabla.style.display === '') {
        tabla.style.display = 'table';
    }
    else {
        tabla.style.display = 'none';
    }
}

function Buscar() {
    var Nombre = $("#txtNombre").val();
    var ApellidoPaterno = $("#txtApellidoPaterno").val();
    var ApellidoMaterno = $("#txtApellidoMaterno").val();
    var IdRol = $("#ddlIdRol").val();
    $.ajax({
        type: 'GET',
        url: '/Usuario/BusquedaAbierta',
        dataType: 'json',

        contentType: 'application/json',
        data: { nombre: Nombre, apellidoPaterno: ApellidoPaterno, apellidoMaterno: ApellidoMaterno, idRol: IdRol },
        success: function (result) {           
        },
        error: function (ex) {
            alert('Failed.' + ex);
        }
    });
}

function LimpiarCampos() {
    $("#txtNombre").text('');
    $("#txtApellidoPaterno").text('');
    $("#txtApellidoMaterno").text('');
    $("#ddlIdRol").empty();
    $("#ddlIdRol").append('<option value="0">' + 'Seleccione una Colonia' + '</option>');
    //$.each(result.Objects, function (i, roles) {
    //    $("#ddlIdRol").append('<option value="' + colonias.IdColonia + '">' + colonias.Nombre + '</option>');
    //});
}