using System.ComponentModel.DataAnnotations;
using Taller2_TP10.Models;

namespace Taller2_TP10.ViewModels
{
    public class ModificarTableroViewModel
    {
        
        public int Id{get;set;}
        [Required (ErrorMessage = "Este campo es requerido.")]
        public int IdUsuarioPropietario{get;set;}
        
        [Required (ErrorMessage = "Debe ingresar un nombre para el tablero")]
        [StringLength(60, ErrorMessage = "El nombre de usuario excede los 60 caracteres")]
        public string Nombre{get;set;}
        
        [StringLength(200, ErrorMessage = "Sobrepasa el limite de 200 caracteres")]
        public string Descripcion{get;set;}
        
        public List<Usuario> Usuarios {get;set;}

        public ModificarTableroViewModel(){
            Usuarios = new List<Usuario>();
        }
        public ModificarTableroViewModel(List<Usuario> usuarios){
            Usuarios = usuarios;
        }

        public ModificarTableroViewModel(Tablero tablero, List<Usuario> usuarios){
            Id = tablero.Id;
            IdUsuarioPropietario = tablero.IdUsuarioPropietario;
            Nombre = tablero.Nombre;
            Descripcion = tablero.Descripcion;
            Usuarios = usuarios;
        }
    }
}