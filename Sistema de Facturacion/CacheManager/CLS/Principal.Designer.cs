namespace A_Shop.GUI
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
            this.añadirEmpleadosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.añadirUsuariosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vistaUsuariosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.almacenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.agregarCategoriasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.agregarProveedoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.agregarProductosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblEstado = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.lblUsuario = new System.Windows.Forms.Label();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.White;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.herramientasToolStripMenuItem,
            this.personalToolStripMenuItem,
            this.almacenToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(763, 25);
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
            this.herramientasToolStripMenuItem.Size = new System.Drawing.Size(114, 21);
            this.herramientasToolStripMenuItem.Text = "Herramientas";
            this.herramientasToolStripMenuItem.Click += new System.EventHandler(this.herramientasToolStripMenuItem_Click);
            // 
            // pruebaDeConectorToolStripMenuItem
            // 
            this.pruebaDeConectorToolStripMenuItem.Name = "pruebaDeConectorToolStripMenuItem";
            this.pruebaDeConectorToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.pruebaDeConectorToolStripMenuItem.Text = "Prueba de conector";
            this.pruebaDeConectorToolStripMenuItem.Click += new System.EventHandler(this.pruebaDeConectorToolStripMenuItem_Click);
            // 
            // personalToolStripMenuItem
            // 
            this.personalToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.añadirEmpleadosToolStripMenuItem,
            this.añadirUsuariosToolStripMenuItem,
            this.vistaUsuariosToolStripMenuItem});
            this.personalToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.personalToolStripMenuItem.Name = "personalToolStripMenuItem";
            this.personalToolStripMenuItem.Size = new System.Drawing.Size(70, 21);
            this.personalToolStripMenuItem.Text = "Personal";
            // 
            // añadirEmpleadosToolStripMenuItem
            // 
            this.añadirEmpleadosToolStripMenuItem.Name = "añadirEmpleadosToolStripMenuItem";
            this.añadirEmpleadosToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.añadirEmpleadosToolStripMenuItem.Text = "Añadir empleados";
            this.añadirEmpleadosToolStripMenuItem.Click += new System.EventHandler(this.añadirEmpleadosToolStripMenuItem_Click);
            // 
            // añadirUsuariosToolStripMenuItem
            // 
            this.añadirUsuariosToolStripMenuItem.Name = "añadirUsuariosToolStripMenuItem";
            this.añadirUsuariosToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.añadirUsuariosToolStripMenuItem.Text = "Añadir Usuarios";
            this.añadirUsuariosToolStripMenuItem.Click += new System.EventHandler(this.añadirUsuariosToolStripMenuItem_Click);
            // 
            // vistaUsuariosToolStripMenuItem
            // 
            this.vistaUsuariosToolStripMenuItem.Name = "vistaUsuariosToolStripMenuItem";
            this.vistaUsuariosToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.vistaUsuariosToolStripMenuItem.Text = "Vista usuarios";
            this.vistaUsuariosToolStripMenuItem.Click += new System.EventHandler(this.vistaUsuariosToolStripMenuItem_Click);
            // 
            // almacenToolStripMenuItem
            // 
            this.almacenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.agregarCategoriasToolStripMenuItem,
            this.agregarProveedoresToolStripMenuItem,
            this.agregarProductosToolStripMenuItem});
            this.almacenToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.almacenToolStripMenuItem.Name = "almacenToolStripMenuItem";
            this.almacenToolStripMenuItem.Size = new System.Drawing.Size(69, 21);
            this.almacenToolStripMenuItem.Text = "Almacen";
            // 
            // agregarCategoriasToolStripMenuItem
            // 
            this.agregarCategoriasToolStripMenuItem.Name = "agregarCategoriasToolStripMenuItem";
            this.agregarCategoriasToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.agregarCategoriasToolStripMenuItem.Text = "Agregar categorias";
            this.agregarCategoriasToolStripMenuItem.Click += new System.EventHandler(this.agregarCategoriasToolStripMenuItem_Click);
            // 
            // agregarProveedoresToolStripMenuItem
            // 
            this.agregarProveedoresToolStripMenuItem.Name = "agregarProveedoresToolStripMenuItem";
            this.agregarProveedoresToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.agregarProveedoresToolStripMenuItem.Text = "Agregar Proveedores";
            this.agregarProveedoresToolStripMenuItem.Click += new System.EventHandler(this.agregarProveedoresToolStripMenuItem_Click);
            // 
            // agregarProductosToolStripMenuItem
            // 
            this.agregarProductosToolStripMenuItem.Name = "agregarProductosToolStripMenuItem";
            this.agregarProductosToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
            this.agregarProductosToolStripMenuItem.Text = "Agregar productos";
            this.agregarProductosToolStripMenuItem.Click += new System.EventHandler(this.agregarProductosToolStripMenuItem_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.BackColor = System.Drawing.Color.White;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.toolStripSeparator2});
            this.toolStrip.Location = new System.Drawing.Point(0, 25);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(763, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "ToolStrip";
            this.toolStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip_ItemClicked);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblEstado});
            this.statusStrip.Location = new System.Drawing.Point(0, 431);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(763, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // lblEstado
            // 
            this.lblEstado.Image = ((System.Drawing.Image)(resources.GetObject("lblEstado.Image")));
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(58, 17);
            this.lblEstado.Text = "Estado";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.Location = new System.Drawing.Point(426, 9);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(55, 16);
            this.lblUsuario.TabIndex = 4;
            this.lblUsuario.Text = "Usuario";
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(763, 453);
            this.Controls.Add(this.lblUsuario);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
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
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripStatusLabel lblEstado;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem herramientasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pruebaDeConectorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem personalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem añadirEmpleadosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem añadirUsuariosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem almacenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem agregarProductosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem agregarCategoriasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem agregarProveedoresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vistaUsuariosToolStripMenuItem;
        private System.Windows.Forms.Label lblUsuario;
    }
}



