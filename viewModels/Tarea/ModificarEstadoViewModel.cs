using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Taller2_TP10.Models;

namespace Taller2_TP10.ViewModels
{
    public class ModificarEstadoViewModel
    {
        
        public int Id{get;set;}

        [Required (ErrorMessage = "Este campo es requerido.")]public string Nombre{get;set;}

        [Required (ErrorMessage = "Este campo es requerido.")]public Estado EstadoTarea{get;set;}


        public ModificarEstadoViewModel(){
            EstadoTarea = Estado.Ideas;
        }

        public ModificarEstadoViewModel(int id, string nombre ,Estado estado){
            Id = id;
            Nombre = nombre;
            EstadoTarea = estado;
        }
    }
}