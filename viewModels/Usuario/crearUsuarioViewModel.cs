using System.ComponentModel.DataAnnotations;
using Taller2_TP10.Models;

namespace Taller2_TP10.ViewModels
{
    public class CrearUsuarioViewModel
    {
        
        public int IdUsuario {get;set;}

        [Required(ErrorMessage = "Ingrese un nombre")][StringLength(30)]
        public string Nombre {get;set;}
        
        [Required(ErrorMessage = "Por favor ingrese una contraseña")]
        [MinLength(4)][MaxLength(12)]
        [DataType(DataType.Password)]
        public string Contrasenia {get;set;}

        [Required]
        public Roles Rol {get;set;}

        public CrearUsuarioViewModel(){}

        public CrearUsuarioViewModel(Usuario usuario)
        {
            IdUsuario =usuario.Id;
            Nombre = usuario.NombreDeUsuario;
            Rol = usuario.Rol;
            Contrasenia = usuario.Contrasenia;
        }
    }
}