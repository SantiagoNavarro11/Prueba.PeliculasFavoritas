// catalogo.js

// Espera que todo el DOM esté cargado antes de ejecutar la lógica
document.addEventListener("DOMContentLoaded", () => {
  // 1. Verifica si hay un usuario logueado, si no, lo redirige al login
  const usuario = JSON.parse(localStorage.getItem("usuario"));
  if (!usuario) {
    window.location.href = "index.html";
    return;
  }

  // 3. Manejo del formulario de agregar película
  const form = document.getElementById("formPelicula");
  form.addEventListener("submit", async (e) => {
    e.preventDefault();

    debugger;
    // 3.1 Obtener valores del formulario
    const titulo = document.getElementById("titulo").value.trim();
    const anio = document.getElementById("anio").value.trim();
    const director = document.getElementById("director").value.trim();
    const genero = document.getElementById("genero").value.trim();

    if (!titulo || !anio || !director || !genero) {
      alert("Por favor completa todos los campos.");
      return;
    }

    // 3.2 Consultar datos extras desde OMDb
    const omdbInfo = await obtenerDatosOMDb(titulo);

    // 3.3 Armar el objeto para enviar al backend
    const nuevaPelicula = {
      usuarioId: usuario.id,
      titulo,
      anio,
      director,
      genero,
      imdbID: "",
      fechaAgregada: new Date().toISOString(),
    };

    // 3.4 Enviar al backend
    try {
      console.log("Pelicula que se enviará al backend:", nuevaPelicula);

      const response = await fetch(ENDPOINTS.crearPelicula, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(nuevaPelicula),
      });

      if (response.ok) {
        alertaConfirmacion("Película guardada con éxito.");
        form.reset();
        limpiarBusqueda();
      } else {
        const errorTexto = await response.text();
        alertaError("Error al guardar la película: " + errorTexto);
      }
    } catch (error) {
      console.error("Error de red al guardar película:", error);
      alertaError("Error de conexión al guardar la película.");
    }
  });
});

// 4. Función para consultar y mostrar las películas registradas
async function cargarPeliculas() {
  const contenedor = document.getElementById("listaPeliculas");
  contenedor.innerHTML = "";

  try {
    const response = await fetch(ENDPOINTS.consultarPeliculas);
    const peliculas = await response.json();

    for (const peli of peliculas) {
      const posterUrl = await buscarPoster(peli.titulo);

      const card = document.createElement("div");
      card.className = "bg-white rounded shadow p-4 flex flex-col";

      card.innerHTML = `
        <img src="${posterUrl}" alt="Poster" class="w-full h-64 object-cover rounded mb-4" onerror="this.src='https://placehold.co/300x450?text=Sin+Imagen'" />
        <h3 class="text-xl font-bold">${peli.titulo}</h3>
        <p><strong>Año:</strong> ${peli.anio}</p>
        <p><strong>Director:</strong> ${peli.director}</p>
        <p><strong>Género:</strong> ${peli.genero}</p>
        <button class="mt-4 bg-red-600 text-white p-2 rounded hover:bg-red-700" onclick="eliminarPelicula(${peli.id})">Eliminar</button>
      `;

      contenedor.appendChild(card);
    }
  } catch (error) {
    console.error("Error al cargar películas:", error);
  }
}

// 5. Función que consulta a OMDb para obtener el póster (por si el backend no lo tiene)
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

// 6. Elimina una película favorita
async function eliminarPelicula(id) {
  if (!confirm("¿Estás seguro de eliminar esta película?")) return;

  try {
    const response = await fetch(ENDPOINTS.eliminarPelicula(id), {
      method: "DELETE",
    });
    if (response.ok) {
    } else {
      limpiarBusqueda();
      alert("No se pudo eliminar.");
    }
  } catch (error) {
    console.error(error);
    alert("Error al eliminar.");
  }
}

// 7. Consulta completa a OMDb (devuelve todos los datos posibles)
async function obtenerDatosOMDb(titulo) {
  try {
    const response = await fetch(ENDPOINTS.omdbApi(titulo));
    const data = await response.json();
    return data;
  } catch {
    return {}; // Si falla, devuelve objeto vacío
  }
}

// 8. Cierra la sesión del usuario
function cerrarSesion() {
  localStorage.removeItem("usuario");
  window.location.href = "../index.html";
}

const inputTitulo = document.getElementById("titulo");
const listaSug = document.getElementById("sugerencias");

// Evento de escritura
inputTitulo.addEventListener("input", async () => {
  const query = inputTitulo.value.trim();
  listaSug.innerHTML = "";
  if (!query) return;

  try {
    const res = await fetch(ENDPOINTS.omdbApi(query));
    const data = await res.json();
    if (data.Response === "True") {
      data.Search.forEach((movie) => {
        const li = document.createElement("li");
        li.className = "cursor-pointer p-2 hover:bg-gray-200 flex items-center";
        li.innerHTML = `
          <img src="${movie.Poster}" alt="póster" class="w-12 h-16 object-cover mr-2" onerror="this.src='https://placehold.co/300x450?text=Sin+Imagen'" />
          <span>${movie.Title} (${movie.Year})</span>
        `;
        li.addEventListener("click", () => seleccionarPelicula(movie.imdbID));
        listaSug.appendChild(li);
      });
    }
  } catch (err) {
    console.error("Error en sugerencias:", err);
  }
});

async function seleccionarPelicula(imdbID) {
  listaSug.innerHTML = "";

  try {
    const res = await fetch(ENDPOINTS.omdbDetalle(imdbID));
    const info = await res.json();

    inputTitulo.value = info.Title;
    document.getElementById("anio").value = info.Year;
    document.getElementById("director").value = info.Director;
    document.getElementById("genero").value = info.Genre;

    // Guardamos los datos OMDb completos para enviar al backend
    window.omdbSeleccion = info;
  } catch (err) {
    console.error("Error al  cargar detalles:", err);
  }
}

async function buscarPostersMultiples(titulo, maxPaginas = 3) {
  const resultados = [];

  for (let page = 1; page <= maxPaginas; page++) {
    try {
      const res = await fetch(ENDPOINTS.omdbApi(titulo, page));
      const data = await res.json();

      if (data.Response === "True") {
        resultados.push(...data.Search);
      } else {
        break; // Si no hay más resultados, se sale del bucle
      }
    } catch (err) {
      console.error("Error en página:", page, err);
    }
  }

  return resultados;
}

async function mostrarSugerenciasMultiples(titulo) {
  const sugerencias = await buscarPostersMultiples(titulo, 3);
  const contenedor = document.getElementById("listaPeliculas");
  contenedor.innerHTML = "";

  const usuario = JSON.parse(localStorage.getItem("usuario"));

  for (const peli of sugerencias) {
    const card = document.createElement("div");
    card.className = "bg-white rounded shadow p-4 flex flex-col";

    card.innerHTML = `
      <img src="${peli.Poster}" alt="Poster" class="w-full h-64 object-cover rounded mb-4"
           onerror="this.src='https://placehold.co/300x450?text=Sin+Imagen'" />
      <h3 class="text-xl font-bold">${peli.Title}</h3>
      <p><strong>Año:</strong> ${peli.Year}</p>
      <p><strong>IMDB ID:</strong> ${peli.imdbID}</p>
      <button class="mt-2 bg-green-600 text-white p-2 rounded hover:bg-green-700"
              onclick="guardarPeliculaOMDb('${peli.imdbID}')">Guardar</button>
    `;

    contenedor.appendChild(card);
  }
}

function buscarDesdeInput() {
  const titulo = document.getElementById("inputBusqueda").value.trim();
  if (titulo) {
    mostrarSugerenciasMultiples(titulo);
  }
}
/**
 * Guarda una película consultada desde OMDb en el backend del usuario actual.
 * @param {string} imdbID - ID único de la película en OMDb.
 */
async function guardarPeliculaOMDb(imdbID) {
  const usuario = JSON.parse(localStorage.getItem("usuario"));
  if (!usuario) {
    alertaAdvertencia("Debes iniciar sesión para guardar películas.");
    return;
  }

  try {
    // Consulta los detalles completos de la película desde OMDb
    const res = await fetch(ENDPOINTS.omdbDetalle(imdbID));
    const data = await res.json();

    const nuevaPelicula = {
      usuarioId: usuario.id,
      titulo: data.Title,
      anio: data.Year,
      director: data.Director,
      genero: data.Genre,
      imdbID: data.imdbID,
      posterUrl: data.Poster,
      sinopsis: data.Plot,
      calificacionIMDB: data.imdbRating,
      duracion: data.Runtime,
      idioma: data.Language,
      pais: data.Country,
      actores: data.Actors,
      premios: data.Awards,
      fechaAgregada: new Date().toISOString(),
    };

    // Envío de datos al backend
    const guardar = await fetch(ENDPOINTS.crearPelicula, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(nuevaPelicula),
    });

    if (guardar.ok) {
      alertaConfirmacion("Película guardada con éxito.");
      limpiarBusqueda();
    } else {
      const errorTexto = await guardar.text();
      alertaError("No se pudo guardar la película: " + errorTexto);
      limpiarBusqueda();
    }
  } catch (err) {
    console.error("Error al guardar:", err);
    alertaError("Error de red al intentar guardar la película.");
  }
}

function limpiarBusqueda() {
  const input = document.getElementById("inputBusqueda");
  input.value = "";
  document.getElementById("listaPeliculas").innerHTML = "";
  document.getElementById("director").value = "";
  document.getElementById("genero").value = "";
  document.getElementById("titulo").value = "";
  document.getElementById("anio").value = "";
}
