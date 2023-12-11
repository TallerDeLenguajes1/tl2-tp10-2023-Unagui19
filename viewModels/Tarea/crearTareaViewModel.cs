using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_Unagui19.Models;

namespace tl2_tp10_2023_Unagui19.ViewModels
{
    public class CrearTareaViewModel
    {

        public int IdTablero {get;set;} 
        public string Nombre {get;set;} 
        public EstadoTarea Estado {get;set;} 
        public string? Descripcion {get;set;} 
        public string? Color {get;set;} 
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

