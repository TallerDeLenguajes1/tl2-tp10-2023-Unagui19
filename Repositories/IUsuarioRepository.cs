using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using Kanban.Models;

namespace Kanban.Repositorios
{
    public interface IUsuarioRepository
    {
        public List<Usuario> GetAll();
        public Usuario GetById(int id);
        public void Create(Usuario usuario);
        public void Remove(int id);
        public void Update(Usuario usuario, int id);
    }
}