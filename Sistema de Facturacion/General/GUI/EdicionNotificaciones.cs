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
namespace General.GUI
{
    public partial class EdicionNotificaciones : Form
    {
        
        private void Cargar()
        {
            try
            {                
                DataTable notificaciones=Cache.ConsultaNotificaciones();
                if (notificaciones.Rows.Count==1)
                {
                    DataRow linea = notificaciones.Rows[0];

                    String proximosAgotar = linea[0].ToString();
                    String productosAgotados = linea[1].ToString();
                    String proximosVencer = linea[2].ToString();
                    String productosVencidos = linea[3].ToString();
                    lblProximosAgotar.Text = proximosAgotar;
                    lblProductosAgotados.Text = productosAgotados;
                    lblProximosVencer.Text = proximosVencer;
                    lblProductosVencidos.Text = productosVencidos;                    
                }
                else
                {
                    lblEstado.Text = "Error interno";
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al consultar notificaciones, porfavor contacte con el desarrollador: "+e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public EdicionNotificaciones()
        {
            InitializeComponent();
            this.Cargar();
        }

        
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EdicionNotificaciones_Load(object sender, EventArgs e)
        {

        }
    }
}
