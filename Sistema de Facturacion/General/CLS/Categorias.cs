using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataManager.CLS;
using System.Windows.Forms;

namespace General.CLS
{
    /// <summary>
    /// Clase entidad: Categorias
    /// </summary>
    public class Categorias
    {
        private String _IDCategoria;
        private String _NombreCategoria;
        private String _Descripcion;

        public String Descripcion
        {
            get { return _Descripcion; }
            set { _Descripcion = value; }
        }

        public String NombreCategoria
        {
            get { return _NombreCategoria; }
            set { _NombreCategoria = value; }
        }

        public String IDCategoria
        {
            get { return _IDCategoria; }
            set { _IDCategoria = value; }
        }

        /// <summary>
        /// Metodo para insertar una categoria al servidor
        /// </summary>
        /// <returns>Boolean</returns>
        public Boolean Guardar()
        {
            Boolean confirmacion = false;
            String cadena = "";
            try
            {
                cadena = "INSERT INTO Categorias (NombreCategoria,Descripcion) VALUES(";
                cadena += @"'" + NombreCategoria + "', ";
                cadena += @"'" + Descripcion + "')";
                
                DBOperacion operacion = new DBOperacion();
                if (operacion.Insertar(cadena) > 0)
                {
                    confirmacion = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("La categoria no pudo ser insertado debido a un error interno contacte con el desarrollador: " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return confirmacion;
        }

        /// <summary>
        /// Metodo actualizar Categorias
        /// </summary>
        /// <returns>Boolean</returns>
        public Boolean Actualizar()
        {
            Boolean confirmacion = false;
            String cadena = "";
            try
            {
                cadena = "UPDATE Categorias SET ";
                cadena += @"NombreCategoria = '" + NombreCategoria + "', ";
                cadena += @"Descripcion = '" + Descripcion + "' WHERE IDCategoria = " + IDCategoria;

                DBOperacion operacion = new DBOperacion();
                if (operacion.Actualizar(cadena) > 0)
                {
                    confirmacion = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("La categoria no pudo ser insertado debido a un error interno contacte con el desarrollador: " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return confirmacion;
        }

        /// <summary>
        /// Eleminar una categoria
        /// </summary>
        /// <returns>Boolean</returns>
        public Boolean Eliminar()
        {
            Boolean confirmacion = false;
            String cadena = "";
            try
            {
                cadena = "DELETE FROM Categorias WHERE IDCategoria = " + IDCategoria;                
                DBOperacion operacion = new DBOperacion();
                if (operacion.Eliminar(cadena) > 0)
                {
                    confirmacion = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("La categoria no pudo ser insertado debido a un error interno contacte con el desarrollador: " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return confirmacion;
        }
    }
}
