using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Taller2_TP10.Models;

namespace Taller2_TP10.ViewModels
{
    public class ModificarTareaViewModel
    {
        
        public int Id{get;set;}
        public int IdTablero{get;set;}
        [Required][StringLength(30)]public string Nombre{get;set;}
        [StringLength(200)]public string Descripcion{get;set;}
        [StringLength(30)]public string Color{get;set;}
        [Required]public Estado EstadoTarea{get;set;}
        public int? IdUsuarioAsignado{get;set;}

        public ModificarTareaViewModel(){
            EstadoTarea = Estado.Ideas;
        }

        public ModificarTareaViewModel(Tarea tarea){
            Id = tarea.Id;
            IdTablero = tarea.IdTablero;
            Nombre = tarea.Nombre;
            Descripcion = tarea.Descripcion;
            Color = tarea.Color;
            EstadoTarea = tarea.EstadoTarea;
            IdUsuarioAsignado = tarea.IdUsuarioAsignado;
        }
    }
}