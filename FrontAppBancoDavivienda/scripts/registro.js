const registroForm = document.getElementById("registroForm");
// Verifica si el formulario de registro existe antes de agregar el evento
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
    // Verifica si el usuario ya existe
    try {
      const response = await fetch(ENDPOINTS.registrarUsuario, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(body),
      });
      // Verifica si la respuesta es exitosa
      if (response.ok) {
        alertaConfirmacion("Usuario registrado exitosamente.");
        setTimeout(() => {
          window.location.href = "../index.html";
        }, 2000);
      } else {
        const error = await response.json(); // <-- aquí procesas como JSON
        alertaError(error.mensaje || "Ocurrió un error.");
      }
    } catch (error) {
      console.error(error);
      alertaError("Ocurrió un error al conectar con el servidor.");
    }
  });
}
