using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Taller2_TP10.Models;

namespace Taller2_TP10.ViewModels
{
    public class IndexUsuarioViewModel
    {
        
        public int IdUsuario {get;set;}
        [Required][StringLength(30)]public string Nombre {get;set;}
        [Required]public Roles Rol {get;set;}

        public IndexUsuarioViewModel(){}

        public IndexUsuarioViewModel(Usuario usuario)
        {
            IdUsuario =usuario.Id;
            Nombre = usuario.NombreDeUsuario;
            Rol = usuario.Rol;
        }
    }
}