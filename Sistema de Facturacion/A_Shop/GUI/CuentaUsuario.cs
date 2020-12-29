using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using General.CLS;
namespace General.GUI
{
    /// <summary>
    /// Clase para obtener los datos del usuario de conexion al servidor 
    /// </summary>
    public partial class CuentaUsuario : Form
    {
        public Boolean Respuesta = false;
        private Boolean VerPassword = false;
        public CuentaUsuario()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Validacion de campos
            if(!txbUsuario.Text.Equals("") && !txbPassword.Text.Equals("")
                && !txbServidor.Text.Equals("") && !txbBaseDatos.Text.Equals(""))
            {
                if(MessageBox.Show("¿Esta seguro de haber escrito correctamente los datos?, "+
                    "la mala escritura de ellos podria provocar fallas en el sistema","Confirmacion"
                    , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Respuesta = true;
                    this.Close();
                }                
            }
            else
            {
                MessageBox.Show("No deje campos vacios !", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        //Permite visualizar la contraseña escrita en el campo de contraseña
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (!VerPassword)
            {
                this.txbPassword.UseSystemPasswordChar = false;
                VerPassword = true;
            }
            else
            {
                this.txbPassword.UseSystemPasswordChar = true;
                VerPassword = false;
            }
        }
    }
}
