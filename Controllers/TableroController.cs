using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp10_2023_Unagui19.Models;
using tl2_tp10_2023_Unagui19.Repositorios;
using tl2_tp10_2023_Unagui19.ViewModels;

namespace tl2_tp10_2023_Unagui19.Controllers;

public class TableroController : Controller
{
    private readonly ILogger<TableroController> _logger;
    private TableroRepository RepoTablero;
    public TableroController(ILogger<TableroController> logger)
    {
        _logger = logger;
        RepoTablero = new TableroRepository();

    }
public IActionResult Index()
    {
        List<Tablero>tableros=RepoTablero.GetAll();
        var VModel = tableros.Select(tablero=> new IndexTableroViewModel(tablero)).ToList();
        
        if (IsUser(HttpContext))
        {
            if (IsAdmin(HttpContext))
            {
                return View(VModel);
            }
            else
            {
                var idUsuario = HttpContext.Session.GetString("IdUsuario");
                var tableros1 = RepoTablero.GetTablerosPorUsuario(Convert.ToInt32(idUsuario));
                var VModel1 = tableros1.Select(tablero=> new IndexTableroViewModel(tablero)).ToList();
                return View(VModel1);
            }
        }
        else{
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }
    }

    [HttpGet]
    public IActionResult CrearTablero()
    {   
        return View(new CrearTableroViewModel());
    }

    [HttpPost]
    public IActionResult CrearTablero(CrearTableroViewModel tableroVm)
    {   
        var tablero = new Tablero(tableroVm);
        RepoTablero.Create(tablero);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult ModificarTablero(int idTablero)
    {  
        var tableroVm = new ModificarTableroViewModel(RepoTablero.GetById(idTablero));
        return View(tableroVm);
    }


    [HttpPost]
    public IActionResult ModificarTablero(ModificarTableroViewModel tableroVm)
    {   
        var tablero = new Tablero(tableroVm);
        RepoTablero.Update(tablero,tablero.Id);
        return RedirectToRoute(new { controller = "Tablero", action = "Index" });
    }

    public IActionResult EliminarTablero(int idTablero)
    {  
        RepoTablero.Remove(idTablero);
        return RedirectToAction("Index");
    }

    //Metodos para ver temas de sesion
    private bool IsAdmin(HttpContext varSesion)
    {
        if (IsUser(varSesion) && HttpContext.Session.GetString("Rol") == NivelDeAcceso.admin.ToString()){
            return true;
        }
        else{
            return false;
        }
    }

    private bool IsUser(HttpContext varSesion)
    {
        if (varSesion.Session.Id != null ){
            return true;
        }
        else{
            return false;
        }
    }

    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }
}
