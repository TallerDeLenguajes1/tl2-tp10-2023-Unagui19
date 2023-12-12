using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_Unagui19.Models;

namespace tl2_tp10_2023_Unagui19.ViewModels
{
    public class ModificarTableroViewModel
    {

        public int Id {get;set;} 
        [Required(ErrorMessage = "Este campo es requerido.")]
        public int IdUsuarioPropietario {get;set;}
        [Required(ErrorMessage = "Este campo es requerido.")]
        public string Nombre {get;set;}
        public string Descripcion {get;set;}


        
        public ModificarTableroViewModel(Tablero tablero)
        {
            Id = tablero.Id;
            IdUsuarioPropietario= tablero.IdUsuarioPropietario;
            Nombre = tablero.Nombre;
            Descripcion = tablero.Descripcion;
        }

        public ModificarTableroViewModel(){}
    }
}