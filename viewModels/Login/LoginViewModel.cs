using System.ComponentModel.DataAnnotations;
using Taller2_TP10.Models;

namespace Taller2_TP10.ViewModels
{
    public class LoginViewModel
    {
        [Required][StringLength(30)]public string Nombre {get;set;}
        [Required][MinLength(4)][MaxLength(12)]public string Contrasenia {get;set;}

        public LoginViewModel(){}

        public LoginViewModel(string nombre, string contrasenia)
        {
            Nombre = nombre;
            Contrasenia = contrasenia;
        }
    }
}