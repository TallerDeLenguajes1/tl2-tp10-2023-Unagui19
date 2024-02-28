using Taller2_TP10.Repositorios;
using Taller2_TP10.Models;
using System.Linq;
using System.Data.SQLite;

namespace Taller2_TP10.Repositorios
{
    public class UsuarioRepository: IUsuarioRepository
    {
        //Inyeccion de dependencia para la bse de datos
        private readonly string? _connectionString;

        public UsuarioRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

       // ● Listar todos los usuarios registrados. (devuelve un List de Usuarios) 
        public List<Usuario> ListarUsuarios(){
            try{
                string queryString = $"SELECT * FROM Usuario;";
                List<Usuario> usuarios = new List<Usuario>();
                using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
                {
                    var command = new SQLiteCommand(queryString, connection);
                    connection.Open();

                    using (SQLiteDataReader reader = command.ExecuteReader())//Devuelve la consulta, es decir que lee la base de datos y trae lo que se pide
                    {
                        while (reader.Read())//revisa si hay tuplas para leer, es decir si esta bien hecha la consulta
                        {
                            var usuario = new Usuario();
                            usuario.Id = Convert.ToInt32(reader["id_usuario"]);
                            usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                            usuario.Contrasenia = reader["contrasenia"].ToString();
                            usuario.Rol = (Roles)Convert.ToInt32(reader["rol"]);
                            usuarios.Add(usuario);
                        }
                    }
                    connection.Close();
                }
                return usuarios;
            }
            catch (System.Exception ex)
            {
                throw new Exception ("Problema al recuperar los usuarios ",ex);
            }
        }

        //         ● Crear un nuevo usuario. (recibe un objeto Usuario)
        public void CrearUsuario(Usuario usuario){
            try{
                string queryString = $"INSERT INTO Usuario (nombre_de_usuario, rol, contrasenia) VALUES (@nombre_de_usuario, @rol, @contrasenia)"; // string on la consulta deseada
                using (SQLiteConnection connection = new SQLiteConnection(_connectionString))//CREO LA VARIABLE DE CONEXION Y LA ESTABLEZCO
                {
                    connection.Open(); //ABRO LA CONEXION
                    var command = new SQLiteCommand(queryString, connection);//paso mi consulta y la conexion 
                        command.Parameters.Add(new SQLiteParameter("@nombre_de_usuario", usuario.NombreDeUsuario));
                        command.Parameters.Add(new SQLiteParameter("@contrasenia", usuario.Contrasenia));
                        command.Parameters.Add(new SQLiteParameter("@rol", usuario.Rol));
                        command.ExecuteNonQuery();//ejecutar la consulta sin que me devuelva un dato, solo se actualiza
                        connection.Close();   
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception ("Error al actualizar la informacion de el usuario",ex);
            }
        }


// ● Modificar un usuario existente. (recibe un Id y un objeto Usuario)
        public void ModificarUsuario(int idUsu,Usuario usuario){
            try{
                string queryString = $@"
                UPDATE Usuario 
                SET nombre_de_usuario = @nombre_de_usuario, rol = @rol, contrasenia = @contrasenia 
                WHERE id_usuario = {idUsu}"; // string on la consulta deseada
                using (SQLiteConnection connection = new SQLiteConnection(_connectionString))//CREO LA VARIABLE DE CONEXION Y LA ESTABLEZCO
                {
                    connection.Open(); //ABRO LA CONEXION
                    var command = new SQLiteCommand(queryString, connection);//paso mi consulta y la conexion 
                        command.Parameters.Add(new SQLiteParameter("@nombre_de_usuario", usuario.NombreDeUsuario));
                        command.Parameters.Add(new SQLiteParameter("@rol", usuario.Rol));
                        command.Parameters.Add(new SQLiteParameter("@contrasenia", usuario.Contrasenia));
                        command.ExecuteNonQuery();//ejecutar la consulta sin que me devuelva un dato, solo se actualiza
                        connection.Close();   
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception ("Error en la obtencion del usuario",ex);
            }
        }


        // ● Eliminar un usuario por ID
        public void EliminarUsuario(int idUsu){
            try{
                string queryString = $@"
                DELETE FROM Usuario
                WHERE id_usuario = {idUsu}"; // string on la consulta deseada
                using (SQLiteConnection connection = new SQLiteConnection(_connectionString))//CREO LA VARIABLE DE CONEXION Y LA ESTABLEZCO
                {
                    connection.Open(); //ABRO LA CONEXION
                    var command = new SQLiteCommand(queryString, connection);//paso mi consulta y la conexion 
                        command.ExecuteNonQuery();//ejecutar la consulta sin que me devuelva un dato, solo se actualiza
                        connection.Close();   
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception ("Error al eliminar el usuario",ex);
            }            
        }

        //METODOS DE BUSQUEDA

// ● Obtener detalles de un usuario por su ID. (recibe un Id y devuelve un Usuario)
        public Usuario BuscarUsuarioPorId(int idUsu){
            var usuario = new Usuario();
            string queryString = $"SELECT * FROM Usuario WHERE id_usuario = @idUsu;";
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                var command = new SQLiteCommand(queryString, connection);
                command.Parameters.Add(new SQLiteParameter ("@idUsu", idUsu));
                connection.Open();
                using (SQLiteDataReader reader = command.ExecuteReader())//Devuelve la consulta, es decir que lee la base de datos y trae lo que se pide
                {
                    if (reader.Read())//revisa si hay tuplas para leer, es decir si esta bien hecha la consulta
                    {
                        usuario.Id = Convert.ToInt32(reader["id_usuario"]);
                        usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                        usuario.Rol = (Roles)Convert.ToInt32(reader["rol"]);
                    }
                    else
                    {
                        connection.Close();
                        return null;
                    }
                }
                connection.Close();
            }
            if (usuario == null)
            {
                throw new Exception ("Usuario no creado");
            }
            return usuario;
        }

        public int ContarAdmins()
        {
            var queryString = @"SELECT count(id_usuario) FROM Usuario WHERE rol = 1;";
            int count = 0;
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
                {
                    SQLiteCommand command = new SQLiteCommand(queryString, connection);
                    connection.Open();
                    count = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al contar la cantidad de usuarios con rol 'admin' en la base de datos.", ex);
            }

            return count;
        }
    }    
}