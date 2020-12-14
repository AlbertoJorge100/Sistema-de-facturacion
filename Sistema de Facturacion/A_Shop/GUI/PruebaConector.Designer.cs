namespace General.GUI
{
    partial class PruebaConector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PruebaConector));
            this.dtgDatos = new System.Windows.Forms.DataGridView();
            this.txbConsulta = new System.Windows.Forms.TextBox();
            this.Consulta = new System.Windows.Forms.Label();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.btnEjecutar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txbSentencia = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDatos)).BeginInit();
            this.SuspendLayout();
            // 
            // dtgDatos
            // 
            this.dtgDatos.BackgroundColor = System.Drawing.Color.LemonChiffon;
            this.dtgDatos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgDatos.Location = new System.Drawing.Point(364, 29);
            this.dtgDatos.Name = "dtgDatos";
            this.dtgDatos.Size = new System.Drawing.Size(377, 113);
            this.dtgDatos.TabIndex = 0;
            // 
            // txbConsulta
            // 
            this.txbConsulta.BackColor = System.Drawing.Color.LightCyan;
            this.txbConsulta.Location = new System.Drawing.Point(18, 29);
            this.txbConsulta.Multiline = true;
            this.txbConsulta.Name = "txbConsulta";
            this.txbConsulta.Size = new System.Drawing.Size(341, 113);
            this.txbConsulta.TabIndex = 1;
            this.txbConsulta.TextChanged += new System.EventHandler(this.txbConsulta_TextChanged);
            // 
            // Consulta
            // 
            this.Consulta.AutoSize = true;
            this.Consulta.BackColor = System.Drawing.Color.Transparent;
            this.Consulta.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Consulta.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Consulta.Location = new System.Drawing.Point(29, 9);
            this.Consulta.Name = "Consulta";
            this.Consulta.Size = new System.Drawing.Size(56, 16);
            this.Consulta.TabIndex = 2;
            this.Consulta.Text = "Consulta";
            // 
            // btnConsultar
            // 
            this.btnConsultar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnConsultar.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConsultar.Location = new System.Drawing.Point(274, 148);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(75, 23);
            this.btnConsultar.TabIndex = 3;
            this.btnConsultar.Text = "Consultar";
            this.btnConsultar.UseVisualStyleBackColor = false;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // btnEjecutar
            // 
            this.btnEjecutar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnEjecutar.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEjecutar.Location = new System.Drawing.Point(274, 326);
            this.btnEjecutar.Name = "btnEjecutar";
            this.btnEjecutar.Size = new System.Drawing.Size(75, 23);
            this.btnEjecutar.TabIndex = 6;
            this.btnEjecutar.Text = "Ejecutar";
            this.btnEjecutar.UseVisualStyleBackColor = false;
            this.btnEjecutar.Click += new System.EventHandler(this.btnEjecutar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(29, 187);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Sentencia";
            // 
            // txbSentencia
            // 
            this.txbSentencia.BackColor = System.Drawing.Color.LightCyan;
            this.txbSentencia.Location = new System.Drawing.Point(18, 207);
            this.txbSentencia.Multiline = true;
            this.txbSentencia.Name = "txbSentencia";
            this.txbSentencia.Size = new System.Drawing.Size(341, 113);
            this.txbSentencia.TabIndex = 4;
            this.txbSentencia.TextChanged += new System.EventHandler(this.txbSentencia_TextChanged);
            // 
            // PruebaConector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(767, 361);
            this.Controls.Add(this.btnEjecutar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txbSentencia);
            this.Controls.Add(this.btnConsultar);
            this.Controls.Add(this.Consulta);
            this.Controls.Add(this.txbConsulta);
            this.Controls.Add(this.dtgDatos);
            this.Name = "PruebaConector";
            this.Text = "PruebaConector";
            this.Load += new System.EventHandler(this.PruebaConector_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgDatos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dtgDatos;
        private System.Windows.Forms.TextBox txbConsulta;
        private System.Windows.Forms.Label Consulta;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.Button btnEjecutar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbSentencia;
    }
}