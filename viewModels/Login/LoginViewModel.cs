using System.ComponentModel;
using System.ComponentModel.DataAnnotations; // para poder usar validaciones (Model.State) y los Espacio de nombres (Ejemplo [StringLength(10)])
using tl2_tp10_2023_Unagui19.Models;

namespace tl2_tp10_2023_Unagui19.ViewModels
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Este campo es requerido.")]
        [StringLength (30)]
        [Display(Name = "Nombre de Usuario")] 
        public string Nombre {get;set;}        
        
        [Required(ErrorMessage = "Este campo es requerido.")]
        [PasswordPropertyText]
        // [Range (8,20)]
        [Display(Name = "Contraseña")]
        public string Contrasenia {get;set;}

    }
}

