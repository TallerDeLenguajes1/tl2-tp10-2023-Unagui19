using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_Unagui19.Models;

namespace tl2_tp10_2023_Unagui19.ViewModels
{
    public class IndexUsuarioViewModel
    {

        public int Id {get;set;}        
        public string NombreDeUsuario {get;set;}        
        public NivelDeAcceso Rol {get;set;}

        public IndexUsuarioViewModel(Usuario usu)
        {
            Id = usu.Id;
            NombreDeUsuario = usu.NombreDeUsuario;
            Rol = usu.Rol;
        }
        
        public IndexUsuarioViewModel(){}
    }
}

