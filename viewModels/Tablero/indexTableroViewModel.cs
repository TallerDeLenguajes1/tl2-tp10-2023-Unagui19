namespace tl2_tp10_2023_Unagui19.Models
{
    public class IndexTablero
    {
        int id ;
        int idUsuarioPropietario;
        string nombre;
        string descripcion;



        public int Id { get => id; set => id = value; }
        public int IdUsuarioPropietario { get => idUsuarioPropietario; set => idUsuarioPropietario = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }

        public IndexTablero(int id, int idUsuarioPropietario, string nombre, string descripcion)
        {
            this.id = id;
            this.idUsuarioPropietario = idUsuarioPropietario;
            this.nombre = nombre;
            this.descripcion = descripcion;
        }

        public IndexTablero(){}
    }
}