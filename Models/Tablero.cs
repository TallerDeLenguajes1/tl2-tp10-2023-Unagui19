using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using tl2_tp10_2023_Unagui19.ViewModels;

namespace tl2_tp10_2023_Unagui19.Models
{
    public class Tablero
    {
        int id ;
        int idUsuarioPropietario;
        string nombre;
        string descripcion;


        public int Id { get => id; set => id = value; }
        public int IdUsuarioPropietario { get => idUsuarioPropietario; set => idUsuarioPropietario = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }

        public Tablero(){}
        public Tablero(CrearTableroViewModel tableroVM)
        {

            this.idUsuarioPropietario = tableroVM.IdUsuarioPropietario;
            this.nombre = tableroVM.Nombre;
            this.descripcion = tableroVM.Descripcion;
        }

        public Tablero(ModificarTableroViewModel tableroVM)
        {
            this.id = tableroVM.Id;
            this.idUsuarioPropietario = tableroVM.IdUsuarioPropietario;
            this.nombre = tableroVM.Nombre;
            this.descripcion = tableroVM.Descripcion;
        }

    }
}