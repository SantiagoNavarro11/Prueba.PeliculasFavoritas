const BASE_URL = "https://localhost:7056/api";
const OMDB_API_KEY = "ca833e20";
// Define los endpoints de la API
const ENDPOINTS = {
  login: `${BASE_URL}/Usuario/Login`,
  registrarUsuario: `${BASE_URL}/Usuario/InsertarUsuario`,
  consultarUsuarios: `${BASE_URL}/Usuario/ConsultarUsuarios`,
  actualizarUsuario: `${BASE_URL}/Usuario/ActualizarUsuario`,
  eliminarUsuario: (id) => `${BASE_URL}/Usuario/EliminarUsuario/${id}`,
  consultarPeliculasPorUsuario: (idUsuario) =>
    `${BASE_URL}/api/PeliculasFavoritas/ConsultarPeliculasFavoritas?usuarioId=${idUsuario}`,

  consultarPeliculas: `${BASE_URL}/PeliculasFavoritas/ConsultarPeliculasFavoritas`,
  crearPelicula: `${BASE_URL}/PeliculasFavoritas/CrearPeliculaFavorita`,
  actualizarPelicula: `${BASE_URL}/PeliculasFavoritas/ActualizarPeliculaFavorita`,
  eliminarPelicula: (id) =>
    `${BASE_URL}/PeliculasFavoritas/EliminarPeliculaFavorita/${id}`,

  omdbApi: (titulo, page = 1) =>
    `https://www.omdbapi.com/?apikey=ca833e20&s=${encodeURIComponent(
      titulo
    )}&page=${page}`,

  omdbDetalle: (imdbID) =>
    `https://www.omdbapi.com/?apikey=${OMDB_API_KEY}&i=${imdbID}&plot=short`,
};
