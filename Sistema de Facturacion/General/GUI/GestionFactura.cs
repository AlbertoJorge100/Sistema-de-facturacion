using System;
using CacheManager.CLS;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataManager.CLS;
using Reportes.GUI;
namespace General.GUI
{
    public partial class GestionFactura : Form
    {
        private String IDFactura;
        private String IDEmpleado;
        private int Contador;
        private Double Total;
        private Double SubTotal;
        private Double Descuentos;
        private Double Cantidad;        
        private DataTable aux2;
        private Boolean Facturado = false;
        private Boolean EstadoFactura = false;
        private int ContadorItems = 0;

        private BindingSource _DATOS = new BindingSource();


        private void Cargar(Boolean RFR = false)
        {
            try
            {                   
                DataTable datos= Cache.CargarProductosFactura();
                _DATOS.DataSource = datos; 
                if (RFR)
                {
                    txbFiltro.Text = "";
                }
                Filtro();
                lblCnt.Text = datos.Rows.Count + " productos encontrados";
            }
            catch(Exception e)
            {
                Console.WriteLine("excepcion: " + e.ToString());
            }
        }


        private void Filtro()
        {
            try
            {
                String filtro = txbFiltro.Text;
                if (filtro.Length > 0)
                {
                    _DATOS.Filter = "Producto LIKE '%" + filtro + @"%' OR Alias LIKE '%" + filtro + @"%'";
                }
                else
                {
                    _DATOS.RemoveFilter();
                }
                dtgProductos.AutoGenerateColumns = false;
                dtgProductos.DataSource = _DATOS;
                //lblEstado.Text = dtgProductos.Rows.Count.ToString() + " Registros encontrados";
            }
            catch (Exception e)
            {
                Console.WriteLine("excepcion: " + e.ToString());
            }
        }


        public GestionFactura(String IDEmpleado)
        {
            InitializeComponent();                     
            this.IDEmpleado = IDEmpleado;
            CargarSeleccionados();
        }

      
        private Boolean Facturar()
        {
            Boolean facturado = false;
            try
            {
                DBOperacion operacion = new DBOperacion();
                String cadena = @"INSERT INTO FacturaProductos (IDProducto, IDFactura, Cantidad,Descuento, SubTotal) VALUES";
                int i = 0;
                int fin = dtgSeleccionados.Rows.Count;
                while(i < fin)
                {
                    DataGridViewRow row = dtgSeleccionados.Rows[i];
                    cadena += "("+ row.Cells["IDProducto2"].Value.ToString();
                    cadena += ", " + IDFactura + ", ";
                    cadena += row.Cells["Cantidad2"].Value.ToString() + ", ";
                    String desc = row.Cells["D_Total2"].Value.ToString();
                    desc=desc.Replace(",", ".");
                    cadena += desc + ", ";
                    desc = row.Cells["SubTotal2"].Value.ToString();
                    desc=desc.Replace(",", ".");
                    if (i != fin-1)
                    {                        
                        cadena += desc + "),";
                    }
                    else
                    {
                        cadena += desc +");";
                    }
                    i++;                    
                }
                              
                if (operacion.Insertar(cadena) > 0)
                {
                    MessageBox.Show("Factura creada con exito");                            
                    VistaReporteFactura vista = new VistaReporteFactura(IDFactura, txbCliente.Text.ToString());
                    vista.ShowDialog();  
                    facturado = true;                    
                }
                else
                {
                    MessageBox.Show("Error al facturar los productos, Porfavor contacte con el desarrollador: ",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al facturar los productos, Porfavor contacte con el desarrollador: " + e.ToString(),
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return facturado;
        }



        private Boolean CrearFactura()
        {
            Boolean fct = false;
            try
            {
                DataTable factura = Cache.ConsultarFactura(this.IDEmpleado);
                if (factura.Rows.Count > 0)
                {
                    this.IDFactura = factura.Rows[0]["IDFactura"].ToString();
                    fct = true;
                }
                else
                {                    
                    MessageBox.Show("Error al crear la factura, Porfavor contacte con el desarrollador",
                        "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            catch (Exception e2)
            {
                MessageBox.Show("Error al crear la factura, Porfavor contacte con el desarrollador: "+ e2.ToString(),
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return fct;
        }


        private void CargarSeleccionados()
        {
            lblTotal.Text = "";
            lblSubTotal.Text = "";
            lblDescuento.Text = "";
            lblProductos.Text = "";
            lblItems.Text = "";
            txbCliente.Text = "";
            lblFactura.Text = "Factura no creada";
            aux2 = new DataTable();
            aux2.Columns.Add("IDProducto2");
            aux2.Columns.Add("Cantidad2");
            aux2.Columns.Add("Producto2");
            aux2.Columns.Add("Marca2");
            aux2.Columns.Add("Precio2");
            aux2.Columns.Add("Descuento2");
            aux2.Columns.Add("D_Total2");
            aux2.Columns.Add("SubTotal2");
            aux2.Columns.Add("Presentacion2");
            dtgSeleccionados.DataSource = aux2;
            this.Total = 0.0;
            this.Descuentos = 0.0;
            this.SubTotal = 0.0;
            this.Contador = 0;
            this.Cantidad = 0.0;
            this.ContadorItems = 0;
            this.Cargar();
        }


        private void VistaProductos_Load(object sender, EventArgs e)
        {
            Cargar();
        }
 

        private void resetCantidades()
        {
            txbCantidad.Text = "1";
            txbCantidad2.Text = "1";            
        }


        private void btnCrearFactura_Click(object sender, EventArgs e)
        {                                     
            resetCantidades();            
            if (CrearFactura())
            {
                lblFactura.Text = "Factura creada";                
                EstadoFactura = true;
            }
            else
            {
                lblFactura.ForeColor = Color.Red;
                lblFactura.Text = "Error al crear la factura";
            }
            //dtgSeleccionados.AutoGenerateColumns = false;            
        }


        private void MostrarPrecios()
        {
            lblSubTotal.Text = SubTotal.ToString();
            lblTotal.Text = Total.ToString();
            lblDescuento.Text = Descuentos.ToString();
            lblProductos.Text = this.Contador.ToString();
        }


        private void Agregar()
        {                      
            Double cantidad = (Double)double.Parse(txbCantidad.Text);
            int existencias =int.Parse(dtgProductos.CurrentRow.Cells["Existencias"].Value.ToString());
            if (cantidad <= existencias)
            {
                if (!txbCantidad.Text.Equals("") && !txbCantidad.Text.Equals("0"))
                {
                    try
                    {                        
                        DataRow linea = aux2.NewRow();
                        int i = dtgProductos.CurrentCell.RowIndex;
                        Double precio = (Double)double.Parse(dtgProductos.CurrentRow.Cells["Precio"].Value.ToString());
                        Double desc = (Double)double.Parse(dtgProductos.CurrentRow.Cells["Descuento"].Value.ToString());
                        Double subtotal = ((precio) * cantidad);
                        Double descuento = (desc * cantidad);                            
                        linea["IDProducto2"] = dtgProductos.CurrentRow.Cells["IDProducto"].Value.ToString();
                        linea["Cantidad2"] = cantidad.ToString();
                        linea["Producto2"] = dtgProductos.CurrentRow.Cells["Producto"].Value.ToString();
                        linea["Marca2"] = dtgProductos.CurrentRow.Cells["Marca"].Value.ToString();
                        linea["Precio2"] = precio.ToString();
                        linea["Descuento2"] = desc.ToString();
                        linea["D_Total2"] = descuento.ToString();
                        linea["SubTotal2"] = subtotal.ToString();
                        linea["Presentacion2"] = dtgProductos.CurrentRow.Cells["Presentacion"].Value.ToString();
                        aux2.Rows.Add(linea);
                        dtgSeleccionados.DataSource = aux2;                            
                        this.Contador += (int)cantidad;
                        this.Descuentos += descuento;
                        this.Total += (Double)(subtotal - descuento);
                        this.SubTotal += subtotal;
                        this.MostrarPrecios();
                        this.ContadorItems++;
                        lblItems.Text = this.ContadorItems.ToString();
                        //si la cantidad que pedimos es igual ala existencias entonces se eliminara el producto de las vistas
                        if (cantidad == existencias)
                        {
                            //Se oculta de la lista un producto si ya se agotaron las existencias
                            _DATOS.RemoveAt(i);
                            //int i0=_DATOS.Position;                            
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error interno contacte con el desarrollador !" + ex.ToString(),
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Ingrese una cantidad valida !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("La cantidad solicitada no puede ser aceptada", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);                
            }
            resetCantidades();
        }


        private void EliminarSeleccionado()
        {
            Double cantidad = (Double)double.Parse(dtgSeleccionados.CurrentRow.Cells["Cantidad2"].Value.ToString());            
            Double descuento = (Double)double.Parse(dtgSeleccionados.CurrentRow.Cells["Descuento2"].Value.ToString());
            Double precio = (Double)double.Parse(dtgSeleccionados.CurrentRow.Cells["Precio2"].Value.ToString());
            Double desctotal = (Double)double.Parse(dtgSeleccionados.CurrentRow.Cells["D_Total2"].Value.ToString());           
            Double auxdesc = (cantidad * descuento);
            Double auxsubt = (cantidad * precio);
            this.Contador -= (int)cantidad;//Contador de productos
            this.Descuentos -= auxdesc;
            this.SubTotal -= auxsubt;
            this.Total -= (auxsubt - auxdesc);
            dtgSeleccionados.Rows.RemoveAt(dtgSeleccionados.CurrentRow.Index);
            this.ContadorItems -= 1;//Contador de items
            lblItems.Text = this.ContadorItems.ToString();
            this.MostrarPrecios();            
        }
        private void EliminarCantidad()
        {
            Double cantidad = (Double)double.Parse(dtgSeleccionados.CurrentRow.Cells["Cantidad2"].Value.ToString());
            Double cant = (Double)double.Parse(txbCantidad2.Text);
            Double descuento = (Double)double.Parse(dtgSeleccionados.CurrentRow.Cells["Descuento2"].Value.ToString());
            Double precio = (Double)double.Parse(dtgSeleccionados.CurrentRow.Cells["Precio2"].Value.ToString());
            Double desctotal = (Double)double.Parse(dtgSeleccionados.CurrentRow.Cells["D_Total2"].Value.ToString());
            string ad= "Se eliminaran: " + cant + " productos";            
            if (cant > 0 && cant <= cantidad)
            {//si la cantidad a eliminar es menor ala seleccionad                            
                if (MessageBox.Show(ad, "Confirmacion",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        Double auxdesc = (cant * descuento);
                        Double auxsubt = (cant * precio);
                        this.Contador -= (int)cant;
                        this.Descuentos -= auxdesc;
                        this.SubTotal -= auxsubt;
                        this.Total -= (auxsubt - auxdesc);
                        Double auxcant = cantidad - cant;                                               
                        dtgSeleccionados.CurrentRow.Cells["Cantidad2"].Value = auxcant.ToString();
                        dtgSeleccionados.CurrentRow.Cells["SubTotal2"].Value = auxcant * precio;
                        dtgSeleccionados.CurrentRow.Cells["D_Total2"].Value = desctotal - auxdesc;                                                
                        this.MostrarPrecios();
                        if (cantidad == cant)
                        {
                            dtgSeleccionados.Rows.RemoveAt(dtgSeleccionados.CurrentRow.Index);
                            this.ContadorItems -= 1;
                            lblItems.Text = this.ContadorItems.ToString();
                        }
                        MessageBox.Show("" + cant.ToString() + " productos han sido eliminados !", "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error interno contacte con el desarrollador !" + ex.ToString(),
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una cantidad correcta !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            if (EstadoFactura)
            {
                if (this.ContadorItems < 10 )
                {                    
                    this.Agregar();
                }
                else
                {
                    MessageBox.Show("Ya no puede agregar productos !", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }                
            }
            else
            {
                MessageBox.Show("Debe crear una factura !", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Se eliminara el producto seleccionado", "Confirmacion",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes
                           && dtgSeleccionados.Rows.Count > 0)
            {
                EliminarSeleccionado();
            }
        }

      
        private void btnFacturar_Click(object sender, EventArgs e)
        {
            if (dtgSeleccionados.Rows.Count > 0 )
            {
                if (!txbCliente.Text.ToString().Equals(""))
                {
                    if (Facturar())
                    {
                        CargarSeleccionados();
                        EstadoFactura = false;
                    }
                }
                else
                {
                    MessageBox.Show("Debe ingresar el nombre del cliente", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }                
            }
            else
            {
                MessageBox.Show("Debe seleccionar productos", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }            
        }

        private void txbFiltro_TextChanged(object sender, EventArgs e)
        {
            Filtro();
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            Cargar(true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dtgSeleccionados.Rows.Count > 0 && !txbCantidad2.Text.Equals("0") && !txbCantidad2.Text.Equals(""))
            {
                EliminarCantidad();
            }
            else
            {
                MessageBox.Show("Error de cantidad o de productos seleccionados !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            resetCantidades();
        }
    }
}
