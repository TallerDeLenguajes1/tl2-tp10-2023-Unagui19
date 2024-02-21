namespace Taller2_TP10.Models
{
    public enum Roles{admin = 1, operador}
    public class Usuario
    {
        public int Id {get;set;}
        public string NombreDeUsuario {get;set;}
        public string Contrasenia {get;set;}
        public Roles Rol {get;set;}

        public Usuario(){}
        public Usuario(int id){
            Id = id;
            Rol = Roles.operador;
        }

        public Usuario(int id, string nombre, string password, Roles rol){
            Id = id;
            NombreDeUsuario = nombre;
            Contrasenia = password;
            Rol = rol;

        }
    }    
}