const registroForm = document.getElementById("registroForm");

if (registroForm) {
  registroForm.addEventListener("submit", async function (e) {
    e.preventDefault();

    const nombre = document.getElementById("nombre").value.trim();
    const correo = document.getElementById("correo").value.trim();
    const contrasena = document.getElementById("contrasena").value.trim();

    if (!nombre || !correo || !contrasena) {
      alertaAdvertencia("Por favor, complete todos los campos.");
      return;
    }

    const body = {
      id: 0,
      nombre,
      correo,
      contrasena,
      fechaRegistro: new Date().toISOString(),
    };

    try {
      const response = await fetch(ENDPOINTS.registrarUsuario, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(body),
      });

      if (response.ok) {
        alertaConfirmacion("Usuario registrado exitosamente.");
        setTimeout(() => {
          window.location.href = "../index.html";
        }, 2000); // Da tiempo a ver la alerta antes de redirigir
      } else {
        const error = await response.text();
        alertaError(error);
      }
    } catch (error) {
      console.error(error);
      alertaError("Ocurrió un error al conectar con el servidor.");
    }
  });
}
