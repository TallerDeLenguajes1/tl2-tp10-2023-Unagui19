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
        try{
            if(IsLogin()==true){
                if (!ModelState.IsValid){return RedirectToAction("Index");}
                List<Usuario> usuarios = _repoUsuario.ListarUsuarios();
                var VModels = usuarios.Select(usu => new IndexUsuarioViewModel(usu)).ToList();

                if (IsAdmin())
                {
                    return View(VModels);
                }
                else
                {
                    return View("IndexOperador",VModels);
                }
            }
            else
            {
                return RedirectToAction("Index","Login");
            }
        }
        catch (Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest(View("Index","Login"));
        }

    }
        

//Crear Usuario
    [HttpGet]
    public IActionResult CrearUsuario(){
        if(!IsLogin()){return BadRequest("No posee autorizacion para ingresar a la url deseada");}
        return View(new CrearUsuarioViewModel());
    }

    [HttpPost]
    public IActionResult CrearUsuario(CrearUsuarioViewModel nuevoUsu){
        try{
            if (!ModelState.IsValid){return View("Index");}
            var usuario = new Usuario(nuevoUsu);
            _repoUsuario.CrearUsuario(usuario);
            _logger.LogInformation("Usuario: " + usuario.NombreDeUsuario+ " ha sido creado.");
            return RedirectToAction("Index");
        }
        catch(Exception ex){
            _logger.LogError(ex,ToString());
            return BadRequest(RedirectToAction("Index"));
        }
    }

//Modificar usuarios
    [HttpGet]
    public IActionResult ModificarUsuario(int idUsuario){
        if(!IsLogin()){return BadRequest("No posee autorizacion para ingresar a la url deseada");}
        var VModel = new ModificarUsuarioViewModel(_repoUsuario.BuscarUsuarioPorId(idUsuario));
        return View(VModel);
    }

    [HttpPost]
    public IActionResult ModificarUsuario(ModificarUsuarioViewModel modUsu){
        try{
            if (!ModelState.IsValid){return View("Index");}
            var usuario = new Usuario(modUsu);
            _repoUsuario.ModificarUsuario(usuario.Id, usuario);
            _logger.LogInformation("Usuario modificado con exito");
            return RedirectToAction("Index");
        }
        catch(Exception ex){
            _logger.LogError(ex,ToString());
            return BadRequest(RedirectToAction("Index"));
        }
    }

    public IActionResult EliminarUsuario(int Id)
    {
        try
        {
            if (!IsLogin()) return RedirectToAction("Index", "Login");

            int idUsuario = Convert.ToInt32(HttpContext.Session.GetString("Id"));

            var usuario = _repoUsuario.BuscarUsuarioPorId(Id);

            if (usuario.Id == 0) return NotFound("No se encontro el recurso.");

            if (!IsAdmin()) {
                if (idUsuario != Id) return NotFound("No se encontro el recurso.");
                _repoUsuario.EliminarUsuario(Id);
                return RedirectToAction("Index", "Login");
            } else {
                if(Id == idUsuario) return NotFound("No se encontro el recurso.");
                _repoUsuario.EliminarUsuario(Id);
                return RedirectToAction("ListarUsuarios", "Usuarios");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,ToString());
            return RedirectToAction("Index");
        }
    }


//Control de sesion
    private bool IsAdmin()
    {
        // if ( HttpContext.Session.GetString("Rol") == Roles.admin.ToString()){
        if ( HttpContext.Session.GetString("Rol") == "admin"){
            return true;
        }
        else{
            return false;
        }
    }

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
