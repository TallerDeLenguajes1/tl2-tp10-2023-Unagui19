using System.ComponentModel;
using Taller2_TP10.Models;

namespace Taller2_TP10.ViewModels
{
    public class ModificarTableroViewModel
    {
        
        public int Id{get;set;}
        public int IdUsuarioPropietario{get;set;}
        public string Nombre{get;set;}
        public string Descripcion{get;set;}

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