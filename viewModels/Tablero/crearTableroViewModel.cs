using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_Unagui19.Models;

namespace tl2_tp10_2023_Unagui19.ViewModels
{
    public class CrearTableroViewModel
    {
        // int id ;
        [Required(ErrorMessage = "Este campo es requerido.")]
        public int IdUsuarioPropietario {get;set;}
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display (Name = "Nombre de tablero")]
        public string Nombre {get;set;} 

        public string Descripcion {get;set;}


        // public int Id { get => id; set => id = value; }

        
        public CrearTableroViewModel(Tablero tablero)
        {
            IdUsuarioPropietario= tablero.IdUsuarioPropietario;
            Nombre = tablero.Nombre;
            Descripcion = tablero.Descripcion;
        }

        public CrearTableroViewModel(){}
    }
}