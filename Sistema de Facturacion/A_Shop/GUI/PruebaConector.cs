using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace General.GUI
{
    public partial class PruebaConector : Form
    {
        public PruebaConector()
        {
            InitializeComponent();
        }

        private void txbConsulta_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            //instanciar objeto DBOperacion para poder realizar la consulta
            DataManager.CLS.DBOperacion oOperacion = new DataManager.CLS.DBOperacion();
            //Aqui llenamos el dataTable para mostrar los datos de la consulta
            dtgDatos.DataSource = oOperacion.Consultar(txbConsulta.Text); //retorna un datatable
        }

        private void btnEjecutar_Click(object sender, EventArgs e)
        {
            DataManager.CLS.DBOperacion oOperacion = new DataManager.CLS.DBOperacion();
            Int32 FilasAfectadas = 0;
            FilasAfectadas = oOperacion.Insertar(txbSentencia.Text);
            MessageBox.Show(FilasAfectadas.ToString()+"Registros afectados");
        }

        private void txbSentencia_TextChanged(object sender, EventArgs e)
        {

        }

        private void PruebaConector_Load(object sender, EventArgs e)
        {

        }
    }
}
