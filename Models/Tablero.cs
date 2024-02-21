using Taller2_TP10.ViewModels;

namespace Taller2_TP10.Models
{
    public class Tablero
    {
        public int Id{get;set;}
        public int IdUsuarioPropietario{get;set;}
        public string Nombre{get;set;}
        public string Descripcion{get;set;}

        public Tablero(){}
        public Tablero(int id){
            Id = id;
        }

        public Tablero(int id, int idUsuProp, string nombre, string descrip){
            Id = id;
            IdUsuarioPropietario =idUsuProp;
            Nombre = nombre;
            Descripcion = descrip;
        }

        public Tablero(CrearTableroViewModel tablero){
            Id = tablero.Id;
            IdUsuarioPropietario = tablero.IdUsuarioPropietario;
            Nombre = tablero.Nombre;
            Descripcion = tablero.Descripcion;
        }
        public Tablero(ModificarTableroViewModel tablero){
            Id = tablero.Id;
            IdUsuarioPropietario = tablero.IdUsuarioPropietario;
            Nombre = tablero.Nombre;
            Descripcion = tablero.Descripcion;
        }
    }   

}