using Taller2_TP10.ViewModels;

namespace Taller2_TP10.Models
{
    public enum Estado{Ideas=1 , ToDo, Doing, Review, Done};
    public class Tarea
    {
        public int Id{get;set;}
        public int IdTablero{get;set;}
        public string Nombre{get;set;}
        public string Descripcion{get;set;}
        public string Color{get;set;}
        public Estado EstadoTarea{get;set;}
        public int? IdUsuarioAsignado{get;set;}

        public Tarea(){
            EstadoTarea = Estado.Ideas;
        }
        public Tarea(int id){
            Id=id;
            EstadoTarea=Estado.Ideas;
        }
        public Tarea(int id, int idTablero, string nombre, string descrip, string color, Estado estado, int? idUsu){
            Id = id;
            IdTablero = idTablero;
            Nombre = nombre;
            Descripcion = descrip;
            Color = color;
            EstadoTarea = estado;
            IdUsuarioAsignado = idUsu;
        }

        public Tarea(CrearTareaViewModel createTarea){
            Id = createTarea.Id;
            IdTablero = createTarea.IdTablero;
            Nombre = createTarea.Nombre;
            Descripcion = createTarea.Descripcion;
            Color = createTarea.Color;
            EstadoTarea = createTarea.EstadoTarea;
            IdUsuarioAsignado = createTarea.IdUsuarioAsignado;
        }
        public Tarea(ModificarTareaViewModel modTarea){
            Id = modTarea.Id;
            IdTablero = modTarea.IdTablero;
            Nombre = modTarea.Nombre;
            Descripcion = modTarea.Descripcion;
            Color = modTarea.Color;
            EstadoTarea = modTarea.EstadoTarea;
            IdUsuarioAsignado = modTarea.IdUsuarioAsignado;
        }

    }
}