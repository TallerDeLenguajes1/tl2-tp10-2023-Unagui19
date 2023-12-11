using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_Unagui19.Models;
using tl2_tp10_2023_Unagui19.Repositorios;
using tl2_tp10_2023_Unagui19.ViewModels;

namespace tl2_tp10_2023_Unagui19.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;

    private List<Usuario> usuarios;
    private UsuarioRepositorio repoUsuario ;
    public LoginController(ILogger<LoginController> logger)
    {
        _logger = logger;
        repoUsuario=new UsuarioRepositorio();
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(new LoginViewModel());
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel loginUsuario)
    {
        //existe el usuario?
        var usuarioLogeado = repoUsuario.GetAll().FirstOrDefault(usu=> usu.NombreDeUsuario==loginUsuario.Nombre && usu.Contrasenia==loginUsuario.Contrasenia);

        // si el usuario no existe devuelvo al index
        if (usuarioLogeado == null) {
            return RedirectToAction("Index");
        }
        else
        {
            //Registro el usuario
            logearUsuario(usuarioLogeado);
            
            //Devuelvo el usuario al Home
            return RedirectToRoute(new { controller = "Home", action = "Index" });
            
        }
        
    }

    private void logearUsuario(Usuario user)
    {
        HttpContext.Session.SetString("IdUsuario", user.Id.ToString());
        HttpContext.Session.SetString("Usuario", user.NombreDeUsuario);
        HttpContext.Session.SetString("Contrasenia", user.Contrasenia);
        HttpContext.Session.SetString("Rol", user.Rol.ToString());
    }


}
