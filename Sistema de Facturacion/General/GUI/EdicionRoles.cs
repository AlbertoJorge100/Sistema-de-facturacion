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
    public partial class EdicionRoles : Form
    {
        private String IDRol;
        public enum Opcion { INSERTAR,ACTUALIZAR}
        private Opcion opcion;
        private Boolean Confirmacion = false;
        public Boolean _Confirmacion{get { return this.Confirmacion; }
        }
        private void Procesar()
        {
            try
            {
                Roles rol = new Roles()
                {
                    IDRol = IDRol,
                    Rol = txbRol.Text,
                    Funcion = txbFuncion.Text
                };

                if (opcion == Opcion.INSERTAR)
                {
                    if (rol.Guardar())
                    {
                        MessageBox.Show("Rol ingresado exitosamente ", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Confirmacion = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("El rol no pudo ser ingresado, porfavor contacte con el desarrollador ", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (rol.Actualizar())
                    {
                        MessageBox.Show("Rol actualizado exitosamente ", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Confirmacion = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("El rol no pudo ser actualizado, porfavor contacte con el desarrollador ", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch
            {

            }
        }


        public EdicionRoles(Opcion opc, String IDRol="")
        {
            InitializeComponent();
            opcion = opc;
            if (opcion == Opcion.ACTUALIZAR)
            {
                this.IDRol = IDRol;
            }            
        }

        private void EdicionRoles_Load(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!txbRol.Text.Equals("") && !txbFuncion.Text.Equals(""))
            {
                Confirmacion = true;
                Procesar();
            }
            else
            {
                MessageBox.Show("No deje campos vacios !", "Advertencia", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
