using System.ComponentModel;
using Taller2_TP10.Models;

namespace Taller2_TP10.ViewModels
{
    public class IndexOperadorViewModel
    {
        public List<Tarea> TareasPropias{get;set;}   // public int Id{get;set;}
        public List<Tarea> TareasAsociadas{get;set;}      // public int Id{get;set;}
        public List<Tarea> TareasNoAsociadas{get;set;}   // public int Id{get;set;}
        public List<Usuario> Usuarios{get;set;}
        public List<Tablero> Tableros{get;set;}

        public IndexOperadorViewModel(){}
        public IndexOperadorViewModel(List<Tarea> tareasPropias, List<Tarea> tareasAsociadas, List<Tarea> tareasNoAsociadas){
            TareasPropias = tareasPropias;
            TareasAsociadas = tareasAsociadas;
            TareasNoAsociadas = tareasNoAsociadas;
        }

        public IndexOperadorViewModel(List<Tarea> tareasPropias, List<Tarea> tareasAsociadas, List<Tarea> tareasNoAsociadas, List<Usuario> usuarios, List<Tablero> tableros){
            TareasPropias = tareasPropias;
            TareasAsociadas = tareasAsociadas;
            TareasNoAsociadas = tareasNoAsociadas;
            Usuarios = usuarios;
            Tableros = tableros;
        }

        public string ObtenerUsuarioAsignado(int? idUsu){
            Usuario usuario = Usuarios.FirstOrDefault(usu => usu.Id == idUsu)!;// aseguro que no es null
            if (idUsu==0 || usuario==null )
            {
                return "Sin usuario asignado";
            }
            string nombreUsuario = usuario.NombreDeUsuario;
            return nombreUsuario;
        }

        public string ObtenerTableroAsociado(int idTablero){
            Tablero tablero = Tableros.FirstOrDefault(tab => tab.Id == idTablero);// aseguro que no es null
            if (idTablero==0 || tablero==null )
            {
                return "Sin Tablero asociado";
            }
            string nombreTablero = tablero.Nombre;
            return nombreTablero;
        }
    }
}