namespace Taller2_TP10.Models
{
    public class Usuario
    {
        public int Id {get;set;}
        public string NombreDeUsuario {get;set;}

        public Usuario(){}
        public Usuario(int id){
            Id = id;
        }

        public Usuario(int id, string nombre){
            Id = id;
            NombreDeUsuario = nombre;
        }
    }    
}