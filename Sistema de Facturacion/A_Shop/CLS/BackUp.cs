using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
//using System.Text;
using System.IO;


namespace General.CLS
{/// <summary>
/// Clase para creacion de un respaldo de la base de datos 
/// y almacenarlo en una ruta predefinida
/// </summary>
    class BackUp
    {
        public BackUp()
        {

        }
        /// <summary>
        /// Funcion que permite crear el respaldo de la base de datos en la ruta predefinida
        /// </summary>
        /// <param name="usuario"></param>
        /// usuario para acceder al servidor
        /// <param name="contrasena"></param>
        /// contraseña para acceder al servidor 
        /// <param name="ruta"></param>
        /// ruta de almacenamiento del respaldo
        /// <param name="DB"></param>
        /// nombre de la base de datos
        /// <param name="servidor"></param>
        /// direccion del servidor
        /// <returns></returns>
        public Boolean EjecutarComando(String usuario, String contrasena, String ruta, String DB = "", String servidor = "")
        {
            try
            {
                //Se abre una consola del cmd 
                ProcessStartInfo procStartInfo = new ProcessStartInfo();
                procStartInfo.FileName = "CMD";
                //Accediendo ala fecha y hora actual
                DateTime myDateTime = DateTime.Now;
                String fecha = myDateTime.ToString("yyyy-MM-dd hh:mm:ss");
                fecha = fecha.Replace("/", "_");
                fecha = fecha.Replace(" ", "_");
                fecha = fecha.Replace(":", "_");
                fecha = fecha.Replace("-", "_");
                String _DB = "";

                if (DB.Equals(""))
                {//creando la ruta y comandos para crear el respaldo del sistema
                    _DB = servidor + "_" + fecha;
                    ruta += @"\\" + _DB + @".sql";
                    procStartInfo.Arguments = @"/C mysqldump --user=" + usuario + @" --password=" + contrasena + @" -A > " + ruta;
                    
                }
                else
                {
                    _DB = DB + "_" + fecha;
                    ruta += @"" + _DB + @".sql";
                    procStartInfo.Arguments = @"/C mysqldump --user=" + usuario + @" --password=" + contrasena + @" " + DB + @" > " + ruta;
                    
                }
                //Ventana del cmd oculta
                procStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                //Inicio de la creacion del respaldo
                Process.Start(procStartInfo);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
