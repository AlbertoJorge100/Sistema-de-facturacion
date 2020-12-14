namespace General.GUI
{
    partial class GestionFactura
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GestionFactura));
            this.dtgProductos = new System.Windows.Forms.DataGridView();
            this.IDProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Marca = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descuento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Existencias = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Presentacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Alias = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtgSeleccionados = new System.Windows.Forms.DataGridView();
            this.IDProducto2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Producto2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Marca2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precio2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descuento2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.D_Total2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubTotal2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Presentacion2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblResultado = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.txbFiltro = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRefrescar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnFacturar = new System.Windows.Forms.Button();
            this.txbCantidad2 = new System.Windows.Forms.TextBox();
            this.lbl1 = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.txbCantidad = new System.Windows.Forms.TextBox();
            this.lblSubTotal = new System.Windows.Forms.Label();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.lblProductos = new System.Windows.Forms.Label();
            this.lbl4 = new System.Windows.Forms.Label();
            this.lblDescuento = new System.Windows.Forms.Label();
            this.lbl33 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnCrearFactura = new System.Windows.Forms.Button();
            this.btnSeleccionar = new System.Windows.Forms.Button();
            this.lblFactura = new System.Windows.Forms.Label();
            this.txbCliente = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.lblCnt = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblItems = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtgProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgSeleccionados)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtgProductos
            // 
            this.dtgProductos.AllowUserToAddRows = false;
            this.dtgProductos.AllowUserToDeleteRows = false;
            this.dtgProductos.AllowUserToResizeColumns = false;
            this.dtgProductos.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.dtgProductos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dtgProductos.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dtgProductos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dtgProductos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.dtgProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgProductos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDProducto,
            this.Producto,
            this.Marca,
            this.Precio,
            this.Descuento,
            this.Existencias,
            this.Presentacion,
            this.Alias});
            this.dtgProductos.Dock = System.Windows.Forms.DockStyle.Top;
            this.dtgProductos.Location = new System.Drawing.Point(0, 31);
            this.dtgProductos.Margin = new System.Windows.Forms.Padding(4);
            this.dtgProductos.Name = "dtgProductos";
            this.dtgProductos.ReadOnly = true;
            this.dtgProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgProductos.Size = new System.Drawing.Size(1451, 382);
            this.dtgProductos.TabIndex = 0;
            // 
            // IDProducto
            // 
            this.IDProducto.DataPropertyName = "IDProducto";
            this.IDProducto.HeaderText = "IDProducto";
            this.IDProducto.Name = "IDProducto";
            this.IDProducto.ReadOnly = true;
            this.IDProducto.Visible = false;
            // 
            // Producto
            // 
            this.Producto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Producto.DataPropertyName = "Producto";
            this.Producto.FillWeight = 200F;
            this.Producto.HeaderText = "Producto";
            this.Producto.MinimumWidth = 150;
            this.Producto.Name = "Producto";
            this.Producto.ReadOnly = true;
            // 
            // Marca
            // 
            this.Marca.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Marca.DataPropertyName = "Marca";
            this.Marca.FillWeight = 200F;
            this.Marca.HeaderText = "Marca";
            this.Marca.MinimumWidth = 150;
            this.Marca.Name = "Marca";
            this.Marca.ReadOnly = true;
            // 
            // Precio
            // 
            this.Precio.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Precio.DataPropertyName = "Precio";
            this.Precio.FillWeight = 150F;
            this.Precio.HeaderText = "Precio";
            this.Precio.MinimumWidth = 100;
            this.Precio.Name = "Precio";
            this.Precio.ReadOnly = true;
            // 
            // Descuento
            // 
            this.Descuento.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Descuento.DataPropertyName = "Descuento";
            this.Descuento.FillWeight = 150F;
            this.Descuento.HeaderText = "Descuento";
            this.Descuento.MinimumWidth = 100;
            this.Descuento.Name = "Descuento";
            this.Descuento.ReadOnly = true;
            // 
            // Existencias
            // 
            this.Existencias.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Existencias.DataPropertyName = "Existencias";
            this.Existencias.FillWeight = 150F;
            this.Existencias.HeaderText = "Existencias";
            this.Existencias.MinimumWidth = 100;
            this.Existencias.Name = "Existencias";
            this.Existencias.ReadOnly = true;
            // 
            // Presentacion
            // 
            this.Presentacion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Presentacion.DataPropertyName = "Presentacion";
            this.Presentacion.FillWeight = 150F;
            this.Presentacion.HeaderText = "Presentacion";
            this.Presentacion.MinimumWidth = 100;
            this.Presentacion.Name = "Presentacion";
            this.Presentacion.ReadOnly = true;
            // 
            // Alias
            // 
            this.Alias.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Alias.DataPropertyName = "Alias";
            this.Alias.FillWeight = 150F;
            this.Alias.HeaderText = "Alias";
            this.Alias.MinimumWidth = 100;
            this.Alias.Name = "Alias";
            this.Alias.ReadOnly = true;
            // 
            // dtgSeleccionados
            // 
            this.dtgSeleccionados.AllowUserToAddRows = false;
            this.dtgSeleccionados.AllowUserToDeleteRows = false;
            this.dtgSeleccionados.AllowUserToResizeColumns = false;
            this.dtgSeleccionados.AllowUserToResizeRows = false;
            this.dtgSeleccionados.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dtgSeleccionados.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dtgSeleccionados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgSeleccionados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDProducto2,
            this.Cantidad2,
            this.Producto2,
            this.Marca2,
            this.Precio2,
            this.Descuento2,
            this.D_Total2,
            this.SubTotal2,
            this.Presentacion2});
            this.dtgSeleccionados.Dock = System.Windows.Forms.DockStyle.Right;
            this.dtgSeleccionados.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dtgSeleccionados.Location = new System.Drawing.Point(476, 413);
            this.dtgSeleccionados.Margin = new System.Windows.Forms.Padding(4);
            this.dtgSeleccionados.Name = "dtgSeleccionados";
            this.dtgSeleccionados.ReadOnly = true;
            this.dtgSeleccionados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgSeleccionados.Size = new System.Drawing.Size(975, 434);
            this.dtgSeleccionados.TabIndex = 6;
            // 
            // IDProducto2
            // 
            this.IDProducto2.DataPropertyName = "IDProducto2";
            this.IDProducto2.HeaderText = "IDProducto2";
            this.IDProducto2.Name = "IDProducto2";
            this.IDProducto2.ReadOnly = true;
            this.IDProducto2.Visible = false;
            // 
            // Cantidad2
            // 
            this.Cantidad2.DataPropertyName = "Cantidad2";
            this.Cantidad2.FillWeight = 50F;
            this.Cantidad2.HeaderText = "Cant";
            this.Cantidad2.Name = "Cantidad2";
            this.Cantidad2.ReadOnly = true;
            this.Cantidad2.Width = 50;
            // 
            // Producto2
            // 
            this.Producto2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Producto2.DataPropertyName = "Producto2";
            this.Producto2.FillWeight = 200F;
            this.Producto2.HeaderText = "Producto";
            this.Producto2.Name = "Producto2";
            this.Producto2.ReadOnly = true;
            // 
            // Marca2
            // 
            this.Marca2.DataPropertyName = "Marca2";
            this.Marca2.HeaderText = "Marca";
            this.Marca2.Name = "Marca2";
            this.Marca2.ReadOnly = true;
            // 
            // Precio2
            // 
            this.Precio2.DataPropertyName = "Precio2";
            this.Precio2.FillWeight = 50F;
            this.Precio2.HeaderText = "Precio";
            this.Precio2.Name = "Precio2";
            this.Precio2.ReadOnly = true;
            this.Precio2.Width = 50;
            // 
            // Descuento2
            // 
            this.Descuento2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Descuento2.DataPropertyName = "Descuento2";
            this.Descuento2.HeaderText = "Descuento";
            this.Descuento2.MinimumWidth = 50;
            this.Descuento2.Name = "Descuento2";
            this.Descuento2.ReadOnly = true;
            // 
            // D_Total2
            // 
            this.D_Total2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.D_Total2.DataPropertyName = "D_Total2";
            this.D_Total2.HeaderText = "D_Total";
            this.D_Total2.MinimumWidth = 50;
            this.D_Total2.Name = "D_Total2";
            this.D_Total2.ReadOnly = true;
            // 
            // SubTotal2
            // 
            this.SubTotal2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SubTotal2.DataPropertyName = "SubTotal2";
            this.SubTotal2.HeaderText = "SubTotal";
            this.SubTotal2.MinimumWidth = 50;
            this.SubTotal2.Name = "SubTotal2";
            this.SubTotal2.ReadOnly = true;
            // 
            // Presentacion2
            // 
            this.Presentacion2.DataPropertyName = "Presentacion2";
            this.Presentacion2.FillWeight = 150F;
            this.Presentacion2.HeaderText = "Presentacion";
            this.Presentacion2.MinimumWidth = 100;
            this.Presentacion2.Name = "Presentacion2";
            this.Presentacion2.ReadOnly = true;
            // 
            // lblResultado
            // 
            this.lblResultado.AutoSize = true;
            this.lblResultado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResultado.Location = new System.Drawing.Point(404, 22);
            this.lblResultado.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblResultado.Name = "lblResultado";
            this.lblResultado.Size = new System.Drawing.Size(0, 25);
            this.lblResultado.TabIndex = 13;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txbFiltro,
            this.toolStripLabel1,
            this.toolStripSeparator2,
            this.btnRefrescar,
            this.toolStripSeparator1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1451, 31);
            this.toolStrip1.TabIndex = 21;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // txbFiltro
            // 
            this.txbFiltro.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.txbFiltro.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txbFiltro.Name = "txbFiltro";
            this.txbFiltro.Size = new System.Drawing.Size(199, 31);
            this.txbFiltro.TextChanged += new System.EventHandler(this.txbFiltro_TextChanged);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripLabel1.Image")));
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(71, 28);
            this.toolStripLabel1.Text = "Filtrar";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // btnRefrescar
            // 
            this.btnRefrescar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnRefrescar.Image = ((System.Drawing.Image)(resources.GetObject("btnRefrescar.Image")));
            this.btnRefrescar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(98, 28);
            this.btnRefrescar.Text = "Refrescar";
            this.btnRefrescar.Click += new System.EventHandler(this.btnRefrescar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // btnFacturar
            // 
            this.btnFacturar.Location = new System.Drawing.Point(41, 209);
            this.btnFacturar.Margin = new System.Windows.Forms.Padding(4);
            this.btnFacturar.Name = "btnFacturar";
            this.btnFacturar.Size = new System.Drawing.Size(105, 101);
            this.btnFacturar.TabIndex = 14;
            this.btnFacturar.Text = "Facturar";
            this.btnFacturar.UseVisualStyleBackColor = true;
            this.btnFacturar.Click += new System.EventHandler(this.btnFacturar_Click);
            // 
            // txbCantidad2
            // 
            this.txbCantidad2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txbCantidad2.Location = new System.Drawing.Point(676, 229);
            this.txbCantidad2.Margin = new System.Windows.Forms.Padding(4);
            this.txbCantidad2.Name = "txbCantidad2";
            this.txbCantidad2.Size = new System.Drawing.Size(74, 27);
            this.txbCantidad2.TabIndex = 11;
            this.txbCantidad2.Text = "1";
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl1.Location = new System.Drawing.Point(182, 183);
            this.lbl1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(91, 24);
            this.lbl1.TabIndex = 15;
            this.lbl1.Text = "Subtotal:";
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl2.Location = new System.Drawing.Point(182, 215);
            this.lbl2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(122, 24);
            this.lbl2.TabIndex = 16;
            this.lbl2.Text = "Descuento: ";
            // 
            // txbCantidad
            // 
            this.txbCantidad.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txbCantidad.Location = new System.Drawing.Point(618, 136);
            this.txbCantidad.Margin = new System.Windows.Forms.Padding(4);
            this.txbCantidad.Name = "txbCantidad";
            this.txbCantidad.Size = new System.Drawing.Size(132, 27);
            this.txbCantidad.TabIndex = 9;
            this.txbCantidad.Text = "1";
            // 
            // lblSubTotal
            // 
            this.lblSubTotal.AutoSize = true;
            this.lblSubTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTotal.Location = new System.Drawing.Point(322, 185);
            this.lblSubTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubTotal.Name = "lblSubTotal";
            this.lblSubTotal.Size = new System.Drawing.Size(21, 24);
            this.lblSubTotal.TabIndex = 17;
            this.lblSubTotal.Text = "0";
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(463, 209);
            this.btnEliminar.Margin = new System.Windows.Forms.Padding(4);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(105, 53);
            this.btnEliminar.TabIndex = 8;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // lblProductos
            // 
            this.lblProductos.AutoSize = true;
            this.lblProductos.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductos.Location = new System.Drawing.Point(322, 247);
            this.lblProductos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProductos.Name = "lblProductos";
            this.lblProductos.Size = new System.Drawing.Size(21, 24);
            this.lblProductos.TabIndex = 18;
            this.lblProductos.Text = "0";
            // 
            // lbl4
            // 
            this.lbl4.AutoSize = true;
            this.lbl4.Font = new System.Drawing.Font("Microsoft Tai Le", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl4.Location = new System.Drawing.Point(178, 142);
            this.lbl4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl4.Name = "lbl4";
            this.lbl4.Size = new System.Drawing.Size(98, 39);
            this.lbl4.TabIndex = 7;
            this.lbl4.Text = "Total:";
            // 
            // lblDescuento
            // 
            this.lblDescuento.AutoSize = true;
            this.lblDescuento.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescuento.Location = new System.Drawing.Point(322, 215);
            this.lblDescuento.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescuento.Name = "lblDescuento";
            this.lblDescuento.Size = new System.Drawing.Size(21, 24);
            this.lblDescuento.TabIndex = 19;
            this.lblDescuento.Text = "0";
            // 
            // lbl33
            // 
            this.lbl33.AutoSize = true;
            this.lbl33.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl33.Location = new System.Drawing.Point(182, 247);
            this.lbl33.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl33.Name = "lbl33";
            this.lbl33.Size = new System.Drawing.Size(115, 24);
            this.lbl33.TabIndex = 4;
            this.lbl33.Text = "productos: ";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Tai Le", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(318, 142);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(34, 39);
            this.lblTotal.TabIndex = 20;
            this.lblTotal.Text = "0";
            // 
            // btnCrearFactura
            // 
            this.btnCrearFactura.Location = new System.Drawing.Point(41, 119);
            this.btnCrearFactura.Margin = new System.Windows.Forms.Padding(4);
            this.btnCrearFactura.Name = "btnCrearFactura";
            this.btnCrearFactura.Size = new System.Drawing.Size(105, 55);
            this.btnCrearFactura.TabIndex = 3;
            this.btnCrearFactura.Text = "Crear factura";
            this.btnCrearFactura.UseVisualStyleBackColor = true;
            this.btnCrearFactura.Click += new System.EventHandler(this.btnCrearFactura_Click);
            // 
            // btnSeleccionar
            // 
            this.btnSeleccionar.Location = new System.Drawing.Point(463, 117);
            this.btnSeleccionar.Margin = new System.Windows.Forms.Padding(4);
            this.btnSeleccionar.Name = "btnSeleccionar";
            this.btnSeleccionar.Size = new System.Drawing.Size(105, 57);
            this.btnSeleccionar.TabIndex = 5;
            this.btnSeleccionar.Text = "Seleccionar";
            this.btnSeleccionar.UseVisualStyleBackColor = true;
            this.btnSeleccionar.Click += new System.EventHandler(this.btnSeleccionar_Click);
            // 
            // lblFactura
            // 
            this.lblFactura.AutoSize = true;
            this.lblFactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFactura.Location = new System.Drawing.Point(36, 76);
            this.lblFactura.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFactura.Name = "lblFactura";
            this.lblFactura.Size = new System.Drawing.Size(223, 29);
            this.lblFactura.TabIndex = 21;
            this.lblFactura.Text = "Factura no creada";
            // 
            // txbCliente
            // 
            this.txbCliente.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txbCliente.Location = new System.Drawing.Point(463, 53);
            this.txbCliente.Name = "txbCliente";
            this.txbCliente.Size = new System.Drawing.Size(221, 27);
            this.txbCliente.TabIndex = 22;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.lblCnt);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lblItems);
            this.panel1.Controls.Add(this.txbCliente);
            this.panel1.Controls.Add(this.lblFactura);
            this.panel1.Controls.Add(this.btnSeleccionar);
            this.panel1.Controls.Add(this.btnCrearFactura);
            this.panel1.Controls.Add(this.lblTotal);
            this.panel1.Controls.Add(this.lbl33);
            this.panel1.Controls.Add(this.lblDescuento);
            this.panel1.Controls.Add(this.lbl4);
            this.panel1.Controls.Add(this.lblProductos);
            this.panel1.Controls.Add(this.btnEliminar);
            this.panel1.Controls.Add(this.lblSubTotal);
            this.panel1.Controls.Add(this.txbCantidad);
            this.panel1.Controls.Add(this.lbl2);
            this.panel1.Controls.Add(this.lbl1);
            this.panel1.Controls.Add(this.txbCantidad2);
            this.panel1.Controls.Add(this.btnFacturar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 413);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(768, 434);
            this.panel1.TabIndex = 23;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Arial", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(618, 229);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(41, 27);
            this.button1.TabIndex = 30;
            this.button1.Text = "-";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblCnt
            // 
            this.lblCnt.AutoSize = true;
            this.lblCnt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCnt.Location = new System.Drawing.Point(37, 20);
            this.lblCnt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCnt.Name = "lblCnt";
            this.lblCnt.Size = new System.Drawing.Size(120, 24);
            this.lblCnt.TabIndex = 29;
            this.lblCnt.Text = "0 productos";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(614, 203);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 24);
            this.label2.TabIndex = 28;
            this.label2.Text = "Cantidad:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(614, 108);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 24);
            this.label1.TabIndex = 27;
            this.label1.Text = "Cantidad:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(459, 20);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 24);
            this.label6.TabIndex = 26;
            this.label6.Text = "Cliente:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(182, 281);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 24);
            this.label4.TabIndex = 24;
            this.label4.Text = "Items: ";
            // 
            // lblItems
            // 
            this.lblItems.AutoSize = true;
            this.lblItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItems.Location = new System.Drawing.Point(322, 281);
            this.lblItems.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblItems.Name = "lblItems";
            this.lblItems.Size = new System.Drawing.Size(21, 24);
            this.lblItems.TabIndex = 25;
            this.lblItems.Text = "0";
            // 
            // GestionFactura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1451, 847);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblResultado);
            this.Controls.Add(this.dtgSeleccionados);
            this.Controls.Add(this.dtgProductos);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "GestionFactura";
            this.Text = "VistaProductos";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.VistaProductos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgSeleccionados)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgProductos;
        private System.Windows.Forms.DataGridView dtgSeleccionados;
        private System.Windows.Forms.Label lblResultado;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripTextBox txbFiltro;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton btnRefrescar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Button btnFacturar;
        private System.Windows.Forms.TextBox txbCantidad2;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.TextBox txbCantidad;
        private System.Windows.Forms.Label lblSubTotal;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Label lblProductos;
        private System.Windows.Forms.Label lbl4;
        private System.Windows.Forms.Label lblDescuento;
        private System.Windows.Forms.Label lbl33;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnCrearFactura;
        private System.Windows.Forms.Button btnSeleccionar;
        private System.Windows.Forms.Label lblFactura;
        private System.Windows.Forms.TextBox txbCliente;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblItems;
        private System.Windows.Forms.Label lblCnt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Producto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Marca;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descuento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Existencias;
        private System.Windows.Forms.DataGridViewTextBoxColumn Presentacion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Alias;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDProducto2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Producto2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Marca2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descuento2;
        private System.Windows.Forms.DataGridViewTextBoxColumn D_Total2;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubTotal2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Presentacion2;
        private System.Windows.Forms.Button button1;
    }
}