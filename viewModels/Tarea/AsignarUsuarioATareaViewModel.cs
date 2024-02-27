using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Taller2_TP10.Models;
using Taller2_TP10.Repositorios;

namespace Taller2_TP10.ViewModels
{
    public class AsignarUsuarioATareaViewModel
    {
        
        public int IdTarea{get;set;}

        [Required (ErrorMessage = "Este campo es requerido.")]
        public int IdUsuarioAsignado{get;set ;}

        public List<Usuario> UsuariosDisponibles{get;set;}
        
        public AsignarUsuarioATareaViewModel(){
            UsuariosDisponibles = new List<Usuario>();
        }

        public AsignarUsuarioATareaViewModel(int idTarea, List<Usuario> usuariosDisponibles){
            IdTarea = idTarea;
            UsuariosDisponibles = usuariosDisponibles;
        }
        public AsignarUsuarioATareaViewModel(int idTarea,int idUsuarioAsignado, List<Usuario> usuariosDisponibles){
            IdTarea = idTarea;
            IdUsuarioAsignado = idUsuarioAsignado;
            UsuariosDisponibles = usuariosDisponibles;
        }

    }
}