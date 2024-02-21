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
        //Existe el usuario?
        // if (usuarioLogueado!=null) // si el usuario esta logueado, es decir existe
        // {
        //     loguearUsuario(usuarioLogueado);
        //     return RedirectToAction("Index","Home");
        // }
        // else//Si el usuario no coincide, es decir no esta logueado, devuelvo directamente al index
        // {
        //     return RedirectToAction("Index");        
        // }
    }

    // private void LoguearUsuario(Usuario user)
    // {
    //     HttpContext.Session.SetInt32("IdUsuario", user.Id);
    //     HttpContext.Session.SetString("Usuario", user.NombreDeUsuario);
    //     // HttpContext.Session.SetString("Contraseña", user.Contrasenia);
    //     HttpContext.Session.SetString("Rol", user.Rol.ToString());
    // }
    

}
