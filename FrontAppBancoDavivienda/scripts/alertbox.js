const alertbox = {
  render: function ({
    alertIcon,
    title,
    message,
    btnTitle,
    themeColor,
    btnColor,
  }) {
    // Elimina si ya hay una alerta activa
    const existingAlert = document.getElementById("customAlertBox");
    if (existingAlert) existingAlert.remove();

    // Crear contenedor
    const box = document.createElement("div");
    box.id = "customAlertBox";
    box.className =
      "fixed inset-0 flex justify-center items-center z-50 bg-black bg-opacity-50";

    // Crear contenido
    const modal = document.createElement("div");
    modal.className =
      "bg-white rounded-lg shadow-lg p-6 w-80 text-center animate-fadeIn";
    modal.style.borderTop = `6px solid ${themeColor}`;

    // Íconos
    let iconEmoji = "ℹ️";
    if (alertIcon === "success") iconEmoji = "✅";
    if (alertIcon === "error") iconEmoji = "❌";
    if (alertIcon === "warning") iconEmoji = "⚠️";

    modal.innerHTML = `
      <div class="text-4xl mb-2">${iconEmoji}</div>
      <h2 class="text-xl font-bold mb-2">${title}</h2>
      <p class="text-gray-700 mb-4">${message}</p>
      <button id="alertBtn" class="px-4 py-2 rounded text-white font-semibold" style="background-color: ${btnColor};">
        ${btnTitle}
      </button>
    `;

    // Agregar a la página
    box.appendChild(modal);
    document.body.appendChild(box);

    // Cerrar al hacer clic
    document.getElementById("alertBtn").onclick = function () {
      box.remove();
    };
  },
};
