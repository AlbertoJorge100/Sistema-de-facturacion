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
    public partial class EdicionUsuario : Form
    {
        private DataTable lista;
        Boolean Encontrado;
        private Boolean Confirmacion;
        public Boolean _Confirmacion { get { return this.Confirmacion; } }
        private BindingSource _DATOS = new BindingSource();
        private DataTable _Roles;

        private void Roles()
        {         
            try
            {
                _Roles = Cache.ConsultaRoles(); //retorna un datatable            
                foreach (DataRow fila in _Roles.Rows)
                {
                    this.cmbRol.Items.Add(fila["Rol"].ToString());
                }                
            }
            catch (Exception e2)
            {
                MessageBox.Show("No se pueden cargar las categorias o proveedores debido a un error interno, "
                    + "por favor contacte con el desarrollador: " + e2.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }        
        }


        private Int32 BuscarRol()
        {
            Int32 i = -1;
            int filas = _Roles.Rows.Count;
            try
            {
                foreach (DataRow fila in _Roles.Rows)
                {
                    if (cmbRol.SelectedItem.ToString().Equals(fila["Rol"].ToString()))
                    {
                        i = int.Parse(fila["IDRol"].ToString());
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            
            return i;
        }


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
                }
                else
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


        private void GenerarCredenciales()
        {
            Int32 rol = BuscarRol(); 
            if (rol >= 0)
            {
                if (MessageBox.Show("¿Desea agregar un usuario con el empleado seleccionado?",
                "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    String nombres = dtgEmpleados.CurrentRow.Cells["Nombres"].Value.ToString();
                    String apellidos = dtgEmpleados.CurrentRow.Cells["Apellidos"].Value.ToString();
                    String idEmpleado = dtgEmpleados.CurrentRow.Cells["IDEmpleado"].Value.ToString();
                    String usuario = generarUsuario(nombres, apellidos) + idEmpleado;
                    String contrasena = generarContrasena();
                    String contrasenaDB = Hash.generarHash(contrasena, Hash.Opcion.SHA_256);
                    Usuarios usr = new Usuarios()
                    {
                        IDEmpleado = idEmpleado,
                        Usuario = usuario,
                        Contrasena = contrasenaDB,
                        IDRol = rol.ToString()
                    };  
                    
                    try
                    {                                                
                        if (usr.Guardar())
                        {
                            lblUsuario.Text = " USUARIO: " + usuario;
                            lblContrasena.Text = " CONTRASEÑA: " + contrasena;
                            MessageBox.Show("Usuario generado exitosamente !", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Confirmacion = true;
                        }
                        else
                        {
                            MessageBox.Show("No se puede generar el usuario" +
                                " debido a un error interno, por favor contacte al desarrollador ",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception e2)
                    {
                        MessageBox.Show("No se puede generar el usuario" +
                                " debido a un error interno, por favor contacte al desarrollador: " + e2.ToString(),
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Rol", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private String generarUsuario(String nombre, String apellido)
        {
            int i = 0;
            int j = 0;
            while (nombre[i]!=' ')
            {
                i++;
            }
            while (apellido[j] != ' ')
            {
                j++;
            }
            return (nombre.Substring(0, i) + apellido.Substring(0, j)).ToUpper();
        }


        private String generarContrasena()
        {
            Random aleatorio=new Random();
            //int id = 0;
            String clave = "";
            for (int i = 0; i < 3; i++)
            {
                clave += Convert.ToChar(aleatorio.Next(97,122)).ToString();                
                clave += aleatorio.Next(0, 9);                
            }
            return (clave+=clave.Substring(0,2).ToUpper()).Substring(2,5);
        }       

        private void btnCredenciales_Click_1(object sender, EventArgs e)
        {
            this.GenerarCredenciales();
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Cargar(true);
        }


        private void txbBuscar_KeyDown(object sender, KeyEventArgs e)
        {
           
        }


        public EdicionUsuario()
        {
            InitializeComponent();
            this._Roles = new DataTable();
            this.Cargar();
            this.Roles();
            
            //this.WindowState = FormWindowState.Maximized;
            //dtgEmpleados.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        




        private void AddUsuario_Load(object sender, EventArgs e)
        {           

        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txbFiltro_TextChanged(object sender, EventArgs e)
        {
            this.Filtro();
        }
    }
}
