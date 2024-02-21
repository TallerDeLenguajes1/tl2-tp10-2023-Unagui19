using System.ComponentModel.DataAnnotations;
using Taller2_TP10.Models;

namespace Taller2_TP10.ViewModels
{
    public class CrearTableroViewModel
    {
        
        public int Id{get;set;}
        [Required]public int IdUsuarioPropietario{get;set;}
        [Required][StringLength(60)]public string Nombre{get;set;}
        [StringLength(200)]public string Descripcion{get;set;}

        public CrearTableroViewModel(){}
        public CrearTableroViewModel(int id){
            Id = id;
        }

        public CrearTableroViewModel(int id, int idUsuProp, string nombre, string descrip){
            Id = id;
            IdUsuarioPropietario =idUsuProp;
            Nombre = nombre;
            Descripcion = descrip;
        }
    }
}