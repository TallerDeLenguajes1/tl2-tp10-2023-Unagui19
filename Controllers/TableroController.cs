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

    public TableroController(ILogger<TableroController> logger,ITableroRepository repoTablero)
    {
        _repoTablero = repoTablero;
        _logger = logger;
    }

//Listar Tableros
    public IActionResult Index()
    {
        if(IsOperator(HttpContext)){
            if (IsAdmin(HttpContext))
            {
                List<Tablero> tableros = _repoTablero.ListarTableros();
                var VModels = tableros.Select(tab => new IndexTableroViewModel(tab)).ToList();
                return View(VModels);  
            }
            else
            {
                int? idUsuario = HttpContext.Session.GetInt32("IdUsuario");
                if (idUsuario==null)
                {
                    idUsuario = 0;
                }
                var tableros1 = _repoTablero.ListarTablerosPorUsuario((int)idUsuario);
                var VModel1 = tableros1.Select(tablero=> new IndexTableroViewModel(tablero)).ToList();
                return View(VModel1);
            }
        }
        else
        {
             return RedirectToAction("Index","Login");;
        }
    }

//Crear Tablero
    [HttpGet]
    public IActionResult CrearTablero(){
        return View(new CrearTableroViewModel());
    }

    [HttpPost]
    public IActionResult CrearTablero(CrearTableroViewModel tab){
        if (!ModelState.IsValid){return RedirectToAction("Index");}
        Tablero tablero = new Tablero(tab);
        _repoTablero.CrearTablero(tablero);
        return RedirectToAction("Index");
    }

//Modificar tableros
    [HttpGet]
    public IActionResult ModificarTablero(int idTablero){
        var VModel = new ModificarTableroViewModel(_repoTablero.BuscarTableroPorId(idTablero));
        return View(VModel);
    }

    [HttpPost]
    public IActionResult ModificarTablero(ModificarTableroViewModel modTablero){
        if (!ModelState.IsValid){return RedirectToAction("Index");}
        var tablero = new Tablero(modTablero);
        _repoTablero.ModificarTablero(tablero.Id,tablero);
        return RedirectToAction("Index");
    }

//Eliminar tablero
    public IActionResult EliminarTablero(int idTablero){
        _repoTablero.EliminarTablero(idTablero);
        return RedirectToAction("Index");
    }

//Control de variables de sesion
    private bool IsAdmin(HttpContext varSesion)
    {
        if (IsOperator(varSesion) && HttpContext.Session.GetString("Rol") == Roles.admin.ToString()){
            return true;
        }
        else{
            return false;
        }
    }

    private bool IsOperator(HttpContext varSesion)
    {
        if (varSesion.Session.Id != null ){
            return true;
        }
        else{
            return false;
        }
    }
}