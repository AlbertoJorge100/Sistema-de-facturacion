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
using Reportes.REP;
using System.Globalization;
using DataManager.CLS;
using SessionManager.CLS;
namespace General.GUI
{
    public partial class Ventas : Form
    {
        //private Sesion _Sesion = Sesion.Instancia;
        private BindingSource _DATOS = new BindingSource();
        private String NumeroAuxiliar = "";
        private Boolean Iterar = false;
        Sesion _Sesion = Sesion.Instancia;

        //Reporte que se genera al dar doble click sobre una factura
        private void GenerarReporte()
        {
            try
            {                
                String idfactura = dtgVentas.CurrentRow.Cells["IDFactura"].Value.ToString();
                String numfactura = dtgVentas.CurrentRow.Cells["NumFactura"].Value.ToString();
                String empleado = dtgVentas.CurrentRow.Cells["Empleado"].Value.ToString();
                String fecha = dtgVentas.CurrentRow.Cells["Hora"].Value.ToString();
                ReporteFactura reporte = new ReporteFactura();
                
                DataTable data = Cache.ReporteFacturaDetalle(idfactura);
                DataRow fila = data.Rows[0];

                reporte.SetDataSource(data);
                reporte.SetParameterValue("pEmpleado", fila["Empleado"].ToString());
                reporte.SetParameterValue("pFecha", fila["Fecha"].ToString());
                reporte.SetParameterValue("pNumFactura", numfactura);
                reporte.SetParameterValue("pCliente",fila["Cliente"].ToString());
                crvFacturaDetalle.ReportSource = reporte;                
            }
            catch (Exception e)
            {
                MessageBox.Show("Excepcion reporte: " + e.ToString(), "Excepcion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //Reporte por numero de factura
        private void NumGenerarReporte(String idfactura)
        {
            try
            {                               
                DataTable data = Cache.ReporteNumFactura(idfactura);
                if (data.Rows.Count>0)
                {
                    ReporteFactura reporte = new ReporteFactura();
                    DataRow fila = data.Rows[0];
                    reporte.SetDataSource(data);
                    reporte.SetParameterValue("pEmpleado", fila["Empleado"].ToString());
                    reporte.SetParameterValue("pFecha", fila["Fecha"].ToString());
                    reporte.SetParameterValue("pNumFactura", idfactura);
                    reporte.SetParameterValue("pCliente", fila["Cliente"].ToString());
                    crvFacturaDetalle.ReportSource = reporte;
                }
                else
                {                    
                    MessageBox.Show("Factura no encontrada !", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txbFiltro.Text = "";
                }                
            }
            catch (Exception e)
            {
                MessageBox.Show("Excepcion reporte: " + e.ToString(), "Excepcion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void GenerarReporteAnio()
        {
            try
            {
                DataTable datos = Cache.ReporteF_Anio(txbAnio.Text.ToString());
                int filas = datos.Rows.Count;
                if (filas > 0)
                {
                    ReporteAnio reporte = new ReporteAnio();
                    reporte.SetDataSource(datos);
                    reporte.SetParameterValue("pOpcionReporte", "REPORTE DE VENTAS PARA EL AÑO: " + txbAnio.Text.ToString());
                    reporte.SetParameterValue("pEmpleado", _Sesion.Informacion.Empleado);
                    reporte.SetParameterValue("pFecha", DateTime.Now.ToString());
                    reporte.SetParameterValue("pTotalMeses", filas.ToString());
                    crvFacturaDetalle.ReportSource = reporte;
                }
                else
                {
                    MessageBox.Show("Datos no encontrados !", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }                
            }
            catch (Exception e)
            {
                MessageBox.Show("Excepcion reporte: " + e.ToString(), "Excepcion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        private void GenerarReporteMes()
        {
            try
            {
                DataTable datos = Cache.ReporteF_Mes((cmbMes.SelectedIndex + 1).ToString(), txbAnio.Text.ToString());
                int filas= datos.Rows.Count;
                if (filas>0)
                {
                    ReporteMes reporte = new ReporteMes();
                    reporte.SetDataSource(datos);
                    reporte.SetParameterValue("pOpcionReporte", "REPORTE DE VENTAS PARA EL MES DE " + cmbMes.SelectedItem.ToString().ToUpper() + ", AÑO " + txbAnio.Text.ToString());
                    reporte.SetParameterValue("pEmpleado", _Sesion.Informacion.Empleado);
                    reporte.SetParameterValue("pFecha",DateTime.Now);
                    reporte.SetParameterValue("pTotalDias", filas.ToString());                    
                    crvFacturaDetalle.ReportSource = reporte;
                }
                else
                {
                    MessageBox.Show("Datos no encontrados !", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Excepcion reporte: " + e.ToString(), "Excepcion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Reporte Movimiento de productos
        /// </summary>
        public void GenerarReporteMovimiento()
        {
            try
            {
                DataTable datos = Cache.Reporte_Movimiento_Productos(dtpDia_Inicio.Value.ToString("yyyy-MM-dd"), dtpFin.Value.ToString("yyyy-MM-dd"));
                int filas = datos.Rows.Count;
                if (filas > 0)
                {
                    ReporteMov_Productos reporte = new ReporteMov_Productos();
                    reporte.SetDataSource(datos);
                    reporte.SetParameterValue("pOpcionReporte", "REPORTE DE MOVIMIENTO DE PRODUCTOS PARA EL PERIODO " + dtpDia_Inicio.Value.ToString("dd-MM-yyyy")
                        + " - " + dtpFin.Value.ToString("dd-MM-yyyy"));                        
                    reporte.SetParameterValue("pEmpleado", _Sesion.Informacion.Empleado);
                    reporte.SetParameterValue("pFecha", DateTime.Now);
                    reporte.SetParameterValue("pTotalProductos", filas.ToString());
                    crvFacturaDetalle.ReportSource = reporte;
                }
                else
                {
                    MessageBox.Show("Datos no encontrados !", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Excepcion reporte: " + e.ToString(), "Excepcion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void GenerarReportePeriodo()
        {
            try
            {
                DataTable datos = Cache.Reporte_Periodos(dtpDia_Inicio.Value.ToString("yyyy-MM-dd"), dtpFin.Value.ToString("yyyy-MM-dd"));
                int filas = datos.Rows.Count;
                if (filas > 0)
                {
                    ReportePeriodos reporte = new ReportePeriodos();
                    reporte.SetDataSource(datos);
                    reporte.SetParameterValue("pOpcionReporte", "REPORTE DE VENTAS PARA EL PERIODO " + dtpDia_Inicio.Value.ToString("dd-MM-yyyy")
                        + " - " + dtpFin.Value.ToString("dd-MM-yyyy"));
                    reporte.SetParameterValue("pEmpleado", _Sesion.Informacion.Empleado);
                    reporte.SetParameterValue("pFecha", DateTime.Now);
                    reporte.SetParameterValue("pTotalProductos", filas.ToString());
                    crvFacturaDetalle.ReportSource = reporte;
                }
                else
                {
                    MessageBox.Show("Datos no encontrados !", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Excepcion reporte: " + e.ToString(), "Excepcion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Generar reporte para un dia especifico
        private void GenerarReporteFecha()
        {
            try
            {
                DataTable resultado = Cache.ReporteF_Dia(dtpDia_Inicio.Value.ToString("yyyy-MM-dd"));
                if(resultado.Rows.Count > 0)
                {    
                    ReporteDia reporte = new ReporteDia();
                    reporte.SetDataSource(resultado);
                    reporte.SetParameterValue("pOpcionReporte", "REPORTE DE VENTAS A LA FECHA: " + dtpDia_Inicio.Value.ToString("dd-MM-yyyy"));
                    reporte.SetParameterValue("pEmpleado", _Sesion.Informacion.Empleado);
                    reporte.SetParameterValue("pFecha", DateTime.Now.ToString());
                    reporte.SetParameterValue("pTotalFacturas", resultado.Rows.Count.ToString());
                    crvFacturaDetalle.ReportSource = reporte;
                }
                else
                {
                    MessageBox.Show("No se han encontrado datos para ese dia !", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
            }
            catch (Exception e)
            {
                MessageBox.Show("Excepcion reporte: " + e.ToString(), "Excepcion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        private void Cargar(Boolean RFR = false)
        {
            try
            {
                _DATOS.DataSource = Cache.ConsultaFacturaVentas();
                dtgVentas.AutoGenerateColumns = false;
                dtgVentas.DataSource = _DATOS;
                lblEstado.Text = dtgVentas.Rows.Count.ToString() + " Registros encontrados";
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
       
        public Ventas()
        {
            InitializeComponent();
            /*panel1.Parent =splitContainer1.Panel2;
            panel2.Parent = splitContainer1.Panel2;
            panel4.Parent = splitContainer1.Panel2;
            panel6.Parent = splitContainer1.Panel2;*/

        }

        private void Ventas_Load(object sender, EventArgs e)
        {
            Cargar();            
            //GenerarReporte();
        }
           

       
        //Evento doble click para ver una factura
        private void Evt(object sender, DataGridViewCellMouseEventArgs e)
        {
            GenerarReporte();
        }

        //Label refrescar
        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            this.Cargar();
        }

        //Buscar
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            String id = txbFiltro.Text.ToString();
            if (!id.Equals(""))
            {
                NumGenerarReporte(id);
            }
            else
            {
                MessageBox.Show("Debe ingresar el id de la factura a buscar ! ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //Eliminar
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            String numfactura = txbFiltro.Text.ToString();
            if (!numfactura.Equals(""))
            {
                if (MessageBox.Show("¿Desea eliminar la factura del sistema definitivamente?",
                "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
                {                                
                    try
                    {
                        DBOperacion operacion = new DBOperacion();
                        String cadena = @"UPDATE Facturas f SET f.Estado=FALSE  WHERE IDFactura=" + (int.Parse(numfactura) - 100).ToString();
                        if (operacion.Actualizar(cadena) > 0)
                        {
                            MessageBox.Show("Factura eliminada con exito !",
                                "Informacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            crvFacturaDetalle.ReportSource = null;
                        }
                        else
                        {
                            MessageBox.Show("Error al eliminar la factura, Porfavor contacte con el desarrollador: ",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al facturar los productos, Porfavor contacte con el desarrollador: " + ex.ToString(),
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }                
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar el id de la factura a eliminar ! ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        
        private void Visibilidad()
        {
            this.dtpDia_Inicio.Enabled = false;
            this.cmbMes.Enabled = false;
            this.cmbMes.SelectedIndex = -1;
            this.txbAnio.Enabled = false;
            this.txbAnio.Text = "";
            lblInicio.Visible = false;
            lblFin.Visible = false;
            dtpFin.Enabled = false;
        }

   
        
        private void button1_Click(object sender, EventArgs e)
        {
            switch (cmbOpcion.SelectedIndex)
            {
                case 0://Reporte dia especifico
                    GenerarReporteFecha();
                    break;
                case 1://Reporte Mes especifico
                    if(cmbMes.SelectedIndex >= 0 && !txbAnio.Text.Equals(""))
                        GenerarReporteMes();
                    else
                        MessageBox.Show("Seleccione un mes, e ingrese un año ! ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case 2:
                    if (!txbAnio.Text.Equals(""))
                        GenerarReporteAnio();
                    else
                        MessageBox.Show("Ingrese un año porfavor ! ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case 3:
                    GenerarReporteMovimiento();
                    break;
                case 4:
                    GenerarReportePeriodo();
                    break;
                default:
                    MessageBox.Show("Seleccione una opcion de inventario ! ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
            }
            
        }

        private void cmbOpcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Visibilidad();
            switch (cmbOpcion.SelectedIndex)
            {
                case 0:
                    this.dtpDia_Inicio.Enabled = true;
                    break;
                case 1:
                    this.cmbMes.Enabled = true;
                    this.txbAnio.Enabled = true;
                    break;
                    case 2:
                    txbAnio.Enabled = true;
                    break;
                case 3:
                    this.dtpDia_Inicio.Enabled = true;
                    this.dtpFin.Enabled = true;
                    this.lblInicio.Visible = true;
                    this.lblFin.Visible = true;
                    break;
                case 4:
                    this.dtpDia_Inicio.Enabled = true;
                    this.dtpFin.Enabled = true;
                    this.lblInicio.Visible = true;
                    this.lblFin.Visible = true;
                    break;
            }
        }

  
    }
}
