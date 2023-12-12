using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using tl2_tp10_2023_Unagui19.Models;
using tl2_tp10_2023_Unagui19.Repositorios;
using tl2_tp10_2023_Unagui19.ViewModels;

namespace tl2_tp10_2023_Unagui19.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private IUsuarioRepository _repoUsuario ;
    public LoginController(ILogger<LoginController> logger, IUsuarioRepository RepoUsuario)
    {
        _logger = logger;
        _repoUsuario=RepoUsuario;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(new LoginViewModel());
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel loginUsuario)
    {
        try
        {  
            var usuarioLogeado = _repoUsuario.GetAll().FirstOrDefault(usu=> usu.NombreDeUsuario==loginUsuario.Nombre && usu.Contrasenia==loginUsuario.Contrasenia);

            if (ModelState.IsValid)
            {
                try
                {
                    if (usuarioLogeado == null) {
                        _logger.LogWarning("Intento de acceso invalido - Usuario:" + loginUsuario.Nombre + "Clava ingresada: " + loginUsuario.Contrasenia);
                        return RedirectToAction("Index");
                    }
                    //Registro el usuario
                    logearUsuario(usuarioLogeado);
                    
                    _logger.LogInformation("El usuario" + usuarioLogeado.NombreDeUsuario + "ingreso correctamente");

                   //Devuelvo el usuario al Home
                    return RedirectToRoute(new { controller = "Home", action = "Index" });
                }
                catch (System.Exception ex)
                {
                    _logger.LogError(ex.ToString());
                    return RedirectToAction("Index");
                }
            }
            // si el usuario no existe devuelvo al index
            return RedirectToAction("Index");
        }
        catch (System.Exception ex2)
        {
            _logger.LogError(ex2,ToString());
            return BadRequest(RedirectToAction("Index"));
        }
        //existe el usuario?

        
    }

    private void logearUsuario(Usuario user)
    {
        try{
            HttpContext.Session.SetString("IdUsuario", user.Id.ToString());
            HttpContext.Session.SetString("Usuario", user.NombreDeUsuario);
            HttpContext.Session.SetString("Contrasenia", user.Contrasenia);
            HttpContext.Session.SetString("Rol", user.Rol.ToString());
        }
        catch(Exception ex)
        {
            throw new Exception("Los datos de usuario no se asignaron correctamente",ex);
        }
    }


}
