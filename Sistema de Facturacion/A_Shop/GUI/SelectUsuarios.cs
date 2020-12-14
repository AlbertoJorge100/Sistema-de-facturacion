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
using CacheManager.CLS;
using SessionManager.CLS;
namespace General.GUI
{
    public partial class SelectUsuarios : Form
    {
        DataTable lista;
        Boolean Encontrado;
        private void cargarUsuarios()
        {
            try
            {
                lista = Cache.consultaUsuariosEmpleados();
                /*for(int i=0;i<aux.Rows.Count;i++){
                    aux.Rows[i]["Contrasena"] = "asdfs";//Hash.generarHash(aux.Rows[i]["Contrasena"].ToString(), Hash.Opcion.SHA_256);
                }*/
                dtgUsuarios.DataSource = lista;
                lblCount.Text = lista.Rows.Count.ToString() + " usuarios";                
            }
            catch (Exception e)
            {
                MessageBox.Show("No se pueden mostrar los datos debido a un error interno, por favor contacte con el desarrollador: " + e.ToString()
                    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public SelectUsuarios()
        {
            InitializeComponent();
            //this.WindowState = FormWindowState.Maximized;
            dtgUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.Encontrado = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.cargarUsuarios();            
            DataGridViewColumn column0 = dtgUsuarios.Columns[0];
            //column0.Width = 70;
            column0.Visible = false;
            /*DataGridViewColumn column = dtgUsuarios.Columns[1];                        
            column.Width = 140;
            DataGridViewColumn column2 = dtgUsuarios.Columns[3];
            column2.Width = 140;
            DataGridViewColumn column3 = dtgUsuarios.Columns[4];
            column3.Width = 140;
            DataGridViewColumn column4 = dtgUsuarios.Columns[2];
            column4.Width = 120;*/
            //lblCount.Text = lista.Rows.Count.ToString()+ " usuarios";
            //dtgUsuarios.CurrentCell.Rows["Contrasena"]
                //dtgLista.DataSource = Cache.consultaEmpleados();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void SelectUsuarios_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            
        }
        



        private Int32 buscarUsuario(String clave)
        {
            int i = 0;
            Boolean enc = false;
            String nombres = "";
            while (i < dtgUsuarios.Rows.Count && !enc)
            {
                nombres = lista.Rows[i]["Usuario"].ToString();                
                if (nombres.Equals(clave))
                {
                    enc = true;
                }
                else { i++; }
            }
            if (enc)
            {
                this.Encontrado = true;
                return i;
            }
            else
            {
                return -1;
            }
        }


        private void limpiarPantalla()
        {
            txbBuscar.Text = "";
        }

        private void Buscar()
        {
            String clave = txbBuscar.Text;
            if (this.Encontrado)
            {
                MessageBox.Show("Debe refrescar la lista antes !","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (!clave.Equals(""))
                {
                    Int32 i = buscarUsuario(clave);
                    if (i >= 0)
                    {
                        DataTable aux = new DataTable();
                        aux.Columns.Add("IDUsuario");
                        aux.Columns.Add("Usuario");
                        aux.Columns.Add("Rol");
                        aux.Columns.Add("Nombres");
                        aux.Columns.Add("Apellidos");
                        aux.Columns.Add("Cargo");
                        dtgUsuarios.AutoGenerateColumns = false;
                        DataRow fila = aux.NewRow();
                        fila["IDUsuario"] = lista.Rows[i]["IDUsuario"].ToString();
                        fila["Usuario"] = lista.Rows[i]["Usuario"].ToString();
                        fila["Rol"] = lista.Rows[i]["Rol"].ToString();
                        fila["Nombres"] = lista.Rows[i]["Nombres"].ToString();
                        fila["Apellidos"] = lista.Rows[i]["Apellidos"].ToString();
                        fila["Cargo"] = lista.Rows[i]["Cargo"].ToString();
                        aux.Rows.Add(fila);
                        dtgUsuarios.DataSource = aux;
                    }
                    else
                    {
                        MessageBox.Show("No existe el Usuario !", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);                        
                        this.limpiarPantalla();
                    }
                }
                else
                {
                    MessageBox.Show("No deje campos vacios !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }            
        }

   

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            
        }

        private void btnModificar_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea modificar el usuario?", "Confirmacion",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ModificarUsuario f = new ModificarUsuario();
                f.txbUsuario.Text = dtgUsuarios.CurrentRow.Cells["Usuario"].Value.ToString();
                f.cmbRol.Items.Add("Administrador");
                f.cmbRol.Items.Add("Usuario_cajero");
                f.cmbRol.Items.Add("Usuario_bodeguero");
                f.cmbRol.SelectedItem = dtgUsuarios.CurrentRow.Cells["Rol"].Value.ToString();
                f.ShowDialog();
                if (f._Confirmacion)
                {
                    String usuario = f.txbUsuario.Text;
                    String contrasena = Hash.generarHash(f.txbContrasena.Text, Hash.Opcion.SHA_256);
                    int idUsuario = int.Parse(dtgUsuarios.CurrentRow.Cells["IDUsuario"].Value.ToString());
                    int idRol = 0;
                    switch (f.cmbRol.SelectedIndex)
                    {
                        case 0:
                            {
                                idRol = 1;
                                break;
                            }
                        case 1:
                            {
                                idRol = 2;
                                break;
                            }
                        case 2:
                            {
                                idRol = 3;
                                break;
                            }
                    }
                    String cadena = @"Update Usuarios set Usuario='" + usuario + @"', IDRol=" + idRol +
                        @", Contrasena='" + contrasena + @"' where IDUsuario=" + idUsuario;
                    try
                    {
                        DBOperacion operacion = new DBOperacion();
                        Int32 resultado = operacion.Actualizar(cadena);
                        if (resultado > 0)
                        {
                            MessageBox.Show("El usuario ha sido actualizado con exito ", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.cargarUsuarios();
                            this.Encontrado = false;
                        }
                        else
                        {
                            MessageBox.Show("El usuario no pudo ser actualizado debido a un error interno, por favor contacte con el desarrollador"
                                , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception e3)
                    {
                        MessageBox.Show("El usuario no pudo ser actualizado debido a un error interno, por favor contacte con el desarrollador: "
                        + e3.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            AddUsuario f = new AddUsuario();
            f.ShowDialog();
            this.cargarUsuarios();
            this.Encontrado = false;
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            this.Encontrado = false;
            this.cargarUsuarios();
            this.limpiarPantalla();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void txbBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void Refrescar()
        {
            this.Encontrado = false;
            this.cargarUsuarios();
            this.limpiarPantalla();
        }


        private void txbBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                {
                    this.Buscar();
                    break;
                }
                case Keys.Back:
                {
                    int cnt = txbBuscar.Text.Length;
                    if (cnt == 1)
                    {
                        this.Refrescar();
                    }
                    break;
                }
            }
        }
        
    }
}
