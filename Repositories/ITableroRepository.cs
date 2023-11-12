using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using Kanban.Models;

namespace Kanban.Repositorios
{  
    public interface ITableroRepository
    {
        public List<Tablero> GetAll();
        public Tablero GetById(int id);
        public void Create(Tablero Tablero);
        public void Remove(int id);
        public void Update(Tablero Tablero, int id);

        public List<Tablero> GetTablerosPorUsuario(int idUsuario);
    }
}