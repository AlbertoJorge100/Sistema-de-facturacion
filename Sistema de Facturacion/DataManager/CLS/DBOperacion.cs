using System;
//using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;
using System.Web;

namespace DataManager.CLS
{   /// <summary>
    /// Clase utilizada para poder ejecutar sentencias sql 
    /// : Insert, Update, Delete
    /// </summary>
    public class DBOperacion : DBConexion
    {
        /// <summary>
        /// Variable para validar si se tiene una imagen en caso que se desee interactuar con una imagen
        /// </summary>
        private bool imagen = false;

        /// <summary>
        /// Variable para poder almacenar una imagen que viene en un array de bytes
        /// </summary>
        private byte[] imagenByte;
        

        /// <summary>
        /// Metodo que ejecuta una sentencia SQL 
        /// (Insert, Update, Delete)
        /// </summary>
        /// <param name="pSentencia">Query a ejecutar</param>
        /// <returns>Int32</returns>
        private Int32 EjecutarSentencia(String pSentencia)
        {
            Int32 FilasAfectadas = 0;
            MySqlCommand Comando = new MySqlCommand();
            DBConexion auxiliarConexion = new DBConexion();
            try
            {
                auxiliarConexion.Conectar();
                MySqlConnection conexion = auxiliarConexion.getConexion();
                if (conexion != null)
                {
                    //aqui llamamos el metodo protegido de la clase padre
                    Comando.Connection = conexion;
                    Comando.CommandText = pSentencia;
                    //En un caso se desee operar una imagen en array de bytes
                    if (this.imagen)
                    {
                        //Insertamos la imagen convertida a array de bytes
                        Comando.Parameters.AddWithValue("imagen", imagenByte);
                    }
                    FilasAfectadas = Comando.ExecuteNonQuery();//retorna numero de filas afectas                                        
                }
            }
            catch (Exception )
            {
                FilasAfectadas = -1;
            }
            finally
            {
                try
                {
                    auxiliarConexion.Desconectar();
                }
                catch (Exception e)
                {
                    Console.WriteLine("error al cerrar la conexion: " + e);
                }
            }
            return FilasAfectadas;
        }

        /// <summary>
        /// Metodo para consultar una imagen del servidor
        /// , que viene en un array de bytes y posterior convertirla en imagen
        /// </summary>
        /// <param name="query">Query a ejecutar</param>
        /// <returns>MemoryStream</returns>
        public MemoryStream ConsultarImagen(String query)
        {
            PictureBox imagen = new PictureBox();
            MemoryStream ms = null;
            MySqlConnection conexion = null;                                    
            DBConexion auxConexion = new DBConexion();
            try
            {
                auxConexion.Conectar();
                conexion = auxConexion.getConexion();
                MySqlCommand comando = new MySqlCommand(query,conexion);
                MySqlDataReader reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    ms = new MemoryStream((byte[])reader["imagen"]);                    
                }
            }
            catch(Exception e) ////ERRROOOOOOOOOOOOORRRRRRRRRRRRRRRRRR!!!!!!!!!!!
            {
                Console.WriteLine("Excepcion: " + e.ToString());
            }

            finally
            {
                try
                {
                    auxConexion.Desconectar();
                }
                catch (Exception e)
                {
                    Console.WriteLine("la conexion no se pudo cerrar: " + e);
                }
            }
            return ms;
        }
        
        /// <summary>
        /// Metodo generico para consultar al servidor
        /// </summary>
        /// <param name="pConsulta">Query a ejecutar</param>
        /// <param name="pOP"></param>
        /// <param name="arr"></param>
        /// <returns>DataTable</returns>
        public DataTable Consultar(String pConsulta, Boolean pOP=false, String [] arr=null)//String nomParam="",String param="")
        {
            String[] sd = new String[30];

            MySqlConnection conexion = null;
            DataTable Resultado = new DataTable();
            MySqlCommand Comando = new MySqlCommand();
            MySqlDataAdapter Adaptador = new MySqlDataAdapter();
            DBConexion auxConexion = new DBConexion();            
            try
            {
                auxConexion.Conectar();
                conexion = auxConexion.getConexion();
                if (conexion!=null)
                {
                    //aqui llamamos el metodo protegido de la clase padre
                    Comando.Connection = conexion;
                    Comando.CommandText = pConsulta;                    
                    if (pOP)
                    {                        
                        Comando.CommandType = CommandType.StoredProcedure;
                        if (arr!=null)
                        {
                            Comando.Parameters.AddWithValue(arr[0], arr[1]);
                            Comando.Parameters.AddWithValue(arr[2], arr[3]);
                        }                        
                    }
                    Adaptador.SelectCommand = Comando;
                    Adaptador.Fill(Resultado);
                    //FilasAfectadas = Comando.ExecuteNonQuery();//retorna numero de filas afectas
                }
            }
            catch ////ERRROOOOOOOOOOOOORRRRRRRRRRRRRRRRRR!!!!!!!!!!!
            {
                Resultado = new DataTable();
            }
 
            finally
            {
                try
                {
                    auxConexion.Desconectar();
                }
                catch(Exception e)
                {
                    Console.WriteLine("la conexion no se pudo cerrar: " + e);
                }
            }
            return Resultado;
        }

        /// <summary>
        /// Metodo Insertar, previo a ejecutar sentencia
        /// </summary>
        /// <param name="pSentencia">Query a ejecutar</param>
        /// <param name="estado">Si se desea operar una imagen</param>
        /// <param name="imgByte">Imagen convertida en Array de bytes</param>
        /// <returns>Int32</returns>
        public Int32 Insertar(String pSentencia, bool estado=false, byte[] imgByte=null)
        {
            this.imagen = estado;
            if (this.imagen)
            {
                imagenByte = imgByte;
            }
            return EjecutarSentencia(pSentencia);
        }

        /// <summary>
        /// Metodo Actualizar, previo a ejecutar sentencia
        /// </summary>
        /// <param name="pSentencia">Query a ejecutar</param>
        /// <param name="estado">Si se desea operar imagen</param>
        /// <param name="imgByte">Imagen convertida en array de bytes</param>
        /// <returns>Int32</returns>
        public Int32 Actualizar(String pSentencia, bool estado=false, byte[] imgByte=null)
        {
            this.imagen = estado;
            if (this.imagen)
            {
                imagenByte = imgByte;
            }
            return EjecutarSentencia(pSentencia);
        }
        public Int32 Eliminar(String pSentencia)
        {
            return EjecutarSentencia(pSentencia);
        }
    }
}
