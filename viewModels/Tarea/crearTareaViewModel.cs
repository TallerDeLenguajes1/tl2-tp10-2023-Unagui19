using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_Unagui19.Models;

namespace tl2_tp10_2023_Unagui19.ViewModels
{
    public class CrearTarea
    {

        public int IdTablero {get;set;} 
        public string Nombre {get;set;} 
        public EstadoTarea Estado {get;set;} 
        public string? Descripcion {get;set;} 
        public string? Color {get;set;} 
        public int? IdUsuarioAsignado {get;set;} 


        
        public CrearTarea(){Estado=EstadoTarea.ToDo;}

        public CrearTarea(int idTablero, string nombre, EstadoTarea estado, string? descripcion, string? color, int? idUsuarioAsignado)
        {
            IdTablero = idTablero;
            Nombre = nombre;
            Estado = estado;
            Descripcion = descripcion;
            Color = color;
            IdUsuarioAsignado = idUsuarioAsignado;
        }
    }
}

