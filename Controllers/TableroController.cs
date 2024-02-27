using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Taller2_TP10.Models;
using Taller2_TP10.ViewModels;
using Taller2_TP10.Repositorios;

namespace Taller2_TP10.Controllers;

public class TableroController : Controller
{
    private readonly ILogger<TableroController> _logger;
    private readonly ITableroRepository _repoTablero;
    private readonly ITareaRepository _repoTarea;
    private readonly IUsuarioRepository _repoUsuario;

    public TableroController(ILogger<TableroController> logger,ITableroRepository repoTablero, ITareaRepository repoTarea, IUsuarioRepository repoUsuario)
    {
        _repoTablero = repoTablero;
        _repoTarea = repoTarea;
        _repoUsuario = repoUsuario;
        _logger = logger;
    }

//-------------------------------------------------------------

//Listar Tableros
    public IActionResult Index()
    {
        List<Usuario> usuarios = _repoUsuario.ListarUsuarios();
        try{
            if(IsLogin()==true){
                if (IsAdmin())
                {
                    List<Tablero> tableros = _repoTablero.ListarTableros();
                    var VModels = tableros.Select(tab => new IndexTabAdminViewModel(tab,usuarios)).ToList();
                    return View("IndexTabAdmin",VModels);  
                }
                else
                {
                    int idUsuario = Convert.ToInt32(HttpContext.Session.GetString("IdUsuario"));
                    if (idUsuario==0)
                    {
                        idUsuario = 0;
                    }

                    var tareasUAsignado = _repoTarea.ListarTareasPorUsuario(idUsuario);
                    var tablerosAsignados = new List<Tablero>();
                    foreach (var item in tareasUAsignado)
                    {
                        Tablero tablero = _repoTablero.BuscarTableroPorId(item.IdTablero);
                        if (!tablerosAsignados.Any(t => t.Id == tablero.Id))
                        {
                            tablerosAsignados.Add(tablero);
                        }
                    }

                    var tablerosPropios = _repoTablero.ListarTablerosPorUsuario((int)idUsuario);
                    var VModel1 = new IndexTabOperadorViewModel(tablerosPropios, tablerosAsignados, usuarios);
                    return View("IndexTabOperador",VModel1);
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


//-------------------------------------------------------------

//Crear Tablero
    [HttpGet]
    public IActionResult CrearTablero(){
        if(!IsLogin()){return RedirectToAction("Index", "Login");}
        var usuarios = _repoUsuario.ListarUsuarios();
        if (IsAdmin())
        {
            return View("crearTableroAdmin",new CrearTableroViewModel(usuarios, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario"))));        
        }
        else{
            return View("crearTableroOpe",new CrearTableroViewModel(usuarios, Convert.ToInt32(HttpContext.Session.GetString("IdUsuario"))));        
        }
    }

    [HttpPost]
    public IActionResult CrearTablero(CrearTableroViewModel tab){
        try{
            Tablero tablero = new Tablero(tab);
            if (!ModelState.IsValid){
                _logger.LogWarning("No se pudo crear el tablero ");
                return RedirectToAction("Index");
            }
            _repoTablero.CrearTablero(tablero);
            _logger.LogInformation("Tablero creado con éxito");
            return RedirectToAction("Index");
        }
        catch(Exception ex){
            _logger.LogError(ex,ToString());
            return BadRequest(RedirectToAction("Index"));
        }
    }

    //-------------------------------------------------------------


//Modificar tableros
    [HttpGet]
    public IActionResult ModificarTablero(int idTablero){
        if(!IsLogin()){return RedirectToAction("Index", "Login");}
        var usuarios = _repoUsuario.ListarUsuarios();
        var VModel = new ModificarTableroViewModel(_repoTablero.BuscarTableroPorId(idTablero), usuarios);
        return View(VModel);
    }

    

    [HttpPost]
    public IActionResult ModificarTablero(ModificarTableroViewModel modTablero){
        try{
            if (!ModelState.IsValid){
                _logger.LogWarning("No se pudo modificar el tablero ");
                return RedirectToAction("Index");
            }
            var tablero = new Tablero(modTablero);
            _repoTablero.ModificarTablero(tablero.Id,tablero);
            _logger.LogInformation("Tablero: "+ tablero.Nombre+", modificado exitosamente");
            return RedirectToAction("Index");
        }
        catch(Exception ex){
            _logger.LogError(ex,ToString());
            return BadRequest(RedirectToAction("Index"));
        }
    }
    //-------------------------------------------------------------


//Eliminar tablero
    public IActionResult EliminarTablero(int idTablero){
        try{
            _repoTablero.EliminarTablero(idTablero);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,ToString());
            return RedirectToAction("Index");
        }
    }

//-------------------------------------------------------------

//Control de variables de sesion
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