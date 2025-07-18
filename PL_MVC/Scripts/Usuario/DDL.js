function FillMunicipios() {
    let ddlIdEstado = $("#ddlEstado").val();
    $.ajax({
        type: 'GET',
        url: '/Usuario/MunicipioGetByIdEstado',
        dataType: 'json',

        contentType: 'application/json',
        data: { IdEstado: ddlIdEstado },
        success: function (result) {
            if (result.Correct) {
                $("#ddlMunicipio").empty();
                $("#ddlColonia").empty();
                $("#ddlMunicipio").append('<option value="0">' + 'Seleccione un Municipio' + '</option>');
                $("#ddlColonia").append('<option value="0">' + 'Seleccione una Colonia' + '</option>');
                $.each(result.Objects, function (i, municipios) {
                    $("#ddlMunicipio").append('<option value="' + municipios.IdMunicipio + '">' + municipios.Nombre + '</option>');
                });
            }
        },
        error: function (ex) {
            alert('Failed.' + ex);
        }
    });
}

function FillColonias() {
    let ddlIdMunicipio = $("#ddlMunicipio").val();
    $.ajax({
        type: 'GET',
        url: '/Usuario/ColoniaGetByIdMunicipio',
        dataType: 'json',

        contentType: 'application/json',
        data: { IdMunicipio: ddlIdMunicipio },
        success: function (result) {
            if (result.Correct) {
                $("#ddlColonia").empty();
                $("#ddlColonia").append('<option value="0">' + 'Seleccione una Colonia' + '</option>');
                $.each(result.Objects, function (i, colonias) {
                    $("#ddlColonia").append('<option value="' + colonias.IdColonia + '">' + colonias.Nombre + '</option>');
                });
            }
        },
        error: function (ex) {
            alert('Failed.' + ex);
        }
    });
}