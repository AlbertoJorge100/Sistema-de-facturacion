using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataManager.CLS;
using General.CLS;
using SessionManager.CLS;
using CacheManager.CLS;//Accediendo ala clase Cache para el manejador de consultas

namespace General.GUI
{
    public partial class Login : Form
    {   //Variables de sesion        
        private Sesion _Sesion = null;//Este atributo se encarga de las variables de sesion del usuario
        private Boolean AccesoSalir;
        private String IDUsuario;
        private String Contrasena;
        private String IdRol;
        private String Rol;
        private String Usuario;
        private String IDRol;
        private String IDEmpleado;
        private String Empleado;
        //Variables temporales de login
        private static int Valor;        
        Boolean Autorizado = false;
        public Boolean _AccesoSalir { get { return this.AccesoSalir; } }
        public Boolean _Autorizado { get { return this.Autorizado; } }


        private void variablesSesion(){//metodo para aplicar variables de sesion
            if(_Sesion==null){
                _Sesion = Sesion.Instancia;
            }
            _Sesion.Informacion.IDUsuario = IDUsuario;
            _Sesion.Informacion.Usuario = Usuario.ToString();
            _Sesion.Informacion.IDRol = IdRol;
            _Sesion.Informacion.Rol=this.Rol;
            _Sesion.Informacion.Contrasena = Contrasena;
            _Sesion.Informacion.IDEmpleado = this.IDEmpleado;
            _Sesion.Informacion.Empleado = this.Empleado;
        }


        private void Validar()
        {/*
          * Trabajamos de esta manera el metodo validar para poder capturar los datos de el usuario al momento de
          * validar que su usuario es el correcto, entonces la validadcion de la contraseña la hacemos de manera local
          * si es correcta la contraseña; damos el acceso al sistema, de lo contrario si es incorrecta, los siguientes intentos 
          * con el mismo usuario no seran consultados ala base de datos seran manejados de manera local para no saturar el servidor **/            
            if (_Sesion==null)// si noexisten variables de sesion
            {
                conexionUsuario();//accedemos al servidor a travez de este metodo                
            }
            else
            {//el usuario ingreso erroneamente su contraseña ya existen variables de sesion
                Console.WriteLine("no es nulo ya tiene variables de sesion");
                if(txbUsuario.Text.Equals(Usuario))//si es el mismo usuario que ingreso anteriormente
                {//tomamos a bien crear una clase con un metodo estatico con el cual indicaremos el tipo de encriptacion a generar
                    String contrasena = Hash.generarHash(txbPassword.Text, Hash.Opcion.SHA_256);
                    if (contrasena.Equals(Contrasena))
                    {//paso todos los filtros y puede acceder al sistema                        
                        Autorizado = true;
                        Close();                                        
                    }
                    else
                    {
                        //label5.Text = "contraseña incorrecta" + (++Valor);
                        lblResultado.ForeColor = Color.DarkRed;
                        lblResultado.Text = Usuario + "\ntu contraseña es incorrecta !";                       
                    }
                }
                else
                {//entonces ay que buscar nuevamente en la base de datos
                    conexionUsuario();                    
                }                
            }                               
        }

        /*
         * Uso del manejador de Consultas 
         * Cache.ConsultaLogin();           
         * **/
        private void conexionUsuario()
        {            
            /*En esta consulta se invoca el idUsuario, contrasena, idrol atravez de el Usuario */            
            DBOperacion operacion = new DBOperacion();
            String usuario = txbUsuario.Text;
            try
            {//hemos creado un metodo de consulta personalizada directamente para el login retorna un arreglo object
                //Object[] lista = Cache.consultaLogin(consulta);///Uso de la clase Cache para manejar consultas
                DataTable lista = Cache.ConsultaLogin(usuario);
                if (lista.Rows.Count > 0)
                {//El usuario existe 
                    DataRow fila = lista.Rows[0];                    
                    this.IDUsuario =fila["IDUsuario"].ToString();
                    //si existe el idusuario. No era necesario llamarlo a travez de la consulta 
                    this.Usuario = txbUsuario.Text;//lo tomamos del textbox
                    this.Contrasena = fila["Contrasena"].ToString();
                    this.IdRol=fila["IDRol"].ToString();
                    this.Rol = fila["Rol"].ToString();
                    this.IDEmpleado = fila["IDEmpleado"].ToString();
                    this.Empleado = fila["Empleado"].ToString();
                    variablesSesion();//llenamos el atributo _sesion con los datos a utilizar en la sesion
                    //pasamos la contraseña a hash= sha256 para validar si es la correcta
                    String contrasena = Hash.generarHash(txbPassword.Text, Hash.Opcion.SHA_256);
                    if (contrasena.Equals(Contrasena))
                    {//si la contraseña que ingreso coincide ala DB es correct                      
                        Autorizado = true;//Accedemos al sistema a travez de esta variable                        
                        Close();                        
                    }
                    else
                    {//El usuario existe pero ha ingresado una contraseña incorrecta los siguientes intentos no seran consultados al DB                      
                        lblResultado.ForeColor = Color.DarkRed;
                        lblResultado.Text = _Sesion.Informacion.Usuario + "\ntu contraseña es incorrecta";//--------------->>
                    }
                }
                else
                {   //No existe el usuario en la base de datos    
                    lblResultado.ForeColor = Color.Red;
                    lblResultado.Text = "El usuario no existe !";                    
                }
            }
            catch (Exception e)
            {//Error interno ocurrido en la consulta
                Console.WriteLine("excepcion: " + e);
            }            
        }   
        
     
        public Login()
        {
            InitializeComponent();
            Valor = 0;            
            IDUsuario = "";
            Usuario = "";
            Contrasena = "";
            IdRol = "";
            this.AccesoSalir = false;
        }


        private void Acceder()
        {
            if (!this.txbUsuario.Text.Equals("") && !this.txbPassword.Text.Equals(""))
            {
                this.Validar();
            }
            else
            {
                MessageBox.Show("No deje campos vacios !","Aviso",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }


        private void btnEntrar_Click(object sender, EventArgs e)
        {
            this.Acceder();
        }


        private void btnSalir_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Saliendo !");
            this._Sesion = null;
            this.AccesoSalir = true;            
            this.Close();//_Autorizado sigue false entonces se saldra sin acceder al sistema
        }

        private void txbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.Acceder();
            }
        }
    }
}
