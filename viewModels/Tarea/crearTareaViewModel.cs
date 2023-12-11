using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_Unagui19.Models;

namespace tl2_tp10_2023_Unagui19.ViewModels
{
    public class CrearTareaViewModel
    {

        public int IdTablero {get;set;} 

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Nombre de la Tarea")] 
        public string Nombre {get;set;} 

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Estado")] 
        public EstadoTarea Estado {get;set;} 

        [Display(Name = "Descripcion")] 
        public string? Descripcion {get;set;} 

        [Display(Name = "Color")] 
        public string? Color {get;set;} 

        [Display(Name = "Id del usuario asignado")] 
        public int? IdUsuarioAsignado {get;set;} 


        
        public CrearTareaViewModel(){Estado=EstadoTarea.ToDo;}

        public CrearTareaViewModel(Tarea tarea)
        {
            IdTablero = tarea.IdTablero;
            Nombre = tarea.Nombre;
            Estado = tarea.Estado;
            Descripcion = tarea.Descripcion;
            Color = tarea.Color;
            IdUsuarioAsignado = tarea.IdUsuarioAsignado;
        }


    }
}

