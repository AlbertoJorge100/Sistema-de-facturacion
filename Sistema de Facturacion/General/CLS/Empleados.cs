using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataManager.CLS;
using System.Windows.Forms;
namespace General.CLS
{
    public class Empleados
    {
        private String _IDEmpleado;
        private String _Nombres;
        private String _Apellidos;
        private String _Correo;
        private String _EstudiosAcademicos;
        private String _Salario;
        private String _FechaIngreso;
        private String _Direccion;
        private String _NumeroCelular;
        private String _Cargo;
        private String _DUI;
        private String _NIT;
        private String _NUP;
        private String _NumeroTelefono;
        private String _Edad;

        public String Edad
        {
            get { return _Edad; }
            set { _Edad = value; }
        }

        public String NumeroTelefono
        {
            get { return _NumeroTelefono; }
            set { _NumeroTelefono = value; }
        }

        public String NUP
        {
            get { return _NUP; }
            set { _NUP = value; }
        }

        public String NIT
        {
            get { return _NIT; }
            set { _NIT = value; }
        }

        public String DUI
        {
            get { return _DUI; }
            set { _DUI = value; }
        }


        public String Cargo
        {
            get { return _Cargo; }
            set { _Cargo = value; }
        }

        public String NumeroCelular
        {
            get { return _NumeroCelular; }
            set { _NumeroCelular = value; }
        }

        public String Direccion
        {
            get { return _Direccion; }
            set { _Direccion = value; }
        }

        public String FechaIngreso
        {
            get { return _FechaIngreso; }
            set { _FechaIngreso = value; }
        }

        public String Salario
        {
            get { return _Salario; }
            set { _Salario = value; }
        }

        public String EstudiosAcademicos
        {
            get { return _EstudiosAcademicos; }
            set { _EstudiosAcademicos = value; }
        }

        public String Correo
        {
            get { return _Correo; }
            set { _Correo = value; }
        }

        public String Apellidos
        {
            get { return _Apellidos; }
            set { _Apellidos = value; }
        }

        public String Nombres
        {
            get { return _Nombres; }
            set { _Nombres = value; }
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
                cadena = "INSERT INTO Empleados (Nombres, Apellidos, Correo, EstudiosAcademicos, Salario, FechaIngreso, " +
                    @"Direccion,NumeroCelular, Cargo, DUI, NIT, NUP, NumeroTelefono, Edad) VALUES(";
                    cadena += @"'" + Nombres + "', ";
                    cadena += @"'" + Apellidos + "', ";
                    cadena += @"'" + Correo + "', ";
                    cadena += @"'" + EstudiosAcademicos + "', ";
                    cadena += @"'" + Salario + "', ";
                    cadena += @"'" + FechaIngreso + "', ";
                    cadena += @"'" + Direccion + "', ";
                    cadena += @"'" + NumeroCelular + "', ";
                    cadena += @"'" + Cargo + "', ";
                    cadena += @"'" + DUI + "', ";
                    cadena += @"'" + NIT + "', ";
                    cadena += @"'" + NUP + "', ";
                    cadena += @"'" + NumeroTelefono + "', ";
                    cadena += @"'" + Edad + "') ";                                
                DBOperacion operacion = new DBOperacion();
                if (operacion.Insertar(cadena) > 0)
                {                    
                    confirmacion = true;
                }                
            }
            catch (Exception e)
            {
                MessageBox.Show("El empleado no pudo ser insertado debido a un error interno contacte con el desarrollador: "+e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return confirmacion;
        }


        public Boolean Actualizar()
        {
            Boolean confirmacion = false;
            String cadena = "";
            try
            {
                cadena = "UPDATE Empleados SET ";
                cadena += @"Nombres = '" + Nombres + "', ";
                cadena += @"Apellidos = '" + Apellidos + "', ";
                cadena += @"Correo = '" + Correo + "', ";
                cadena += @"EstudiosAcademicos = '" + EstudiosAcademicos + "', ";
                cadena += @"Salario = " + Salario + ", ";
                cadena += @"FechaIngreso = '" + FechaIngreso + "', ";
                cadena += @"Direccion = '" + Direccion + "', ";
                cadena += @"NumeroCelular = '" + NumeroCelular + "', ";
                cadena += @"Cargo = '" + Cargo + "', ";
                cadena += @"DUI = '" + DUI + "', ";
                cadena += @"NIT = '" + NIT + "', ";
                cadena += @"NUP = '" + NUP + "', ";
                cadena += @"NumeroTelefono = '" + NumeroTelefono + "', ";
                cadena += @"Edad = " + Edad + " ";
                cadena += @" WHERE IDEmpleado=" + IDEmpleado;
                DBOperacion operacion = new DBOperacion();
                if (operacion.Actualizar(cadena) > 0)
                {                    
                    confirmacion = true;
                }                
            }
            catch (Exception e)
            {
                MessageBox.Show("El empleado no pudo ser actualizado debido a un error interno contacte con el desarrollador: " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return confirmacion;
        }


        public Boolean Eliminar()
        {
            Boolean confirmacion = false;
            String cadena = "";
            try
            {
                //cadena = @"DELETE FROM Empleados WHERE IDEmpleado=" + IDEmpleado;
                cadena = @"UPDATE Empleados e SET e.Estado = false WHERE IDEmpleado=" + IDEmpleado;
                
                DBOperacion operacion = new DBOperacion();
                if (operacion.Eliminar(cadena) > 0)
                {                    
                    confirmacion = true;
                }                
            }
            catch (Exception e)
            {
                MessageBox.Show("El empleado no pudo ser eliminado debido a un error interno contacte con el desarrollador: " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return confirmacion;
        }
    }    
}

