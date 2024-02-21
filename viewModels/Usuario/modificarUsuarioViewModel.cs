using System.ComponentModel;
using Taller2_TP10.Models;

namespace Taller2_TP10.ViewModels
{
    public class ModificarUsuarioViewModel
    {
        
        public int IdUsuario {get;set;}
        public string Nombre {get;set;}
        public Roles Rol {get;set;}

        public ModificarUsuarioViewModel(){}

        public ModificarUsuarioViewModel(Usuario usuario)
        {
            IdUsuario =usuario.Id;
            Nombre = usuario.NombreDeUsuario;
            Rol = usuario.Rol;
        }
    }
}