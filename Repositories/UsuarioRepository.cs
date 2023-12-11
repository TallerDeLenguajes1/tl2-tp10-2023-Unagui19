using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;
using tl2_tp10_2023_Unagui19.Models;
using tl2_tp10_2023_Unagui19.ViewModels;

namespace tl2_tp10_2023_Unagui19.Repositorios
{
    public class UsuarioRepositorio: IUsuarioRepository
    {
        private string cadenaConexion = "Data Source=db/Kanban.db;Cache=Shared"; // crea la conexion 
        
        public void Create(Usuario usuario){
            var queryString = $"INSERT INTO usuario (nombre_de_usuario,contrasenia, rol) VALUES (@nombre_de_usuario,@contrasenia, @rol)"; //mi consulta
            using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))//crea un dato tipo SQLiteConnetion para usarlo e el comando
            {
                connection.Open();//abre la conexion
                var command = new SQLiteCommand(queryString, connection);//paso mi consulta y la conexion para ejectuar el comando
 
                //command.Parameters.Add(new SQLiteParameter("@id_usuario", usuario.Id));//agrego el valor del parametro
                command.Parameters.Add(new SQLiteParameter("@nombre_de_usuario", usuario.NombreDeUsuario));
                command.Parameters.Add(new SQLiteParameter("@contrasenia", usuario.Contrasenia));
                command.Parameters.Add(new SQLiteParameter("@rol", usuario.Rol));
                command.ExecuteNonQuery();
                connection.Close();   
            }
        }
        public void Update (Usuario Usuario, int id)//actualizar la tabla
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);//conectando
            SQLiteCommand command = connection.CreateCommand();//creando comando
            command.CommandText = @"
            UPDATE Usuario
            SET nombre_de_usuario = @nombre_de_usuario, rol=@rol
            WHERE id_usuario = @id_usuario;";
            // command.CommandText = $"UPDATE Usuarios SET nombre_de_usuario = '{Usuario.NombreDeUsuario}' WHERE id_usuario = '{Usuario.Id}';";
            connection.Open();//abrir conexion
            command.Parameters.Add(new SQLiteParameter("@nombre_de_usuario", Usuario.NombreDeUsuario));
            command.Parameters.Add(new SQLiteParameter("@id_usuario",id));
            command.Parameters.Add(new SQLiteParameter("@rol",Usuario.Rol));
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
                        Usuario.Contrasenia = reader["contrasenia"].ToString();
                        Usuario.Rol = (NivelDeAcceso)Convert.ToInt32(reader["rol"]);
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
            // command.CommandText = $"SELECT * FROM Usuario WHERE id_usuario = '{idUsu}';";
            command.CommandText = @"SELECT * FROM Usuario WHERE id_usuario = @id_usuario"; //otra opcion
            connection.Open();
            command.Parameters.Add(new SQLiteParameter("@id_usuario", idUsu));//comando necesario para buscar los usuarios que cumplan con la condicion
            using(SQLiteDataReader reader = command.ExecuteReader())// pa leer los datos de la base
            {
                while (reader.Read())//mientras haya tuplas que leer
                {
                    Usuario.Id = Convert.ToInt32(reader["id_usuario"]);
                    Usuario.NombreDeUsuario = reader["nombre_de_usuario"].ToString();
                    Usuario.Rol = (NivelDeAcceso)Convert.ToInt32(reader["rol"]);
                }
            }
            connection.Close();
            return (Usuario);
        }




        public void Remove(int idUsu)
        {
            SQLiteConnection connection = new SQLiteConnection(cadenaConexion);// crear variable de conexion
            SQLiteCommand command = connection.CreateCommand();// comando pa moverme
            command.CommandText = $"DELETE FROM Usuario WHERE id_usuario = '{idUsu}';";// consulta para eliminar tuplas con su condicion
            connection.Open();
            command.ExecuteNonQuery();//realiza la consulta pero no me devuelve nada
            connection.Close();
        }

        // public void UpdateUsuarioPorNombre (int id, string nombre)//actualizar la Tarea
        // {
        //     using (SQLiteConnection connection = new SQLiteConnection(cadenaConexion))
        //     {//conectando
        //     SQLiteCommand command = connection.CreateCommand();//creando comando
        //     command.CommandText = @"
        //     UPDATE Usuario 
        //     SET nombre_de_usuario = @nombre  
        //     WHERE id_usuario = @id_usuario;";//EN AMBOS LUGARES USAR EL NOMBRE QUE APARECE EN LA BASE DE DATOS
        //     connection.Open();//abrir conexion
        //     command.Parameters.Add(new SQLiteParameter("@nombre_de_usuario", nombre));
        //     command.Parameters.Add(new SQLiteParameter("@id_usuario", id));
        //     command.ExecuteNonQuery();// no me devuelve nada, solo modifica la bd
        //     connection.Close();
        //     }   
        // }
        // public List<Usuario> GetAll();
        // public Usuario GetById(int id);
        // public void Remove(int id);
        // public void Update(Usuario usuario, int id);


    }

}