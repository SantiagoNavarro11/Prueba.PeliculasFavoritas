function alertaConfirmacion(mensaje) {
  alertbox.render({
    alertIcon: "success",
    title: "Confirmación",
    message: mensaje,
    btnTitle: "Ok",
    themeColor: "#000000",
    btnColor: "#4CAF50",
  });
}

function alertaError(mensaje) {
  alertbox.render({
    alertIcon: "error",
    title: "Error",
    message: mensaje,
    btnTitle: "Ok",
    themeColor: "#000000",
    btnColor: "#ff4c33",
  });
}

function alertaAdvertencia(mensaje) {
  alertbox.render({
    alertIcon: "warning",
    title: "Advertencia",
    message: mensaje,
    btnTitle: "Ok",
    themeColor: "#000000",
    btnColor: "#FF9800",
  });
}
