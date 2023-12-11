using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using tl2_tp10_2023_Unagui19.Models;

//Esta es la mejor de todas y la mas completa
namespace tl2_tp10_2023_Unagui19.Repositorios
{
    public class TareaRepository:ITareaRepository
    {
        private string cadenaConexion = "Data Source=db/Kanban.db;Cache=Shared"; // crea la conexion 
        public void Create(Tarea tarea)
        {
            var queryString = @"
            INSERT INTO Tarea (id_tablero, nombre,estado, descripcion, color, id_usuario_asignado ) 
            VALUES (@id_tablero, @nombre, @estado, @descripcion, @color, @id_usuario_asignado )"; //mi consulta
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))//crea un dato tipo SQLiteConnetion para usarlo e el comando
            {
                connection.Open();//abre la conexion
                var command = new SQLiteCommand(queryString, connection);//paso mi consulta y la conexion para ejectuar el comando
 
                command.Parameters.Add(new SQLiteParameter("@id_tablero", tarea.IdTablero));
                command.Parameters.Add(new SQLiteParameter("@nombre", tarea.Nombre));
                command.Parameters.Add(new SQLiteParameter("@estado", tarea.Estado));
                command.Parameters.Add(new SQLiteParameter("@descripcion", tarea.Descripcion));
                command.Parameters.Add(new SQLiteParameter("@color", tarea.Color));
                command.Parameters.Add(new SQLiteParameter("@id_usuario_asignado", tarea.IdUsuarioAsignado));

                command.ExecuteNonQuery();

                connection.Close();   
            }
        }
        public void Update(Tarea TareaMod, int idTarea)//actualizar la Tarea
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);//conectando
            SQLiteCommand command = connection.CreateCommand();//creando comando
            command.CommandText = @"
            UPDATE Tarea 
            SET id_tablero = @id_tablero, nombre = @nombre, estado = @estado, 
            descripcion = @descripcion, color = @color, id_usuario_asignado = @id_usuario_asignado
            WHERE id = @id;";
            connection.Open();//abrir conexion
            command.Parameters.Add(new SQLiteParameter("@id", idTarea));
            command.Parameters.Add(new SQLiteParameter("@id_tablero", TareaMod.IdTablero));
            command.Parameters.Add(new SQLiteParameter("@nombre", TareaMod.Nombre));
            command.Parameters.Add(new SQLiteParameter("@estado", TareaMod.Estado));
            command.Parameters.Add(new SQLiteParameter("@descripcion", TareaMod.Descripcion));
            command.Parameters.Add(new SQLiteParameter("@color", TareaMod.Color));
            command.Parameters.Add(new SQLiteParameter("@id_usuario_asignado", TareaMod.IdUsuarioAsignado)); 
            command.ExecuteNonQuery();// no me devuelve nada, solo modifica la bd
            connection.Close();
        }
        public void UpdatePorNombre (int id, string nombre)//actualizar la Tarea
        {
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {//conectando
            SQLiteCommand command = connection.CreateCommand();//creando comando
            command.CommandText = @"
            UPDATE Tarea 
            SET nombre = @nombre
            WHERE id = @id;";
            connection.Open();//abrir conexion
            command.Parameters.Add(new SQLiteParameter("@nombre", nombre));
            command.Parameters.Add(new SQLiteParameter("@id", id));
            command.ExecuteNonQuery();// no me devuelve nada, solo modifica la bd
            connection.Close();
            }   
        }

        public void UpdatePorEstado (int id, EstadoTarea estado)//actualizar la Tarea
        {
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {//conectando
            SQLiteCommand command = connection.CreateCommand();//creando comando
            command.CommandText = @"
            UPDATE Tarea 
            SET estado = @estado
            WHERE id = @id;";
            connection.Open();//abrir conexion
            command.Parameters.Add(new SQLiteParameter("@estado", estado));
            command.Parameters.Add(new SQLiteParameter("@id", id));
            command.ExecuteNonQuery();// no me devuelve nada, solo modifica la bd
            connection.Close();
            }   
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
                        var tarea = new Tarea();
                        tarea.Id = Convert.ToInt32(reader["id"]);
                        tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Color = reader["color"].ToString();
                        tarea.IdUsuarioAsignado = reader["id_usuario_asignado"] != DBNull.Value? (int?)Convert.ToInt32(reader["id_usuario_asignado"]): null;//esto me indica que si el valor es null ponga null en el campo
                        //tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                        Tareas.Add(tarea);//agrego a la lista de Tareas el Tarea con sus datos recuperados de la base de datos
                    }
                }
                connection.Close();// cierro la conexion
            }
            return Tareas;
        }
        public List<Tarea> GetTareasPorEstado(EstadoTarea estado)
        {
             var queryString = $"SELECT * FROM Tarea WHERE estado = {(int)estado};";
            List<Tarea> Tareas = new List<Tarea>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection); //creando comadno sqlLiteConnection
                connection.Open();
                command.Parameters.Add(new SQLiteParameter("@estado",estado));
            
                using(SQLiteDataReader reader = command.ExecuteReader())// Devuelve la consulta, es decir que lee la base de datos y trae lo que se pide 
                {
                    while (reader.Read())//revisa si hay tuplas para leer, es decir si esta bien hecha la consulta
                    {
                        var tarea = new Tarea();
                        tarea.Id = Convert.ToInt32(reader["id"]);
                        tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Color = reader["color"].ToString();
                        tarea.IdUsuarioAsignado = reader["id_usuario_asignado"] != DBNull.Value? (int?)Convert.ToInt32(reader["id_usuario_asignado"]): null;
                        Tareas.Add(tarea);//agrego a la lista de Tareas el Tarea con sus datos recuperados de la base de datos
                    }
                }
                connection.Close();// cierro la conexion
            }
            return Tareas;
        }
        public List<Tarea> GetTareasPorUsuarioAsignado(int idUsu)
        {
             var queryString = $"SELECT * FROM Tarea WHERE  id_usuario_asignado= {idUsu};";
            List<Tarea> Tareas = new List<Tarea>();
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                SQLiteCommand command = new SQLiteCommand(queryString, connection); //creando comadno sqlLiteConnection
                connection.Open();
                command.Parameters.Add(new SQLiteParameter("@id_usuario_asignado",idUsu));
            
                using(SQLiteDataReader reader = command.ExecuteReader())// Devuelve la consulta, es decir que lee la base de datos y trae lo que se pide 
                {
                    while (reader.Read())//revisa si hay tuplas para leer, es decir si esta bien hecha la consulta
                    {
                        var tarea = new Tarea();
                        tarea.Id = Convert.ToInt32(reader["id"]);
                        tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.Estado = (EstadoTarea)Convert.ToInt32(reader["estado"]);
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Color = reader["color"].ToString();
                        tarea.IdUsuarioAsignado = reader["id_usuario_asignado"] != DBNull.Value? (int?)Convert.ToInt32(reader["id_usuario_asignado"]): null;
                        Tareas.Add(tarea);//agrego a la lista de Tareas el Tarea con sus datos recuperados de la base de datos
                    }
                }
                connection.Close();// cierro la conexion
            }
            return Tareas;
        }
        public List<Tarea> GetTareasPorTablero(int idTablero)
        {
             var queryString = $"SELECT * FROM Tarea WHERE id_tablero = {idTablero};";
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
                        TareaRecup.IdUsuarioAsignado = reader["id_usuario_asignado"] != DBNull.Value? (int?)Convert.ToInt32(reader["id_usuario_asignado"]): null;
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