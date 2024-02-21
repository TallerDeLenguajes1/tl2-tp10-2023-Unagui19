using Taller2_TP10.Models;

namespace Taller2_TP10.Repositorios
{
    public interface ITareaRepository
    {
         public void CrearTarea(Tarea tarea);
        public void ModificarTarea(int idTarea, Estado estado);
        public Tarea BuscarTareaPorId(int idTarea);
        public List<Tarea> ListarTareasPorUsuario(int idUsuario);
        public List<Tarea> ListarTareasPorTablero(int idTablero);
        public List<Tarea> ListarTareas();
        public void EliminarTarea(int IdTarea);
        public void AsignarTareaAUsuario(int idUsuario, int idTarea);
        public int ContarTareasPorEstado(Estado estado);
    }  
}