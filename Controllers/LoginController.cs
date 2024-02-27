using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Taller2_TP10.Models;
using Taller2_TP10.Repositorios;
using Taller2_TP10.ViewModels;

namespace Taller2_TP10.Controllers;

public class LoginController : Controller
{
     
    private readonly ILogger<LoginController> _logger;
    private readonly IUsuarioRepository _repoUsuario;

    public LoginController(ILogger<LoginController> logger, IUsuarioRepository repoUsuario)
    {
        _repoUsuario = repoUsuario;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(new LoginViewModel());
    }

    [HttpPost]
    public IActionResult IniciarSesion(LoginViewModel loginUsuario){
        Usuario usuarioLogueado = new Usuario();
        List<Usuario> usuarios = _repoUsuario.ListarUsuarios();
        try
        {
            usuarioLogueado = usuarios.FirstOrDefault(usu => usu.NombreDeUsuario == loginUsuario.Nombre && usu.Contrasenia == loginUsuario.Contrasenia);
            if (usuarioLogueado == null)
            {
                TempData["usuInvalido"] = "Ingresó datos inválidos.";
                var loginVM = new LoginViewModel() ;
                _logger.LogWarning("Intento de acceso invalido - Usuario: " + loginUsuario.Nombre + " \nClave ingresada: " + loginUsuario.Contrasenia);
                return View("Index",loginVM); 
            }
            
            _logger.LogInformation("El usuario " + usuarioLogueado.NombreDeUsuario + " ingreso correctamente");
            HttpContext.Session.SetString("IdUsuario", usuarioLogueado.Id.ToString());
            HttpContext.Session.SetString("Usuario", usuarioLogueado.NombreDeUsuario);
            HttpContext.Session.SetString("Rol", usuarioLogueado.Rol.ToString());
            return RedirectToAction("Index","Usuario");
        }
        catch (Exception ex)
        {
            
            _logger.LogError(ex.ToString());
            return BadRequest(RedirectToAction("Index"));
        }

    }

    [HttpGet]
    public IActionResult CerrarSesion(){
        try
        {
            if(!IsLogin()){return View("Index");}
            DesloguearUsuario();

            if (HttpContext.Session.GetString("Nombre") == null)
            {
                _logger.LogInformation("Sesion cerrada exitosamente");
            }
            
            return View("Index"); 
        }
        catch (Exception ex)
        {
            if (HttpContext.Session.GetString("Nombre") != null)
            {
                _logger.LogWarning("No se pudeo cerrar sesion");
                return RedirectToAction("Index", "Home");
            }
            _logger.LogError(ex.ToString());
            return BadRequest(RedirectToAction("Index"));    
        }
    }
    
    private void DesloguearUsuario()
    {
        HttpContext.Session.Clear();
        HttpContext.Session.Remove("Usuario");
        HttpContext.Session.Remove("Contrasenia");
    }

//Control de sesion
    private bool IsLogin()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("Rol"))){
            return false;
        }
        else{
            return true;
        }
    }
}

