using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Taller2_TP10.Models;
using Taller2_TP10.ViewModels;
using Taller2_TP10.Repositorios;

namespace Taller2_TP10.Controllers;

public class TareaController : Controller
{
    private readonly ILogger<TareaController> _logger;
    private readonly ITareaRepository _repoTarea;
    private readonly IUsuarioRepository _repoUsuario;
    private readonly ITableroRepository _repoTablero;

    public TareaController(ILogger<TareaController> logger, ITareaRepository repoTarea, IUsuarioRepository repoUsuario, ITableroRepository repoTablero)
    {
        _repoTarea = repoTarea;
        _repoUsuario = repoUsuario;
        _repoTablero = repoTablero;
        _logger = logger;
    }

//Listar Usuarios
    public IActionResult Index()
    {
        var usuarios = _repoUsuario.ListarUsuarios();
        try{
            if(IsLogin()==true){
                if (IsAdmin())
                {
                    List<Tarea> tareas = _repoTarea.ListarTareas();
                    List<Tablero> tableros = _repoTablero.ListarTableros();
                    var VModel = tareas.Select(tar => new IndexTareaViewModel(tar, usuarios, tableros)).ToList();
                    return View(VModel);
                }
                else
                {
                    int idUsuario = Convert.ToInt32(HttpContext.Session.GetString("IdUsuario"));
                    if (idUsuario==0)
                    {
                        idUsuario = 0;
                    }
                    
                    //creadas por el usuario
                    var tablerosPropios = _repoTablero.ListarTablerosPorUsuario(idUsuario);
                    var taPropias = new List<Tarea>();
                    foreach (var item in tablerosPropios)
                    {
                        foreach (var item2 in _repoTarea.ListarTareasPorTablero(item.Id))
                        {
                            taPropias.Add(item2);
                        }
                    }
                    //
                
                    var tAsociadas = _repoTarea.ListarTareasPorUsuario((int)idUsuario);
                    var tNoAsociadas = _repoTarea.ListarTareas().Where(tar => tar.IdUsuarioAsignado == null || tar.IdUsuarioAsignado !=(int) idUsuario).ToList();
                    

                    List<Tablero> tableros = _repoTablero.ListarTableros();
                    var VModel1 = new IndexOperadorViewModel(taPropias,tAsociadas, tNoAsociadas, usuarios, tableros);
                    return View("IndexOperador",VModel1);
                }
            }
            else
            {
                return RedirectToAction("Index","Login");
            }
        }
        catch (Exception ex){
            _logger.LogError(ex.ToString());
            return BadRequest(RedirectToAction("Index"));
        }

    }


//Crear Usuario
    [HttpGet]
    public IActionResult CrearTarea(){
        if(!IsLogin()){return BadRequest();}
        var usuarios = _repoUsuario.ListarUsuarios();
        List<Tablero> tableros = _repoTablero.ListarTableros();
        if (IsAdmin())
        {
            return View("crearTareaAdmin",new CrearTareaViewModel(usuarios, tableros));        
        }
        else{
            return View("crearTareaOpe",new CrearTareaViewModel(usuarios, tableros));        
        }
    }

    [HttpPost]
    public IActionResult CrearTarea(CrearTareaViewModel nuevaTarea){
        try{
            if (!ModelState.IsValid){
                _logger.LogWarning("No se pudo crear la tarea");
                return RedirectToAction("Index");
            }
            var tarea = new Tarea(nuevaTarea);
            _repoTarea.CrearTarea(tarea);
            _logger.LogInformation("Tarea "+tarea.Nombre+" creada exitosamente");
            return RedirectToAction("Index");
        }
        catch(Exception ex){
            _logger.LogError(ex,ToString());
            return BadRequest(RedirectToAction("Index"));
        }
    }

//Modificar tarea
    [HttpGet]
    public IActionResult ModificarTarea(int idTarea){
        if(!IsLogin()){return BadRequest();}
        var usuarios = _repoUsuario.ListarUsuarios();
        List<Tablero> tableros = _repoTablero.ListarTableros();
        var VModel = new ModificarTareaViewModel(_repoTarea.BuscarTareaPorId(idTarea), usuarios, tableros);
        return View(VModel);
    }

    [HttpPost]
    public IActionResult ModificarTarea(ModificarTareaViewModel modTarea){
        try{   
            if (!ModelState.IsValid){
                _logger.LogWarning("No se pudo modificar la tarea");
                return RedirectToAction("Index");
            }
            var tarea = new Tarea(modTarea);
            _repoTarea.ModificarTarea(tarea.Id,tarea);
            _logger.LogInformation("Tarea "+modTarea.Nombre+" modificada con exito.");
            return RedirectToAction("Index");
        }
        catch(Exception ex){
            _logger.LogError(ex,ToString());
            return BadRequest(RedirectToAction("Index"));
        }
    }

//Eliminar tarea
    public IActionResult EliminarTarea(int idTarea){
        try{
            _repoTarea.EliminarTarea(idTarea);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,ToString());
            return RedirectToAction("Index");
        }
    }

//Asignar usuario a una tarea
    [HttpGet]
    public IActionResult AsignarUsuarioATarea(int idTarea){
        if(!IsLogin()){return BadRequest();}
        if(!IsLogin())
        {
            TempData["Mensaje"] = "Debe iniciar sesión para acceder a esta página.";
            return RedirectToAction("Index", "Login");
        }
        var tarea = _repoTarea.BuscarTareaPorId(idTarea);
        List<Usuario> usuarios = _repoUsuario.ListarUsuarios();
        var VModel = new AsignarUsuarioATareaViewModel(idTarea, usuarios);
        // var VModel = new AsignarUsuarioATareaViewModel(_repoTarea.BuscarTareaPorId(idTarea), usuarios);
        return View(VModel);
    }

    [HttpPost]
    public IActionResult AsignarUsuarioATarea(AsignarUsuarioATareaViewModel asignarId){
        try{
            if(asignarId.IdUsuarioAsignado==0){
                _repoTarea.AsignarUsuarioATarea(0, asignarId.IdTarea);
                _logger.LogInformation("Se quitó al usuario asignado y no se asignó uno nuevo de momento.");            
                return RedirectToAction("Index");
            }
            else{
                var usuario = _repoUsuario.BuscarUsuarioPorId(asignarId.IdUsuarioAsignado);
                _repoTarea.AsignarUsuarioATarea(usuario.Id, asignarId.IdTarea);
                _logger.LogInformation("Usuario "+usuario.NombreDeUsuario+" asignado a la tarea.");            
                return RedirectToAction("Index");
            }
        }
        catch(Exception ex){
            _logger.LogError(ex,ToString());
            return BadRequest(RedirectToAction("Index"));
        }
    }


//Modificar estado de la tarea
    [HttpGet]
    public IActionResult ModificarEstado(int idTarea){
        if(!IsLogin()){return BadRequest();}
        var tarea = _repoTarea.BuscarTareaPorId(idTarea);
        var VModel = new ModificarEstadoViewModel(tarea.Id,tarea.Nombre,tarea.EstadoTarea);
        return View(VModel);
    }

    [HttpPost]
    public IActionResult ModificarEstado(ModificarEstadoViewModel modTarea){
        try{
            if (!ModelState.IsValid){
                _logger.LogWarning("Error al modificar el estado de la tarea");    
                return RedirectToAction("Index");
            }
            _repoTarea.ModificarTarea(modTarea.Id,modTarea.EstadoTarea);
            _logger.LogInformation("Estado de la tarea "+modTarea.Nombre+" cambiado a "+modTarea.EstadoTarea);
            return RedirectToAction("Index");
        }
        catch(Exception ex){
            _logger.LogError(ex,ToString());
            return BadRequest(RedirectToAction("Index"));
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