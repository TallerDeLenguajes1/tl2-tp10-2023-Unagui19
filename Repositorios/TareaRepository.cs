using Taller2_TP10.Repositorios;
using Taller2_TP10.Models;
using System.Linq;
using System.Data.SQLite;

namespace Taller2_TP10.Repositorios
{
    public class TareaRepository: ITareaRepository
    {
        private string cadenaConexion = "Data Source=Data/Kanban.db;Cache=Shared"; // crea la conexion , es el string que me enlaza a la base de datos


        public void CrearTarea(Tarea tarea){
            string queryString = $@"
            INSERT INTO Tarea (id_tablero, nombre, estado, descripcion, color, id_usuario_asignado) 
            VALUES (@idTablero, @nombreNuevo, @estadoNuevo,@descripcionNueva, @colorNuevo, @idUsuarioAsignadoNuevo)"; // string on la consulta deseada
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))//CREO LA VARIABLE DE CONEXION Y LA ESTABLEZCO
            {
                connection.Open(); //ABRO LA CONEXION
                var command = new SQLiteCommand(queryString, connection);//paso mi consulta y la conexion 
                    command.Parameters.Add(new SQLiteParameter("@idTablero", tarea.IdTablero));
                    command.Parameters.Add(new SQLiteParameter("@nombreNuevo", tarea.Nombre));
                    command.Parameters.Add(new SQLiteParameter("@estadoNuevo", tarea.EstadoTarea));
                    command.Parameters.Add(new SQLiteParameter("@descripcionNueva", tarea.Descripcion));
                    command.Parameters.Add(new SQLiteParameter("@colorNuevo", tarea.Color));
                    command.Parameters.Add(new SQLiteParameter("@idUsuarioAsignadoNuevo", tarea.IdUsuarioAsignado));
                    command.ExecuteNonQuery();//ejecutar la consulta sin que me devuelva un dato, solo se actualiza
                connection.Close();   
            }
        }

    // ● Modificar una tarea existente. (recibe un id y un objeto Tarea)
        public void ModificarTarea(int idTarea, Tarea tarea){
            string queryString = $@"
            UPDATE Tarea 
            SET id_tablero = @idTablero, nombre = @nombreNuevo, estado = @estadoNuevo, 
            descripcion = @descripcionNueva, color = @colorNuevo, id_usuario_asignado = @idUsuarioAsignadoNuevo
            WHERE id_tarea = @idTarea"; // string on la consulta deseada
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))//CREO LA VARIABLE DE CONEXION Y LA ESTABLEZCO
            {
                var command = new SQLiteCommand(queryString, connection);//paso mi consulta y la conexion 
                command.Parameters.Add(new SQLiteParameter ("@idTarea", idTarea));
                connection.Open(); //ABRO LA CONEXION
                    command.Parameters.Add(new SQLiteParameter("@idTablero", tarea.IdTablero));
                    command.Parameters.Add(new SQLiteParameter("@nombreNuevo", tarea.Nombre));
                    command.Parameters.Add(new SQLiteParameter("@estadoNuevo", tarea.EstadoTarea));
                    command.Parameters.Add(new SQLiteParameter("@descripcionNueva", tarea.Descripcion));
                    command.Parameters.Add(new SQLiteParameter("@colorNuevo", tarea.Color));
                    command.Parameters.Add(new SQLiteParameter("@idUsuarioAsignadoNuevo", tarea.IdUsuarioAsignado));
                    command.ExecuteNonQuery();//ejecutar la consulta sin que me devuelva un dato, solo se actualiza
                    connection.Close();   
            }
        }

// // ● Modificar nombre de la tarea buscada. 
//         public void ModificarTarea(int idTarea, string nombre){
//             string queryString = $@"
//             UPDATE Tarea 
//             SET  nombre = @nombreNuevo
//             WHERE id_tarea = @idTarea"; // string on la consulta deseada
//             using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))//CREO LA VARIABLE DE CONEXION Y LA ESTABLEZCO
//             {
//                 var command = new SQLiteCommand(queryString, connection);//paso mi consulta y la conexion 
//                 command.Parameters.Add(new SQLiteParameter ("@idTarea", idTarea));
//                 connection.Open(); //ABRO LA CONEXION
//                     command.Parameters.Add(new SQLiteParameter("@nombreNuevo", nombre));
//                     command.ExecuteNonQuery();//ejecutar la consulta sin que me devuelva un dato, solo se actualiza
//                     connection.Close();   
//             }
//         }

// // ● Modificar estado de la tarea buscada. 
//         public void ModificarTarea(int idTarea, Estado estado){
//             string queryString = $@"
//             UPDATE Tarea 
//             SET  estado = @estado
//             WHERE id_tarea = @idTarea"; // string on la consulta deseada
//             using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))//CREO LA VARIABLE DE CONEXION Y LA ESTABLEZCO
//             {
//                 var command = new SQLiteCommand(queryString, connection);//paso mi consulta y la conexion 
//                 command.Parameters.Add(new SQLiteParameter ("@idTarea", idTarea));
//                 connection.Open(); //ABRO LA CONEXION
//                     command.Parameters.Add(new SQLiteParameter("@estado", estado));
//                     command.ExecuteNonQuery();//ejecutar la consulta sin que me devuelva un dato, solo se actualiza
//                     connection.Close();   
//             }
//         }

    // ● Obtener detalles de una tarea por su ID. (devuelve un objeto Tarea)
        public Tarea BuscarTareaPorId(int idTarea)
        {
            var tarea = new Tarea();
            string queryString = $"SELECT * FROM Tarea WHERE id_tarea = @idTarea;";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                var command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter ("@idTarea", idTarea));
                connection.Open();
                using (SQLiteDataReader reader = command.ExecuteReader())//Devuelve la consulta, es decir que lee la base de datos y trae lo que se pide
                {
                    if (reader.Read())//revisa si hay tuplas para leer, es decir si esta bien hecha la consulta
                    {
                        tarea.Id = Convert.ToInt32(reader["id_tarea"]);
                        tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.EstadoTarea = (Estado)Convert.ToInt32(reader["estado"]);
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Color = reader["color"].ToString();
                        if (reader["id_usuario_asignado"]!=DBNull.Value)
                        {tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);}
                        else{tarea.IdUsuarioAsignado = null;}
                    }
                    else
                    {
                        connection.Close();
                        return null;
                    }
                }
                connection.Close();
            }
            return tarea;
        }
    // ● Listar todas las tareas asignadas a un usuario específico.(recibe un idUsuario,
    // devuelve un list de tareas)
        public List<Tarea> ListarTareasPorUsuario(int idUsuario)
        {
            var tareas = new List<Tarea>();
            string queryString = $"SELECT * FROM Tarea WHERE id_usuario_asignado = @idUsuAsig;";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                var command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter ("@idUsuAsig", idUsuario));//para darle el valor que usa el where
                connection.Open();
                using (SQLiteDataReader reader = command.ExecuteReader())//Devuelve la consulta, es decir que lee la base de datos y trae lo que se pide
                {
                    while (reader.Read())//revisa si hay tuplas para leer, es decir si esta bien hecha la consulta
                    {
                        var tarea = new Tarea();
                        tarea.Id = Convert.ToInt32(reader["id_tarea"]);
                        tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.EstadoTarea = (Estado)Convert.ToInt32(reader["estado"]);
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Color = reader["color"].ToString();
                        tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                        tareas.Add(tarea);
                    }
                }
                connection.Close();
            }
            return tareas;
        }
    // ● Listar todas las tareas de un tablero específico. (recibe un idTablero, devuelve un list
    // de tareas)
        public List<Tarea> ListarTareasPorTablero(int idTablero)
        {
            var tareas = new List<Tarea>();
            string queryString = $"SELECT * FROM Tarea WHERE id_tablero = @idTablero;";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                var command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter ("@idTablero", idTablero));//para darle el valor que usa el where
                connection.Open();
                using (SQLiteDataReader reader = command.ExecuteReader())//Devuelve la consulta, es decir que lee la base de datos y trae lo que se pide
                {
                    while (reader.Read())//revisa si hay tuplas para leer, es decir si esta bien hecha la consulta
                    {
                        var tarea = new Tarea();
                        tarea.Id = Convert.ToInt32(reader["id_tarea"]);
                        tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.EstadoTarea = (Estado)Convert.ToInt32(reader["estado"]);
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Color = reader["color"].ToString();
                        tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                        tareas.Add(tarea);
                    }
                }
                connection.Close();
            }
            return tareas;
        }

        //Listar tareas
        public List<Tarea> ListarTareas()
        {
            var tareas = new List<Tarea>();
            string queryString = $"SELECT * FROM Tarea;";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                var command = new SQLiteCommand(queryString, connection);
                //para darle el valor que usa el where
                connection.Open();
                using (SQLiteDataReader reader = command.ExecuteReader())//Devuelve la consulta, es decir que lee la base de datos y trae lo que se pide
                {
                    while (reader.Read())//revisa si hay tuplas para leer, es decir si esta bien hecha la consulta
                    {
                        var tarea = new Tarea();
                        tarea.Id = Convert.ToInt32(reader["id_tarea"]);
                        tarea.IdTablero = Convert.ToInt32(reader["id_tablero"]);
                        tarea.Nombre = reader["nombre"].ToString();
                        tarea.EstadoTarea = (Estado)Convert.ToInt32(reader["estado"]);
                        tarea.Descripcion = reader["descripcion"].ToString();
                        tarea.Color = reader["color"].ToString();
                        if (reader["id_usuario_asignado"]!=DBNull.Value)
                        {
                            tarea.IdUsuarioAsignado = Convert.ToInt32(reader["id_usuario_asignado"]);
                        }else
                        {
                            tarea.IdUsuarioAsignado = null;
                        }
                        tareas.Add(tarea);
                    }
                }
                connection.Close();
            }
            return tareas;
        }
    // ● Eliminar una tarea (recibe un IdTarea)
        public void EliminarTarea(int IdTarea){
            string queryString = $@"
            DELETE FROM Tarea
            WHERE id_tarea = {IdTarea}"; // string on la consulta deseada
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))//CREO LA VARIABLE DE CONEXION Y LA ESTABLEZCO
            {
                connection.Open(); //ABRO LA CONEXION
                var command = new SQLiteCommand(queryString, connection);//paso mi consulta y la conexion 
                    command.ExecuteNonQuery();//ejecutar la consulta sin que me devuelva un dato, solo se actualiza
                    connection.Close();   
            }            
        }
    // ● Asignar Usuario a Tarea (recibe idUsuario y un idTarea)
        public void AsignarTareaAUsuario(int idUsuario, int idTarea)
        {
            string queryString = $@"
            UPDATE Tarea 
            SET id_usuario_asignado = @idUsuarioAsignadoNuevo
            WHERE id_tarea = @idTarea"; // string on la consulta deseada
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))//CREO LA VARIABLE DE CONEXION Y LA ESTABLEZCO
            {
                var command = new SQLiteCommand(queryString, connection);//paso mi consulta y la conexion 
                command.Parameters.Add(new SQLiteParameter ("@idTarea", idTarea));
                connection.Open(); //ABRO LA CONEXION
                    command.Parameters.Add(new SQLiteParameter("@idUsuarioAsignadoNuevo", idUsuario));
                    command.ExecuteNonQuery();//ejecutar la consulta sin que me devuelva un dato, solo se actualiza
                    connection.Close();   
            }
        }

        public int ContarTareasPorEstado(Estado estado)
        {
            int cantidad;
            string queryString = $@"
            SELECT count(estado) 
            FROM Tarea 
            WHERE estado = @estado"; // string on la consulta deseada

            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                var command = new SQLiteCommand(queryString, connection);//paso mi consulta y la conexion 
                command.Parameters.Add(new SQLiteParameter("@estado", estado));
                connection.Open();
                cantidad = Convert.ToInt32(command.ExecuteScalar());//me devuelve un unico valor, que es el valor del count, es para las funciones agregadas de una consulta. No es compatible con ExecuteReader().
                connection.Close();
            }
            return cantidad;
        }
    }
}