using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Reportes.REP;
using CacheManager.CLS;
using SessionManager.CLS;

namespace Reportes.GUI
{
    public partial class VistaReporteProductos : Form
    {
        private int Opcion = 0;
        Sesion _Sesion = Sesion.Instancia;
        private void GenerarReporte()
        {
            DataTable lista = Cache.ReporteProductos();
            
            try
            {
                if (lista.Rows.Count > 0)
                {
                    ReporteProductos reporte = new ReporteProductos();
                    reporte.SetDataSource(Cache.ReporteProductos());
                    reporte.SetParameterValue("pEmpleado", _Sesion.Informacion.Empleado);
                    reporte.SetParameterValue("pTotalProductos","Total de productos: "+ lista.Rows.Count.ToString());
                    reporte.SetParameterValue("pFecha", DateTime.Now.ToString());
                    crvProductos.ReportSource = reporte;
                }                
            }
            catch (Exception e)
            {
                MessageBox.Show("Excepcion reporte: " + e.ToString(), "Excepcion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void GenerarReporteCategoria()
        {
            DataTable lista = Cache.ConsultaCategorias("", 2);

            try
            {
                if (lista.Rows.Count > 0)
                {
                    ReporteCategorias reporte = new ReporteCategorias();
                    reporte.SetDataSource(lista);
                    reporte.SetParameterValue("pEmpleado", _Sesion.Informacion.Empleado);
                    reporte.SetParameterValue("pTotalProductos", "Total de categorias: "+lista.Rows.Count.ToString());
                    reporte.SetParameterValue("pFecha", DateTime.Now.ToString());
                    crvProductos.ReportSource = reporte;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Excepcion reporte: " + e.ToString(), "Excepcion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public VistaReporteProductos(int i)
        {
            InitializeComponent();
            this.Opcion = i;
        }

        private void VistaReporteProductos_Load(object sender, EventArgs e)
        {
            switch (this.Opcion)
            {
                case 1:
                    GenerarReporte();
                    break;
                case 2:
                    GenerarReporteCategoria();
                    break;
            }
            
        }
    }
}
