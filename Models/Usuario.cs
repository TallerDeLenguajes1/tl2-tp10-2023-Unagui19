using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using tl2_tp10_2023_Unagui19.ViewModels;

namespace tl2_tp10_2023_Unagui19.Models
{
    public enum NivelDeAcceso{admin, operador};
    public class Usuario
    {

        public int Id { get; set; }
        public string NombreDeUsuario{ get; set; }
        public NivelDeAcceso Rol{ get; set; }
        public string Contrasenia{ get; set; }


        public Usuario(int id, string nombreDeUsuario, NivelDeAcceso rol, string contrasenia)
        {
            Id = id;
            NombreDeUsuario = nombreDeUsuario;
            Rol = rol;
            Contrasenia = contrasenia;
        }
        public Usuario(CrearUsuarioViewModel usu)
        {
            NombreDeUsuario = usu.NombreDeUsuario;
            Rol = usu.Rol;
            Contrasenia = usu.Contrasenia;
        }

        public Usuario(ModificarUsuarioViewModel usu)
        {
            Id = usu.Id;
            NombreDeUsuario = usu.NombreDeUsuario;
            Rol = usu.Rol;
            Contrasenia = usu.Contrasenia;
        }
        public Usuario(){}
    }
    
}