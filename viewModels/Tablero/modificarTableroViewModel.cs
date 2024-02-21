using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Taller2_TP10.Models;

namespace Taller2_TP10.ViewModels
{
    public class ModificarTableroViewModel
    {
        
        public int Id{get;set;}
        [Required]public int IdUsuarioPropietario{get;set;}
        [Required][StringLength(60)]public string Nombre{get;set;}
        [StringLength(200)]public string Descripcion{get;set;}

        public ModificarTableroViewModel(){}
        public ModificarTableroViewModel(int id){
            Id = id;
        }

        public ModificarTableroViewModel(Tablero tablero){
            Id = tablero.Id;
            IdUsuarioPropietario = tablero.IdUsuarioPropietario;
            Nombre = tablero.Nombre;
            Descripcion = tablero.Descripcion;
        }
    }
}