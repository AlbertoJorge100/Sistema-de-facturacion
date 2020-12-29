using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Xml;

namespace DataManager.CLS
{
    /// <summary>
    /// Clase para gestionar la conexion al servidor de la base de datos
    /// </summary>
    public class DBConexion
    {
        /// <summary>
        /// Variable donde se almacenaran los datos obtenidos del xml
        /// </summary>
        DataTable _DATOS = new DataTable();
        /// <summary>
        /// Variable de conexion
        /// </summary>
        private MySqlConnection Conexion;


        /*private const String Servidor = "localhost";
        private const String URL = "Server=" + Servidor + @";Port=3306;";
        private const String DB = "Database=sistemaFacturaciondb;";
        private const String USUARIO = "Uid=root;";
        private const String CONTRASENA = "Pwd=jorge_perez100;";
        public static String _Servidor { get { return Servidor; } }
        //private const String _Cadena = "Server=localhost;Port=3306;Database=a_shopdb;Uid=root;Pwd=jorge_perez100;SslMode=None;";
        private const String CADENA = URL + DB + USUARIO + CONTRASENA + "SslMode=None;" + "CheckParameters=False;";
        */


        /// <summary>
        /// Los parametros de conexion a la base de datos se toman 
        /// desde un archivo xml que al iniciarse el sistema por primera vez en una pc se piden al usuario 
        /// y posterior se crea el archivo xml donde las posteriores conexiones al servidor se haran utilizando
        /// el archivo xml
        /// </summary>
        /// <param name="opcion"></param>
        public DBConexion(bool opcion=false)
        {
            this.Conexion = null;
            if (!opcion)
            {
                Configurar();
                try
                {
                    LeerLista();                           
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            } 
        }

        /// <summary>
        /// Metodo de conexion a la base de datos
        /// </summary>
        public void Conectar()
        {
            /*https://www.connectionstrings.com/ esta pagina es para gestionar servidor DB online para pruebas
            * esta la añadimos en el proyecto DataManager y nos vamos a 
            referencias y luego agregar y luego en extensiones y seleccionamos Mysql.data para q nos funcion*/
            
            //Lectura de datos obtenidos en un DataTable
            int i = _DATOS.Rows.Count;
            String usuario = _DATOS.Rows[i-1]["Usuario"].ToString();
            String contraseña = _DATOS.Rows[i-1]["Password"].ToString();
            String servidor = _DATOS.Rows[i-1]["Servidor"].ToString();
            String baseDatos= _DATOS.Rows[i-1]["BaseDatos"].ToString();
            String conexion = "Server=" + servidor + @";Port=3306;Database="+baseDatos+";Uid=" +
                   usuario + ";Pwd=" + contraseña
                   + ";SslMode=None;" + "CheckParameters=False;";
            Conexion = new MySqlConnection(conexion);
            try
            {
                Conexion.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("error en la conexion: " + e.ToString());
            }
        }

        /// <summary>
        /// Obtener la conexion
        /// </summary>
        /// <returns></returns>
        public MySqlConnection getConexion()
        {
            return this.Conexion;
        }

        /// <summary>
        /// Desconectar del servidor
        /// </summary>
        public void Desconectar()
        {
            try
            {
                if (Conexion.State == System.Data.ConnectionState.Open)
                {
                    Conexion.Close();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("error!");
            }
        }
        
        
        /// <summary>
        /// Configuracion de DataTable donde se almacenaran las credenciales obtenidas
        /// del archivo xml
        /// </summary>
        public void Configurar()
        {
            _DATOS.Columns.Add("Usuario");
            _DATOS.Columns.Add("Password");
            _DATOS.Columns.Add("Servidor");
            _DATOS.Columns.Add("BaseDatos");
        }

        /// <summary>
        /// Metodo que se invoca cuando se crea por primera vez el archivo xml
        /// , o se modifica
        /// </summary>
        public void GuardarLista()
        {
            _DATOS.TableName = "Cuenta";
            _DATOS.WriteXml("Cuenta.xml");                 
        }
        
        /// <summary>
        /// Metodo para leer los datos del archivo xml 
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable LeerLista()
        {
            try
            {
                _DATOS.TableName = "Cuenta";
                _DATOS.ReadXml("Cuenta.xml");
            }
            catch (Exception e)
            {
                
                Console.WriteLine(e.ToString());
            }
            return _DATOS;
        }

        /// <summary>
        /// Obtener el DataTable encargado de los datos de conexion al servidor
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable get_DATOS()
        {
            return this._DATOS;
        }
    }
}