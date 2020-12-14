using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataManager.CLS;

namespace General.CLS
{
    public class Productos
    {
        String _IDProducto;
        String _NombreProducto;
        String _Marca;
        String _fotoProducto;
        String _PrecioVenta;
        String _Descuento;
        String _IDCategoria;
        String _Existencias;
        String _IDProveedor;
        String _PrecioCompra;
        String _fechaCompra;
        String _fechaVencimiento;
        String _Presentacion;
        String _Alias;

        public String Descuento
        {
            get { return _Descuento; }
            set { _Descuento = value; }
        }

        public String Alias
        {
          get { return _Alias; }
          set { _Alias = value; }
        }

        public String Presentacion
        {
          get { return _Presentacion; }
          set { _Presentacion = value; }
        }

        public String FechaVencimiento
        {
          get { return _fechaVencimiento; }
          set { _fechaVencimiento = value; }
        }

        public String FechaCompra
        {
          get { return _fechaCompra; }
          set { _fechaCompra = value; }
        }

        public String PrecioCompra
        {
          get { return _PrecioCompra; }
          set { _PrecioCompra = value; }
        }

        public String IDProveedor
        {
          get { return _IDProveedor; }
          set { _IDProveedor = value; }
        }

        public String Existencias
        {
          get { return _Existencias; }
          set { _Existencias = value; }
        }

        public String IDCategoria
        {
          get { return _IDCategoria; }
          set { _IDCategoria = value; }
        }

        public String PrecioVenta
        {
          get { return _PrecioVenta; }
          set { _PrecioVenta = value; }
        }
   
        public String FotoProducto
        {
            get { return _fotoProducto; }
            set { _fotoProducto = value; }
        }

        public String Marca
        {
            get { return _Marca; }
            set { _Marca = value; }
        }

        public String NombreProducto
        {
            get { return _NombreProducto; }
            set { _NombreProducto = value; }
        }

        public String IDProducto
        {
            get { return _IDProducto; }
            set { _IDProducto = value; }
        }
        
        
        public Boolean Guardar()
        {
            Boolean confirmacion = false;
            String cadena = "";
            try
            {
                cadena = "INSERT INTO Productos (NombreProducto, Marca,  PrecioVenta, Descuento, IDCategoria, Existencias, " +
                    @"IDProveedor, PrecioCompra, fechaCompra, fechaVencimiento, Presentacion, Alias) VALUES(";
                cadena += @"'" + NombreProducto + "', ";
                cadena += @"'" + Marca + "', ";
                cadena += @" " + PrecioVenta + ", ";
                cadena += @" " + Descuento + ", ";
                cadena += @"" + IDCategoria + ", ";
                cadena += @"" + Existencias + ", ";
                cadena += @"" + IDProveedor + ", ";
                cadena += @"" + PrecioCompra + ", ";
                cadena += @"'" + FechaCompra + "', ";
                cadena += @"'" + FechaVencimiento + "', ";
                cadena += @"'" + Presentacion + "', ";
                cadena += @"'" + Alias + "')";                
                DBOperacion operacion = new DBOperacion();
                if (operacion.Insertar(cadena) > 0)
                {
                    confirmacion = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("El producto no pudo ser insertado debido a un error interno contacte con el desarrollador: " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return confirmacion;
        }


        public Boolean Actualizar()
        {
            Boolean confirmacion = false;
            String cadena = "";
            try
            {
                cadena = "UPDATE Productos SET ";
                cadena += @"NombreProducto = '" + NombreProducto + "', ";
                cadena += @"Marca = '" + Marca + "', ";
                cadena += @"PrecioVenta = " + PrecioVenta + ", ";
                cadena += @"Descuento = " + Descuento + ", ";
                cadena += @"IDCategoria = " + IDCategoria + ", ";
                cadena += @"Existencias = " + Existencias + ", ";
                cadena += @"IDProveedor = " + IDProveedor + ", ";
                cadena += @"PrecioCompra = " + PrecioCompra + ", ";
                cadena += @"fechaCompra = '" + FechaCompra + "', ";
                cadena += @"fechaVencimiento = '" + FechaVencimiento + "', ";
                cadena += @"Presentacion = '" + Presentacion + "', ";
                cadena += @"Alias = '" + Alias + "' WHERE IDProducto="+IDProducto;  
                DBOperacion operacion = new DBOperacion();
                if (operacion.Actualizar(cadena) > 0)
                {
                    confirmacion = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("El producto no pudo ser actualizado debido a un error interno contacte con el desarrollador: " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return confirmacion;
        }


        public Boolean Eliminar()
        {
            Boolean confirmacion = false;
            String cadena = "";
            try
            {
                //cadena = @"DELETE FROM Productos WHERE IDProducto=" + IDProducto;
                cadena = @"UPDATE Productos SET Estado = FALSE WHERE IDProducto=" + IDProducto;
                DBOperacion operacion = new DBOperacion();
                if (operacion.Eliminar(cadena) > 0)
                {
                    confirmacion = true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("El producto no pudo ser eliminado debido a un error interno contacte con el desarrollador: " + e.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return confirmacion;
        }

    }
}
