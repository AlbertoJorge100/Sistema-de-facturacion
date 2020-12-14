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
    public partial class EdicionProveedores : Form
    {
        private Boolean Confirmacion;
        public Boolean _Confirmacion { get { return this.Confirmacion; } }
        private String IDProveedor;
        public enum Opcion { INSERTAR, ACTUALIZAR };
        private Opcion OPCION;
        private void Procesar()
        {
            try
            {
                Proveedores pro = new Proveedores()
                {
                    IDProveedor = this.IDProveedor,
                    Nombre = txbNombre.Text,
                    Correo = txbCorreo.Text,
                    Telefono = txbTelefono.Text,
                    Direccion = txbDireccion.Text,
                    Codigo = txbCodigo.Text,
                    Giro = txbGiro.Text,
                    Telefono2 = txbTelefono2.Text,
                    Correo2 = txbCorreo2.Text,
                    Nombre2 = txbNombre2.Text,
                    Apellido2 = txbApellido2.Text,
                    //Edad2 = txbEdad2.Text,
                    Cargo2 = txbCargo2.Text,
                    Celular2 = txbCelular.Text
                };
                
                
                if (this.OPCION == Opcion.INSERTAR)
                {//INSERCION
                    if (pro.Guardar())
                    {
                        MessageBox.Show("Proveedor ingresado exitosamente ", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Confirmacion = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("El proveedor no pudo ser ingresado, porfavor contacte con el desarrollador ", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {//ACTUALIZAR
                    if (pro.Actualizar())
                    {
                        MessageBox.Show("Proveedor actualizado exitosamente ", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Confirmacion = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("El proveedor no pudo ser actualizado, porfavor contacte con el desarrollador ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("Excepcion: " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public EdicionProveedores(Opcion opcion, String IDP="")
        {
            InitializeComponent();
            Confirmacion = false;
            this.OPCION = opcion;
            if (this.OPCION == Opcion.ACTUALIZAR)
            {
                this.IDProveedor = IDP;
            }            
        }

        private void AddProveedor_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.MinimizeBox = false; 
        }
        
        
        public Boolean validarTelefono(String telefono)
        {
            int i = 0;
            while (i < telefono.Length) { i++; }
            return (i <= 9);
        }


        private Boolean validarCaracter(String clave)
        {
            int i = 0;
            Boolean enc = false;
            int ascii;
            while (i < clave.Length && !enc)
            {
                ascii = (int)clave[i];
                if (ascii < 45 || ascii > 57)
                {
                    enc = true;
                }
                i++;
            }
            return enc;//true = existe un error en caracteres
        }


        private void button1_Click(object sender, EventArgs e)
        {//boton aceptar
            if(!txbNombre.Text.Equals("") && !txbCorreo.Text.Equals("") && !txbTelefono.Text.Equals("")
                && !txbDireccion.Text.Equals("") && !txbCodigo.Text.Equals("") && !txbGiro.Text.Equals("") 
                && !txbNombre2.Text.Equals("") && !txbApellido2.Text.Equals("") && !txbTelefono2.Text.Equals("")
                && !txbCorreo2.Text.Equals("") && !txbCargo2.Text.Equals("") && !txbCelular.Text.Equals(""))
            {
                if (validarTelefono(txbTelefono.Text) && validarTelefono(txbTelefono2.Text) && validarTelefono(txbCelular.Text))
                {
                    if (!validarCaracter(txbTelefono.Text) && !validarCaracter(txbCelular.Text) && !validarCaracter(txbTelefono2.Text))
                    {                        
                        this.Procesar();                        
                    }
                    else { MessageBox.Show("Formato incorrecto en un numero telefonico !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                else
                {
                    MessageBox.Show("Ingrese Telefonos correctos !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }                
            }
            else
            {
                MessageBox.Show("No deje campos vacios !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void txbTelefono_TextChanged(object sender, EventArgs e)
        {

        }
        
        private void txbDireccion_TextChanged(object sender, EventArgs e)
        {

        }

        private void txbCorreo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txbCodigo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txbGiro_TextChanged(object sender, EventArgs e)
        {

        }

        private void txbNombre2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txbApellido2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txbNombre_TextChanged(object sender, EventArgs e)
        {

        }

        private void txbCelular_TextChanged(object sender, EventArgs e)
        {

        }

        private void txbTelefono2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txbCorreo2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txbEdad2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txbCargo2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
