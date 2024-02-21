using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Taller2_TP10.Models;
using Taller2_TP10.ViewModels;
using Taller2_TP10.Repositorios;

namespace Taller2_TP10.Controllers;

public class TableroController : Controller
{
    private readonly ILogger<TableroController> _logger;
    private TableroRepository repoTablero;

    public TableroController(ILogger<TableroController> logger)
    {
        repoTablero = new TableroRepository();
        _logger = logger;
    }

//Listar Tableros
    public IActionResult Index()
    {
        if(IsOperator(HttpContext)){
            if (IsAdmin(HttpContext))
            {
                List<Tablero> tableros = repoTablero.ListarTableros();
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
                var tableros1 = repoTablero.ListarTablerosPorUsuario((int)idUsuario);
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
        Tablero tablero = new Tablero(tab);
        repoTablero.CrearTablero(tablero);
        return RedirectToAction("Index");
    }

//Modificar tableros
    [HttpGet]
    public IActionResult ModificarTablero(int idTablero){
        var VModel = new ModificarTableroViewModel(repoTablero.BuscarTableroPorId(idTablero));
        return View(VModel);
    }

    [HttpPost]
    public IActionResult ModificarTablero(ModificarTableroViewModel modTablero){
        var tablero = new Tablero(modTablero);
        repoTablero.ModificarTablero(tablero.Id,tablero);
        return RedirectToAction("Index");
    }

//Eliminar tablero
    public IActionResult EliminarTablero(int idTablero){
        repoTablero.EliminarTablero(idTablero);
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