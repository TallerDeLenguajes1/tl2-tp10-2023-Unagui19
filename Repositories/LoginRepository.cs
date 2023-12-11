// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using System.Data.SQLite;
// using tl2_tp10_2023_Unagui19.Models; 
// using System.Diagnostics;
// using Microsoft.AspNetCore.Mvc;

// namespace tl2_tp10_2023_Unagui19.Repositorios;
// public class LoginRepository
// {
//     private bool IsAdmin()
//     {
//         if (HttpContext.Session != null && HttpContext.Session.GetString("Rol") == "Admin")
//             return true;

//         return false;
//     }

//     private bool IsUser()
//     {
//         if (HttpContext.Session != null && (HttpContext.Session.GetString("Rol") == "Admin" || HttpContext.Session.GetString("Rol") == "Operador"))
//             return true;

//         return false;
//     }
// }
