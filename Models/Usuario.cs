namespace tl2_tp10_2023_Unagui19.Models
{
    public enum NivelDeAcceso{admin, operador};
    public class Usuario
    {

        public int Id { get; set; }
        public string NombreDeUsuario{ get; set; }
        public NivelDeAcceso Rol{ get; set; }
        public string Contrasenia{ get; set; }


        public Usuario(int id, string nombreDeUsuario, NivelDeAcceso rol, string contrasenia)
        {
            Id = id;
            NombreDeUsuario = nombreDeUsuario;
            Rol = rol;
            Contrasenia = contrasenia;
        }

        public Usuario(){}
    }
    
}