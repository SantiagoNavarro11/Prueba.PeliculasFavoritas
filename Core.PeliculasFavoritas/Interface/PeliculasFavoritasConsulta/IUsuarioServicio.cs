namespace Core.PeliculasFavoritas.Interface.PeliculasFavoritasConsulta
{
    using Core.PeliculasFavoritas.Dto;
    using Core.PeliculasFavoritas.Entidades;
    using Core.PeliculasFavoritas.EntidadesPersonalizadas;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Define las operaciones de negocio relacionadas con la gestión de usuarios en el sistema de películas favoritas.
    /// </summary>
    public interface IUsuarioServicio
    {
        #region Instancias
        /// <summary>
        /// Consulta los registros de la entidad <see cref="Usuario"/> según los parámetros de búsqueda.
        /// </summary>
        /// <param name="parametrosBusqueda">Parámetros de entrada para la consulta de usuarios.</param>
        /// <returns>
        /// Una lista de objetos <see cref="UsuarioDto"/> que cumplen con los criterios de búsqueda.
        /// </returns>
        Task<IEnumerable<UsuarioDto>> ConsultarUsuarios(ParametrosUsuario parametrosBusqueda);

        /// <summary>
        /// Inserta un nuevo registro de usuario.
        /// </summary>
        /// <param name="usuario">Entidad <see cref="Usuario"/> a insertar.</param>
        /// <returns>Entidad insertada con su identificador asignado.</returns>
        Task<Usuario> InsertarUsuario(Usuario usuario);

        /// <summary>
        /// Actualiza un registro existente de Usuario.
        /// </summary>
        /// <param name="usuario">Entidad <see cref="Usuario"/> con los datos actualizados.</param>
        /// <returns>Valor booleano que indica si la actualización fue exitosa.</returns>
        Task<bool> ActualizarUsuario(Usuario usuario);

        /// <summary>
        /// Elimina un registro de Usuario según su identificador.
        /// </summary>
        /// <param name="id">Identificador único del usuario a eliminar.</param>
        /// <returns>Valor booleano que indica si la eliminación fue exitosa.</returns>
        Task<bool> EliminarUsuario(int id);

        /// <summary>
        /// Consulta un usuario específico por su identificador.
        /// </summary>
        /// <param name="id">Identificador del usuario a consultar.</param>
        /// <returns>Objeto <see cref="UsuarioDto"/> correspondiente al registro solicitado.</returns>
        Task<UsuarioDto> ConsultarUsuarioPorId(int id);
        
        /// <summary>
        /// Valida las credenciales del usuario buscando por su correo.
        /// </summary>
        /// <param name="correo">Correo electrónico del usuario.</param>
        /// <returns>Un objeto <see cref="UsuarioDto"/> con la información del usuario, incluyendo la contraseña encriptada (solo para validación interna), o null si no se encuentra.</returns>
        Task<(UsuarioDto? usuario , string error)> ValidarCredenciales(string correo, string contrasena);
        #endregion
    }
}
