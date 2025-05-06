function UpdateEstatus(IdUsuario, Estatus) {
    /*Estatus = $("#SwitchEstatus").val();*/
    $.ajax({
        type: 'POST',
        url: '@Url.Action("UpdateIdEstatus")',
        dataType: 'json',

        contentType: 'application/json',
        data: { idusuario: IdUsuario, estatus: Estatus },
        success: function (result) {
            if (result.Correct) {
                TempData["Success"] = "Estatus Actualizado Correctamente.";
            }
            else {
                TempData["Success"] = "Hubo un error al Actualizar el Estatus.";
            }
        },
        error: function (ex) {
            alert('Failed.' + ex);
        }
    });
}