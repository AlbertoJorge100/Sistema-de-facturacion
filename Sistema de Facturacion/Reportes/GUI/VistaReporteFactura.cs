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
using SessionManager.CLS;
namespace Reportes.GUI
{
    public partial class VistaReporteFactura : Form
    {
        private Sesion _Sesion = Sesion.Instancia;
        private String IDFactura;
        private String Cliente;
        public VistaReporteFactura(String IDFactura, String Cliente)
        {             
            InitializeComponent();
            this.IDFactura = IDFactura;
            this.Cliente = Cliente;
        }


        private void GenerarReporte()
        {
            try
            {
                DateTime myDateTime = DateTime.Now;
                String fecha = myDateTime.ToString("yyyy-MM-dd hh:mm:ss");
                ReporteFactura reporte = new ReporteFactura();
                reporte.SetDataSource(Cache.CerrarFactura(this.IDFactura, this.Cliente));
                reporte.SetParameterValue("pEmpleado", _Sesion.Informacion.Empleado);
                reporte.SetParameterValue("pNumFactura", (int.Parse(IDFactura) + 100).ToString());
                reporte.SetParameterValue("pCliente", this.Cliente);
                reporte.SetParameterValue("pFecha", fecha);
                /*ReporteFactura aux = reporte;
                reporte.PrintOptions.PaperSize = aux.PrintOptions.PaperSize;
                reporte.PrintToPrinter(1, false, 0, 50);*/
                crvFactura.ReportSource = reporte;                
                
            }
            catch (Exception e)
            {
                MessageBox.Show("Excepcion reporte: "+e.ToString(), "Excepcion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        private void VistaReporteFactura_Load(object sender, EventArgs e)
        {
            GenerarReporte();
        }
    }
}
