using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CacheManager.CLS;
using System.Data;
using DataManager.CLS;
using General.CLS;
using General.GUI;
using SessionManager.CLS;

namespace General.CLS
{
    class AppManager : ApplicationContext
    {
        private Sesion _Sesion = Sesion.Instancia;
        private bool resultadoServidor = true;
        private Boolean AccesoSalir;
        public AppManager()
        {
            DatosServidor();                        
        }
  
        /// <summary>
        /// Accedemos a la lectura de los datos de acceso al servidor
        /// previamente guardados en un archivo xml
        /// </summary>
        private void DatosServidor()
        {
            try
            {
                DBConexion db = new DBConexion(true);
                db.Configurar();
                db.LeerLista();
                /*String usuario = db.LeerLista().Rows[0]["Usuario"].ToString();
                String usuario = db.LeerLista().Rows[0]["Password"].ToString();
                String usuario2 = db.LeerLista().Rows[0]["Servidor"].ToString();*/
                if (db.LeerLista().Rows.Count == 0 || !resultadoServidor)
                {
                    CuentaUsuario cuenta = new CuentaUsuario();
                    cuenta.ShowDialog();
                    if (cuenta.Respuesta)
                    {
                        DataRow fila = db.get_DATOS().NewRow();
                        fila["Usuario"] = cuenta.txbUsuario.Text;
                        fila["Password"] = cuenta.txbPassword.Text;
                        fila["Servidor"] = cuenta.txbServidor.Text;
                        fila["BaseDatos"] = cuenta.txbBaseDatos.Text;
                        db.get_DATOS().Rows.Add(fila);
                        db.GuardarLista();
                        this.Proceso();
                    }
                }
                else
                {
                    this.Proceso();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void AccesoRespaldo()
        {
            try
            {
                DataTable resultado = Cache.ConsultaRespaldos();
                if (resultado.Rows.Count > 0)
                {
                    DataRow fila = resultado.Rows[0];
                    if (Convert.ToBoolean(fila["Estado"]))//este dia toca hacer un respaldo de la base de datos
                    {//y no existe un respaldo aun
                        String ruta = fila["Ruta"].ToString();
                        String usuario = fila["Usuario"].ToString();
                        String contrasena = fila["Contrasena"].ToString();
                        String baseDatos = fila["BaseDatos"].ToString();
                        BackUp respaldo = new BackUp();
                        /*es un metodo que devuelve un dato Boolean pero no he hecho validacion porque no tengo 
                            * control sobre sus procesos*/
                        respaldo.EjecutarComando(usuario, contrasena, ruta.Replace(@" ", @"\"), baseDatos, "localhost");
                        DBOperacion operacion = new DBOperacion();
                        String cadena = @"INSERT INTO Respaldos (Fecha, Estado, IDRespaldoOpcion) VALUES(NOW(), TRUE, 1)";
                        if (operacion.Insertar(cadena) <= 0)
                        {
                            MessageBox.Show("No se pudo ingresar el respaldo de la base de datos al sistema",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        //ruta  C:\Users\Alberto\Desktop                        
                    }//De lo contrario ya fue hecho el respaldo correspondiente a este dia
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("no existe una fila en la posicion " + e.ToString());
            }
        }

        private void Proceso()
        {
            //GUI.Principal f = new GUI.Principal();
            //f.ShowDialog();
            this.AccesoSalir = false;
            Boolean opcion = false;
            this.AccesoRespaldo();
            if (Splash())
            {//validacion de respaldos                                
                do
                {
                    if (Login())
                    {
                        GUI.Principal f = new GUI.Principal();
                        f.ShowDialog();
                        opcion = f._SesionLogin;
                    }
                } while (opcion && !AccesoSalir);
            }
        }


        private Boolean Splash()
        {
            Boolean Resultado = true;
            GUI.Splash f = new GUI.Splash();
            f.ShowDialog();
            if (f.OpcionDatosServidor)
            {
                this.resultadoServidor = false;
                DatosServidor();
                this.resultadoServidor = true;
            }
            return Resultado;
        }


        private Boolean Login()
        {
            GUI.Login f = new GUI.Login();
            f.ShowDialog();
            this.AccesoSalir = f._AccesoSalir;
            return f._Autorizado;
        }
    }
}