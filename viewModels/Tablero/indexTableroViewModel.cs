using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_Unagui19.Models;

namespace tl2_tp10_2023_Unagui19.ViewModels
{
    public class IndexTableroViewModel
    {
        
        public int Id {get;set;}
        public int IdUsuarioPropietario {get;set;}
        public string Nombre {get;set;} 

        public string Descripcion {get;set;}

        public IndexTableroViewModel(Tablero tablero)
        {
            Id = tablero.Id;
            IdUsuarioPropietario= tablero.IdUsuarioPropietario;
            Nombre = tablero.Nombre;
            Descripcion = tablero.Descripcion;
        }

        public IndexTableroViewModel(){}
    }
}