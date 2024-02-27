using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Taller2_TP10.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Taller2_TP10.ViewModels
{
    public class ModificarUsuarioViewModel
    {
        
        public int IdUsuario {get;set;}

        [Required(ErrorMessage = "Ingrese un nombre de usuario")]
        [StringLength(30)]public string Nombre {get;set;}

        [Required]public Roles Rol {get;set;}

        [StringLength(12, ErrorMessage = "La contrseña debe tener entre 4 y 12 caracteres", MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string Contrasenia {get;set;} 


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