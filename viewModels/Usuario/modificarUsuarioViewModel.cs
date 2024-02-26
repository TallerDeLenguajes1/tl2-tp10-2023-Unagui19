using System.ComponentModel.DataAnnotations;
using Taller2_TP10.Models;

namespace Taller2_TP10.ViewModels
{
    public class ModificarUsuarioViewModel
    {
        
        public int IdUsuario {get;set;}
        [Required][StringLength(30)]public string Nombre {get;set;}
        [Required]public Roles Rol {get;set;}

        [Required(ErrorMessage = "Por favor ingrese una contraseña")]
        [StringLength(12, ErrorMessage = "La contrseña debe tener entre 4 y 12 caracteres", MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string Contrasenia {get;set;} 

        // [Required(ErrorMessage = "Por favor vuelva a escribir la contraseña")]
        // [StringLength(12, ErrorMessage = "La contrseña debe tener entre 4 y 12 caracteres", MinimumLength = 4)]
        // [DataType(DataType.Password)]
        // [Compare("Contrasenia", ErrorMessage = "Las contraseñas no coinciden")]
        // public string ConfirmarContrasenia {get;set;} 

        public ModificarUsuarioViewModel(){}

        public ModificarUsuarioViewModel(Usuario usuario)
        {
            IdUsuario =usuario.Id;
            Nombre = usuario.NombreDeUsuario;
            Rol = usuario.Rol;
            Contrasenia = usuario.Contrasenia;
        }
    }
}