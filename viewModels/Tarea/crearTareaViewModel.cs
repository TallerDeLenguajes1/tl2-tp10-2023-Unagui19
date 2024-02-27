using System.ComponentModel.DataAnnotations;
using Taller2_TP10.Models;

namespace Taller2_TP10.ViewModels
{
    public class CrearTareaViewModel
    {
        
        public int Id{get;set;}
        public int IdTablero{get;set;}

        [Required (ErrorMessage = "Este campo es requerido.")]
        [StringLength(30)]
        public string Nombre{get;set;}

        [StringLength(200)]public string Descripcion{get;set;}
        [StringLength(30)]public string Color{get;set;}
        
        [Required (ErrorMessage = "Este campo es requerido.")]
        public Estado EstadoTarea{get;set;}
        
        public int? IdUsuarioAsignado{get;set;}
        public List<Usuario> Usuarios{get;set;}
        public List<Tablero> Tableros{get;set;}

        public CrearTareaViewModel(){
            Usuarios = new List<Usuario>();
            Tableros = new List<Tablero>();
        }
        
        public CrearTareaViewModel(List<Usuario> usuarios, List<Tablero> tableros){
            Usuarios = usuarios;
            Tableros = tableros;
        }

        public CrearTareaViewModel(Tarea tarea){
            Id = tarea.Id;
            IdTablero = tarea.IdTablero;
            Nombre = tarea.Nombre;
            Descripcion = tarea.Descripcion;
            Color = tarea.Color;
            EstadoTarea = tarea.EstadoTarea;
            IdUsuarioAsignado = tarea.IdUsuarioAsignado;
        }

        public List<Tablero> TablerosPropios(int idUsuario){
            List<Tablero> tablerosPropios = new List<Tablero>(); 
            tablerosPropios = Tableros.Where(tab => tab.IdUsuarioPropietario == idUsuario).ToList();
            return tablerosPropios;
        }
    }
}