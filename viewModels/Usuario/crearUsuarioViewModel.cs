using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using tl2_tp10_2023_Unagui19.Models;

namespace tl2_tp10_2023_Unagui19.ViewModels
{
    public class CrearUsuarioViewModel
    {
 
        [Required(ErrorMessage = "Este campo es requerido.")]
        [MaxLength (100)]
        public string NombreDeUsuario {get;set;} 

        [Required(ErrorMessage = "Este campo es requerido.")]  
        public NivelDeAcceso Rol {get;set;}

        [Required(ErrorMessage = "Este campo es requerido.")]
        public string Contrasenia {get;set;}


        
        public CrearUsuarioViewModel(){}

        public CrearUsuarioViewModel(Usuario usu)
        {
            NombreDeUsuario = usu.NombreDeUsuario;
            Rol = usu.Rol;
            Contrasenia = usu.Contrasenia;
        }
    }
}

