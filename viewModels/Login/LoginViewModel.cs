using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Taller2_TP10.Models;

namespace Taller2_TP10.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength(30)]
        [Display(Name = "Nombre de Usuario")] 
        public string Nombre {get;set;}

        [Required(ErrorMessage = "Este campo es requerido.")]
        [PasswordPropertyText]
        [MinLength(4)][MaxLength(12)]
        public string Contrasenia {get;set;}
        public string MensajeDeErro{get;set;}

        public LoginViewModel(){}

        public LoginViewModel(string nombre, string contrasenia)
        {
            Nombre = nombre;
            Contrasenia = contrasenia;
        }
    }
}