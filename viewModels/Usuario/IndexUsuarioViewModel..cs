using System.ComponentModel;
using Taller2_TP10.Models;

namespace Taller2_TP10.ViewModels
{
    public class IndexUsuarioViewModel
    {
        
        public int IdUsuario {get;set;}
        public string Nombre {get;set;}
        public Roles Rol {get;set;}

        public IndexUsuarioViewModel(){}

        public IndexUsuarioViewModel(Usuario usuario)
        {
            IdUsuario =usuario.Id;
            Nombre = usuario.NombreDeUsuario;
            Rol = usuario.Rol;
        }
    }
}