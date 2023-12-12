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
        public void Create(Tarea tarea);
        public void Remove(int id);
        public void Update(Tarea TareaMod, int idTarea);
        public void UpdatePorNombre (int id, string nombre);
        public void UpdatePorEstado (int id, EstadoTarea estado);
        public List<Tarea> GetTareasPorTablero(int id);
        public List<Tarea> GetTareasPorEstado(EstadoTarea estado);
        public List<Tarea> GetTareasPorUsuarioAsignado(int idUsu);
        public void AsignarUsuarioATarea(int idUsuario, int idTarea);
    }
}
