using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using General.CLS;
using System.Windows.Forms;
using DataManager.CLS;
namespace General.CLS
{
    public class Usuarios
    {
        private String _IDUsuario;        
        private String _IDEmpleado;
        private String _Usuario;
        private String _Contrasena;
        private String _IDRol;

        public String IDUsuario
        {
            get { return _IDUsuario; }
            set { _IDUsuario = value; }
        }


        public String IDRol
        {
            get { return _IDRol; }
            set { _IDRol = value; }
        }

        public String Contrasena
        {
            get { return _Contrasena; }
            set { _Contrasena = value; }
        }

        public String Usuario
        {
            get { return _Usuario; }
            set { _Usuario = value; }
        }

        public String IDEmpleado
        {
            get { return _IDEmpleado; }
            set { _IDEmpleado = value; }
        }

        public Boolean Guardar()
        {
            Boolean confirmacion = false;
            String cadena = "";
            try
            {
                cadena = "INSERT INTO Usuarios (IDEmpleado, Usuario, Contrasena, IDRol) VALUES(";
                    
                cadena += @"" + IDEmpleado + ", ";
                cadena += @"'" + Usuario + "', ";
                cadena += @"'" + Contrasena + "', ";
                cadena += @"" + IDRol + ")";
                
                DBOperacion operacion = new DBOperacion();
                if (operacion.Insertar(cadena) > 0)
                {
                    confirmacion = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("El usuario no pudo ser insertado debido a un error interno contacte con el desarrollador: " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return confirmacion;
        }


        public Boolean Actualizar(String cadena)
        {
            Boolean confirmacion = false;            
            try
            {                
                DBOperacion operacion = new DBOperacion();
                if (operacion.Actualizar(cadena) > 0)
                {
                    confirmacion = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("El usuario no pudo ser actualizado debido a un error interno contacte con el desarrollador: " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return confirmacion;
        }


        public Boolean Eliminar()
        {
            Boolean confirmacion = false;
            String cadena = "";
            try
            {
                cadena = "DELETE FROM Usuarios WHERE IDUsuario = " + IDUsuario;                
                DBOperacion operacion = new DBOperacion();
                if (operacion.Eliminar(cadena) > 0)
                {
                    confirmacion = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("El usuario no pudo ser eliminado debido a un error interno contacte con el desarrollador: " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return confirmacion;
        }

        public Usuarios()
        {

        }
    }
}
