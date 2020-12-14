using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
//using System.Text;
using System.IO;
//using MySql.Data;
//using MySql.Data.MySqlClient;

namespace General.CLS
{
    class BackUp
    {
        public BackUp()
        {

        }
        public Boolean EjecutarComando(String usuario, String contrasena, String ruta, String DB = "", String servidor = "")
        {
            try
            {
                ProcessStartInfo procStartInfo = new ProcessStartInfo();
                procStartInfo.FileName = "CMD";
                DateTime myDateTime = DateTime.Now;
                String fecha = myDateTime.ToString("yyyy-MM-dd hh:mm:ss");
                fecha = fecha.Replace("/", "_");
                fecha = fecha.Replace(" ", "_");
                fecha = fecha.Replace(":", "_");
                fecha = fecha.Replace("-", "_");
                String _DB = "";

                if (DB.Equals(""))
                {
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
                procStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
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
