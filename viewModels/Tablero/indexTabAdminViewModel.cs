using System.ComponentModel;
using Taller2_TP10.Models;

namespace Taller2_TP10.ViewModels
{
    public class IndexTabAdminViewModel
    {
        
        public int Id{get;set;}
        public int IdUsuarioPropietario{get;set;}
        public string Nombre{get;set;}
        public string Descripcion{get;set;}
        public List<Usuario> UsuariosProp{get;set;}
        
        

        public IndexTabAdminViewModel(){
            UsuariosProp = new List<Usuario>();
        }

        public IndexTabAdminViewModel(Tablero tablero){
            Id = tablero.Id;
            IdUsuarioPropietario = tablero.IdUsuarioPropietario;
            Nombre = tablero.Nombre;
            Descripcion = tablero.Descripcion;
        }
        public IndexTabAdminViewModel(Tablero tablero, List<Usuario> usuariosProp){
            Id = tablero.Id;
            IdUsuarioPropietario = tablero.IdUsuarioPropietario;
            Nombre = tablero.Nombre;
            Descripcion = tablero.Descripcion;
            UsuariosProp = usuariosProp;
        }

        public string ObtenerUsuarioPropietario(int idUsu){
            Usuario usuario = UsuariosProp.FirstOrDefault(usu => usu.Id == idUsu)!;
            if (usuario ==null )
            {
                return string.Empty;
            }
            string nombreUsuario = usuario.NombreDeUsuario;
            return nombreUsuario; 
        }
    }
}