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
    public class DBConexion
    {
        DataTable _DATOS = new DataTable();
        private MySqlConnection Conexion;
        private const String Servidor = "localhost";
        private const String URL = "Server=" + Servidor + @";Port=3306;";
        private const String DB = "Database=sistemaFacturaciondb;";
        private const String USUARIO = "Uid=root;";
        private const String CONTRASENA = "Pwd=jorge_perez100;";
        public static String _Servidor { get { return Servidor; } }
        //private const String _Cadena = "Server=localhost;Port=3306;Database=a_shopdb;Uid=root;Pwd=jorge_perez100;SslMode=None;";
        private const String CADENA = URL + DB + USUARIO + CONTRASENA + "SslMode=None;" + "CheckParameters=False;";
        public DBConexion(bool opcion=false)
        {
            this.Conexion = null;
            if (!opcion)
            {
                Configurar();
                try
                {
                    LeerLista();
                    /*
                    if (_DATOS.Rows.Count == 0)
                    {
                        DataRow fila = _DATOS.NewRow();
                        fila["Usuario"] = "root";
                        fila["Password"] = "jorge_perez100";
                        fila["Servidor"] = "localhost";
                        _DATOS.Rows.Add(fila);
                        GuardarLista();
                    }*/
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            
            /*if (!opcion)
            {
                
            }*/
        }
        public void Conectar()
        {
            /*https://www.connectionstrings.com/ esta pagina es para gestionar servidor DB online para pruebas
            * esta la añadimos en el proyecto DataManager y nos vamos a 
            referencias y luego agregar y luego en extensiones y seleccionamos Mysql.data para q nos funcion*/
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
        public MySqlConnection getConexion()
        {
            return this.Conexion;
        }
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
     
        
        public void Configurar()//añadimos una fila con las siguientes columnas
        {
            _DATOS.Columns.Add("Usuario");
            _DATOS.Columns.Add("Password");
            _DATOS.Columns.Add("Servidor");
            _DATOS.Columns.Add("BaseDatos");
        }
        public void GuardarLista()//Xml para respaldo de datos
        {
            _DATOS.TableName = "Cuenta";
            _DATOS.WriteXml("Cuenta.xml");                 
        }
        public void BorrarLista()
        {
            //_DATOS.XmlDelete("");
            XmlDocument documento = new XmlDocument();
            documento.Load("Cuenta.xml");
            XmlElement root = documento.DocumentElement;
            root.RemoveAttribute("Cuenta.xml");
        }

        public DataTable LeerLista()//Leer los datos del xml
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
        public DataTable get_DATOS()
        {
            return this._DATOS;
        }
    }
}