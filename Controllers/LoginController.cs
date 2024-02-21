using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Taller2_TP10.Models;
using Taller2_TP10.Repositorios;
using Taller2_TP10.ViewModels;

namespace Taller2_TP10.Controllers;

public class LoginController : Controller
{
     
    private readonly ILogger<LoginController> _logger;
    private UsuarioRepository repoUsuario;

    public LoginController(ILogger<LoginController> logger)
    {
        repoUsuario = new UsuarioRepository();
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(new LoginViewModel());
    }

    [HttpPost]
    public IActionResult Logueo(LoginViewModel loginUsuario){
<<<<<<< HEAD
<<<<<<< HEAD
        Usuario usuarioLogueado = new Usuario();
        List<Usuario> usuarios = repoUsuario.ListarUsuarios();
        usuarioLogueado = usuarios.FirstOrDefault(usu => usu.NombreDeUsuario == loginUsuario.Nombre && usu.Contrasenia == loginUsuario.Contrasenia);
         if (usuarioLogueado == null)
            {
                var loginVM = new LoginViewModel() ;
                // {
                //     MensajeDeError = "Usuario no existente"
                // };
                return View("Index",loginVM); 
            }
            
            HttpContext.Session.SetInt32("IdUsuario", usuarioLogueado.Id);
            HttpContext.Session.SetString("Usuario", usuarioLogueado.NombreDeUsuario);
            // HttpContext.Session.SetString("Contraseña", user.Contrasenia);
            HttpContext.Session.SetString("Rol", usuarioLogueado.Rol.ToString());
            return RedirectToAction("Index","Home");
=======
=======
>>>>>>> parent of 8065788 (Terminado la parte de sesiones)
        //Existe el usuario?
        var usuarioLogueado = repoUsuario.ListarUsuarios().FirstOrDefault(usu=> usu.NombreDeUsuario == loginUsuario.Nombre);

        if (usuarioLogueado != null)
        {
            return RedirectToAction("Index");
        }
        else//Si el usuario no coincide, es decir no esta logueado, devuelvo directamente al index
        {
            return RedirectToAction("Index");        
        }
<<<<<<< HEAD
>>>>>>> parent of 8065788 (Terminado la parte de sesiones)
=======
>>>>>>> parent of 8065788 (Terminado la parte de sesiones)
    }

}
