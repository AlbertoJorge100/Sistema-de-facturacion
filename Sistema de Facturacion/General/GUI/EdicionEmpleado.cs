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
using General.CLS;
using System.IO;
using System.Drawing.Imaging;

namespace General.GUI
{
    public partial class EdicionEmpleado : Form
    {
        private Boolean ValidarFotografia = false;
        public enum Documento { _dui, _nit, _nup };
        public enum Opcion { INSERTAR, ACTUALIZAR};
        private String IDEmpleado;
        private Opcion opcion;
        public Boolean Confirmacion;       
        public Boolean _Confirmacion { get { return this.Confirmacion; } }
        private void Procesar()
        {            
            try
            {
                //Conversion de imagen a array de bytes
                MemoryStream ms = new MemoryStream();
                pbImagen.Image.Save(ms, ImageFormat.Jpeg);
                byte[] aByte = ms.ToArray();                
                Empleados emp = new Empleados()
                { 
                    IDEmpleado=this.IDEmpleado,
                    Nombres = txbNombres.Text,
                    Apellidos = txbApellidos.Text,
                    Correo = txbCorreo.Text,
                    EstudiosAcademicos = txbEstudios.Text,
                    Salario = txbSalario.Text,
                    FechaIngreso = fechaIngreso.Text,
                    Direccion = txbDireccion.Text,
                    NumeroCelular = txbCelular.Text,
                    Cargo = txbCargo.Text,
                    DUI = txbDUI.Text,
                    NIT = txbNIT.Text,
                    NUP = txbNUP.Text,
                    NumeroTelefono = txbTelefono.Text,
                    Edad = txbEdad.Text,
                    Foto=aByte
                };  

                if (this.opcion == Opcion.INSERTAR)
                {                                        
                    if (emp.Guardar())
                    {
                        MessageBox.Show("Empleado ingresado exitosamente ", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Confirmacion = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("El empleado no pudo ser ingresado, porfavor contacte con el desarrollador ", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }                    
                }
                else
                {
                    if (emp.Actualizar())
                    {
                        MessageBox.Show("Empleado actualizado exitosamente ", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Confirmacion = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("El empleado no pudo ser actualizado, porfavor contacte con el desarrollador ", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch
            {

            }                        
        }

        private void Agregar()
        {
            if (!txbNombres.Text.Equals("") && !txbApellidos.Text.Equals("") && !txbDUI.Text.Equals("")
                && !txbNIT.Text.Equals("") && !txbNUP.Text.Equals("") && !txbEdad.Text.Equals("") && !txbCelular.Text.Equals("")
                && !txbTelefono.Text.Equals("") && !txbDireccion.Text.Equals("") && !txbEstudios.Text.Equals("")
                && !txbCargo.Text.Equals("") && !txbSalario.Text.Equals("") && !txbCorreo.Text.Equals(""))
            {
                String[] lista = new String[3];
                lista[0] = txbDUI.Text;
                lista[1] = txbNIT.Text;
                lista[2] = txbNUP.Text;
                String errorDocumento = resultadoDocumento(lista);
                if (errorDocumento.Equals(""))
                {
                    if (validarTelefono(txbCelular.Text))
                    {
                        if (validarTelefono(txbTelefono.Text))
                        {
                            if (!validarCaracter(txbDUI.Text) && !validarCaracter(txbNIT.Text) && !validarCaracter(txbNUP.Text))
                            {
                                if (!validarCaracter(txbTelefono.Text) && !validarCaracter(txbCelular.Text))
                                {
                                    if (!validarCaracter(txbEdad.Text))
                                    {
                                        if (!validarCaracter(txbSalario.Text))
                                        {
                                            //podremos hacer la insercion ala DB
                                            if (this.ValidarFotografia)
                                            {
                                                this.Procesar();
                                            }
                                            else{MessageBox.Show("Debe ingresar una fotografia", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);}                                            
                                        }
                                        else { MessageBox.Show("Formato incorrecto en Salario", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                                    }
                                    else { MessageBox.Show("Formato incorrecto en edad", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                                }
                                else { MessageBox.Show("Formato incorrecto en un numero telefonico", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                            }
                            else { MessageBox.Show("Formato incorrecto en un documento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        }
                        else { MessageBox.Show("Numero de telefono erroneo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                    else { MessageBox.Show("Numero de celular erroneo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                else { MessageBox.Show(errorDocumento, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            else { MessageBox.Show("No deje campos vacios", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }


        private String resultadoDocumento(String[] documento)
        {
            int validacion = validarDocumento(documento);
            switch (validacion)
            {
                case 1: { return "DUI incorrecto !"; }
                case 2: { return "NIT incorrecto !"; }
                case 3: { return "NUP incorrecto !"; }
                case 4: { return "DUI y NIT incorrectos !"; }
                case 5: { return "DUI y NUP incorrectos !"; }
                case 6: { return "NIT y NUP incorrectos !"; }
                case 7: { return "DUI, NIT y NUP incorrectos !"; }
                default: { return ""; };
            }
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


        private int validarDocumento(String[] documento)
        {
            int iD = 0, iN = 0, iNU = 0;
            while (iD < documento[0].Length) { iD++; }//dui  10
            while (iN < documento[1].Length) { iN++; }//nit 17
            while (iNU < documento[2].Length) { iNU++; }//nup 12
            if (iD > 10 && iN > 17 && iNU > 12) { return 7; }//dui nit nup
            else if (iD > 10 && iN > 17) { return 4; }//dui nit
            else if (iD > 10 && iNU > 12) { return 5; }//dui nup
            else if (iN > 17 && iNU > 12) { return 6; }//nit nup            
            else if (iD > 10) { return 1; }//dui
            else if (iN > 17) { return 2; }//nit
            else if (iNU > 12) { return 3; }//nup            
            else { return 0; }
        }


        public Boolean validarTelefono(String telefono)
        {
            int i = 0;
            while (i < telefono.Length) { i++; }
            return (i <= 9);
        }


        public EdicionEmpleado(Opcion opcion,String IDE="")
        {
            InitializeComponent();
            this.opcion = opcion;
            this.Confirmacion = false;
            if (!IDE.Equals(""))
            {
                this.IDEmpleado = IDE;
            }
        }                     

        private void AddUsuario_Load(object sender, EventArgs e)
        {
            fechaIngreso.Format = DateTimePickerFormat.Custom;
            fechaIngreso.CustomFormat = "yyyy/MM/dd";
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            try
            {
                if (this.opcion == Opcion.ACTUALIZAR)
                {
                    //Pendiente de configurar
                    PictureBox aux= CacheManager.CLS.Cache.CargarImagen(this.IDEmpleado);
                    pbImagen.Image = aux.Image;
                    

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Excepcion al mostrar la imagen: " + ex.ToString());
            }
        }
        
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            this.Agregar();   
        }
        
    
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void cargarImagen()
        {
            try
            {
                OpenFileDialog ofdSeleccionar = new OpenFileDialog();
                ofdSeleccionar.Filter = "Imagenes|*.jpg; *.png; *.jpeg";
                ofdSeleccionar.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                ofdSeleccionar.Title = "Seleccionar la fotografia";
                if (ofdSeleccionar.ShowDialog() == DialogResult.OK)
                {
                    pbImagen.Image = Image.FromFile(ofdSeleccionar.FileName);
                    this.ValidarFotografia = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void btnCargarImagen_Click(object sender, EventArgs e)
        {
            cargarImagen();
        }

     
    }
}
