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
using General.CLS;
namespace General.GUI
{
    public partial class GestionCategorias : Form
    {

        private BindingSource _DATOS = new BindingSource();
        private void Cargar(Boolean RFR=false)
        {
            try
            {
                _DATOS.DataSource = Cache.ConsultaCategorias("",2);
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
                    _DATOS.Filter = "NombreCategoria LIKE '%" + filtro + @"%' OR Descripcion LIKE '%" + filtro + @"%'";
                }
                else
                {
                    _DATOS.RemoveFilter();
                }
                dtgCategorias.AutoGenerateColumns = false;
                dtgCategorias.DataSource = _DATOS;
                lblEstado.Text = dtgCategorias.Rows.Count.ToString() + " Registros encontrados";
            }
            catch
            {

            }
        }


        private void Modificar()
        {
            if (MessageBox.Show("¿Desea modificar la categoria?", "Confirmacion", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                String IDC = dtgCategorias.CurrentRow.Cells["IDCategoria"].Value.ToString();
                EdicionCategoria f = new EdicionCategoria(EdicionCategoria.Opcion.ACTUALIZAR,IDC);
                f.Text = "Modificar Categoria";
                f.btnAceptar.Text = "Actualizar";
                f.txbNombre.Text = dtgCategorias.CurrentRow.Cells["NombreCategoria"].Value.ToString();
                f.txbDescripcion.Text = dtgCategorias.CurrentRow.Cells["Descripcion"].Value.ToString();                
                f.ShowDialog();
                if (f._Confirmacion)
                {
                    this.Cargar(true);
                }
            }
        }


        private void Eliminar()
        {
            if (MessageBox.Show("¿Desea eliminar la categoria?", "Confirmacion", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {                
                try
                {
                    Categorias catg = new Categorias()
                    {
                        IDCategoria = dtgCategorias.CurrentRow.Cells["IDCategoria"].Value.ToString()
                    };

                    if (catg.Eliminar())
                    {
                        MessageBox.Show("La categoria fue eliminada exitosamente", "Informacion", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        this.Cargar(true);
                    }
                    else
                    {
                        MessageBox.Show("La categoria no pudo ser eliminad debido a un error interno, " +
                            @"porfavor contacte con el desarrollador", "Informacion", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                catch (Exception e3)
                {
                    MessageBox.Show("La categoria no ha podido ser eliminado, debido a un error interno" +
                            ", por favor contacte con el desarrollador: " + e3.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
        }

        public GestionCategorias()
        {
            InitializeComponent();
            this.Cargar();
        }

        private void GestionCategorias_Load(object sender, EventArgs e)
        {

        }

        private void txbFiltro_TextChanged(object sender, EventArgs e)
        {
            this.Filtro();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            EdicionCategoria f = new EdicionCategoria(EdicionCategoria.Opcion.INSERTAR);
            f.ShowDialog();
            if (f._Confirmacion)
            {
                this.Cargar();
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            this.Modificar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            this.Eliminar();
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            this.Cargar(true);
        }
    }
}
