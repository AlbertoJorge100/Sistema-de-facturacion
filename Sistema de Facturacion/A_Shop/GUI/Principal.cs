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
using CacheManager.CLS;
using General.GUI;
using Reportes.GUI;
namespace General.GUI
{
    public partial class Principal : Form
    {
        private Boolean Temporizador;
        private Sesion _Sesion = Sesion.Instancia;
        //private int childFormNumber = 0;
        private Boolean SesionLogin;
        public Boolean _SesionLogin { get { return this.SesionLogin; } }
        
        //Prestado de login ------------------------------- Despues de testear borrar
        private void variablesSesion()
        {//metodo para aplicar variables de sesion
            if (_Sesion == null)
            {
                _Sesion = Sesion.Instancia;
            }
            /*_Sesion.Informacion.IDUsuario = "64";
            _Sesion.Informacion.Usuario = "Admin";
            _Sesion.Informacion.IDRol = "1";
            _Sesion.Informacion.Rol = "Administrador";
            _Sesion.Informacion.Contrasena = "4bdbc215d8dc3c571e802a69bced0c3071cc4a1f129ad97e15b357018aac6cd4";
            _Sesion.Informacion.IDEmpleado = "27";
            _Sesion.Informacion.Empleado = "Jorge Alberto Perez Nolasco";*/
        }
        private void CargarNotificaciones()
        {
            try
            {
                DataTable notificaciones = Cache.ConsultaNotificaciones();
                if (notificaciones.Rows.Count == 1)
                {
                    DataRow linea = notificaciones.Rows[0];
                    lblProximosAgotar.ForeColor = Color.Green;
                    lblAgotados.ForeColor = Color.Green;
                    lblProximosVencer.ForeColor = Color.Green;
                    lblVencidos.ForeColor = Color.Green;
                    String proxagotar, agotados, proxvencer, vencidos;
                    _Sesion.Informacion.ProximosAgotar = proxagotar = linea[0].ToString();
                    _Sesion.Informacion.Agotados = agotados = linea[1].ToString();
                    _Sesion.Informacion.ProximosVencer = proxvencer = linea[2].ToString();
                    _Sesion.Informacion.Vencidos = vencidos = linea[3].ToString();                    
                    if (!proxagotar.Equals("0"))
                    {
                        lblProximosAgotar.ForeColor = Color.Red;                     
                    }                                                            
                    if (!agotados.Equals("0"))
                    {
                        lblAgotados.ForeColor = Color.Red;                        
                    }                    
                    if (!proxvencer.Equals("0"))
                    {
                        lblProximosVencer.ForeColor = Color.Red;                        
                    }                    
                    if (!vencidos.Equals("0"))
                    {
                        lblVencidos.ForeColor = Color.Red;                        
                    }
                    lblProximosAgotar.Text = proxagotar;
                    lblAgotados.Text = agotados;
                    lblProximosVencer.Text = proxvencer;
                    lblVencidos.Text = vencidos;
                }                
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al consultar notificaciones, porfavor contacte con el desarrollador: " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public Principal()
        {
            InitializeComponent();      
        }

        private void pruebaDeConectorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PruebaConector f = new PruebaConector();
            f.ShowDialog();            
        }

        private void añadirUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EdicionUsuario f = new EdicionUsuario();
            f.ShowDialog();
        }

        private void datosSesion() {
            lblUsuario.Text = _Sesion.Informacion.Usuario.ToUpper();
            lblRol.Text = _Sesion.Informacion.Rol.ToUpper();
            lblServidor.Text = _Sesion.Informacion.Servidor.ToUpper();
        }

        private void AsignarAcciones()
        {
            try
            {
                DataTable lista = _Sesion.Informacion.Permisos();
                foreach (DataRow fila in lista.Rows)
                {
                    switch (int.Parse(fila["IDOpcion"].ToString()))
                    {
                        case 1:
                        {//EMPLEADOS
                            pclEmpleados.Visible = true;
                            break;
                        }
                        case 2:
                        {//ROLES
                            pclRol.Visible = true;
                            break;
                        }
                        case 3:
                        {//PERMISOS
                            pclPermisos.Visible = true;
                            break;
                        }
                        case 4:
                        {//USUARIOS
                            pclUsuarios.Visible = true;
                            break;
                        }
                        case 5:
                        {//PRODUCTOS
                            pclProductos.Visible = true;
                            break;
                        }
                        case 6:
                        {//CATEGORIAS
                            pclCategorias.Visible = true;
                            break;
                        }
                        case 7:
                        {//PROVEEDORES
                            pclProveedores.Visible = true;
                            break;
                        }
                        case 8:
                        {//MODIFICACION DE USUARIOS
                            pclCredenciales.Visible = true;
                            break;
                        }
                        case 9:
                        {//NOTIIFICACIONES
                            lblPA.Visible = true;
                            lblA.Visible = true;
                            lblPV.Visible = true;
                            lblV.Visible = true;
                            lblProximosAgotar.Visible = true;
                            lblAgotados.Visible = true;
                            lblProximosVencer.Visible = true;
                            lblVencidos.Visible = true;
                            this.CargarNotificaciones();
                            timer1.Start();
                            this.Temporizador = true;
                            break;
                        }
                        case 10:
                        {
                            btnRespaldo.Visible = true;
                            break;
                        }
                        case 11:
                        {
                            pclFacturacion.Visible = true;
                            break;
                        }
                        case 12:
                        {
                            pclReportes.Visible = true;
                            break;
                        }
                        case 13:
                        {
                            pclVentas.Visible = true;
                            break;
                        }                     
                        default:
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Excepcion: " + e.ToString(),"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }


        private void vistaUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {//USUARIOS            
            try
            {//IDOpcion 4 Usuarios
                if (_Sesion.Informacion.VerificarPermisos(4))
                {
                    GestionUsuarios f = new GestionUsuarios();
                    f.MdiParent = this;
                    f.Show();
                }
                else
                {
                    MessageBox.Show("No tiene permiso para esta accion", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception e2)
            {
                Console.WriteLine("Excepcion en principal: " + e2.ToString());
            }
        }


        private void vistaEmpleadosToolStripMenuItem_Click(object sender, EventArgs e)
        {//EMPLEADOS
            try
            {//IDOPCION = 1
                if (_Sesion.Informacion.VerificarPermisos(1))
                {
                    GestionEmpleados f = new General.GUI.GestionEmpleados();
                    f.MdiParent = this;
                    f.Dock = DockStyle.Fill;
                    f.Show();
                }
                else
                {
                    MessageBox.Show("No tiene permiso para esta accion", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception e2)
            {
                Console.WriteLine("Excepcion en principal: " + e2.ToString());
            }
        }

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {//Productos            
            try
            {//IDOpcion 5 Productos
                if (_Sesion.Informacion.VerificarPermisos(5))
                {
                    GestionProductos_ f = new GestionProductos_();
                    f.Dock = DockStyle.Fill;
                    f.MdiParent = this;
                    f.Show();
                }
                else
                {
                    MessageBox.Show("No tiene permiso para esta accion", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception e2)
            {
                Console.WriteLine("Excepcion en principal: " + e2.ToString());
            }
        }


        private void categoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {//CATEGORIAS
            try
            {//IDOpcion 6 categorias
                if (_Sesion.Informacion.VerificarPermisos(6))
                {
                    GestionCategorias f = new GestionCategorias();
                    f.MdiParent = this;
                    f.Show();
                }
                else
                {
                    MessageBox.Show("No tiene permiso para esta accion", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception e2)
            {
                Console.WriteLine("Excepcion en principal: " + e2.ToString());
            }
        }


        private void proveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {//PROVEEDORES
            try
            {//IDOpcion 7 Proveedores
                if (_Sesion.Informacion.VerificarPermisos(7))
                {
                    GestionProveedores f = new GestionProveedores();
                    f.MdiParent = this;
                    f.Show();
                }
                else
                {
                    MessageBox.Show("No tiene permiso para esta accion", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception e2)
            {
                Console.WriteLine("Excepcion en principal: " + e2.ToString());
            }
        }

        private void cambiarCredencialesToolStripMenuItem_Click(object sender, EventArgs e)
        {///-----------------------------------------------------------------------------------
            try
            {//IDOpcion 7 Proveedores
                if (_Sesion.Informacion.VerificarPermisos(8))
                {
                    EdicionCredenciales f = new EdicionCredenciales(_Sesion.Informacion.Usuario,_Sesion.Informacion.Contrasena,_Sesion.Informacion.IDUsuario);
                    f.MdiParent = this;
                    f.Show();
                }
                else
                {
                    MessageBox.Show("No tiene permiso para esta accion", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception e2)
            {
                Console.WriteLine("Excepcion en principal: " + e2.ToString());
            }
        }

        private void btnSesion_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cerra sesion?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.SesionLogin = true;
                this.Close();
            }            
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Desea cerra sesion?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (Temporizador)
                {
                    timer1.Stop();
                }
                this.SesionLogin = true;
                this.Close();
            }
        }

        private void bACKUPDBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {//IDOpcion 10 Proveedores
                if (_Sesion.Informacion.VerificarPermisos(10))
                {
                    EdicionRespaldo f = new EdicionRespaldo(_Sesion.Informacion.Contrasena);
                    f.MdiParent = this;
                    f.Show();
                }
                else
                {
                    MessageBox.Show("No tiene permiso para esta accion", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception e2)
            {
                Console.WriteLine("Excepcion en principal: " + e2.ToString());
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //this.CargarNotificaciones();------------------------------Modificar
        }

        private void rOLESToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            try
            {//IDOpcion 10 Proveedores
                if (_Sesion.Informacion.VerificarPermisos(3))
                {
                    GestionPermisos f = new GestionPermisos();
                    f.MdiParent = this;
                    f.Show();
                }
                else
                {
                    MessageBox.Show("No tiene permiso para esta accion", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception e2)
            {
                Console.WriteLine("Excepcion en principal: " + e2.ToString());
            }
        }

        private void pclFacturacion_Click(object sender, EventArgs e)
        {
            try
            {//IDOpcion 10 Proveedores
                if (_Sesion.Informacion.VerificarPermisos(11))
                {
                    GestionFactura f = new GestionFactura(_Sesion.Informacion.IDEmpleado);
                    f.MdiParent = this;
                    f.Dock = DockStyle.Fill;
                    f.Show();
                }
                else
                {
                    MessageBox.Show("No tiene permiso para esta accion", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception e2)
            {
                Console.WriteLine("Excepcion en principal: " + e2.ToString());
            }
            
        }

        private void pclRol_Click(object sender, EventArgs e)
        {
            try
            {//IDOpcion 10 Proveedores
                if (_Sesion.Informacion.VerificarPermisos(2))
                {
                    GestionRoles f = new GestionRoles();
                    f.MdiParent = this;
                    f.Show();
                }
                else
                {
                    MessageBox.Show("No tiene permiso para esta accion", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception e2)
            {
                Console.WriteLine("Excepcion en principal: " + e2.ToString());
            }           
        }

            private void vENTASToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {//IDOpcion 10 Proveedores
                if (_Sesion.Informacion.VerificarPermisos(13))
                {
                    Ventas f = new Ventas();
                    f.MdiParent = this;
                    f.Dock = DockStyle.Fill;
                    f.Show();
                }
                else
                {
                    MessageBox.Show("No tiene permiso para esta accion", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception e2)
            {
                Console.WriteLine("Excepcion en principal: " + e2.ToString());
            }            
        }

          private void rEPORTESToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void Principal_Load(object sender, EventArgs e)
        {
            variablesSesion();
            this.AsignarAcciones();// -------ZZZ Modificaaaaaaaaaaaarrrr----------------                    
            //this.datosSesion();//----------------- Modificaaaaaaaaaaaaaarrrrrrrrrrrrrrr
            //this.SesionLogin = false;
            //this.Temporizador = false;
        }

        private void reporteDeProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {//IDOpcion 12 ProductosReportes
                if (_Sesion.Informacion.VerificarPermisos(12))
                {
                    VistaReporteProductos f = new VistaReporteProductos(1);
                    f.MdiParent = this;
                    f.Dock = DockStyle.Fill;
                    f.Show();
                }
                else
                {
                    MessageBox.Show("No tiene permiso para esta accion", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception e2)
            {
                Console.WriteLine("Excepcion en principal: " + e2.ToString());
            }
        }

        private void reporteDeCategoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {//IDOpcion 10 Proveedores
                if (_Sesion.Informacion.VerificarPermisos(12))
                {
                    VistaReporteProductos f = new VistaReporteProductos(2);
                    f.MdiParent = this;
                    f.Dock = DockStyle.Fill;
                    f.Show();
                }
                else
                {
                    MessageBox.Show("No tiene permiso para esta accion", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception e2)
            {
                Console.WriteLine("Excepcion en principal: " + e2.ToString());
            }            
        }
    }
}
