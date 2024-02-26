using System.ComponentModel;
using Taller2_TP10.Models;

namespace Taller2_TP10.ViewModels
{
    public class IndexTareaViewModel
    {
        
        public int Id{get;set;}
        public int IdTablero{get;set;}
        public string Nombre{get;set;}
        public string Descripcion{get;set;}
        public string Color{get;set;}
        public Estado EstadoTarea{get;set;}
        public int? IdUsuarioAsignado{get;set;}
        public List<Usuario> Usuarios{get;set;}
        public List<Tablero> Tableros{get;set;}

        public IndexTareaViewModel(){}
        public IndexTareaViewModel(Tarea tarea){
            Id = tarea.Id;
            IdTablero = tarea.IdTablero;
            Nombre = tarea.Nombre;
            Descripcion = tarea.Descripcion;
            Color = tarea.Color;
            EstadoTarea = tarea.EstadoTarea;
            IdUsuarioAsignado = tarea.IdUsuarioAsignado;
        }
        public IndexTareaViewModel(Tarea tarea, List<Usuario> usuarios, List<Tablero> tableros){
            Id = tarea.Id;
            IdTablero = tarea.IdTablero;
            Nombre = tarea.Nombre;
            Descripcion = tarea.Descripcion;
            Color = tarea.Color;
            EstadoTarea = tarea.EstadoTarea;
            IdUsuarioAsignado = tarea.IdUsuarioAsignado;
            Usuarios = usuarios;
            Tableros = tableros;
        }

        public string ObtenerUsuarioAsignado(int? idUsu){
            Usuario usuario = Usuarios.FirstOrDefault(usu => usu.Id == idUsu);
            if (idUsu==0 || usuario == null)
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