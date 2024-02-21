using System.ComponentModel;
using Taller2_TP10.Models;

namespace Taller2_TP10.ViewModels
{
    public class CrearUsuarioViewModel
    {
        
        public int IdUsuario {get;set;}
        public string Nombre {get;set;}
        public string Contrasenia {get;set;}
        public Roles Rol {get;set;}

        public CrearUsuarioViewModel(){}

        public CrearUsuarioViewModel(Usuario usuario)
        {
            IdUsuario =usuario.Id;
            Nombre = usuario.NombreDeUsuario;
            Rol = usuario.Rol;
            Contrasenia = usuario.Contrasenia;
        }
    }
}