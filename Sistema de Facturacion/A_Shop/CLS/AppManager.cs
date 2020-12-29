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

/// <summary>
/// En esta clase se lleva el control total del sistema es a travez de esta clase que permitimos poder acceder a 
/// las ventanas principales del sistema
/// </summary>
namespace General.CLS
{
    class AppManager : ApplicationContext
    {   
        //Variable para validar si existe peticion de modificacion de credenciales de acceso al servidor
        private bool resultadoServidor = true;
        //Valida si se sale por completo del login 
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
                //Establecemos conexion al servidor de la base de datos
                DBConexion db = new DBConexion(true);
                db.Configurar();
                db.LeerLista();
                /*String usuario = db.LeerLista().Rows[0]["Usuario"].ToString();
                String usuario = db.LeerLista().Rows[0]["Password"].ToString();
                String usuario2 = db.LeerLista().Rows[0]["Servidor"].ToString();*/

                //Intento de lectura del archivo xml
                DataTable Datos = db.LeerLista();
                if (Datos.Rows.Count == 0 || !resultadoServidor)
                {
                    //El archivo xml no existe o ya existe pero requiere una modificacion
                    CuentaUsuario cuenta = new CuentaUsuario();
                    //Validacion si existe y requiere cambio de credenciales
                    if (!resultadoServidor)
                    {
                        //Si se desea modificar los datos de acceso al servidor mostrara los ya existentes
                        int i = Datos.Rows.Count;
                        cuenta.txbUsuario.Text = Datos.Rows[i - 1]["Usuario"].ToString();
                        cuenta.txbPassword.Text = Datos.Rows[i - 1]["Password"].ToString();
                        cuenta.txbServidor.Text = Datos.Rows[i - 1]["Servidor"].ToString();
                        cuenta.txbBaseDatos.Text = Datos.Rows[i - 1]["BaseDatos"].ToString();
                    }                    
                    cuenta.ShowDialog();

                    //Se guardaran los cambios en el archivo xml o se creara si aun no existe 
                    if (cuenta.Respuesta)
                    {//Se modificaron los accesos al sistema
                        DataRow fila = db.get_DATOS().NewRow();
                        fila["Usuario"] = cuenta.txbUsuario.Text;
                        fila["Password"] = cuenta.txbPassword.Text;
                        fila["Servidor"] = cuenta.txbServidor.Text;
                        fila["BaseDatos"] = cuenta.txbBaseDatos.Text;
                        db.get_DATOS().Rows.Add(fila);
                        db.GuardarLista();//creando el archivo xml o guardando los cambios
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
        /// <summary>
        /// Metodo para poder validar si en la fecha actual corresponde hacer un respaldo ala base de datos de acuerdo a los
        /// párametros seteados por el administrador del sistema
        /// </summary>
        private void AccesoRespaldo()
        {            
            try
            {
                //Consulta al servidor de la fecha actual si toca respaldo o no existe
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

                        //Insertando la informacion en la base de datos del respaldo recien creado
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

        /// <summary>
        /// Acceso al sistema principal 
        /// </summary>
        private void Proceso()
        {
            //GUI.Principal f = new GUI.Principal();
            //f.ShowDialog();
            this.AccesoSalir = false;
            Boolean opcion = false;
            this.AccesoRespaldo();
            if (Splash())
            {
                /*
                 * Cada vez que se cierre la sesion regresara a la ventana de login 
                 * hasta que el usuario presione el boton de Cancelar 
                 * **/
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
        
        /// <summary>
        /// Splash de inicio del sistema
        /// </summary>
        /// <returns>true</returns>
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

        /// <summary>
        /// Ingreso al sistema a travez del login
        /// </summary>
        /// <returns></returns>
        private Boolean Login()
        {
            GUI.Login f = new GUI.Login();
            f.ShowDialog();
            this.AccesoSalir = f._AccesoSalir;
            return f._Autorizado;
        }
    }
}