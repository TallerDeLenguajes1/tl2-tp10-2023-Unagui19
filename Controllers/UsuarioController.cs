using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Taller2_TP10.Models;
using Taller2_TP10.Repositorios;
using Taller2_TP10.ViewModels;

namespace Taller2_TP10.Controllers;

public class UsuarioController : Controller
{
    private readonly ILogger<UsuarioController> _logger;
    private readonly IUsuarioRepository _repoUsuario;

    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository repoUsuario)
    {
        _repoUsuario = repoUsuario;
        _logger = logger;
    }

//Listar Usuarios
    public IActionResult Index()
    {
        if (!ModelState.IsValid){return RedirectToAction("Index");}
        List<Usuario> usuarios = _repoUsuario.ListarUsuarios();
        var VModels = usuarios.Select(usu => new IndexUsuarioViewModel(usu)).ToList();
        return View(VModels);
    }

//Crear Usuario
    [HttpGet]
    public IActionResult CrearUsuario(){
        return View(new CrearUsuarioViewModel());
    }

    [HttpPost]
    public IActionResult CrearUsuario(CrearUsuarioViewModel nuevoUsu){
        if (!ModelState.IsValid){return View("Index");}
        var usuario = new Usuario(nuevoUsu);
        _repoUsuario.CrearUsuario(usuario);
        return RedirectToAction("Index");
    }

//Modificar usuarios
    [HttpGet]
    public IActionResult ModificarUsuario(int idUsuario){
        var VModel = new ModificarUsuarioViewModel(_repoUsuario.BuscarUsuarioPorId(idUsuario));
        return View(VModel);
    }

    [HttpPost]
    public IActionResult ModificarUsuario(ModificarUsuarioViewModel modUsu){
        if (!ModelState.IsValid){return View("Index");}
        var usuario = new Usuario(modUsu);
        _repoUsuario.ModificarUsuario(usuario.Id, usuario);
        return RedirectToAction("Index");
    }

    public IActionResult EliminarUsuario(int idUsuario){
        _repoUsuario.EliminarUsuario(idUsuario);
        return RedirectToAction("Index");
    }

}
