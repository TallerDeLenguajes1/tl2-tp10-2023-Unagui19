using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using tl2_tp10_2023_Unagui19.Models;

namespace tl2_tp10_2023_Unagui19.Repositorios
{
    public interface IUsuarioRepository
    {
        public List<Usuario> GetAll();
        public Usuario GetById(int idUsu);
        public void Create(Usuario usuario);
        public void Remove(int id);
        public void Update(Usuario usuario, int id);
    }
}