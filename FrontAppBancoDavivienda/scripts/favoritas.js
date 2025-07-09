// Espera que todo el DOM esté cargado antes de ejecutar la lógica
document.addEventListener("DOMContentLoaded", () => {
  // 1. Verifica si hay un usuario logueado, si no, lo redirige al login
  const usuario = JSON.parse(localStorage.getItem("usuario"));
  if (!usuario) {
    window.location.href = "index.html";
    return;
  }

  // 2. Carga la lista de películas al iniciar
  cargarPeliculas();
});
// Función para consultar y mostrar las películas registradas desde el backend
async function cargarPeliculas() {
  const contenedor = document.getElementById("listaPeliculas");
  contenedor.innerHTML = "";

  const usuario = JSON.parse(localStorage.getItem("usuario"));
  if (!usuario || !usuario.id) {
    alertaError("Usuario no autenticado.");
    return;
  }

  try {
    const response = await fetch(
      `${ENDPOINTS.consultarPeliculas}?usuarioId=${usuario.id}`
    );

    if (!response.ok) {
      throw new Error("Error en la respuesta del servidor.");
    }

    const peliculas = await response.json();

    if (peliculas.length === 0) {
      alertaAdvertencia("No tienes películas registradas.");
      return;
    }

    for (const peli of peliculas) {
      const posterUrl = await buscarPoster(peli.titulo);

      const card = document.createElement("div");
      card.className = "bg-white rounded shadow p-4 flex flex-col";

      card.innerHTML = `
  <img src="${posterUrl}" alt="Poster" class="poster w-full h-64 object-cover rounded mb-4 cursor-pointer" 
       onerror="this.src='https://placehold.co/300x450?text=Sin+Imagen'" />
  <h3 class="text-xl font-bold">${peli.titulo}</h3>
  <p><strong>Año:</strong> ${peli.anio}</p>
  <p><strong>Director:</strong> ${peli.director}</p>
  <p><strong>Género:</strong> ${peli.genero}</p>
  <button class="mt-4 bg-red-600 text-white p-2 rounded hover:bg-red-700" 
          onclick="eliminarPelicula(${peli.id})">Eliminar</button>
`;

      const poster = card.querySelector("img");
      poster.addEventListener("click", () => abrirFormularioEdicion(peli));

      contenedor.appendChild(card);

      contenedor.appendChild(card);
    }
  } catch (error) {
    console.error("Error al cargar películas:", error);
    alertaError("Ocurrió un error al cargar las películas.");
  }
}
// Consulta a OMDb para obtener el póster.
async function buscarPoster(titulo) {
  try {
    const response = await fetch(ENDPOINTS.omdbApi(titulo));
    const data = await response.json();

    if (data.Response === "True" && data.Search.length > 0) {
      const primerResultado = data.Search[0];
      return primerResultado.Poster !== "N/A"
        ? primerResultado.Poster
        : "https://placehold.co/300x450?text=Sin+Imagen";
    }

    return "https://placehold.co/300x450?text=Sin+Imagen";
  } catch {
    return "https://placehold.co/300x450?text=Sin+Imagen";
  }
}
// Elimina una película favorita
async function eliminarPelicula(id) {
  try {
    const response = await fetch(ENDPOINTS.eliminarPelicula(id), {
      method: "DELETE",
    });

    if (response.ok) {
      alertaConfirmacion("Película eliminada correctamente.");
      cargarPeliculas(); // Recarga la lista
    } else {
      const error = await response.text();
      alertaError("No se pudo eliminar la película. " + error);
    }
  } catch (error) {
    console.error(error);
    alertaError("Error de conexión al eliminar la película.");
  }
}
// Consulta completa a OMDb con todos los datos posibles
async function obtenerDatosOMDb(titulo) {
  try {
    const response = await fetch(ENDPOINTS.omdbApi(titulo));
    const data = await response.json();
    return data;
  } catch {
    return {};
  }
}
// Cierra la sesión del usuario
function cerrarSesion() {
  localStorage.removeItem("usuario");
  window.location.href = "../index.html";
}
// Limpia el input
function limpiarBusqueda() {
  const input = document.getElementById("inputBusqueda");
  input.value = "";
  document.getElementById("listaPeliculas").innerHTML = "";
}
// Maneja el envío del formulario de búsqueda
const formEditar = document.getElementById("formEditarPelicula");
// Maneja el envío del formulario de edición
formEditar.addEventListener("submit", async (e) => {
  e.preventDefault();

  const id = document.getElementById("editarId").value;
  const titulo = document.getElementById("editarTitulo").value.trim();
  const anio = document.getElementById("editarAnio").value.trim();
  const director = document.getElementById("editarDirector").value.trim();
  const genero = document.getElementById("editarGenero").value.trim();
  const idioma = document.getElementById("editarIdioma").value.trim();
  const pais = document.getElementById("editarPais").value.trim();
  const duracion = document.getElementById("editarDuracion").value.trim();
  const calificacionIMDB = document
    .getElementById("editarCalificacion")
    .value.trim();
  const premios = document.getElementById("editarPremios").value.trim();
  const actores = document.getElementById("editarActores").value.trim();
  const posterUrl = document.getElementById("editarPosterUrl").value.trim();
  const sinopsis = document.getElementById("editarSinopsis").value.trim();
  const imdbID = document.getElementById("editarImdbID").value.trim();

  if (
    !titulo ||
    !anio ||
    !director ||
    !genero ||
    !idioma ||
    !pais ||
    !duracion ||
    !calificacionIMDB ||
    !premios ||
    !actores ||
    !posterUrl ||
    !sinopsis
  ) {
    alertaAdvertencia("Todos los campos son obligatorios.");
    return;
  }

  const usuario = JSON.parse(localStorage.getItem("usuario"));
  if (!usuario || !usuario.id) {
    alertaError("Usuario no autenticado.");
    return;
  }

  const body = {
    id,
    usuarioId: usuario.id,
    titulo,
    anio,
    director,
    genero,
    idioma,
    pais,
    duracion,
    calificacionIMDB,
    premios,
    actores,
    posterUrl,
    sinopsis,
    imdbID,
    fechaAgregada: new Date().toISOString(),
  };

  try {
    const response = await fetch(ENDPOINTS.actualizarPelicula, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(body),
    });

    if (response.ok) {
      alertaConfirmacion("Película actualizada correctamente.");
      cancelarEdicion();
      cargarPeliculas();
    } else {
      const error = await response.text();
      alertaError("Error al actualizar la película: " + error);
    }
  } catch (err) {
    console.error("Error en actualización:", err);
    alertaError("Error de conexión al actualizar la película.");
  }
});
// Cancela la edición y oculta el formulario
function cancelarEdicion() {
  document.getElementById("formEditarPelicula").reset();
  document.getElem;
  entById("formularioEditar").classList.add("hidden");
}
// Abre el formulario de edición con los datos de la película
function abrirFormularioEdicion(pelicula) {
  document.getElementById("formularioEditar").classList.remove("hidden");

  document.getElementById("editarId").value = pelicula.id;
  document.getElementById("editarTitulo").value = pelicula.titulo || "";
  document.getElementById("editarAnio").value = pelicula.anio || "";
  document.getElementById("editarDirector").value = pelicula.director || "";
  document.getElementById("editarGenero").value = pelicula.genero || "";
  document.getElementById("editarIdioma").value = pelicula.idioma || "";
  document.getElementById("editarPais").value = pelicula.pais || "";
  document.getElementById("editarDuracion").value = pelicula.duracion || "";
  document.getElementById("editarCalificacion").value =
    pelicula.calificacionIMDB || "";
  document.getElementById("editarPremios").value = pelicula.premios || "";
  document.getElementById("editarActores").value = pelicula.actores || "";
  document.getElementById("editarPosterUrl").value = pelicula.posterUrl || "";
  document.getElementById("editarSinopsis").value = pelicula.sinopsis || "";
  document.getElementById("editarImdbID").value = pelicula.imdbID || "";
}
// Agrega el evento al poster de la tarjeta para abrir el formulario de edición
const poster = card.querySelector("img");
poster.addEventListener("click", () => abrirFormularioEdicion(peli));
