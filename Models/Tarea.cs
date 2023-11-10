namespace Entidades.Models
{   
    enum EstadoTarea {Ideas, ToDo, Doing, Review, Done};
    public class Tarea
    {
        int id;
        int idTablero;
        string nombre;
        string descripcion;
        string color;
        EstadoTarea estado;
        int? idUsuarioAsignado;

        public int Id { get => Id; set => Id = value; }
        public int IdTablero { get => idTablero; set => idTablero = value; }
        public string Nombre { get => Nombre; set => Nombre = value; }
        public string Descripcion { get => Descripcion; set => Descripcion = value; }
        public string Color { get => Color; set => Color = value; }
        internal EstadoTarea Estado { get => Estado; set => Estado = value; }
        public int? IdUsuarioAsignado { get => IdUsuarioAsignado; set => IdUsuarioAsignado = value; }
    }
}