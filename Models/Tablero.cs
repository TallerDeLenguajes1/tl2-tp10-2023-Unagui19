namespace Entidades.Models
{
    public class Tablero
    {
        int id ;
        int idUsuarioPropietario;
        string Nombre;
        string descripcion;

        public int Id { get => id; set => id = value; }
        public int Id_usuario_propietario { get => idUsuarioPropietario; set => idUsuarioPropietario = value; }
        public string Nombre_de_Tablero { get => Nombre; set => Nombre = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
    }
}