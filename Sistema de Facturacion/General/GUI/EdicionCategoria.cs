using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using General.CLS;

namespace General.GUI
{
    public partial class EdicionCategoria : Form
    {
        private Boolean Confirmacion;
        private String IDCategoria;
        public Boolean _Confirmacion { get { return this.Confirmacion; } }
        public enum Opcion { INSERTAR, ACTUALIZAR };
        private Opcion Opciones;
        private void Procesar()
        {
            try
            {
                Categorias catg = new Categorias()
                {
                    IDCategoria = this.IDCategoria,
                    NombreCategoria = txbNombre.Text,
                    Descripcion = txbDescripcion.Text
                };

                if (this.Opciones == Opcion.INSERTAR)
                {
                    if (catg.Guardar())
                    {
                        MessageBox.Show("Categoria ingresada exitosamente ", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Confirmacion = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("La categoria no pudo ser ingresado, porfavor contacte con el desarrollador ", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (catg.Actualizar())
                    {
                        MessageBox.Show("Categoria actualizada exitosamente ", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Confirmacion = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("La categoria no pudo ser actualizada, porfavor contacte con el desarrollador ", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch
            {

            }
        }


        public EdicionCategoria(Opcion Opciones, String IDC="")
        {
            InitializeComponent();
            this.Confirmacion = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Opciones = Opciones;
            if (this.Opciones == Opcion.ACTUALIZAR)
            {
                this.IDCategoria = IDC;
            }            
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!txbNombre.Text.Equals("") && !txbDescripcion.Text.Equals(""))
            {
                this.Procesar();
            }
            else
            {
                MessageBox.Show("No deje campos vacios !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddCategoria_Load(object sender, EventArgs e)
        {

        }
    }
}
