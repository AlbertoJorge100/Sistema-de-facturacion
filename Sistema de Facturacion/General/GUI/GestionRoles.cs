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
    public partial class GestionRoles : Form
    {
        private BindingSource _DATOS = new BindingSource();
        private void Cargar(Boolean RFR = false)
        {
            try
            {
                _DATOS.DataSource = Cache.ConsultaRoles();
                if (RFR)
                {
                    txbFiltro.Text = "";
                }
                Filtro();
            }
            catch
            {

            }

        }


        private void Filtro()
        {
            try
            {
                String filtro = txbFiltro.Text;
                if (filtro.Length > 0)
                {
                    _DATOS.Filter = "Rol LIKE '%" + filtro + @"%' OR Funcion LIKE '%" + filtro + @"%'";
                }
                else
                {
                    _DATOS.RemoveFilter();
                }
                dtgRoles.AutoGenerateColumns = false;
                dtgRoles.DataSource = _DATOS;
                lblEstado.Text = dtgRoles.Rows.Count.ToString() + " Registros encontrados";
            }
            catch
            {

            }
        }


        private void Actualizar()
        {
            if (MessageBox.Show("¿Desea actualizar el rol?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                EdicionRoles f = new EdicionRoles(EdicionRoles.Opcion.ACTUALIZAR, dtgRoles.CurrentRow.Cells["IDRol"].Value.ToString());
                f.txbRol.Text = dtgRoles.CurrentRow.Cells["Rol"].Value.ToString();
                f.txbFuncion.Text = dtgRoles.CurrentRow.Cells["Funcion"].Value.ToString();
                f.ShowDialog();
                if (f._Confirmacion)
                {
                    Cargar(true);
                }
            }
        }


        private void Eliminar()
        {
            if (MessageBox.Show("¿Desea eliminar el rol?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                General.CLS.Roles rol = new General.CLS.Roles()
                {
                    IDRol = dtgRoles.CurrentRow.Cells["IDRol"].Value.ToString()
                };
                if (rol.Eliminar())
                {
                    MessageBox.Show("Rol eliminado correctamente !", "Aviso", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    Cargar(true);
                }
                else
                {
                    MessageBox.Show("El Rol no pudo ser eliminado !", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }


        public GestionRoles()
        {
            InitializeComponent();
            this.Cargar();
        }

        private void GestionRoles_Load(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            EdicionRoles f = new EdicionRoles(EdicionRoles.Opcion.INSERTAR);
            f.ShowDialog();
            if (f._Confirmacion)
            {
                Cargar();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Actualizar();            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Eliminar();
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            Cargar(true);
        }

        private void txbFiltro_TextChanged(object sender, EventArgs e)
        {
            Filtro();
        }
    }
}
