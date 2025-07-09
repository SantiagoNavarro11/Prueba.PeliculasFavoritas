const loginForm = document.getElementById("loginForm");

if (loginForm) {
  loginForm.addEventListener("submit", async function (e) {
    e.preventDefault();

    const correo = document.getElementById("usuario").value.trim();
    const contrasena = document.getElementById("contrasena").value.trim();

    if (!correo || !contrasena) {
      alertaAdvertencia("Por favor, complete todos los campos.");
      return;
    }

    const body = { correo, contrasena };

    try {
      const response = await fetch(ENDPOINTS.login, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(body),
      });

      if (response.ok) {
        const data = await response.json();
        localStorage.setItem("usuario", JSON.stringify(data));
        alertaConfirmacion("Inicio de sesión exitoso.");
        setTimeout(() => {
          window.location.href = "../views/catalogo.html";
        }, 1500); // Tiempo para ver la alerta
      } else {
        const error = await response.json();
        const mensaje = error?.mensaje || "Correo o contraseña incorrectos.";
        alertaError(mensaje);
      }
    } catch (error) {
      console.error("Error al iniciar sesión:", error);
      alertaError("Ocurrió un error al conectar con el servidor.");
    }
  });
}
