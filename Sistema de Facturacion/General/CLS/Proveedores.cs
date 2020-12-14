using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataManager.CLS;
using System.Windows.Forms;
namespace General.CLS
{
    class Proveedores
    {
        private String _IDProveedor;
        private String _Nombre;
        private String _Correo;
        private String _Telefono;
        private String _Direccion;
        private String _Codigo;
        private String _Giro;
        private String _Telefono2;
        private String _Correo2;
        private String _Nombre2;
        private String _Apellido2;
        //private String _Edad2;
        private String _Cargo2;
        private String _Celular2;

        public String Celular2
        {
            get { return _Celular2; }
            set { _Celular2 = value; }
        }

        public String Cargo2
        {
            get { return _Cargo2; }
            set { _Cargo2 = value; }
        }

        /*public String Edad2
        {
            get { return _Edad2; }
            set { _Edad2 = value; }
        }*/


        public String Apellido2
        {
            get { return _Apellido2; }
            set { _Apellido2 = value; }
        }

        public String Nombre2
        {
            get { return _Nombre2; }
            set { _Nombre2 = value; }
        }

        public String Correo2
        {
            get { return _Correo2; }
            set { _Correo2 = value; }
        }

        public String Telefono2
        {
            get { return _Telefono2; }
            set { _Telefono2 = value; }
        }

        public String Giro
        {
            get { return _Giro; }
            set { _Giro = value; }
        }

        public String Codigo
        {
            get { return _Codigo; }
            set { _Codigo = value; }
        }

        public String Direccion
        {
            get { return _Direccion; }
            set { _Direccion = value; }
        }

        public String Telefono
        {
            get { return _Telefono; }
            set { _Telefono = value; }
        }

        public String Correo
        {
            get { return _Correo; }
            set { _Correo = value; }
        }

        public String Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        public String IDProveedor
        {
            get { return _IDProveedor; }
            set { _IDProveedor = value; }
        }


        public Boolean Guardar()
        {
            Boolean confirmacion = false;
            String cadena = "";
            try
            {
                cadena = "INSERT INTO Proveedores (Nombre, Correo, Telefono, Direccion, Codigo, Giro, Telefono2,"+ 
                @" Correo2, Nombre2, Apellido2, Cargo2, Celular2) VALUES(";

                cadena += @"'" + Nombre + "', ";
                cadena += @"'" + Correo + "', ";
                cadena += @"'" + Telefono + "', ";
                cadena += @"'" + Direccion + "',";
                cadena += @"'" + Codigo + "',";
                cadena += @"'" + Giro + "',";
                cadena += @"'" + Telefono2 + "',";
                cadena += @"'" + Correo2 + "',";
                cadena += @"'" + Nombre2 + "',";
                cadena += @"'" + Apellido2 + "',";
                //cadena += @"" + Edad2 + ",";
                cadena += @"'" + Cargo2 + "',";
                cadena += @"'" + Celular2 + "')";

                DBOperacion operacion = new DBOperacion();
                if (operacion.Insertar(cadena) > 0)
                {
                    confirmacion = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("El proveedor no pudo ser insertado debido a un error interno contacte con el desarrollador: " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return confirmacion;
        }


        public Boolean Actualizar()
        {
            Boolean confirmacion = false;
            String cadena = "";
            try
            {
                cadena = "UPDATE Proveedores SET ";

                cadena += @"Nombre = '" + Nombre + "', ";
                cadena += @"Correo = '" + Correo + "', ";
                cadena += @"Telefono = '" + Telefono + "', ";
                cadena += @"Direccion = '" + Direccion + "',";
                cadena += @"Codigo = '" + Codigo + "',";
                cadena += @"Giro = '" + Giro + "',";
                cadena += @"Telefono2 = '" + Telefono2 + "',";
                cadena += @"Correo2 = '" + Correo2 + "',";
                cadena += @"Nombre2 = '" + Nombre2 + "',";
                cadena += @"Apellido2 = '" + Apellido2 + "',";
                //cadena += @"Edad2 = " + Edad2 + ",";
                cadena += @"Cargo2 = '" + Cargo2 + "',";
                cadena += @"Celular2 = '" + Celular2 + "' WHERE IDProveedor = " + IDProveedor;
                DBOperacion operacion = new DBOperacion();
                if (operacion.Actualizar(cadena) > 0)
                {
                    confirmacion = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("El proveedor no pudo ser Actualizado debido a un error interno contacte con el desarrollador: " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return confirmacion;
        }


        public Boolean Eliminar()
        {
            Boolean confirmacion = false;
            String cadena = "";
            try
            {                
                cadena = "DELETE FROM Proveedores WHERE IDProveedor = " + IDProveedor;
                DBOperacion operacion = new DBOperacion();
                if (operacion.Eliminar(cadena) > 0)
                {
                    confirmacion = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("El proveedor no pudo ser Actualizado debido a un error interno contacte con el desarrollador: " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return confirmacion;
        }
    }
}
