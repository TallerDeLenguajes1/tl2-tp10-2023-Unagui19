using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_Unagui19.Models;

namespace tl2_tp10_2023_Unagui19.ViewModels
{
    public class ModificarUsuario
    {

        public int Id {get;set;}        
        public string NombreDeUsuario {get;set;}        
        public string Rol {get;set;}

        public ModificarUsuario(int id, string nombreDeUsuario, string rol)
        {
            Id = id;
            NombreDeUsuario = nombreDeUsuario;
            Rol = rol;
        }
        
        public ModificarUsuario(){}
    }
}

