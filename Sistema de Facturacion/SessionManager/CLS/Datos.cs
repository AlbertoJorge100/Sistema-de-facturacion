using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CacheManager.CLS;
using DataManager.CLS;
//estudiar singleton 
//y Monostate
namespace SessionManager.CLS
{
    public class Datos
    {
        private DataTable _Permisos;
        private String _IDUsuario;
        private String _Usuario;
        private String _IDRol;
        private String _Rol;
        private String _Contrasena;
        private String _Servidor;
        private String _IDEmpleado;
        private String _ProximosAgotar;
        private String _Agotados;
        private String _ProximosVencer;
        private String _Vencidos;
        private String _Empleado;

        public String Empleado
        {
            get { return _Empleado; }
            set { _Empleado = value; }
        }

        public String Vencidos
        {
            get { return _Vencidos; }
            set { _Vencidos = value; }
        }

        public String ProximosVencer
        {
            get { return _ProximosVencer; }
            set { _ProximosVencer = value; }
        }

        public String Agotados
        {
            get { return _Agotados; }
            set { _Agotados = value; }
        }

        public String ProximosAgotar
        {
            get { return _ProximosAgotar; }
            set { _ProximosAgotar = value; }
        }

        public String IDEmpleado
        {
            get { return _IDEmpleado; }
            set { _IDEmpleado = value; }
        }


        public String Servidor
        {
            get
            {
                return this._Servidor;
            }
            set
            {
                this._Servidor = value;
            }
        }
        public void ObtenerPermisos()
        {
            try
            {
                _Permisos = Cache.RolPermisos(_IDRol);
            }
            catch (Exception e)
            {
                Console.WriteLine("Excepcion: " + e.ToString());
            }
        }

        public DataTable Permisos()
        {
            return this._Permisos;
        }

        public Boolean VerificarPermisos(Int32 IDOpcion)
        {
            Boolean autorizado = false;
            try
            {

                foreach (DataRow fila in _Permisos.Rows)
                {
                    if (fila["IDOpcion"].ToString().Equals(IDOpcion.ToString()))
                    {
                        autorizado = true;
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Excepcion: " + e.ToString());
            }
            return autorizado;
        }

        public String IDUsuario
        {
            get
            {
                return this._IDUsuario;
            }
            set
            {
                this._IDUsuario = value;
            }
        }


        public String Usuario
        {
            get
            {
                return this._Usuario;
            }
            set
            {
                this._Usuario = value;
            }
        }


        public String IDRol
        {
            get
            {
                return this._IDRol;
            }
            set
            {
                this._IDRol = value;
                this.ObtenerPermisos();
            }
        }


        public String Rol
        {
            get
            {
                return this._Rol;
            }
            set
            {
                this._Rol = value;
            }
        }


        public String Contrasena
        {
            get
            {
                return this._Contrasena;
            }
            set
            {
                this._Contrasena = value;
            }
        }


        public Datos()
        {
            this._Permisos = new DataTable();
            this.Servidor = "Localhost";
        }
    }
}