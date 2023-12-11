using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_Unagui19.Models;

namespace tl2_tp10_2023_Unagui19.ViewModels
{
    public class CrearTableroViewModel
    {
        // int id ;
        [Required(ErrorMessage = "Este campo es requerido.")]
        int idUsuarioPropietario;
        [Required(ErrorMessage = "Este campo es requerido.")]
        string nombre;

        string descripcion;


        // public int Id { get => id; set => id = value; }
        public int IdUsuarioPropietario { get => idUsuarioPropietario; set => idUsuarioPropietario = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }

        
        public CrearTableroViewModel(Tablero tablero)
        {
            idUsuarioPropietario= tablero.IdUsuarioPropietario;
            nombre = tablero.Nombre;
            descripcion = tablero.Descripcion;
        }

        public CrearTableroViewModel(){}
    }
}