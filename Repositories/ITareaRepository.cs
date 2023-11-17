using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using tl2_tp10_2023_Unagui19.Models;

namespace tl2_tp10_2023_Unagui19.Repositorios
{
    public interface ITareaRepository
    {
        public List<Tarea> GetAll();
        public Tarea GetById(int id);
        public void Create(Tarea Tarea);
        public void Remove(int id);
        public void Update(Tarea Tarea, int id);
        public List<Tarea> GetTareasPorTablero(int id);
        public void AsignarUsuarioATarea(int idUsuario, int idTarea);
    }
}
