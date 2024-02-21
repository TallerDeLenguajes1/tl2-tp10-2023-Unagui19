<<<<<<< HEAD
using System.ComponentModel.DataAnnotations;
=======
using System.ComponentModel;
>>>>>>> parent of 8065788 (Terminado la parte de sesiones)
using Taller2_TP10.Models;

namespace Taller2_TP10.ViewModels
{
    public class LoginViewModel
    {
<<<<<<< HEAD
        
        [Required][StringLength(30)] public string Nombre {get;set;}
        [Required][MinLength(4)][MaxLength(12)] public string Contrasenia {get;set;}
=======
        public string Nombre {get;set;}
        public string Contrasenia {get;set;}
>>>>>>> parent of 8065788 (Terminado la parte de sesiones)

        public LoginViewModel(){}
    }
}