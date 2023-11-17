using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_Unagui19.Models;
using tl2_tp10_2023_Unagui19.Repositorios;

namespace tl2_tp10_2023_Unagui19.Controllers;

public class UsuarioController : Controller
{
    private readonly ILogger<UsuarioController> _logger;
    private UsuarioRepositorio RepoUsuario;
    // List<Usuario> usuarios=new List<Usuario>();
    public UsuarioController(ILogger<UsuarioController> logger)
    {
        _logger = logger;
        RepoUsuario = new UsuarioRepositorio();

    }

    // [HttpGet]
    public IActionResult Index()
    {
        List<Usuario>usuarios=RepoUsuario.GetAll();
        return View(usuarios);
    }

    [HttpGet]
    public IActionResult CrearUsuario()
    {   
        return View(new Usuario());
    }

    [HttpPost]
    public IActionResult CrearUsuario(Usuario usuario)
    {   
        RepoUsuario.Create(usuario);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult ModificarUsuario(int idUsuario)
    {  
        return View(RepoUsuario.GetById(idUsuario));
    }


    [HttpPost]
    public IActionResult ModificarUsuario(Usuario usuario)
    {   
        RepoUsuario.Update(usuario,usuario.Id);
        return RedirectToAction("Index");
    }

    public IActionResult EliminarUsuario(int idUsuario)
    {  
        RepoUsuario.Remove(idUsuario);
        return RedirectToAction("Index");
    }

    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }
}
