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
using DataManager.CLS;
using General.CLS;
namespace General.GUI
{
    public partial class GestionEmpleados : Form
    {
        private BindingSource _DATOS = new BindingSource();
        
        
        private void Cargar(Boolean RFR=false)
        {
            try
            {
                _DATOS.DataSource = Cache.consultaEmpleados(); 
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
                    _DATOS.Filter = "Nombres LIKE '%" + filtro + @"%' OR Apellidos LIKE '%" + filtro + @"%'";
                }else
                {
                    _DATOS.RemoveFilter();
                }
                dtgEmpleados.AutoGenerateColumns = false;
                dtgEmpleados.DataSource = _DATOS;
                lblEstado.Text = dtgEmpleados.Rows.Count.ToString() + " Registros encontrados";
            }
            catch
            {

            }
        }


        private void Agregar()
        {
            EdicionEmpleado f = new EdicionEmpleado(EdicionEmpleado.Opcion.INSERTAR);
            f.ShowDialog();
            if (f._Confirmacion)
            {
                this.Cargar();
            }            
        }

        
        private void Modificar()
        {
            if (MessageBox.Show("¿Desea modificar el empleado?", "Confirmacion", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                String idEmpleado = dtgEmpleados.CurrentRow.Cells["IDEmpleado"].Value.ToString();
                EdicionEmpleado f = new EdicionEmpleado(EdicionEmpleado.Opcion.ACTUALIZAR, idEmpleado);
                f.txbNombres.Text = dtgEmpleados.CurrentRow.Cells["Nombres"].Value.ToString();
                f.txbApellidos.Text = dtgEmpleados.CurrentRow.Cells["Apellidos"].Value.ToString();
                f.txbCorreo.Text = dtgEmpleados.CurrentRow.Cells["Correo"].Value.ToString();
                f.txbEstudios.Text = dtgEmpleados.CurrentRow.Cells["EstudiosAcademicos"].Value.ToString();
                f.txbSalario.Text = dtgEmpleados.CurrentRow.Cells["Salario"].Value.ToString().Replace(",", ".");
                f.fechaIngreso.Text = dtgEmpleados.CurrentRow.Cells["FechaIngreso"].Value.ToString();
                f.txbDireccion.Text = dtgEmpleados.CurrentRow.Cells["Direccion"].Value.ToString();
                f.txbCelular.Text = dtgEmpleados.CurrentRow.Cells["NumeroCelular"].Value.ToString();
                f.txbCargo.Text = dtgEmpleados.CurrentRow.Cells["Cargo"].Value.ToString();
                f.txbDUI.Text = dtgEmpleados.CurrentRow.Cells["DUI"].Value.ToString();
                f.txbNIT.Text = dtgEmpleados.CurrentRow.Cells["NIT"].Value.ToString();
                f.txbNUP.Text = dtgEmpleados.CurrentRow.Cells["NUP"].Value.ToString();
                f.txbTelefono.Text = dtgEmpleados.CurrentRow.Cells["NumeroTelefono"].Value.ToString();
                f.txbEdad.Text = dtgEmpleados.CurrentRow.Cells["Edad"].Value.ToString();
                f.ShowDialog();
                if (f._Confirmacion)
                {
                    this.Cargar(true);
                }
            }           
        }


        private void Eliminar()
        {
            if (MessageBox.Show("¿Desea eliminar el empleado?", "Confirmacion", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                try
                {                
                    Empleados empleado = new Empleados()
                    {
                        IDEmpleado=dtgEmpleados.CurrentRow.Cells["IDEmpleado"].Value.ToString()
                    };

                    if (empleado.Eliminar())
                    {
                        MessageBox.Show("El empleado fue eliminado exitosamente", "Informacion", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        this.Cargar(true);
                    }
                    else
                    {
                        MessageBox.Show("El empleado no pudo ser eliminado debido a un error interno, "+
                            @"porfavor contacte con el desarrollador", "Informacion", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }                
                }
                catch (Exception e3)
                {
                    MessageBox.Show("El empleado no ha podido ser eliminado, debido a un error interno" +
                            ", por favor contacte con el desarrollador: " + e3.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }            
        }


        public GestionEmpleados()
        {
            InitializeComponent(); 
            this.Cargar();
        }

        private void GestionEmpleados_Load(object sender, EventArgs e)
        {

        }


        private void txbFiltro_TextChanged(object sender, EventArgs e)
        {
            Filtro();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            this.Agregar();
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
