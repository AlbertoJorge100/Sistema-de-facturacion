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
using SessionManager.CLS;
using General.CLS;
namespace General.GUI
{
    public partial class GestionUsuarios : Form
    {
        private Sesion _sesion = Sesion.Instancia;
        private BindingSource _DATOS = new BindingSource();
        private void Cargar(Boolean RFR=false)
        {
            try
            {
                _DATOS.DataSource = Cache.consultaUsuariosEmpleados();
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
                    _DATOS.Filter = "Usuario LIKE '%" + filtro + @"%' OR Nombres LIKE '%" + filtro + @"%'";
                }
                else
                {
                    _DATOS.RemoveFilter();
                }
                dtgUsuarios.AutoGenerateColumns = false;
                dtgUsuarios.DataSource = _DATOS;
                lblEstado.Text = dtgUsuarios.Rows.Count.ToString() + " Registros encontrados";
            }
            catch
            {

            }
        }

        public GestionUsuarios()
        {
            InitializeComponent();
            this.Cargar();
        }

        private void GestionUsuarios_Load(object sender, EventArgs e)
        {

        }

        private void  txbFiltro_TextChanged(object sender, EventArgs e)
        {
            this.Filtro();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            EdicionUsuario f = new EdicionUsuario();
            f.ShowDialog();
            if (f._Confirmacion)
            {
                this.Cargar();
            }
        }


        private void Eliminar()
        {
            if (MessageBox.Show("¿Desea eliminar el usuario?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int id = int.Parse(dtgUsuarios.CurrentRow.Cells["IDUsuario"].Value.ToString());                
                try
                {
                    Usuarios usr=new Usuarios()
                    {
                        IDUsuario=dtgUsuarios.CurrentRow.Cells["IDUsuario"].Value.ToString()
                    };

                    if (usr.Eliminar())
                    {
                        MessageBox.Show("El usuario fue eliminado exitosamente", "Informacion", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        this.Cargar(true);
                    }
                    else
                    {
                        MessageBox.Show("El usuario no pudo ser eliminado debido a un error interno, " +
                            @"porfavor contacte con el desarrollador", "Informacion", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
                catch (Exception e2)
                {
                    MessageBox.Show("El usuario no pudo ser eliminado debido a un error interno, por favor contacte con el desarrollador: " + e2.ToString()
                        , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void Modificar()
        {
            String usuario = dtgUsuarios.CurrentRow.Cells["Usuario"].Value.ToString();
            String contrasena = dtgUsuarios.CurrentRow.Cells["Contrasena"].Value.ToString();
            String IDUsuario = dtgUsuarios.CurrentRow.Cells["IDUsuario"].Value.ToString();
            EdicionCredenciales user = new EdicionCredenciales(usuario,contrasena,IDUsuario);
            user.ShowDialog();
            if (user._Confirmacion)
            {
                this.Cargar();
            }
        }
        /*Private void Modificar(){
             * if (MessageBox.Show("¿Desea modificar el usuario?", "Confirmacion",
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
        }*/
        


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            this.Eliminar();
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
