using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataManager.CLS;
using System.Windows.Forms;
namespace General.CLS
{
    public class Permisos
    {
        private String _IDPermiso;
        private String _IDRol;
        private String _IDOpcion;
        public String IDPermiso
        {
            get { return _IDPermiso; }
            set { _IDPermiso = value; }
        }
        

        public String IDOpcion
        {
            get { return _IDOpcion; }
            set { _IDOpcion = value; }
        }
        

        public String IDRol
        {
            get { return _IDRol; }
            set { _IDRol = value; }
        }

        public Boolean Conceder()
        {
            Boolean confirmacion = false;
            String cadena = "";
            try
            {
                cadena = "INSERT INTO Permisos (IDOpcion, IDRol) VALUES(";
                cadena += @"" + IDOpcion + ", ";
                cadena += @"" + IDRol + ")";
                DBOperacion operacion = new DBOperacion();
                if (operacion.Insertar(cadena) > 0)
                {
                    confirmacion = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("El permiso no pudo ser concedido, Porfavor contacte con el desarrollador: " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return confirmacion;
        }


        public Boolean Revocar()
        {
            Boolean confirmacion = false;
            String cadena = "";
            try
            {
                cadena = @"DELETE FROM Permisos WHERE IDPermiso = " + IDPermiso;                
                DBOperacion operacion = new DBOperacion();
                if (operacion.Eliminar(cadena) > 0)
                {
                    confirmacion = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("El permiso no pudo ser revocado, Porfavor contacte con el desarrollador: " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return confirmacion;
        }

    }
}
