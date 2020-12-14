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
using SessionManager.CLS;
namespace A_Shop.GUI
{
    public partial class Principal : Form
    {
        private Sesion _sesion = Sesion.Instancia;
        private int childFormNumber = 0;

        public Principal()
        {
            InitializeComponent();
        }

        private void pruebaDeConectorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PruebaConector f = new PruebaConector();
            f.ShowDialog();            
        }

        private void toolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void herramientasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void asdfdsafToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void agregarUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
                                
        }

        private void agregarUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddUsuario f = new AddUsuario();
            f.ShowDialog();
        }

        private void añadirEmpleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddEmpleado f = new AddEmpleado();
            f.ShowDialog();
            if (f._Confirmacion)// Aceptar
            {//ay que ingresar los campos ala DB
                String nombres = f.txbNombres.Text;
                String apellidos = f.txbApellidos.Text;
                String celular = f.txbCelular.Text;
                String telefono = f.txbTelefono.Text;
                String cargo = f.txbCargo.Text;
                String edad = f.txbEdad.Text;
                String correo = f.txbCorreo.Text;
                String direccion = f.txbDireccion.Text;
                String fecha = f.fechaIngreso.Text;
                String dui = f.txbDUI.Text;
                String nit = f.txbNIT.Text;
                String nup = f.txbNUP.Text;
                String Salario = f.txbSalario.Text;
                String estudios = f.txbEstudios.Text;
                DBOperacion operacion = new DBOperacion();
                //Consulta SQL
                String cadena = @"insert into Empleados
                (Nombres,Apellidos,Correo,EstudiosAcademicos,Salario,FechaIngreso,Direccion,NumeroTelefono,NumeroCelular,Cargo,DUI,NIT,NUP,Edad)
                values('" + nombres + @"','" + apellidos + @"','" + correo + @"','" + estudios + @"'" +
                "," + Salario + @",'" + fecha + @"','" + direccion + @"','" + telefono +
                @"','" + celular + @"','" + cargo + @"','" + dui + @"','" + nit + @"','" + nup + @"'," + edad + ");";
                Int32 resultado = operacion.Insertar(cadena);
                if (resultado > 0)
                {//Usuario se agrego con exito
                    MessageBox.Show("Empleado registrado con exito", "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {//Error en la conexion 
                    MessageBox.Show("No se puede registrar el empleado"+
                    " debido a un error interno, por favor contacte al desarrollador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }// si no entonces solo dio en cancelar                       
        }

        private void añadirUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddUsuario f = new AddUsuario();
            f.ShowDialog();
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            String rol = "";
            switch (_sesion.Informacion.IDRol)
            {
                case 1:
                {
                    rol = "Administrador";
                    break;
                }
                case 2:
                {
                    rol = "Usuario";//cajero, bodeguero, etc
                    break;
                }
            }
            lblEstado.Text = "Id: " + _sesion.Informacion.IDUsuario + "  Usuario: " + _sesion.Informacion.Usuario
                             + "  Rol: " + rol;
        }


        private int buscarProveedor(DataTable pro,String clave)
        {
            int i = 0;            
            Boolean enc = false;
            while (i < pro.Rows.Count && !enc)
            {
                if (clave.Equals(pro.Rows[i]["Nombre"]))
                {
                    enc = true;
                }
                else { i++; }                
            }
            return int.Parse(pro.Rows[i]["IDProveedor"].ToString());
        }


        private int buscarCategoria(DataTable pro, String clave)
        {
            int i = 0;
            Boolean enc = false;
            while (i < pro.Rows.Count && !enc)
            {
                if (clave.Equals(pro.Rows[i]["NombreCategoria"]))
                {
                    enc = true;
                }
                else { i++; }
            }
            return int.Parse(pro.Rows[i]["IDCategoria"].ToString());
        }


        private void agregarProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {//Productos
            AddProducto pro = new AddProducto();
            pro.ShowDialog();
            if (pro._Confirmacion)
            {
                String nombre = pro.txbNombre.Text;
                String marca = pro.txbMarca.Text;
                String alias = pro.txbMarca.Text;
                String precioCompra = pro.txbPrecioCompra.Text;
                String precioVenta = (pro.txbPrecioVenta.Text);
                String presentacion = pro.txbPresentacion.Text;
                int idCategoria = buscarCategoria(pro.dtCategoria, pro.cmbCategoria.SelectedItem.ToString());
                int idProveedor = buscarProveedor(pro.dtProveedor, pro.cmbProveedor.SelectedItem.ToString());
                int existencias = int.Parse(pro.txbExistencias.Text);
                String fechaCompra=pro.dtpFechaCompra.Text;
                String fechaVencimiento = pro.dtpFechaVencimiento.Text;
                DBOperacion operacion = new DBOperacion();
                String consulta = @"insert into Productos (NombreProducto,Marca,PrecioVenta,IDCategoria,Existencias," +
                @"IDProveedor,PrecioCompra,FechaCompra,FechaVencimiento,Presentacion,Alias) values(" +
                @"'" + nombre + @"','" + marca + @"','" + precioVenta + @"','" + idCategoria + @"','" + existencias + @"','" + idProveedor
                + @"','" + precioCompra + @"','" + fechaCompra + @"','" + fechaVencimiento + @"','" + presentacion + @"','" + alias + @"')";
               int resultado = operacion.Insertar(consulta);
               if (resultado > 0)
               {
                   MessageBox.Show("Producto registrado con exito", "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
               }
               else
               {
                   MessageBox.Show("No se puede registrar el producto" +
                       " debido a un error interno, por favor contacte al desarrollador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               }

            }
            else
            {
                Console.WriteLine("presiono el boton de cancelar ");
            }
        }

        private void agregarCategoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddCategoria ctg = new AddCategoria();
            ctg.ShowDialog();
            if (ctg._Confirmacion)
            {
                String categoria = ctg.txbNombre.Text;
                String descripcion = ctg.txbDescripcion.Text;
                String cadena = @"insert into Categorias(NombreCategoria,Descripcion) values('" + categoria + @"','" + descripcion + @"')";
                DBOperacion operacion = new DBOperacion();
                int resultado = operacion.Insertar(cadena);
                if (resultado > 0)
                {
                    MessageBox.Show("Categoria registrada con exito", "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se puede registrar la categoria" +
                       " debido a un error interno, por favor contacte al desarrollador", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void agregarProveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddProveedor f = new AddProveedor();
            f.ShowDialog();
            if (f._Confirmacion)
            {
                String nombre = f.txbNombre.Text;
                String correo = f.txbCorreo.Text;
                String telefono = f.txbTelefono.Text;
                String direccion = f.txbDireccion.Text;
                String codigo = f.txbCodigo.Text;
                String giro = f.txbGiro.Text;
                String nombre2 = f.txbNombre2.Text;
                String apellido2 = f.txbApellido2.Text;
                String telefono2 = f.txbTelefono2.Text;
                String correo2 = f.txbCorreo2.Text;
                String edad2 = f.txbEdad2.Text;
                String cargo2 = f.txbCargo2.Text;
                String celular2 = f.txbCelular.Text;
                DBOperacion operacion = new DBOperacion();
                String cadena = @"insert into Proveedores(Nombre, Correo, Telefono, Direccion, Codigo, Giro, Nombre2, Apellido2," +
                 @" Telefono2, Correo2, Edad2, Cargo2, Celular2) values('" + nombre + @"','" + correo + @"','" + telefono + @"','" + direccion + @"','"
                 + codigo + @"','" + giro + @"','" + nombre2 + @"','" + apellido2 + @"','" + telefono2 + "','" + correo2 + @"'," + edad2 + @",'" + cargo2 + "','" + celular2 + @"')";
                int resultado = operacion.Insertar(cadena);
                if(resultado>0){
                    MessageBox.Show("Proveedor registrado con exito", "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se puede registrar el proveedor debido a "+
                        "un error interno, por favor contacte al desarrollador ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void vistaUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectUsuarios f = new SelectUsuarios();
            f.ShowDialog();
        }

      

    }
}
