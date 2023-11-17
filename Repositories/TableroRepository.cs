using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using tl2_tp10_2023_Unagui19.Models;

namespace tl2_tp10_2023_Unagui19.Repositorios
{
    public class TableroRepository:ITableroRepository
    {
           private string cadenaConexion = "Data Source=db/Kanban.DB;Cache=Shared"; // crea la conexion 
           public void Create(Tablero Tablero){
            var queryString = @"
            INSERT INTO Tablero (id_usuario_propietario, nombre, descripcion) 
            VALUES (@id_usuario_propietario,@nombre, @descripcion)"; //mi consulta
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))//crea un dato tipo SQLiteConnetion para usarlo e el comando
            {
                connection.Open();//abre la conexion
                var command = new SQLiteCommand(queryString, connection);//paso mi consulta y la conexion para ejectuar el comando
 
                command.Parameters.Add(new SQLiteParameter("@id_usuario_propietario", Tablero.IdUsuarioPropietario));
                command.Parameters.Add(new SQLiteParameter("@nombre", Tablero.Nombre));
                command.Parameters.Add(new SQLiteParameter("@descripcion", Tablero.Descripcion));
                command.ExecuteNonQuery();

                connection.Close();   
            }
        }

    public void Update (Tablero tablero, int id)//actualizar la Tarea
        {
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {//conectando
            SQLiteCommand command = connection.CreateCommand();//creando comando
            command.CommandText = @"
            UPDATE Tablero
            SET id_usuario_propietario=@id_usuario_propietario, nombre = @nombre, descripcion=@descripcion
            WHERE id = @id;";
            connection.Open();//abrir conexion
            command.Parameters.Add(new SQLiteParameter("@id", id));
            command.Parameters.Add(new SQLiteParameter("@id_usuario_propietario", tablero.IdUsuarioPropietario));
            command.Parameters.Add(new SQLiteParameter("@nombre", tablero.Nombre));
            command.Parameters.Add(new SQLiteParameter("@descripcion", tablero.Descripcion));
            command.ExecuteNonQuery();// no me devuelve nada, solo modifica la bd
            connection.Close();
            }   
        }
        public Tablero GetById(int id)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);//crear variable de conexion
            var tablero = new Tablero();
            SQLiteCommand command = connection.CreateCommand();//comando para usar la base
            command.CommandText = $"SELECT * FROM Tablero WHERE id = '{id}';";
            //command.CommandText = "SELECT * FROM Tableros WHERE id = @idTablero"; otra opcion
            command.Parameters.Add(new SQLiteParameter("@id", id));//comando necesario para buscar los Tableros que cumplan con la condicion
            connection.Open();
            using(SQLiteDataReader reader = command.ExecuteReader())// pa leer los datos de la base
            {
                while (reader.Read())//mientras haya tuplas que leer
                {
                    tablero.Id = Convert.ToInt32(reader["id"]);
                    tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                    tablero.Nombre = reader["nombre"].ToString();
                    tablero.Descripcion = reader["descripcion"].ToString();

                }
            }
            connection.Close();
            return (tablero);
        }
        public List<Tablero> GetAll()
        {
            var queryString = @"SELECT * FROM Tablero;";
            List<Tablero> tableros = new List<Tablero>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection); //creando comadno sqlLiteConnection
                connection.Open();
            
                using(SQLiteDataReader reader = command.ExecuteReader())// Devuelve la consulta, es decir que lee la base de datos y trae lo que se pide
                {
                    while (reader.Read())//revisa si hay tuplas para leer, es decir si esta bien hecha la consulta
                    {
                        var tablero = new Tablero();
                        tablero.Id = Convert.ToInt32(reader["id"]);
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                        tablero.Nombre = reader["nombre"].ToString();
                        tablero.Descripcion = reader["descripcion"].ToString();
                        tableros.Add(tablero);//agrego a la lista de Tableros el Tablero con sus datos recuperados de la base de datos
                    }
                }
                connection.Close();// cierro la conexion
            }
            return tableros;
        }

        public void Remove(int id)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);// crear variable de conexion
            SQLiteCommand command = connection.CreateCommand();// comando pa moverme
            command.CommandText = $"DELETE FROM Tablero WHERE id = '{id}';";// consulta para eliminar tuplas con su condicion
            connection.Open();
            command.ExecuteNonQuery();//realiza la consulta pero no me devuelve nada
            connection.Close();
        }

        public List<Tablero> GetTablerosPorUsuario(int idUsuario)
        {
            var queryString = $"SELECT * FROM Tablero WHERE id = {idUsuario};";
            List<Tablero> tableros = new List<Tablero>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection); //creando comadno sqlLiteConnection
                connection.Open();
            
                using(SQLiteDataReader reader = command.ExecuteReader())// Devuelve la consulta, es decir que lee la base de datos y trae lo que se pide
                {
                    while (reader.Read())//revisa si hay tuplas para leer, es decir si esta bien hecha la consulta
                    {
                        var tablero = new Tablero();
                        tablero.Id = Convert.ToInt32(reader["id_Tablero"]);
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                        tablero.Nombre = reader["nombre"].ToString();
                        tablero.Descripcion = reader["descripcion"].ToString();
                        tableros.Add(tablero);//agrego a la lista de Tableros el Tablero con sus datos recuperados de la base de datos
                    }
                }
                connection.Close();// cierro la conexion
            }
            return tableros;
        }
        
    }
}