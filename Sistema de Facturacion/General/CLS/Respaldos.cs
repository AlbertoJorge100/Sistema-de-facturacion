using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataManager.CLS;
using System.Windows.Forms;
namespace General.CLS
{
    class Respaldos
    {
        private String _IDRespaldo;
        private String _Fecha;
        private String _Estado;
        private String _IDRespaldoOpcion;

        public String IDRespaldoOpcion
        {
            get { return _IDRespaldoOpcion; }
            set { _IDRespaldoOpcion = value; }
        }

        public String Estado
        {
            get { return _Estado; }
            set { _Estado = value; }
        }

        public String Fecha
        {
            get { return _Fecha; }
            set { _Fecha = value; }
        }


        public String IDRespaldo
        {
            get { return _IDRespaldo; }
            set { _IDRespaldo = value; }
        }

        public Boolean Guardar()
        {
            Boolean confirmacion = false;
            String cadena = "";
            try
            {
                cadena = "INSERT INTO Respaldos (Fecha,Estado,IDRespaldoOpcion)" +
                    @" VALUES(";
                cadena += @"'NOW()', ";
                cadena += @"'" + Estado + "', ";
                cadena += @"" + IDRespaldoOpcion + "); ";
                
                DBOperacion operacion = new DBOperacion();
                if (operacion.Insertar(cadena) > 0)
                {
                    confirmacion = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("El respaldo no pudo ser insertado debido a un error interno contacte con el desarrollador: " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return confirmacion;
        }
    }
}
