using Taller2_TP10.Models;
using Taller2_TP10.Repositorios;
using System.Linq;
using System.Data.SQLite;

namespace Taller2_TP10.Repositorios
{
    public class TableroRepository : ITableroRepository
    {
        private string cadenaConexion = "Data Source=Data/Kanban.db;Cache=Shared"; // crea la conexion , es el string que me enlaza a la base de datos

//         ● Crear un nuevo tablero (devuelve un objeto Tablero)
        public void CrearTablero(Tablero tablero){
            string queryString = $@"
            INSERT INTO Tablero (id_usuario_propietario, nombre, descripcion) 
            VALUES (@id_usuario_propietario, @nombre, @descripcion)"; // string on la consulta deseada
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))//CREO LA VARIABLE DE CONEXION Y LA ESTABLEZCO
            {
                connection.Open(); //ABRO LA CONEXION
                var command = new SQLiteCommand(queryString, connection);//paso mi consulta y la conexion 
                    command.Parameters.Add(new SQLiteParameter("@id_usuario_propietario", tablero.IdUsuarioPropietario));
                    command.Parameters.Add(new SQLiteParameter("@nombre", tablero.Nombre));
                    command.Parameters.Add(new SQLiteParameter("@descripcion", tablero.Descripcion));
                    command.ExecuteNonQuery();//ejecutar la consulta sin que me devuelva un dato, solo se actualiza
                connection.Close();   
            }
        }
// ● Modificar un tablero existente (recibe un id y un objeto Tablero)
        public void ModificarTablero(int idTablero,Tablero tablero){
            string queryString = $@"
            UPDATE Tablero 
            SET id_usuario_propietario = @id_usuario_propietario, nombre = @nombre, descripcion = @descripcion
            WHERE id_usuario_propietario = {idTablero}"; // string on la consulta deseada
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))//CREO LA VARIABLE DE CONEXION Y LA ESTABLEZCO
            {
                connection.Open(); //ABRO LA CONEXION
                var command = new SQLiteCommand(queryString, connection);//paso mi consulta y la conexion 
                    command.Parameters.Add(new SQLiteParameter("@id_usuario_propietario", tablero.IdUsuarioPropietario));
                    command.Parameters.Add(new SQLiteParameter("@nombre", tablero.Nombre));
                    command.Parameters.Add(new SQLiteParameter("@descripcion", tablero.Descripcion));
                    command.ExecuteNonQuery();//ejecutar la consulta sin que me devuelva un dato, solo se actualiza
                    connection.Close();   
            }
        }
// ● Obtener detalles de un tablero por su ID. (recibe un id y devuelve un Tablero)
        public Tablero BuscarTableroPorId(int idTablero){
            var tablero = new Tablero();
            string queryString = $@"SELECT * FROM Tablero WHERE id_tablero = {idTablero};";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                var command = new SQLiteCommand(queryString, connection);
                connection.Open();
                using (SQLiteDataReader reader = command.ExecuteReader())//Devuelve la consulta, es decir que lee la base de datos y trae lo que se pide
                {
                    while (reader.Read())//revisa si hay tuplas para leer, es decir si esta bien hecha la consulta
                    {
                        tablero.Id = Convert.ToInt32(reader["id_tablero"]);
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                        tablero.Nombre = reader["nombre"].ToString();
                        tablero.Descripcion = reader["descripcion"].ToString();
                    }
                }
                connection.Close();
            }
            return tablero;
        }
// ● Listar todos los tableros existentes (devuelve un list de tableros)
        public List<Tablero> ListarTableros(){
            var tableros = new List<Tablero>();
            string queryString = $@"SELECT * FROM Tablero;";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                var command = new SQLiteCommand(queryString, connection);
                connection.Open();
                using (SQLiteDataReader reader = command.ExecuteReader())//Devuelve la consulta, es decir que lee la base de datos y trae lo que se pide
                {
                    while (reader.Read())//revisa si hay tuplas para leer, es decir si esta bien hecha la consulta
                    {
                        var tablero = new Tablero();
                        tablero.Id = Convert.ToInt32(reader["id_tablero"]);
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                        tablero.Nombre = reader["nombre"].ToString();
                        tablero.Descripcion = reader["descripcion"].ToString(); 
                        tableros.Add(tablero);
                    }
                }
                connection.Close();
            }
            return tableros;
        }    

// ● Listar todos los tableros de un usuario específico. (recibe un IdUsuario, devuelve un
// list de tableros)
        public List<Tablero> ListarTablerosPorUsuario(int IdUsuario){
            var tableros = new List<Tablero>();
            string queryString = $@"SELECT * FROM Tablero WHERE id_usuario_propietario ={IdUsuario};";
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
            {
                var command = new SQLiteCommand(queryString, connection);
                connection.Open();
                using (SQLiteDataReader reader = command.ExecuteReader())//Devuelve la consulta, es decir que lee la base de datos y trae lo que se pide
                {
                    while (reader.Read())//revisa si hay tuplas para leer, es decir si esta bien hecha la consulta
                    {
                        var tablero = new Tablero();
                        tablero.Id = Convert.ToInt32(reader["id_tablero"]);
                        tablero.IdUsuarioPropietario = Convert.ToInt32(reader["id_usuario_propietario"]);
                        tablero.Nombre = reader["nombre"].ToString();
                        tablero.Descripcion = reader["descripcion"].ToString(); 
                        tableros.Add(tablero);
                    }
                }
                connection.Close();
            }
            return tableros;
        }    

// ● Eliminar un tablero por ID

        public void EliminarTablero(int idTablero){
            string queryString = $@"
            DELETE FROM Tablero
            WHERE id_tablero = {idTablero}"; // string on la consulta deseada
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))//CREO LA VARIABLE DE CONEXION Y LA ESTABLEZCO
            {
                connection.Open(); //ABRO LA CONEXION
                var command = new SQLiteCommand(queryString, connection);//paso mi consulta y la conexion 
                command.ExecuteNonQuery();//ejecutar la consulta sin que me devuelva un dato, solo se actualiza
                connection.Close();   
            }            
        }
    }   

}