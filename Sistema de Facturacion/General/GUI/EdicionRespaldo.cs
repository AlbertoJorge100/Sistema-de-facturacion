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
using System.Globalization;
using SessionManager.CLS;
namespace General.GUI
{
    public partial class EdicionRespaldo : Form
    {
        private FolderBrowserDialog fbdFolder;
        private Boolean Seleccion;
        private String aContrasena;

        public EdicionRespaldo(String contrasena)
        {
            InitializeComponent();
            fbdFolder = new FolderBrowserDialog();
            this.Seleccion = false;
            this.aContrasena = contrasena;
        }

        private void Procesar()
        {
            String opcion = "";
            String ruta = fbdFolder.SelectedPath.ToString() + @"\"; ;
            ruta = ruta.Replace(@"\", @" ");
            Int32 duracion = 0;                        
            //String usuario=txbUsuario.Text;
            //String contrasena=txbContrasena.Text;
            switch (cmbTiempo.SelectedIndex)
            {
                case 0:
                {//SEMANAL
                    duracion = 10;
                    opcion="DIAS";
                    break;
                }
                case 1:
                {//QUINCENAL
                    duracion = 20;
                    opcion = "DIAS";
                    break;
                }
                case 2:
                {//MENSUAL
                    duracion = 30;
                    opcion = "DIAS";
                    break;
                }
                case 3:
                {//TRIMESTRAL
                    duracion = 3;
                    opcion = "MESES";
                    break;
                }
                case 4:
                {//SEMESTRAL
                    duracion = 6;
                    opcion = "MESES";
                    break;
                }
                default:
                {
                    duracion = 10;
                    opcion = "DIAS";
                    break;
                }                    
            }
            String cadena=@"UPDATE RespaldoOpciones SET ";
            cadena+=@"Opcion = '"+opcion+@"', ";
            cadena+=@"FechaInicio = NOW(), ";
            cadena+=@"Duracion = "+duracion+@", ";
            cadena+=@"Ruta = '"+ruta+"'";
            cadena+=@" WHERE IDRespaldoOpcion = 1 ";

            try
            {
                DBOperacion operacion =new DBOperacion();
                if(operacion.Actualizar(cadena) > 0)
                {
                    this.Seleccion = false;
                    fbdFolder = new FolderBrowserDialog();
                    MessageBox.Show("La informacion fue guardada exitosamente"
                    , "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); 
                }
                else
                {
                    MessageBox.Show("Error interno al guardar la informacion,"
                        +" Porfavor contacte con el desarrollador"
                        , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error interno al guardar la informacion,"
                        + " Porfavor contacte con el desarrollador: "+e.ToString()
                        , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EdicionRespaldo_Load(object sender, EventArgs e)
        {
            this.cmbTiempo.Items.Add("Semanual");
            this.cmbTiempo.Items.Add("Quincenal");
            this.cmbTiempo.Items.Add("Mensual");
            this.cmbTiempo.Items.Add("Trimestral");
            this.cmbTiempo.Items.Add("Semestral");
        }

        private void btnCarpeta_Click(object sender, EventArgs e)
        {
            if (fbdFolder.ShowDialog() == DialogResult.OK)
            {
                this.Seleccion = true;
                //lblRuta.Text = fbdFolder.SelectedPath.ToString() + @"\";
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (fbdFolder.ShowDialog() == DialogResult.OK)
            {
                this.Seleccion = true;
                //lblRuta.Text = fbdFolder.SelectedPath.ToString() + @"\";
            }
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (fbdFolder.ShowDialog() == DialogResult.OK)
            {
                this.Seleccion = true;
                lblRuta.Text = fbdFolder.SelectedPath.ToString() + @"\";
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!txbContrasena.Text.Equals(""))
            {
                if (aContrasena.Equals(Hash.generarHash(txbContrasena.Text,Hash.Opcion.SHA_256)))
                {
                    if (this.Seleccion)
                    {
                        if (cmbTiempo.SelectedIndex > 0)
                        {
                            this.Procesar();
                        }
                        else
                        {
                            MessageBox.Show("Debe especificar el tiempo para respaldar la base de datos !"
                                , "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Debe especificar una ruta para guardar el respaldo de la base de datos !"
                            , "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    txbContrasena.Text = "";
                    MessageBox.Show("Contraseña incorrecta !"
                        , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                    
                }                
            }
            else
            {
                MessageBox.Show("Ingrese su contraseña !"
                    , "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        
    }
}
