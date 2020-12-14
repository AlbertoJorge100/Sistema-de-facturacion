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
using A_Shop.CLS;
using SessionManager.CLS;
namespace A_Shop.GUI
{
    public partial class AddUsuario : Form
    {
        private DataTable lista;
        public AddUsuario()
        {
            InitializeComponent();
            lista = new DataTable();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AddUsuario_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            dtgLista.DataSource = Cache.consultaEmpleados();
            cmbRol.Items.Add("Administrador");
            cmbRol.Items.Add("Cajero");
            cmbRol.Items.Add("Bodeguero");          
            //dtgDatos.DataSource = oOperacion.Consultar(txbConsulta.Text); //retorna un datatable
            
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
            int id = 0;
            String clave = "";
            for (int i = 0; i < 3; i++)
            {
                clave += Convert.ToChar(aleatorio.Next(97,122)).ToString();                
                clave += aleatorio.Next(0, 9);                
            }
            return (clave+=clave.Substring(0,2).ToUpper()).Substring(2,5);
        }

        private int generarRol()
        {
            switch (cmbRol.SelectedIndex)
            {
                case 0: { return 1; }
                case 1: { return 2; }
                case 2: { return 3; }
                default: { return -1; }                
            }
        }


        private void btnCredenciales_Click(object sender, EventArgs e)
        {            
            int rol = generarRol();
            if (rol >= 0)
            {
                if (MessageBox.Show("¿Desea agregar un usuario con el empleado seleccionado?",
                "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int a = dtgLista.CurrentCell.RowIndex;
                    String nombres = dtgLista.CurrentRow.Cells["Nombres"].Value.ToString();
                    String apellidos = dtgLista.CurrentRow.Cells["Apellidos"].Value.ToString();
                    String idEmpleado = dtgLista.CurrentRow.Cells["IDEmpleado"].Value.ToString();
                    String idRol = rol.ToString();
                    DBOperacion operacion = new DBOperacion();
                    String usuario = generarUsuario(nombres, apellidos) + idEmpleado;
                    String contrasena = generarContrasena();
                    String contrasenaDB = Hash.generarHash(contrasena, Hash.Opcion.SHA_256);
                    String cadena = @"insert into Usuarios(IDEmpleado,Usuario,Contrasena,IDRol) values(" + idEmpleado + @", '" + usuario +
                            @"', '" + contrasenaDB + @"', " + idRol + ")";
                    int resultado = operacion.Insertar(cadena);
                    if (resultado > 0)
                    {                        
                        lblUsuario.Text = usuario;
                        lblContrasena.Text = contrasena;
                        MessageBox.Show("Usuario generado exitosamente !", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No se puede generar el usuario" +
                                " debido a un error interno, por favor contacte al desarrollador",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar un Rol", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
    

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
