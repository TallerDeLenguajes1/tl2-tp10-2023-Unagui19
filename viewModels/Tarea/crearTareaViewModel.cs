using System.ComponentModel;
using Taller2_TP10.Models;

namespace Taller2_TP10.ViewModels
{
    public class CrearTareaViewModel
    {
        
        public int Id{get;set;}
        public int IdTablero{get;set;}
        public string Nombre{get;set;}
        public string Descripcion{get;set;}
        public string Color{get;set;}
        public Estado EstadoTarea{get;set;}
        public int? IdUsuarioAsignado{get;set;}

        public CrearTareaViewModel(){}

        public CrearTareaViewModel(Tarea tarea){
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