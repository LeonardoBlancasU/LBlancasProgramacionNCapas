function UpdateEstatus(IdUsuario, Estatus) {
    $("#lblEstatusUpdate").text('').removeClass('alert alert-success alert-danger');
    $.ajax({
        type: 'GET',
        url: '/Usuario/UpdateIdEstatus',
        dataType: 'json',

        contentType: 'application/json',
        data: { idusuario: IdUsuario, estatus: Estatus },
        success: function (result) {
            if (result.Correct) {
                $("#lblEstatus_" + IdUsuario).text('' + Estatus);
                $("#lblEstatusUpdate").addClass('bi bi-check-all alert alert-success').text('Estatus Actualizado');
            }
            else {
                $("#SwitchEstatus_" + IdUsuario).prop('checked', !Estatus);
                $("#lblEstatusUpdate").addClass('bi bi-x-square alert alert-danger').text('Hubo un Error al Actualizar el Estatus');
            }
        },
        error: function (ex) {
            alert('Failed.' + ex);
        }
    });
}