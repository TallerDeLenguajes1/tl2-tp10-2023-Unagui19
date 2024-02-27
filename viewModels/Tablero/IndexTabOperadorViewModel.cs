using System.ComponentModel;
using Taller2_TP10.Models;

namespace Taller2_TP10.ViewModels
{
    public class IndexTabOperadorViewModel
    {
        

        public List<Tablero> TablerosPropios{get;set;}   // public int Id{get;set;}
        public List<Tablero> TablerosAsignados{get;set;}
        public List<Usuario> Usuarios{get;set;}

        public IndexTabOperadorViewModel(){
            TablerosAsignados= new List<Tablero>();
            TablerosPropios= new List<Tablero>();
            Usuarios= new List<Usuario>();
        }
        public IndexTabOperadorViewModel(List<Tablero> tablerosPropios, List<Tablero> tablerosAsignados, List<Usuario> usuarios){
            TablerosPropios = tablerosPropios;
            TablerosAsignados = tablerosAsignados;
            Usuarios = usuarios;
        }

        public string ObtenerUsuarioPropietario(int idUsu){
            Usuario usuario = Usuarios.FirstOrDefault(usu => usu.Id == idUsu);// aseguro que no es null
            if (usuario ==null )
            {
                return string.Empty;
            }
            string nombreUsuario = usuario.NombreDeUsuario;
            return nombreUsuario; 
        }
    }
}