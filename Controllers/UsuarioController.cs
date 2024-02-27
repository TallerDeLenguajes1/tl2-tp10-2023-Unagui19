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
    private readonly ITableroRepository _repoTablero;

    public UsuarioController(ILogger<UsuarioController> logger, IUsuarioRepository repoUsuario, ITableroRepository repoTablero)
    {
        _repoUsuario = repoUsuario;
        _repoTablero = repoTablero;
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
                    return View("IndexAdmin",VModels);
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
        if(!IsLogin()){return RedirectToAction("Index", "Login");}
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
        if(!IsLogin()){return RedirectToAction("Index", "Login");}
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

//Eliminar Usuarios
    public IActionResult EliminarUsuario(int IdUsuario)
    {
        try
        {
            if (!IsLogin()) return RedirectToAction("Index", "Login");

            int idSesion = Convert.ToInt32(HttpContext.Session.GetString("IdUsuario"));

            var usuario = _repoUsuario.BuscarUsuarioPorId(IdUsuario);

            if (usuario.Id == 0) return NotFound("No se encontro el recurso.");

            if (idSesion==0) return NotFound("No se encontro el recurso.");

            List<Tablero> tablerosAsociados = _repoTablero.ListarTablerosPorUsuario(IdUsuario);

            if (!IsAdmin()) {
                if (idSesion != IdUsuario) return NotFound("No se encontro el recurso.");
                _repoUsuario.EliminarUsuario(IdUsuario);
                _repoTablero.EliminarTablerosPorUsuario(IdUsuario);
                HttpContext.Session.Clear();
                _logger.LogInformation("Usuario eliminado satisfactoriamente");
                return RedirectToAction("Index", "Login");
            } else {
                if(IdUsuario == idSesion && _repoUsuario.ContarAdmins()==1){
                    string mensaje = "El usuario es el unico administrador, no se puede eliminar la cuenta.";
                    TempData["unicoAdmin"] = mensaje;
                    _logger.LogWarning(mensaje);
                    return RedirectToAction("Index");
                }
                else {
                    int admins = _repoUsuario.ContarAdmins();
                    if((IdUsuario == idSesion) && (admins>1)){
                        _repoUsuario.EliminarUsuario(IdUsuario);
                        _logger.LogInformation("Usuario admin eliminado satisfactoriamente");
                        _repoTablero.EliminarTablerosPorUsuario(IdUsuario);
                        _logger.LogInformation("Tableros asociados al usuario tambien eliminados");
                        return RedirectToAction("Index", "Login");
                    }
                    else{
                        _repoUsuario.EliminarUsuario(IdUsuario);
                        _logger.LogInformation("Usuario eliminado satisfactoriamente");
                        _repoTablero.EliminarTablerosPorUsuario(IdUsuario);
                        _logger.LogInformation("Tableros asociados al usuario tambien eliminados");
                        return RedirectToAction("Index", "Usuario");
                    }
                }
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
