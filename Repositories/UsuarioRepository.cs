using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using Kanban.Models;

namespace Kanban.Repositorios
{
    public class UsuarioRepository: IUsuarioRepository
    {
        private string cadenaConexion = "Data Source=db/Kanban.db;Cache=Shared"; // crea la conexion 
        
        public void Create(Usuario usuario){
            var queryString = $"INSERT INTO usuario (nombre_de_usuario) VALUES (@nombre_de_usuario)"; //mi consulta
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))//crea un dato tipo SQLiteConnetion para usarlo e el comando
            {
                connection.Open();//abre la conexion
                var command = new SQLiteCommand(queryString, connection);//paso mi consulta y la conexion para ejectuar el comando
 
                //command.Parameters.Add(new SQLiteParameter("@id_usuario", usuario.Id));//agrego el valor del parametro
                command.Parameters.Add(new SQLiteParameter("@nombre_de_usuario", usuario.NombreDeUsuario));
                command.ExecuteNonQuery();
                connection.Close();   
            }
        }
        public void Update (Usuario Usuario, int id)//actualizar la tabla
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);//conectando
            SQLiteCommand command = connection.CreateCommand();//creando comando
            command.CommandText = $"UPDATE Usuarios SET nombre_de_usuario = '{Usuario.NombreDeUsuario}' WHERE id_usuario = '{Usuario.Id}';";
            connection.Open();//abrir conexion
            command.ExecuteNonQuery();// no me devuelve nada, solo modifica la bd
            connection.Close();
        }

        public List<Usuario> GetAll()
        {
            var queryString = @"SELECT * FROM Usuario;";
            List<Usuario> Usuarios = new List<Usuario>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection); //creando comadno sqlLiteConnection
                connection.Open();
            
                using(SQLiteDataReader reader = command.ExecuteReader())// Devuelve la consulta, es decir que lee la base de datos y trae lo que se pide
                {
                    while (reader.Read())//revisa si hay tuplas para leer, es decir si esta bien hecha la consulta
                    {
                        var Usuario = new Usuario();
                        Usuario.Id = Convert.ToInt32(reader["id_usuario"]);
                        Usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                        Usuarios.Add(Usuario);//agrego a la lista de usuarios el usuario con sus datos recuperados de la base de datos
                    }
                }
                connection.Close();// cierro la conexion
            }
            return Usuarios;
        }

        public Usuario GetById(int idUsu)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);//crear variable de conexion
            var Usuario = new Usuario();
            SQLiteCommand command = connection.CreateCommand();//comando para usar la base
            command.CommandText = $"SELECT * FROM Usuario WHERE id_usuario = '{idUsu}';";
            //command.CommandText = "SELECT * FROM Usuarios WHERE idUsu = @idUsuario"; otra opcion
            command.Parameters.Add(new SQLiteParameter("@id_usuario", idUsu));//comando necesario para buscar los usuarios que cumplan con la condicion
            connection.Open();
            using(SQLiteDataReader reader = command.ExecuteReader())// pa leer los datos de la base
            {
                while (reader.Read())//mientras haya tuplas que leer
                {
                    Usuario.Id = Convert.ToInt32(reader["id_usuario"]);
                    Usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();

                }
            }
            connection.Close();
            return (Usuario);
        }



        public void Remove(int idUsu)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);// crear variable de conexion
            SQLiteCommand command = connection.CreateCommand();// comando pa moverme
            command.CommandText = $"DELETE FROM Usuarios WHERE id_usuario = '{idUsu}';";// consulta para eliminar tuplas con su condicion
            connection.Open();
            command.ExecuteNonQuery();//realiza la consulta pero no me devuelve nada
            connection.Close();
        }

        public void UpdateUsuarioPorNombre (int id, string nombre)//actualizar la Tarea
        {
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {//conectando
            SQLiteCommand command = connection.CreateCommand();//creando comando
            command.CommandText = @"
            UPDATE Tarea 
            SET nombre = @nombre  
            WHERE id = @id;";//EN AMBOS LUGARES USAR EL NOMBRE QUE APARECE EN LA BASE DE DATOS
            connection.Open();//abrir conexion
            command.Parameters.Add(new SQLiteParameter("@nombre", nombre));
            command.Parameters.Add(new SQLiteParameter("@id", id));
            command.ExecuteNonQuery();// no me devuelve nada, solo modifica la bd
            connection.Close();
            }   
        }
        // public List<Usuario> GetAll();
        // public Usuario GetById(int id);
        // public void Remove(int id);
        // public void Update(Usuario usuario, int id);


    }

}