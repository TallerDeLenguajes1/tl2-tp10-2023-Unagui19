using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Taller2_TP10.Models;
using Taller2_TP10.Repositorios;

namespace Taller2_TP10.Controllers;

public class UsuarioController : Controller
{
    private readonly ILogger<UsuarioController> _logger;
    private UsuarioRepository repoUsuario;

    public UsuarioController(ILogger<UsuarioController> logger)
    {
        repoUsuario = new UsuarioRepository();
        _logger = logger;
    }

//Listar Usuarios
    public IActionResult Index()
    {
        return View(repoUsuario.ListarUsuarios());
    }

//Crear Usuario
    [HttpGet]
    public IActionResult CrearUsuario(){
        return View(new Usuario());
    }

    [HttpPost]
    public IActionResult CrearUsuario(Usuario usuario){
        repoUsuario.CrearUsuario(usuario);
        return RedirectToAction("Index");
    }

//Modificar usuarios
    [HttpGet]
    public IActionResult ModificarUsuario(int idUsuario){
        return View(repoUsuario.BuscarUsuarioPorId(idUsuario));
    }

    [HttpPost]
    public IActionResult ModificarUsuario(Usuario usuario){
        repoUsuario.ModificarUsuario(usuario.Id, usuario);
        return RedirectToAction("Index");
    }

    public IActionResult EliminarUsuario(int idUsuario){
        repoUsuario.EliminarUsuario(idUsuario);
        return RedirectToAction("Index");
    }
    // public IActionResult Privacy()
    // {
    //     return View();
    // }

    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }
}
