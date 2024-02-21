using System.ComponentModel;
using Taller2_TP10.Models;

namespace Taller2_TP10.ViewModels
{
    public class IndexTableroViewModel
    {
        
        public int Id{get;set;}
        public int IdUsuarioPropietario{get;set;}
        public string Nombre{get;set;}
        public string Descripcion{get;set;}

        public IndexTableroViewModel(){}
        public IndexTableroViewModel(Tablero tablero){
            Id = tablero.Id;
            IdUsuarioPropietario = tablero.IdUsuarioPropietario;
            Nombre = tablero.Nombre;
            Descripcion = tablero.Descripcion;
        }
    }
}