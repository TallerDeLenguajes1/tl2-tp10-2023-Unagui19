using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using Entidades.Models;

namespace Entidades.Repositorios
{
    public class TareaRepository:ITareaRepository
    {
           private string cadenaConexion = "Data Source=db/Kanban.db;Cache=Shared"; // crea la conexion 
           public void Create(Tarea Tarea){
            var queryString = @"
            INSERT INTO Tarea (id_tablero, nombre, descripcion, color, estado, id_usuario_asignado ) 
            VALUES (@Id_tablero, @nombre, @descripcion, @color, @estado, @id_usuario_asignado )"; //mi consulta
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))//crea un dato tipo SQLiteConnetion para usarlo e el comando
            {
                connection.Open();//abre la conexion
                var command = new SQLiteCommand(queryString, connection);//paso mi consulta y la conexion para ejectuar el comando
 
                command.Parameters.Add(new SQLiteParameter("@id_tablero", Tarea.IdTablero));
                command.Parameters.Add(new SQLiteParameter("@nombre", Tarea.Nombre));
                command.Parameters.Add(new SQLiteParameter("@descripcion", Tarea.Descripcion));
                command.Parameters.Add(new SQLiteParameter("@color", Tarea.Color));
                command.Parameters.Add(new SQLiteParameter("@estado", Tarea.Estado));
                command.Parameters.Add(new SQLiteParameter("@id_usuario_asignado", Tarea.IdUsuarioAsignado));

                command.ExecuteNonQuery();

                connection.Close();   
            }
        }
        public void Update (Tarea TareaMod, int idTarea)//actualizar la Tarea
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);//conectando
            SQLiteCommand command = connection.CreateCommand();//creando comando
            command.CommandText = @"
            UPDATE Tarea 
            SET id_tablero = @idTablero, nombre = @nombreNuevo, estado = @estadoNuevo, 
            descripcion = @descripcionNueva, color = @colorNuevo, id_usuario_asignado = @idUsuarioAsignadoNuevo
            WHERE id = @idTarea;";
            connection.Open();//abrir conexion
            command.ExecuteNonQuery();// no me devuelve nada, solo modifica la bd
            connection.Close();
        }

        public Tarea GetById(int id)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);//crear variable de conexion
            var TareaRecup = new Tarea();
            SQLiteCommand command = connection.CreateCommand();//comando para usar la base
            command.CommandText = $"SELECT * FROM Tarea WHERE id = '{id}';";
            //command.CommandText = "SELECT * FROM Tareas WHERE id = @idTarea"; otra opcion
            command.Parameters.Add(new SQLiteParameter("@id", id));//comando necesario para buscar los Tareas que cumplan con la condicion
            connection.Open();
            using(SQLiteDataReader reader = command.ExecuteReader())// pa leer los datos de la base
            {
                while (reader.Read())//mientras haya tuplas que leer
                {
                    TareaRecup.Id = Convert.ToInt32(reader["id"]);
                    TareaRecup.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                    TareaRecup.Nombre = reader["nombre"].ToString();
                    TareaRecup.Descripcion = reader["descripcion"].ToString();
                    TareaRecup.Color = reader["color"].ToString();
                    TareaRecup.Estado = (EstadoTarea) Convert.ToInt32(reader["estado"]);
                    TareaRecup.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                }
            }
            connection.Close();
            return (TareaRecup);
        }
        public List<Tarea> GetAll()
        {
            var queryString = @"SELECT * FROM Tarea;";
            List<Tarea> Tareas = new List<Tarea>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection); //creando comadno sqlLiteConnection
                connection.Open();
            
                using(SQLiteDataReader reader = command.ExecuteReader())// Devuelve la consulta, es decir que lee la base de datos y trae lo que se pide
                {
                    while (reader.Read())//revisa si hay tuplas para leer, es decir si esta bien hecha la consulta
                    {
                        var TareaRecup = new Tarea();
                        TareaRecup.Id = Convert.ToInt32(reader["id"]);
                        TareaRecup.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        TareaRecup.Nombre = reader["nombre"].ToString();
                        TareaRecup.Descripcion = reader["descripcion"].ToString();
                        TareaRecup.Color = reader["color"].ToString();
                        TareaRecup.Estado = (EstadoTarea) Convert.ToInt32(reader["estado"]);
                        TareaRecup.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                        Tareas.Add(TareaRecup);//agrego a la lista de Tareas el Tarea con sus datos recuperados de la base de datos
                    }
                }
                connection.Close();// cierro la conexion
            }
            return Tareas;
        }
        public List<Tarea> GetTareasPorTablero(int idTablero)
        {
             var queryString = $"SELECT * FROM Tarea WHERE id = {idTablero};";
            List<Tarea> Tareas = new List<Tarea>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection); //creando comadno sqlLiteConnection
                connection.Open();
            
                using(SQLiteDataReader reader = command.ExecuteReader())// Devuelve la consulta, es decir que lee la base de datos y trae lo que se pide
                {
                    while (reader.Read())//revisa si hay tuplas para leer, es decir si esta bien hecha la consulta
                    {
                        var TareaRecup = new Tarea();
                        TareaRecup.Id = Convert.ToInt32(reader["id"]);
                        TareaRecup.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        TareaRecup.Nombre = reader["nombre"].ToString();
                        TareaRecup.Descripcion = reader["descripcion"].ToString();
                        TareaRecup.Color = reader["color"].ToString();
                        TareaRecup.Estado = (EstadoTarea) Convert.ToInt32(reader["estado"]);
                        TareaRecup.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                        Tareas.Add(TareaRecup);//agrego a la lista de Tareas el Tarea con sus datos recuperados de la base de datos
                    }
                }
                connection.Close();// cierro la conexion
            }
            return Tareas;
        }




        public void Remove(int id)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);// crear variable de conexion
            SQLiteCommand command = connection.CreateCommand();// comando pa moverme
            command.CommandText = $"DELETE FROM Tarea WHERE id = '{id}';";// consulta para eliminar tuplas con su condicion
            connection.Open();
            command.ExecuteNonQuery();//realiza la consulta pero no me devuelve nada
            connection.Close();
        }
        
        public void AsignarUsuarioATarea(int idUsuario, int idTarea)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);//conectando
            SQLiteCommand command = connection.CreateCommand();//creando comando
            command.CommandText = "UPDATE Tarea" + 
            $"SET id_usuario_asignado = {idUsuario} WHERE id = {idTarea};";
            connection.Open();//abrir conexion
            command.ExecuteNonQuery();// no me devuelve nada, solo modifica la bd
            connection.Close();
        }
    }
}