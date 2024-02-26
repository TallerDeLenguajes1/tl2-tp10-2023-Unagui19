using System.ComponentModel;
using Taller2_TP10.Models;

namespace Taller2_TP10.ViewModels
{
    public class IndexTableroViewModel
    {
        
        public int Id{get;set;}
        public int IdUsuarioPropietario{get;set;}
        public string Nombre{get;set;}
        public string Descripcion{get;set;}
        public List<Usuario> UsuariosProp{get;set;}
        
        

        public IndexTableroViewModel(){
            UsuariosProp = new List<Usuario>();
        }

        public IndexTableroViewModel(Tablero tablero){
            Id = tablero.Id;
            IdUsuarioPropietario = tablero.IdUsuarioPropietario;
            Nombre = tablero.Nombre;
            Descripcion = tablero.Descripcion;
        }
        public IndexTableroViewModel(Tablero tablero, List<Usuario> usuariosProp){
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