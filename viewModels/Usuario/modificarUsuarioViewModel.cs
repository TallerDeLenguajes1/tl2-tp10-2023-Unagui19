using System.ComponentModel.DataAnnotations;
using Taller2_TP10.Models;

namespace Taller2_TP10.ViewModels
{
    public class ModificarUsuarioViewModel
    {
        
        public int IdUsuario {get;set;}
        [Required][StringLength(30)]public string Nombre {get;set;}
        [Required]public Roles Rol {get;set;}

        public ModificarUsuarioViewModel(){}

        public ModificarUsuarioViewModel(Usuario usuario)
        {
            IdUsuario =usuario.Id;
            Nombre = usuario.NombreDeUsuario;
            Rol = usuario.Rol;
        }
    }
}