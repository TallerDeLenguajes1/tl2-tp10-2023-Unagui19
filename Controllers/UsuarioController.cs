using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_Unagui19.Models;
using tl2_tp10_2023_Unagui19.Repositorios;
using tl2_tp10_2023_Unagui19.ViewModels;

namespace tl2_tp10_2023_Unagui19.Controllers;

public class UsuarioController : Controller
{
    private readonly ILogger<UsuarioController> _logger;
    private IUsuarioRepository _repoUsuario;
    // List<Usuario> usuarios=new List<Usuario>();
    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository RepoUsuario)
    {
        _logger = logger;
        _repoUsuario = RepoUsuario;

    }

    // [HttpGet]
    public IActionResult Index()
    {
        List<Usuario>usuarios=_repoUsuario.GetAll();
        var VModel = usuarios.Select(usu=> new IndexUsuarioViewModel(usu)).ToList();
        return View(VModel);

    }

    [HttpGet]
    public IActionResult CrearUsuario()
    {   
        return View(new CrearUsuarioViewModel());
    }

    [HttpPost]
    public IActionResult CrearUsuario(CrearUsuarioViewModel nuevoUsu)
    {   
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Index");
        }
        var usuario = new Usuario(nuevoUsu);
        _repoUsuario.Create(usuario);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult ModificarUsuario(int idUsuario)
    {  
        var VModel = new ModificarUsuarioViewModel(_repoUsuario.GetById(idUsuario));
        return View(VModel);
    }


    [HttpPost]
    public IActionResult ModificarUsuario(ModificarUsuarioViewModel VModel)
    {   
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Index");
        }
        var usuario = new Usuario(VModel);
        _repoUsuario.Update(usuario,usuario.Id);
        return RedirectToRoute(new { controller = "Usuario", action = "Index" });
    }

    public IActionResult EliminarUsuario(int idUsuario)
    {  
        _repoUsuario.Remove(idUsuario);
        return RedirectToAction("Index");
    }


    

//Metodos para ver temas de sesion
    private bool IsAdmin()
    {
        if (HttpContext.Session != null && HttpContext.Session.GetString("Rol") == "Admin")
            return true;

        return false;
    }

    private bool IsUser()
    {
        if (HttpContext.Session != null && (HttpContext.Session.GetString("Rol") == "Admin" || HttpContext.Session.GetString("Rol") == "Operador"))
            return true;

        return false;
    }




    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }
}
