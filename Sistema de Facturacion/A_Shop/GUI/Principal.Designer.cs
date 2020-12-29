namespace General.GUI
{
    partial class Principal
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Principal));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.herramientasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pruebaDeConectorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.personalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pclUsuarios = new System.Windows.Forms.ToolStripMenuItem();
            this.pclEmpleados = new System.Windows.Forms.ToolStripMenuItem();
            this.pclCredenciales = new System.Windows.Forms.ToolStripMenuItem();
            this.almacenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pclProductos = new System.Windows.Forms.ToolStripMenuItem();
            this.pclCategorias = new System.Windows.Forms.ToolStripMenuItem();
            this.pclProveedores = new System.Windows.Forms.ToolStripMenuItem();
            this.pclReportes = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteDeProductosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reporteDeCategoriasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pclPermisos = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRespaldo = new System.Windows.Forms.ToolStripMenuItem();
            this.pclFacturacion = new System.Windows.Forms.ToolStripMenuItem();
            this.pclRol = new System.Windows.Forms.ToolStripMenuItem();
            this.pclVentas = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCerrarSesion = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.lblPA = new System.Windows.Forms.ToolStripLabel();
            this.lblProximosAgotar = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.lblA = new System.Windows.Forms.ToolStripLabel();
            this.lblAgotados = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.lblPV = new System.Windows.Forms.ToolStripLabel();
            this.lblProximosVencer = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.lblV = new System.Windows.Forms.ToolStripLabel();
            this.lblVencidos = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblUsuario = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblRol = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblServidor = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.White;
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.herramientasToolStripMenuItem,
            this.personalToolStripMenuItem,
            this.almacenToolStripMenuItem,
            this.pclReportes,
            this.pclPermisos,
            this.btnRespaldo,
            this.pclFacturacion,
            this.pclRol,
            this.pclVentas,
            this.btnCerrarSesion});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip.Size = new System.Drawing.Size(1709, 38);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // herramientasToolStripMenuItem
            // 
            this.herramientasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pruebaDeConectorToolStripMenuItem});
            this.herramientasToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.herramientasToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("herramientasToolStripMenuItem.Image")));
            this.herramientasToolStripMenuItem.Name = "herramientasToolStripMenuItem";
            this.herramientasToolStripMenuItem.Size = new System.Drawing.Size(154, 34);
            this.herramientasToolStripMenuItem.Text = "Herramientas";
            this.herramientasToolStripMenuItem.Visible = false;
            // 
            // pruebaDeConectorToolStripMenuItem
            // 
            this.pruebaDeConectorToolStripMenuItem.Name = "pruebaDeConectorToolStripMenuItem";
            this.pruebaDeConectorToolStripMenuItem.Size = new System.Drawing.Size(236, 28);
            this.pruebaDeConectorToolStripMenuItem.Text = "Prueba de conector";
            // 
            // personalToolStripMenuItem
            // 
            this.personalToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pclUsuarios,
            this.pclEmpleados,
            this.pclCredenciales});
            this.personalToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.personalToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("personalToolStripMenuItem.Image")));
            this.personalToolStripMenuItem.Name = "personalToolStripMenuItem";
            this.personalToolStripMenuItem.Size = new System.Drawing.Size(135, 34);
            this.personalToolStripMenuItem.Text = "PERSONAL";
            // 
            // pclUsuarios
            // 
            this.pclUsuarios.Name = "pclUsuarios";
            this.pclUsuarios.Size = new System.Drawing.Size(248, 28);
            this.pclUsuarios.Text = "Usuarios";
            this.pclUsuarios.Visible = false;
            this.pclUsuarios.Click += new System.EventHandler(this.vistaUsuariosToolStripMenuItem_Click);
            // 
            // pclEmpleados
            // 
            this.pclEmpleados.Name = "pclEmpleados";
            this.pclEmpleados.Size = new System.Drawing.Size(248, 28);
            this.pclEmpleados.Text = "Empleados";
            this.pclEmpleados.Visible = false;
            this.pclEmpleados.Click += new System.EventHandler(this.vistaEmpleadosToolStripMenuItem_Click);
            // 
            // pclCredenciales
            // 
            this.pclCredenciales.Name = "pclCredenciales";
            this.pclCredenciales.Size = new System.Drawing.Size(248, 28);
            this.pclCredenciales.Text = "Cambiar credenciales";
            this.pclCredenciales.Visible = false;
            this.pclCredenciales.Click += new System.EventHandler(this.cambiarCredencialesToolStripMenuItem_Click);
            // 
            // almacenToolStripMenuItem
            // 
            this.almacenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pclProductos,
            this.pclCategorias,
            this.pclProveedores});
            this.almacenToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.almacenToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("almacenToolStripMenuItem.Image")));
            this.almacenToolStripMenuItem.Name = "almacenToolStripMenuItem";
            this.almacenToolStripMenuItem.Size = new System.Drawing.Size(130, 34);
            this.almacenToolStripMenuItem.Text = "ALMACEN";
            // 
            // pclProductos
            // 
            this.pclProductos.Name = "pclProductos";
            this.pclProductos.Size = new System.Drawing.Size(180, 28);
            this.pclProductos.Text = "Productos";
            this.pclProductos.Visible = false;
            this.pclProductos.Click += new System.EventHandler(this.productosToolStripMenuItem_Click);
            // 
            // pclCategorias
            // 
            this.pclCategorias.Name = "pclCategorias";
            this.pclCategorias.Size = new System.Drawing.Size(180, 28);
            this.pclCategorias.Text = "Categorias";
            this.pclCategorias.Visible = false;
            this.pclCategorias.Click += new System.EventHandler(this.categoriasToolStripMenuItem_Click);
            // 
            // pclProveedores
            // 
            this.pclProveedores.Name = "pclProveedores";
            this.pclProveedores.Size = new System.Drawing.Size(180, 28);
            this.pclProveedores.Text = "Proveedores";
            this.pclProveedores.Visible = false;
            this.pclProveedores.Click += new System.EventHandler(this.proveedoresToolStripMenuItem_Click);
            // 
            // pclReportes
            // 
            this.pclReportes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reporteDeProductosToolStripMenuItem,
            this.reporteDeCategoriasToolStripMenuItem});
            this.pclReportes.Image = ((System.Drawing.Image)(resources.GetObject("pclReportes.Image")));
            this.pclReportes.Name = "pclReportes";
            this.pclReportes.Size = new System.Drawing.Size(119, 34);
            this.pclReportes.Text = "REPORTES";
            this.pclReportes.Visible = false;
            // 
            // reporteDeProductosToolStripMenuItem
            // 
            this.reporteDeProductosToolStripMenuItem.Name = "reporteDeProductosToolStripMenuItem";
            this.reporteDeProductosToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
            this.reporteDeProductosToolStripMenuItem.Text = "Reporte de productos";
            this.reporteDeProductosToolStripMenuItem.Click += new System.EventHandler(this.reporteDeProductosToolStripMenuItem_Click);
            // 
            // reporteDeCategoriasToolStripMenuItem
            // 
            this.reporteDeCategoriasToolStripMenuItem.Name = "reporteDeCategoriasToolStripMenuItem";
            this.reporteDeCategoriasToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
            this.reporteDeCategoriasToolStripMenuItem.Text = "Reporte de categorias";
            this.reporteDeCategoriasToolStripMenuItem.Click += new System.EventHandler(this.reporteDeCategoriasToolStripMenuItem_Click);
            // 
            // pclPermisos
            // 
            this.pclPermisos.Image = ((System.Drawing.Image)(resources.GetObject("pclPermisos.Image")));
            this.pclPermisos.Name = "pclPermisos";
            this.pclPermisos.Size = new System.Drawing.Size(120, 34);
            this.pclPermisos.Text = "PERMISOS";
            this.pclPermisos.Visible = false;
            this.pclPermisos.Click += new System.EventHandler(this.rOLESToolStripMenuItem_Click);
            // 
            // btnRespaldo
            // 
            this.btnRespaldo.Image = ((System.Drawing.Image)(resources.GetObject("btnRespaldo.Image")));
            this.btnRespaldo.Name = "btnRespaldo";
            this.btnRespaldo.Size = new System.Drawing.Size(130, 34);
            this.btnRespaldo.Text = "BACKUP DB";
            this.btnRespaldo.Visible = false;
            this.btnRespaldo.Click += new System.EventHandler(this.bACKUPDBToolStripMenuItem_Click);
            // 
            // pclFacturacion
            // 
            this.pclFacturacion.Image = ((System.Drawing.Image)(resources.GetObject("pclFacturacion.Image")));
            this.pclFacturacion.Name = "pclFacturacion";
            this.pclFacturacion.Size = new System.Drawing.Size(148, 34);
            this.pclFacturacion.Text = "FACTURACION";
            this.pclFacturacion.Visible = false;
            this.pclFacturacion.Click += new System.EventHandler(this.pclFacturacion_Click);
            // 
            // pclRol
            // 
            this.pclRol.Image = ((System.Drawing.Image)(resources.GetObject("pclRol.Image")));
            this.pclRol.Name = "pclRol";
            this.pclRol.Size = new System.Drawing.Size(94, 34);
            this.pclRol.Text = "ROLES";
            this.pclRol.Visible = false;
            this.pclRol.Click += new System.EventHandler(this.pclRol_Click);
            // 
            // pclVentas
            // 
            this.pclVentas.Image = ((System.Drawing.Image)(resources.GetObject("pclVentas.Image")));
            this.pclVentas.Name = "pclVentas";
            this.pclVentas.Size = new System.Drawing.Size(104, 34);
            this.pclVentas.Text = "VENTAS";
            this.pclVentas.Visible = false;
            this.pclVentas.Click += new System.EventHandler(this.vENTASToolStripMenuItem_Click);
            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrarSesion.Image")));
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(159, 34);
            this.btnCerrarSesion.Text = "CERRAR SESION";
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnCerrarSesion_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.BackColor = System.Drawing.Color.White;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblPA,
            this.lblProximosAgotar,
            this.toolStripSeparator1,
            this.lblA,
            this.lblAgotados,
            this.toolStripSeparator2,
            this.lblPV,
            this.lblProximosVencer,
            this.toolStripSeparator3,
            this.lblV,
            this.lblVencidos,
            this.toolStripSeparator4});
            this.toolStrip.Location = new System.Drawing.Point(0, 38);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(1709, 26);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "ToolStrip";
            // 
            // lblPA
            // 
            this.lblPA.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPA.Name = "lblPA";
            this.lblPA.Size = new System.Drawing.Size(139, 23);
            this.lblPA.Text = "Proximos agotar:";
            this.lblPA.Visible = false;
            this.lblPA.Click += new System.EventHandler(this.lblPA_Click);
            // 
            // lblProximosAgotar
            // 
            this.lblProximosAgotar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProximosAgotar.Name = "lblProximosAgotar";
            this.lblProximosAgotar.Size = new System.Drawing.Size(20, 23);
            this.lblProximosAgotar.Text = "0";
            this.lblProximosAgotar.Visible = false;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 26);
            // 
            // lblA
            // 
            this.lblA.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblA.Name = "lblA";
            this.lblA.Size = new System.Drawing.Size(87, 23);
            this.lblA.Text = "Agotados:";
            this.lblA.Visible = false;
            this.lblA.Click += new System.EventHandler(this.lblA_Click);
            // 
            // lblAgotados
            // 
            this.lblAgotados.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAgotados.Name = "lblAgotados";
            this.lblAgotados.Size = new System.Drawing.Size(20, 23);
            this.lblAgotados.Text = "0";
            this.lblAgotados.Visible = false;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 26);
            // 
            // lblPV
            // 
            this.lblPV.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPV.Name = "lblPV";
            this.lblPV.Size = new System.Drawing.Size(139, 23);
            this.lblPV.Text = "Proximos vencer:";
            this.lblPV.Visible = false;
            this.lblPV.Click += new System.EventHandler(this.lblPV_Click);
            // 
            // lblProximosVencer
            // 
            this.lblProximosVencer.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProximosVencer.Name = "lblProximosVencer";
            this.lblProximosVencer.Size = new System.Drawing.Size(20, 23);
            this.lblProximosVencer.Text = "0";
            this.lblProximosVencer.Visible = false;
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 26);
            // 
            // lblV
            // 
            this.lblV.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblV.Name = "lblV";
            this.lblV.Size = new System.Drawing.Size(82, 23);
            this.lblV.Text = "Vencidos:";
            this.lblV.Visible = false;
            this.lblV.Click += new System.EventHandler(this.lblV_Click);
            // 
            // lblVencidos
            // 
            this.lblVencidos.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVencidos.Name = "lblVencidos";
            this.lblVencidos.Size = new System.Drawing.Size(20, 23);
            this.lblVencidos.Text = "0";
            this.lblVencidos.Visible = false;
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 26);
            // 
            // statusStrip
            // 
            this.statusStrip.AutoSize = false;
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblUsuario,
            this.lblRol,
            this.lblServidor});
            this.statusStrip.Location = new System.Drawing.Point(0, 517);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip.Size = new System.Drawing.Size(1709, 41);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = false;
            this.lblUsuario.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.lblUsuario.Image = ((System.Drawing.Image)(resources.GetObject("lblUsuario.Image")));
            this.lblUsuario.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(150, 36);
            this.lblUsuario.Text = "Usuario";
            this.lblUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRol
            // 
            this.lblRol.AutoSize = false;
            this.lblRol.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.lblRol.Image = ((System.Drawing.Image)(resources.GetObject("lblRol.Image")));
            this.lblRol.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblRol.Name = "lblRol";
            this.lblRol.Size = new System.Drawing.Size(150, 36);
            this.lblRol.Text = "Rol";
            this.lblRol.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblServidor
            // 
            this.lblServidor.AutoSize = false;
            this.lblServidor.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.lblServidor.Image = ((System.Drawing.Image)(resources.GetObject("lblServidor.Image")));
            this.lblServidor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblServidor.Name = "lblServidor";
            this.lblServidor.Size = new System.Drawing.Size(150, 36);
            this.lblServidor.Text = "Servidor";
            this.lblServidor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // timer1
            // 
            this.timer1.Interval = 300000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1709, 558);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Principal";
            this.Text = "Principal";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Principal_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel lblUsuario;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem herramientasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pruebaDeConectorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem personalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem almacenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pclUsuarios;
        private System.Windows.Forms.ToolStripMenuItem pclEmpleados;
        private System.Windows.Forms.ToolStripMenuItem pclProductos;
        private System.Windows.Forms.ToolStripMenuItem pclCategorias;
        private System.Windows.Forms.ToolStripMenuItem pclProveedores;
        private System.Windows.Forms.ToolStripMenuItem pclCredenciales;
        private System.Windows.Forms.ToolStripStatusLabel lblRol;
        private System.Windows.Forms.ToolStripStatusLabel lblServidor;
        private System.Windows.Forms.ToolStripMenuItem btnCerrarSesion;
        private System.Windows.Forms.ToolStripMenuItem btnRespaldo;
        private System.Windows.Forms.ToolStripLabel lblPA;
        private System.Windows.Forms.ToolStripLabel lblProximosAgotar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel lblA;
        private System.Windows.Forms.ToolStripLabel lblAgotados;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel lblPV;
        private System.Windows.Forms.ToolStripLabel lblProximosVencer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel lblV;
        private System.Windows.Forms.ToolStripLabel lblVencidos;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem pclPermisos;
        private System.Windows.Forms.ToolStripMenuItem pclFacturacion;
        private System.Windows.Forms.ToolStripMenuItem pclRol;
        private System.Windows.Forms.ToolStripMenuItem pclReportes;
        private System.Windows.Forms.ToolStripMenuItem pclVentas;
        private System.Windows.Forms.ToolStripMenuItem reporteDeProductosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reporteDeCategoriasToolStripMenuItem;
    }
}



