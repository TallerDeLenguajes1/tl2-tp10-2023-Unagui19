using System.ComponentModel;
using Taller2_TP10.Models;

namespace Taller2_TP10.ViewModels
{
    public class LoginViewModel
    {
        public string Nombre {get;set;}
        public string Contrasenia {get;set;}

        public LoginViewModel(){}

        public LoginViewModel(string nombre, string contrasenia)
        {
            Nombre = nombre;
            Contrasenia = contrasenia;
        }
    }
}