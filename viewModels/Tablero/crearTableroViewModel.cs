using System.ComponentModel;
using Taller2_TP10.Models;

namespace Taller2_TP10.ViewModels
{
    public class CrearTableroViewModel
    {
        
        public int Id{get;set;}
        public int IdUsuarioPropietario{get;set;}
        public string Nombre{get;set;}
        public string Descripcion{get;set;}

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