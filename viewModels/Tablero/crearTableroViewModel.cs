using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Taller2_TP10.Models;

namespace Taller2_TP10.ViewModels
{
    public class CrearTableroViewModel
    {
        
        
        [Required(ErrorMessage = "Este campo es requerido.")]
        public int IdUsuarioPropietario{get;set;}
        
        [Required (ErrorMessage = "Debe ingresar un nombre para el tablero")]
        [StringLength(60, ErrorMessage = "El nombre de usuario excede los 60 caracteres")]
        public string Nombre{get;set;}
        
        [StringLength(200, ErrorMessage = "Sobrepasa el limite de 200 caracteres")]
        public string Descripcion{get;set;}
        
        public List<Usuario> Usuarios {get;set;}

        public CrearTableroViewModel(){
            Usuarios = new List<Usuario>();
        }
        public CrearTableroViewModel(List<Usuario> usuarios, int idUsuProp){
            Usuarios = usuarios;
            IdUsuarioPropietario = idUsuProp;
        }

        public CrearTableroViewModel(int idUsuProp, string nombre, string descrip, List<Usuario> usuarios){
            // IdTablero = id;
            IdUsuarioPropietario =idUsuProp;
            Nombre = nombre;
            Descripcion = descrip;
            Usuarios = usuarios;
        }
    }
}