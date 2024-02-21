using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Taller2_TP10.Models;
using Taller2_TP10.Repositorios;
using Taller2_TP10.ViewModels;

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
        List<Usuario> usuarios = repoUsuario.ListarUsuarios();
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
        var usuario = new Usuario(nuevoUsu);
        repoUsuario.CrearUsuario(usuario);
        return RedirectToAction("Index");
    }

//Modificar usuarios
    [HttpGet]
    public IActionResult ModificarUsuario(int idUsuario){
        var VModel = new ModificarUsuarioViewModel(repoUsuario.BuscarUsuarioPorId(idUsuario));
        return View(VModel);
    }

    [HttpPost]
    public IActionResult ModificarUsuario(ModificarUsuarioViewModel modUsu){
        var usuario = new Usuario(modUsu);
        repoUsuario.ModificarUsuario(usuario.Id, usuario);
        return RedirectToAction("Index");
    }

    public IActionResult EliminarUsuario(int idUsuario){
        repoUsuario.EliminarUsuario(idUsuario);
        return RedirectToAction("Index");
    }

}
