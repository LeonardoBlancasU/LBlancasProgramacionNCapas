function LimpiarCampos() {
    $('#txtNombre').val('');
    $('#txtApellidoPaterno').val('');
    $('#txtApellidoMaterno').val('');
    $('#ddlRol').prop('selectedIndex', 0); // Reinicia el dropdown
    $('#formBusquedaAbierta form').submit();
}