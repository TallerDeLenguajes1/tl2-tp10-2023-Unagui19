using Taller2_TP10.Models;
using Taller2_TP10.Repositorios;
using System.Linq;
using System.Data.SQLite;

namespace Taller2_TP10.Repositorios
{
    public interface IUsuarioRepository
    {
        public List<Usuario> ListarUsuarios();
        public void CrearUsuario(Usuario usuario);
        public void ModificarUsuario(int idUsu,Usuario usuario);
        public Usuario BuscarUsuarioPorId(int idUsu);
        public void EliminarUsuario(int idUsu);
    }    
}