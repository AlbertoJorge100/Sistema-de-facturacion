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
    public partial class GestionPermisos : Form
    {
        private BindingSource Opciones;
        public GestionPermisos()
        {
            InitializeComponent();
            this.Opciones = new BindingSource();
            this.Cargar();
            this.CargarAsignaciones();            
        }


        private void Cargar()
        {
            try
            {
                DataTable lista = new DataTable();
                lista = Cache.ConsultaRoles();
                cmbRoles.DataSource = lista;
                cmbRoles.DisplayMember = "Rol";
                cmbRoles.ValueMember = "IDRol";                
            }
            catch (Exception e)
            {
                Console.WriteLine("excepcion en roles: " + e.ToString());
            }
        }


        private void CargarAsignaciones()
        {            
            try
            {
                Opciones.DataSource = Cache.AsignacionPermisos(cmbRoles.SelectedValue.ToString());
                dtgOpciones.AutoGenerateColumns = false;
                dtgOpciones.DataSource = Opciones;
            }
            catch(Exception e)
            {
                MessageBox.Show("Excepcion: " + e.ToString());
            }
            
            
            
        }
        private void EdicionPermisos_Load(object sender, EventArgs e)
        {

        }

        private void cmbRoles_SelectedValueChanged(object sender, EventArgs e)
        {
            this.CargarAsignaciones();
        }

        private void dtgOpciones_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0 && e.RowIndex >= 0)
                {
                    Int32 valor = 0;
                    Permisos permiso = new Permisos();
                    permiso.IDRol = cmbRoles.SelectedValue.ToString();
                    valor = int.Parse(dtgOpciones.CurrentRow.Cells["IDPermiso"].Value.ToString());
                    if (valor > 0)
                    {
                        permiso.IDPermiso = valor.ToString();
                        if (permiso.Revocar())
                        {
                            MessageBox.Show("Revocado");
                        }
                    }
                    else
                    {
                        permiso.IDOpcion = dtgOpciones.CurrentRow.Cells["IDOpcion"].Value.ToString();
                        if (permiso.Conceder())
                        {
                            MessageBox.Show("Concedido");
                        }
                    }
                    this.CargarAsignaciones();
                }
            }
            catch (Exception ed)
            {
                MessageBox.Show("Excepcion: " + ed.ToString());
            }
        }   
    }
}
