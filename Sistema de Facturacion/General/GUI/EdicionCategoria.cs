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
    /// <summary>
    /// Clase Editar categorias 
    /// </summary>
    public partial class EdicionCategoria : Form
    {
        /// <summary>
        /// Variable para validar que se ha ejecutado con exito  una operacion especifica
        /// </summary>
        private Boolean Confirmacion;
        private String IDCategoria;
        public Boolean _Confirmacion { get { return this.Confirmacion; } }
        //Opciones para insertar o actualizar
        public enum Opcion { INSERTAR, ACTUALIZAR };
        private Opcion Opciones;

        /// <summary>
        /// Metodo para procesar y acceder a ejecutar las operaciones de insertar o actualizar
        /// </summary>
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
                    //Validamos si se inserto con exito a la base de datos
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
                    //Validacion si se actualizo con exito en la base de datos
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
            catch(Exception e)
            {
                MessageBox.Show("Excepcion: " + e.Message);
            }
        }


        public EdicionCategoria(Opcion Opciones, String IDC="")
        {
            InitializeComponent();
            this.Confirmacion = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Opciones = Opciones;
            //Si la opcion es actualizar setear el idcategoria
            if (this.Opciones == Opcion.ACTUALIZAR)
            {
                this.IDCategoria = IDC;
            }            
        }

        /// <summary>
        /// Boton aceptar donde se validan los campos antes de proceder a procesar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
