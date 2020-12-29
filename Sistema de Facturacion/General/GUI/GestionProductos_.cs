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
using SessionManager.CLS;
namespace General.GUI
{
    public partial class GestionProductos_ : Form
    {        
        private Sesion _Sesion = Sesion.Instancia;
        private BindingSource _DATOS = new BindingSource();
        private String NumeroAuxiliar = "";        
        private void Cargar(Boolean RFR=false)
        {
            try
            {
                _DATOS.DataSource = Cache.consultaProductos();
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


       
        private void Filtro(Boolean pOpcion=false)
        {
            try
            {
                if (ValidarFiltro())
                {
                    _DATOS.RemoveFilter();                    
                    Setear();
                }                
                String filtro = txbFiltro.Text;                              
                if (filtro.Length > 0)
                {
                    _DATOS.Filter = "NombreProducto LIKE '%" + filtro + @"%' OR Alias LIKE '%" + filtro + @"%'";
                }
                else
                {
                    _DATOS.RemoveFilter();
                }
                dtgProductos.AutoGenerateColumns = false;
                dtgProductos.DataSource = _DATOS;
                lblEstado.Text = dtgProductos.Rows.Count.ToString() + " Registros encontrados";                                              
            }
            catch
            {

            }          
        }


        private Boolean ValidarFiltro()
        {
            return ((cmbCategorias.SelectedIndex >= 0 || cmbOpcion.SelectedIndex >= 0) ||
                (cmbCategorias.SelectedIndex >= 0 && cmbOpcion.SelectedIndex >= 0));            
        }


        private void Setear()
        {
            cmbCategorias.SelectedIndex = -1;
            cmbOpcion.SelectedIndex = -1;
            txbCantidad.Text = "";
        }
        private Boolean ValidarCampos()
        {
            return !txbCantidad.Text.ToString().Equals("");
        }                 

        public void FiltroPersonalizado(Boolean pOpcion = false)
        {
            try
            {
                Boolean valopcion = false, validar_campos=true;                            
                int opcion = cmbOpcion.SelectedIndex;
                String condicion = "";
                if (opcion >= 0)
                {
                    valopcion = true;       
                    switch (opcion)
                    {
                        case 0: {condicion = "Existencias > 1 AND Existencias <= 30"; break;}
                        case 1: { condicion = "Existencias <= 1"; break; }
                        case 2:
                        {
                            condicion = "FechaVencimiento > '" + DateTime.Now + "' AND FechaVencimiento < '" + DateTime.Now.AddDays(+60).ToString() + "'";
                            break;
                        }                                               
                        case 3:{condicion = "'" + DateTime.Now + "' >= FechaVencimiento"; break;}
                        case 4:
                        {
                            if (ValidarCampos())
                            {
                                condicion = " Existencias > " + txbCantidad.Text.ToString();
                            }
                            else
                            {
                                validar_campos = false;
                                MessageBox.Show("Ingrese una cantidad", "Aviso", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                            }                            
                            break;
                        }
                        case 5:
                        {
                            if (ValidarCampos())
                            {
                                condicion = " Existencias < " + txbCantidad.Text.ToString();
                            }
                            else
                            {
                                MessageBox.Show("Ingrese una cantidad", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                validar_campos = false;
                            }                                
                            break;
                        }        
                    }
                }
                if (cmbCategorias.SelectedIndex >= 0 && validar_campos)
                {
                    if (valopcion)
                    {
                        condicion += @" AND IDCategoria = " + cmbCategorias.SelectedValue.ToString();
                    }
                    else
                    {
                        condicion += @"IDCategoria = " + cmbCategorias.SelectedValue.ToString();
                    }                    
                }                
                _DATOS.Filter = condicion;                
                dtgProductos.AutoGenerateColumns = false;
                dtgProductos.DataSource = _DATOS;
                lblEstado.Text = dtgProductos.Rows.Count.ToString() + " Registros encontrados";
            }
            catch(Exception e)
            {
                MessageBox.Show("Excepcion: " + e.ToString());
            }
        }


        private void CargarCategorias()
        {
            try
            {                
                DataTable lista = new DataTable();
                lista = Cache.ConsultaCategorias("", 1);
                cmbCategorias.Items.Add("Todas las categorias");
                cmbCategorias.DataSource = lista;                
                cmbCategorias.DisplayMember = "NombreCategoria";
                cmbCategorias.ValueMember = "IDCategoria";
                cmbCategorias.SelectedIndex = -1;
            }
            catch (Exception e)
            {
                Console.WriteLine("excepcion en roles: " + e.ToString());
            }
        }


        private void Modificar()
        {
            if (MessageBox.Show("¿Desea modificar el producto?", "Confirmacion", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                String IDP = dtgProductos.CurrentRow.Cells["IDProducto"].Value.ToString();
                EdicionProducto f = new EdicionProducto(EdicionProducto.Opcion.ACTUALIZAR, IDP);
                f.Text = "Actualizar producto";
                f.btnAceptar.Text = "Actualizar";
                f.addCmb();
                f.txbNombre.Text = dtgProductos.CurrentRow.Cells["NombreProducto"].Value.ToString();
                f.txbMarca.Text = dtgProductos.CurrentRow.Cells["Marca"].Value.ToString();
                f.txbPrecioVenta.Text = dtgProductos.CurrentRow.Cells["PrecioVenta"].Value.ToString().Replace(",", ".");
                f.cmbCategoria.SelectedItem = dtgProductos.CurrentRow.Cells["NombreCategoria"].Value.ToString();
                f.txbExistencias.Text = dtgProductos.CurrentRow.Cells["Existencias"].Value.ToString();
                f.cmbProveedor.SelectedItem = dtgProductos.CurrentRow.Cells["Nombre"].Value.ToString();
                f.txbPrecioCompra.Text = dtgProductos.CurrentRow.Cells["PrecioCompra"].Value.ToString().Replace(",", ".");
                f.dtpFechaCompra.Text = dtgProductos.CurrentRow.Cells["FechaCompra"].Value.ToString();
                f.dtpFechaVencimiento.Text = dtgProductos.CurrentRow.Cells["FechaVencimiento"].Value.ToString();
                f.txbPresentacion.Text = dtgProductos.CurrentRow.Cells["Presentacion"].Value.ToString();
                f.txbAlias.Text = dtgProductos.CurrentRow.Cells["Alias"].Value.ToString();
                f.ShowDialog();
                if (f._Confirmacion)
                {
                    this.Cargar(true);
                }
            }
        }


        private void Eliminar()
        {
            if (MessageBox.Show("¿Desea eliminar el producto?", "Confirmacion", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    Productos pro=new Productos()  
                    {
                        IDProducto=dtgProductos.CurrentRow.Cells["IDProducto"].Value.ToString()
                    };

                    if (pro.Eliminar())
                    {
                        MessageBox.Show("El producto fue eliminado exitosamente", "Informacion", MessageBoxButtons.OK,
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


        public GestionProductos_()
        {
            InitializeComponent();
            this.Cargar();
            CargarCategorias();
            //CargarNotificaciones();
            //Temp.Start();
        }

        private void GestionProductos__Load(object sender, EventArgs e)
        {
            try
            {//IDOPCION = 1
                if (_Sesion.Informacion.VerificarPermisos(14))
                {
                    this.panel1.Visible=true;
                }              
            }
            catch (Exception e2)
            {
                Console.WriteLine("Excepcion en principal: " + e2.ToString());
            }
        }

     
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            EdicionProducto f = new EdicionProducto(EdicionProducto.Opcion.INSERTAR);
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            this.Eliminar();
        }

        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            this.Cargar(true);
        }                

        private void txbFiltro_TextChanged(object sender, EventArgs e)
        {            
            this.Filtro();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            FiltroPersonalizado();
        }


        private void btnReset_Click(object sender, EventArgs e)
        {
            cmbCategorias.SelectedIndex = -1;
        }
    }
}
