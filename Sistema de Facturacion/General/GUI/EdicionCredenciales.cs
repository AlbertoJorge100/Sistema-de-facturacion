using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CacheManager.CLS;
using SessionManager.CLS;
using General.CLS;
namespace General.GUI
{
    /// <summary>
    /// Clase que se encarga para Modificar un usuario 
    /// </summary>
    public partial class EdicionCredenciales : Form
    {
        private String Usuario;
        private String Contrasena;
        private String IDUsuario;
        private String IDEmpleado;
        private String IDRol;

        /// <summary>
        /// Variable para validar si es modificacion de contraseña
        /// </summary>
        private Boolean Contrasena_M;

        /// <summary>
        /// Variable para validar si es modificacion de usuario 
        /// </summary>
        private Boolean Usuario_M;

        private Boolean Confirmacion;
        private Boolean Revisado;
        private Boolean Revisado_;
        private Usuarios USUARIO;
        private Boolean AuxUsuario;
        public Boolean _Revisado { get { return this.Revisado; } }
        public Boolean _Usuario_M { get { return this.Usuario_M; } }
        public Boolean _Confirmacion { get { return this.Confirmacion; } }
        public Boolean _Contrasena_M { get { return this.Contrasena_M; } }
        public Boolean _AuxUsuario { get { return this.AuxUsuario; } }

        /// <summary>
        /// Metodo para ejecutar una operacion de modificacion de usuario o contraseña
        /// </summary>
        private void Procesar()
        {
            String cadena = "";                           
            if (_Usuario_M)
            {                    
                if (_Contrasena_M)
                {
                    //Cambio de usuario y contraseña                                                
                    String contrasena = Hash.generarHash(txbContrasenaN.Text, Hash.Opcion.SHA_256);
                    this.USUARIO = new Usuarios()
                    {
                        IDUsuario = this.IDUsuario,
                        Usuario = txbUsuario.Text,
                        Contrasena = contrasena
                    };                    
                    cadena = @"UPDATE Usuarios SET ";
                    cadena += "Usuario = '" + USUARIO.Usuario + "', ";
                    cadena += "Contrasena = '" + USUARIO.Contrasena + "' ";
                    cadena += " WHERE IDUsuario = " + USUARIO.IDUsuario;
                    this.Contrasena_M = false;
                }
                else
                {
                    String cntr=txbContrasena.Text;                    
                    if(!cntr.Equals(""))
                    {
                        String contrasenaAux = Hash.generarHash(cntr, Hash.Opcion.SHA_256);
                        if (this.Contrasena.Equals(contrasenaAux))
                        {
                            this.USUARIO = new Usuarios()
                            {
                                IDUsuario = this.IDUsuario,
                                Usuario = txbUsuario.Text
                            };
                            cadena = @"UPDATE Usuarios SET ";
                            cadena += "Usuario = '" + USUARIO.Usuario + "' ";
                            cadena += " WHERE IDUsuario = " + USUARIO.IDUsuario;                            
                        }
                        else
                        {
                            MessageBox.Show("Contraseña incorrecta !",
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe ingresar su contraseña !",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }                                        
                }
                this.Usuario_M = false;
            }
            else
            {
                if (_Contrasena_M)
                {//Solo cambio de contraseña                        
                    String contrasena = Hash.generarHash(txbContrasenaN.Text, Hash.Opcion.SHA_256);
                    this.USUARIO = new Usuarios()
                    {
                        IDUsuario = this.IDUsuario,
                        Contrasena = contrasena
                    };
                    cadena = @"UPDATE Usuarios SET ";
                    cadena += "Contrasena = '" + USUARIO.Contrasena + "' ";
                    cadena += " WHERE IDUsuario = " + USUARIO.IDUsuario;
                    this.Revisado_ = true;//Acceso
                }
            }
            try
            {
                if (this.USUARIO.Actualizar(cadena))
                {
                    MessageBox.Show("Datos actualizados con exito", "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se pudieron actualizar los datos debido a " +
                        "un error interno, por favor contacte al desarrollador",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception e4)
            {
                MessageBox.Show("No se pudieron actualizar los datos debido a " +
                    "un error interno, por favor contacte al desarrollador: " + e4.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }                                                 
        }

        /**
         * Logica de modificacion de usuarios
         * validaciones
         **/
        
        private Boolean CambioContrasena()
        {
            return (!txbContrasenaN.Text.Equals(""));                
        }


        private Boolean ValidacionCampos()
        {
            return (!txbContrasenaN.Text.Equals("") 
                && !txbContrasenaNR.Text.Equals(""));
        }


        private void Conceder(Boolean Contra=false)
        {
            if (txbContrasenaN.Text.Equals(txbContrasenaNR.Text))
            {
                if (Contra)
                {//Ya se valido el acceso en el metodo anterior 
                    if (MessageBox.Show("Esta seguro de modificar los datos?", "Confirmacion",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.Contrasena_M = true;
                        this.Confirmacion = true;
                        this.Procesar();
                    }
                }else
                {//Validar el acceso localmente
                    if (this.Contrasena.Equals(Hash.generarHash(txbContrasena.Text, Hash.Opcion.SHA_256)))
                    {
                        if (MessageBox.Show("Esta seguro de modificar los datos?", "Confirmacion",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            this.Contrasena_M = true;
                            this.Confirmacion = true;
                            this.Procesar();
                        }
                    }
                    else
                    {
                        txbContrasena.Text = "";
                        MessageBox.Show("Contraseña incorrecta !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }               
                }                                                  
            }
            else{MessageBox.Show("Las contraseñas no coinciden!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);}
        }


        private Boolean CambioUsuario()
        {
            return (!this.Usuario.Equals(txbUsuario.Text));
        }


        private void ConcederUsuario(Boolean Opcion = false)
        {
            //Boolean user = false;
            String usuario = this.Usuario;
            String usuarioN = txbUsuario.Text;
            if (!usuario.Equals(usuarioN))
            {//Existe un nuevo usuario 
                String Cadena = @"SELECT u.IDUsuario, u.Usuario FROM Usuarios u WHERE u.Usuario = BINARY '" + usuarioN + @"'";
                try
                {
                    if (Cache.modificacionUsuario(usuarioN).Rows.Count <= 0)
                    {
                        if (Opcion)
                        {//Se accede directamente a procesar la modificacion del usuario                            
                            if (this.Contrasena.Equals(Hash.generarHash(txbContrasena.Text, Hash.Opcion.SHA_256)))
                            {
                                if (MessageBox.Show("Esta seguro de modificar los datos?", "Confirmacion",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    this.Usuario_M = true;
                                    this.Confirmacion = true;
                                    this.Procesar();
                                    this.AuxUsuario = true;
                                }
                            }
                            else
                            {
                                txbContrasena.Text = "";
                                MessageBox.Show("Contraseña incorrecta !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }                                                         
                        }else
                        {//Nada mas se valida pues falta hacer un cambio de contraseña
                            this.Usuario_M = true;
                        }                        
                    }
                    else
                    {
                        MessageBox.Show("El usuario que eligio ya esta en uso",
                        "Excepcion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.txbUsuario.Text = this.Usuario;
                    }
                }
                catch (Exception e2)
                {
                    MessageBox.Show("Excepcion en busqueda de usuario para modificacion: " + e2.ToString(),
                        "Excepcion", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Aceptar()
        {
            if (!txbUsuario.Text.Equals(""))
            {
                if(CambioContrasena())
                {//Cambio de contraseña, Usuario aun no 
                    if(!txbContrasena.Text.Equals("") && !txbContrasenaN.Text.Equals("") && !txbContrasenaNR.Text.Equals(""))
                    {//No dejar campos vacios
                        if (this.CambioUsuario())
                        {//Si hay cambio de usuario
                            this.ConcederUsuario();
                            if (_Usuario_M)
                            {//Si se valido que el usuario a modificar cumple los requisitos
                                if (_AuxUsuario) { this.Conceder(true); this.AuxUsuario = false; }//ya valido la contraseña
                                else { this.Conceder(); }//EL usuario cumplio los requisitos pero no valido la contraseña
                            }
                        }
                        else
                        {//Solo cambio de contraseña
                            this.Conceder();
                        }                                                                         
                    }
                    else
                    {
                        MessageBox.Show("No deje campos vacios !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {//Solo cambio de usuario
                    if (this.CambioUsuario())
                    {
                        this.ConcederUsuario(true);
                    }
                    else
                    {
                        txbContrasena.Text = "";
                        MessageBox.Show("Debe realizar una accion !", "Aviso", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    }
                }          
            }
            else
            {
                MessageBox.Show("Usuario obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public EdicionCredenciales(String Usuario, String Contrasena, String IDUsuario)
        {
            InitializeComponent();
            this.Contrasena_M = false;
            this.Usuario_M = false;
            this.Confirmacion = false;
            this.Revisado = false;
            this.Revisado_ = false;
            this.Usuario = Usuario;
            this.Contrasena = Contrasena;
            this.USUARIO = null;
            this.IDUsuario = IDUsuario;
            this.AuxUsuario = false;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void ModificarUsuario2_Load(object sender, EventArgs e)
        {
            lblNota.Text = "Nota: Si solamente va a cambiar su usuario \n " +
            "puede oviar los demas campos. De la misma \n manera" +
            "puede oviar el campo de el usuario si solamente \n" +
            "va a cambiar la contraseña";
            txbUsuario.Text = this.Usuario;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.Aceptar();                 
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txbContrasenaN_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
