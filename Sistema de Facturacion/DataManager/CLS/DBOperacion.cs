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
{   //Clase dedicada a gestionar consultas y sentencias sql
    public class DBOperacion : DBConexion
    {
        private bool imagen = false;
        private byte[] imagenByte;
        //descargar conector mysql c.net 6.9.10
        private void ScriptExecute(String pSentencia)
        {

        }


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
                //              MessageBox.Show("");
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
        public Int32 Insertar(String pSentencia, bool estado=false, byte[] imgByte=null)
        {
            this.imagen = estado;
            if (this.imagen)
            {
                imagenByte = imgByte;
            }
            return EjecutarSentencia(pSentencia);
        }
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
