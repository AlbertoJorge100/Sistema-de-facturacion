using System;
using DataManager.CLS;
using CacheManager.CLS;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using General.CLS;

namespace General.GUI
{
    public partial class EdicionProducto : Form
    {
        private Boolean Confirmacion;
        public DataTable dtProveedor;
        public DataTable dtCategoria;
        private String IDProducto;
        public enum Opcion { INSERTAR, ACTUALIZAR };
        private Opcion Opciones;
        public Opcion _Opcion { get { return this.Opciones; } }
        public Boolean _Confirmacion { get { return this.Confirmacion; } }
        private void Procesar()
        {
            try
            {
                int idCategoria = buscarCategoria(dtCategoria, cmbCategoria.SelectedItem.ToString());
                int idProveedor = buscarProveedor(dtProveedor, cmbProveedor.SelectedItem.ToString());
                Productos prod = new Productos()
                {
                    IDProducto = this.IDProducto,
                    NombreProducto = txbNombre.Text,
                    Marca = txbMarca.Text,
                    PrecioVenta = txbPrecioVenta.Text,
                    Descuento = txbDescuento.Text,
                    IDCategoria = idCategoria.ToString(),
                    Existencias = txbExistencias.Text,
                    IDProveedor = idProveedor.ToString(),
                    PrecioCompra = txbPrecioCompra.Text,
                    FechaCompra = dtpFechaCompra.Text,
                    FechaVencimiento = dtpFechaVencimiento.Text,
                    Presentacion = txbPresentacion.Text,
                    Alias = txbAlias.Text
                };                
                if (this.Opciones == Opcion.INSERTAR)
                {
                    if (prod.Guardar())
                    {
                        this.LimpiarPantalla();
                        MessageBox.Show("Producto ingresado exitosamente ", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Confirmacion = true;
                        //this.Close();
                    }
                    else
                    {
                        MessageBox.Show("El producto no pudo ser ingresado, porfavor contacte con el desarrollador ", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (prod.Actualizar())
                    {
                        MessageBox.Show("Producto actualizado exitosamente ", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Confirmacion = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("El producto no pudo ser actualizado, porfavor contacte con el desarrollador ", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("El producto no pudo ser actualizado, porfavor contacte con el desarrollador: " + e.ToString(), "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarPantalla()
        {
            txbNombre.Text = "";
            txbAlias.Text = "";
            txbMarca.Text = "";
            txbPrecioCompra.Text = "";
            txbPrecioVenta.Text = "";
            txbPresentacion.Text = "";
            cmbCategoria.SelectedIndex = -1;
            cmbProveedor.SelectedIndex = -1;
            txbExistencias.Text = "";
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }


        //Setear el combobox desde el servidor
        public void addCmb()
        {
            try
            {
                dtCategoria = Cache.ConsultaCategorias("",1); //retorna un datatable            
                int i = 0;
                while (i < dtCategoria.Rows.Count)
                {
                    this.cmbCategoria.Items.Add(dtCategoria.Rows[i]["NombreCategoria"].ToString());
                    i++;
                }
                dtProveedor = Cache.consultaProveedores("",5);
                i = 0;
                while (i < dtProveedor.Rows.Count)
                {
                    this.cmbProveedor.Items.Add(dtProveedor.Rows[i]["Nombre"].ToString());
                    i++;
                }
            }
            catch (Exception e2)
            {
                MessageBox.Show("No se pueden cargar las categorias o proveedores debido a un error interno, "
                    + "por favor contacte con el desarrollador: " + e2.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private Boolean validarPrecios(String clave)
        {
            int i = 0;
            Boolean enc = false;
            int ascii;
            while (i < clave.Length && !enc)
            {
                ascii = (int)clave[i];
                if (ascii < 46 || ascii > 57)
                {
                    enc = true;
                }
                i++;
            }
            return enc;
        }


        public int buscarProveedor(DataTable pro, String clave)
        {
            int i = 0;
            Boolean enc = false;
            while (i < pro.Rows.Count && !enc)
            {
                if (clave.Equals(pro.Rows[i]["Nombre"].ToString()))
                {
                    enc = true;
                }
                else { i++; }
            }
            return int.Parse(pro.Rows[i]["IDProveedor"].ToString());
        }


        public int buscarCategoria(DataTable pro, String clave)
        {
            int i = 0;
            Boolean enc = false;
            while (i < pro.Rows.Count && !enc)
            {
                if (clave.Equals(pro.Rows[i]["NombreCategoria"].ToString()))
                {
                    enc = true;
                }
                else { i++; }
            }
            return int.Parse(pro.Rows[i]["IDCategoria"].ToString());
        }

        /*
        private Boolean addProducto()
        {
            String nombre = txbNombre.Text;
            String marca = txbMarca.Text;
            String alias = txbAlias.Text;
            String precioCompra = txbPrecioCompra.Text;
            String precioVenta = (txbPrecioVenta.Text);
            String presentacion = txbPresentacion.Text;
            int idCategoria = buscarCategoria(dtCategoria, cmbCategoria.SelectedItem.ToString());
            int idProveedor = buscarProveedor(dtProveedor, cmbProveedor.SelectedItem.ToString());
            int existencias = int.Parse(txbExistencias.Text);
            String fechaCompra = dtpFechaCompra.Text;
            String fechaVencimiento = dtpFechaVencimiento.Text;
            String consulta = @"insert into Productos (NombreProducto,Marca,PrecioVenta,IDCategoria,Existencias," +
            @"IDProveedor,PrecioCompra,FechaCompra,FechaVencimiento,Presentacion,Alias) values(" +
            @"'" + nombre + @"','" + marca + @"','" + precioVenta + @"','" + idCategoria + @"','" + existencias + @"','" + idProveedor
            + @"','" + precioCompra + @"','" + fechaCompra + @"','" + fechaVencimiento + @"','" + presentacion + @"','" + alias + @"')";
            try
            {
                DBOperacion operacion = new DBOperacion();
                int resultado = operacion.Insertar(consulta);
                if (resultado > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }*/

        //Seteamos todos los textview 
        private void limpiarPantalla()
        {
            this.txbNombre.Text = "";
            txbMarca.Text = "";
            txbAlias.Text = "";
            txbExistencias.Text = "";
            txbPrecioCompra.Text = "";
            txbPrecioVenta.Text = "";
            txbPresentacion.Text = "";
            cmbCategoria.SelectedIndex = -1;
            cmbProveedor.SelectedIndex = -1;
        }
        
        
        private void AddProducto_Load(object sender, EventArgs e)
        {
            dtpFechaCompra.Format = DateTimePickerFormat.Custom;
            dtpFechaCompra.CustomFormat = "yyyy/MM/dd";
            dtpFechaVencimiento.Format = DateTimePickerFormat.Custom;
            dtpFechaVencimiento.CustomFormat = "yyyy/MM/dd";
            this.MaximizeBox = false;
            this.MinimizeBox = false; 
            //Aqui llenamos el dataTable para mostrar los datos de la consulta                                   
        }

        //Constructor
        public EdicionProducto(Opcion opc, String IDP = "")
        {
            InitializeComponent();
            dtProveedor = new DataTable();
            dtCategoria = new DataTable();
            this.Confirmacion = false;
            this.Opciones = opc;
            //Validacion de Opciones {Insertar || Actualizar}
            if (this.Opciones == Opcion.INSERTAR)
            {//Si insertar traemos las categorias del servidor
                this.addCmb();
            }
            else
            {
                this.IDProducto = IDP;
                this.panel1.Visible = true;
                this.txbExistencias.Enabled = false;
            }
        }


        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!txbNombre.Text.Equals("") && !txbMarca.Text.Equals("") && !txbExistencias.Text.Equals("")
                && !txbPrecioCompra.Text.Equals("") && !txbPrecioVenta.Text.Equals("") && !txbDescuento.Text.Equals(""))
                {
                    if (cmbCategoria.SelectedIndex >= 0 && cmbProveedor.SelectedIndex >= 0)
                    {
                        if (!validarPrecios(txbPrecioCompra.Text) && 
                                !validarPrecios(txbPrecioVenta.Text) && !validarPrecios(txbExistencias.Text))
                        {
                            this.Procesar();
                            this.Confirmacion = true;
                        }
                        else
                        {
                            MessageBox.Show("Formato incorrecto de Precios o Existencias", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }                        
                    }
                    else
                    {
                        MessageBox.Show("Debe seleccionar una categoria y un proveedor !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            else
            {
                MessageBox.Show("No deje campos vacios !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMenos_Click(object sender, EventArgs e)
        {            
            String cant = txbAdd.Text.ToString();
            int existencias = int.Parse(txbExistencias.Text.ToString());
            if (!cant.Equals(""))
            {//si textview cantidad tiene datos
                int cantidad = int.Parse(cant);
                if (cantidad < existencias && cantidad>0)
                {//si la cantidad es valida
                    MessageBox.Show("Se han restado: " + cant + " productos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txbExistencias.Text = (existencias - cantidad).ToString();
                    txbAdd.Text = "";
                }
                else
                {
                    MessageBox.Show("No se puede restar esa cantidad de productos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txbAdd.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Ingrese una cantidad", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }            
        }

        private void btnMas_Click(object sender, EventArgs e)
        {
            String cant = txbAdd.Text.ToString();
            int existencias = int.Parse(txbExistencias.Text.ToString());
            if (!cant.Equals(""))
            {//si textview cantidad tiene datos
                int cantidad = int.Parse(cant);
                if (cantidad > 0)
                {//si la cantidad es valida
                    MessageBox.Show("Se han sumado: " + cant + " productos", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txbExistencias.Text = (existencias + cantidad).ToString();                    
                    txbAdd.Text = "";
                }
                else
                {
                    MessageBox.Show("No se puede sumar esa cantidad de productos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txbAdd.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Ingrese una cantidad", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
