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
    public partial class GestionProveedores : Form
    {
        private BindingSource _DATOS = new BindingSource();
        private void Cargar(Boolean RFR=false)
        {
            try
            {
                _DATOS.DataSource = Cache.consultaProveedores();
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
                    _DATOS.Filter = "Nombre LIKE '%" + filtro + @"%' OR Giro LIKE '%" + filtro + @"%'";
                }
                else
                {
                    _DATOS.RemoveFilter();
                }
                dtgProveedores.AutoGenerateColumns = false;
                dtgProveedores.DataSource = _DATOS;
                lblEstado.Text = dtgProveedores.Rows.Count.ToString() + " Registros encontrados";
            }
            catch
            {

            }
        }


        private void Modificar()
        {
            if (MessageBox.Show("¿Desea modificar el proveedor?", "Confirmacion", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                String IDP = dtgProveedores.CurrentRow.Cells["IDProveedor"].Value.ToString();
                EdicionProveedores f = new EdicionProveedores(EdicionProveedores.Opcion.ACTUALIZAR, IDP);
                f.Text = "Actualizar proveedor";
                f.btnAceptar.Text = "Actualizar";
                f.txbNombre.Text = dtgProveedores.CurrentRow.Cells["Nombre"].Value.ToString();
                f.txbCorreo.Text = dtgProveedores.CurrentRow.Cells["Correo"].Value.ToString();
                f.txbTelefono.Text = dtgProveedores.CurrentRow.Cells["Telefono"].Value.ToString();
                f.txbDireccion.Text = dtgProveedores.CurrentRow.Cells["Direccion"].Value.ToString();
                f.txbCodigo.Text = dtgProveedores.CurrentRow.Cells["Codigo"].Value.ToString();
                f.txbGiro.Text = dtgProveedores.CurrentRow.Cells["Giro"].Value.ToString();
                f.txbTelefono2.Text = dtgProveedores.CurrentRow.Cells["Telefono2"].Value.ToString();
                f.txbCorreo2.Text = dtgProveedores.CurrentRow.Cells["Correo2"].Value.ToString();
                f.txbNombre2.Text = dtgProveedores.CurrentRow.Cells["Nombre2"].Value.ToString();
                f.txbApellido2.Text = dtgProveedores.CurrentRow.Cells["Apellido2"].Value.ToString();
                //f.txbEdad2.Text = dtgProveedores.CurrentRow.Cells["Edad2"].Value.ToString();
                f.txbCargo2.Text = dtgProveedores.CurrentRow.Cells["Cargo2"].Value.ToString();
                f.txbCelular.Text = dtgProveedores.CurrentRow.Cells["Celular2"].Value.ToString();
                f.ShowDialog();
                if (f._Confirmacion)
                {
                    this.Cargar(true);
                }
            }
        }



        private void Eliminar()
        {
            if (MessageBox.Show("¿Desea eliminar el proveedor?", "Confirmacion", MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    Proveedores pro = new Proveedores()
                    {
                        IDProveedor = dtgProveedores.CurrentRow.Cells["IDProveedor"].Value.ToString()
                    };

                    if (pro.Eliminar())
                    {
                        MessageBox.Show("El proveedor fue eliminado exitosamente", "Informacion", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        this.Cargar(true);
                    }
                    else
                    {
                        MessageBox.Show("El producto no pudo ser eliminado debido a un error interno, " +
                            @"porfavor contacte con el desarrollador", "Informacion", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                catch (Exception e3)
                {
                    MessageBox.Show("El producto no ha podido ser eliminado, debido a un error interno" +
                            ", por favor contacte con el desarrollador: " + e3.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        
        }


        


        public GestionProveedores()
        {
            InitializeComponent();
            this.Cargar();
        }

        private void GestionProveedores_Load(object sender, EventArgs e)
        {

        }

        private void txbFiltro_TextChanged(object sender, EventArgs e)
        {
            this.Filtro();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            this.Eliminar();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            EdicionProveedores f = new EdicionProveedores(EdicionProveedores.Opcion.INSERTAR);
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

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            this.Cargar(true);
        }
    }
}
