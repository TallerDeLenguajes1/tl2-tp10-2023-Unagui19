using Taller2_TP10.Models;

namespace Taller2_TP10.Repositorios
{
    public interface ITableroRepository
    {
        public void CrearTablero(Tablero tablero);
        public void ModificarTablero(int idTablero,Tablero tablero);
        public Tablero BuscarTableroPorId(int idTablero);
        public List<Tablero> ListarTableros();
        public List<Tablero> ListarTablerosPorUsuario(int IdUsuario);
        public void EliminarTablero(int idTablero);
        
    }   

}