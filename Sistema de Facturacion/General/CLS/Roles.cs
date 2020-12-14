using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataManager.CLS;
using System.Windows.Forms;

namespace General.CLS
{
    class Roles
    {
        private String _IDRol;
        private String _Rol;
        private String _Funcion;

        public String Funcion
        {
            get { return _Funcion; }
            set { _Funcion = value; }
        }

        public String Rol
        {
            get { return _Rol; }
            set { _Rol = value; }
        }

        public String IDRol
        {
            get { return _IDRol; }
            set { _IDRol = value; }
        }

        public Boolean Guardar()
        {
            Boolean confirmacion = false;
            String cadena = "";
            try
            {
                cadena = "INSERT INTO Roles (Rol, Funcion) VALUES(";
                cadena += @"'" + Rol + "', ";
                cadena += @"'" + Funcion + "')";

                DBOperacion operacion = new DBOperacion();
                if (operacion.Insertar(cadena) > 0)
                {
                    confirmacion = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("El rol no pudo ser guardado debido a un error  interno contacte con el desarrollador: " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return confirmacion;
        }


        public Boolean Actualizar()
        {
            Boolean confirmacion = false;
            String cadena = "";
            try
            {
                cadena = "UPDATE Roles SET ";
                cadena += @"Rol = '" + Rol + "', ";
                cadena += @"Funcion = '" + Funcion + "' WHERE IDRol = " + IDRol;
                DBOperacion operacion = new DBOperacion();
                if (operacion.Actualizar(cadena) > 0)
                {
                    confirmacion = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("El rol no pudo ser actualizado debido a un error  interno contacte con el desarrollador: " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return confirmacion;
        }


        public Boolean Eliminar()
        {
            Boolean confirmacion = false;
            String cadena = "";
            try
            {
                cadena = "DELETE FROM Roles WHERE IDRol = " + IDRol;                
                DBOperacion operacion = new DBOperacion();
                if (operacion.Eliminar(cadena) > 0)
                {
                    confirmacion = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("El rol no pudo ser eliminado debido a un error  interno contacte con el desarrollador: " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return confirmacion;
        }
    }
}
