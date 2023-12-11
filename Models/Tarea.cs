using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using tl2_tp10_2023_Unagui19.ViewModels;

namespace tl2_tp10_2023_Unagui19.Models
{   
    public enum EstadoTarea {Ideas, ToDo, Doing, Review, Done}
    public class Tarea
    {
        int id;
        int idTablero;
        string nombre;
        EstadoTarea estado;
        string? descripcion;
        string? color;
        int? idUsuarioAsignado;



        public int Id { get => id; set => id = value; }
        public int IdTablero { get => idTablero; set => idTablero = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public EstadoTarea Estado { get => estado; set => estado = value; }
        public string? Descripcion { get => descripcion; set => descripcion = value; }
        public string? Color { get => color; set => color = value; }
        public int? IdUsuarioAsignado { get => idUsuarioAsignado; set => idUsuarioAsignado = value; }

        public Tarea (){}
        public Tarea(CrearTareaViewModel tareaVM)
        {
            this.idTablero = tareaVM.IdTablero;
            this.nombre = tareaVM.Nombre;
            this.estado = tareaVM.Estado;
            this.descripcion = tareaVM.Descripcion;
            this.color = tareaVM.Color;
            this.idUsuarioAsignado = tareaVM.IdUsuarioAsignado;
        }

        public Tarea(ModificarTareaViewModel tareaVM)
        {
            this.id = tareaVM.Id;
            this.idTablero = tareaVM.IdTablero;
            this.nombre = tareaVM.Nombre;
            this.estado = tareaVM.Estado;
            this.descripcion = tareaVM.Descripcion;
            this.color = tareaVM.Color;
            this.idUsuarioAsignado = tareaVM.IdUsuarioAsignado;
        }
    }
}