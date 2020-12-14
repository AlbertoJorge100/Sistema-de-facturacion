﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace General.GUI
{
    public partial class Splash : Form
    {
        public Boolean OpcionDatosServidor=false;
        public Splash()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Cronometro_Tick(object sender, EventArgs e)
        {
            Cronometro.Stop();            
            this.Close();
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            Cronometro.Start();
        }

        private void Splash_KeyUp(object sender, KeyEventArgs e)
        {
            if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.Control) + Convert.ToInt32(Keys.N))
            {
                this.OpcionDatosServidor = true;
            }
        }
    }
}