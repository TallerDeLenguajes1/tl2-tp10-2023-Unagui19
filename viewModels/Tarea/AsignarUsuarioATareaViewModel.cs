using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Taller2_TP10.Models;
using Taller2_TP10.Repositorios;

namespace Taller2_TP10.ViewModels
{
    public class AsignarUsuarioATareaViewModel
    {
        
        public int IdTarea{get;set;}
        // public int IdTablero{get;set;}
        
        //[Required][StringLength(30)]
        // public string NombreTarea{get;set;}
        // public string NombreUsuario{get;set;}

        [Required (ErrorMessage = "Este campo es requerido.")]
        public int IdUsuarioAsignado{get;set ;}

        public List<Usuario> UsuariosDisponibles{get;set;}
        
        public AsignarUsuarioATareaViewModel(){
            UsuariosDisponibles = new List<Usuario>();
        }
        public AsignarUsuarioATareaViewModel(Tarea tarea, List<Usuario> usuariosDisponibles){
            IdTarea = tarea.Id;
            // NombreTarea = tarea.Nombre;
            UsuariosDisponibles = usuariosDisponibles;
        }

        public AsignarUsuarioATareaViewModel(int idTarea, List<Usuario> usuariosDisponibles){
            IdTarea = idTarea;
            UsuariosDisponibles = usuariosDisponibles;
        }

    }
}