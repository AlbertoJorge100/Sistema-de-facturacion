using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using DataManager.CLS;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CacheManager.CLS
{
    public static class Cache
    {
        public static DataTable consultaEmpleados(int i = 0) {
            DBOperacion cs = new DBOperacion();
            DataTable rs = new DataTable();
            String cadena = "";
            if (i != 0) {
                cadena = @"select e.IDEmpleado, e.Nombres, e.Apellidos, e.Correo, e.EstudiosAcademicos," +
                @" e.Salario, e.FechaIngreso, e.Direccion, e.NumeroCelular, e.Cargo," +
                @" e.Edad from Empleados e WHERE e.Estado=true";
            }
            else
            {
                cadena = @"select e.IDEmpleado, e.Nombres, e.Apellidos, e.Correo, e.EstudiosAcademicos," +
                @" e.Salario, e.FechaIngreso, e.Direccion, e.NumeroCelular, e.Cargo, e.DUI, e.NIT, e.NUP," +
                @"e.NumeroTelefono, e.Edad from Empleados e WHERE e.Estado=true";
            }

            try {
                rs = cs.Consultar(cadena);

            } catch (Exception ) {
                rs = new DataTable();
            }
            return rs;
        }


        public static DataTable ConsultaLogin(String pUsuario)
        {
            DBOperacion cs = new DBOperacion();
            DataTable rs = new DataTable();
            String cadena = "";

            cadena = @"SELECT u.IDUsuario,u.Contrasena,u.IDRol, r.Rol, u.IDEmpleado, CONCAT(e.Nombres,' ',e.Apellidos) 'Empleado' 
                        FROM Usuarios u INNER JOIN Empleados e
                        ON u.IDEmpleado = e.IDEmpleado INNER JOIN Roles r ON u.IDRol = r.IDRol WHERE u.Usuario = '" + pUsuario + "'";
            try
            {
                rs = cs.Consultar(cadena);
            }
            catch (Exception )
            {
                rs = new DataTable();
            }
            return rs;
        }

        public static DataTable ConsultaNotificaciones()
        {
            DBOperacion cs = new DBOperacion();
            DataTable rs = new DataTable();
            String cadena = "";

            cadena = @"ConsultaNotificaciones";
            try
            {
                rs = cs.Consultar(cadena, true);

            }
            catch (Exception )
            {
                rs = new DataTable();
            }
            return rs;
        }


        public static DataTable CerrarFactura(String idFactura, String pCliente)
        {//Procedimiento almacenado
            DBOperacion cs = new DBOperacion();
            DataTable rs = new DataTable();
            String cadena = "";

            cadena = @"CerrarFactura";
            try
            {
                //   rs = cs.Consultar(cadena, true, "idfactura", idFactura);
                rs = cs.Consultar(cadena, true, new String[] { "idfactura", idFactura, "Cliente", pCliente });

            }
            catch (Exception )
            {
                rs = new DataTable();
            }
            return rs;
        }

        public static DataTable CargarProductosFactura()
        {//Procedimiento almacenado
            DBOperacion cs = new DBOperacion();
            DataTable rs = new DataTable();
            String cadena = "";

            cadena = @"CargarProductos";
            try
            {
                //   rs = cs.Consultar(cadena, true, "idfactura", idFactura);
                rs = cs.Consultar(cadena, true);
            }
            catch (Exception )
            {
                rs = new DataTable();
            }
            return rs;
        }


        public static DataTable ConsultaRespaldos(Boolean ST = false)
        {
            DBOperacion cs = new DBOperacion();
            DataTable rs = new DataTable();
            String cadena = "";
            
            cadena = @"Respaldos";
            
            try
            {
                rs = cs.Consultar(cadena, true);
            }
            catch (Exception )
            {
                rs = new DataTable();
            }
            return rs;
        }

        public static PictureBox CargarImagen(String id)
        {
            DBOperacion cs = new DBOperacion();
            PictureBox pb = new PictureBox();
            String cadena = "";
            Bitmap bm;
            cadena = @"SELECT Imagen FROM Empleados WHERE IDEmpleado="+id;
            try
            {                
                bm =new Bitmap((MemoryStream)cs.ConsultarImagen(cadena));
                pb.Image = bm;
            }
            catch (Exception)
            {
                pb = null;
            }
            return pb;
        }

        public static DataTable RolPermisos(String IDR)
        {            
            DBOperacion cs = new DBOperacion();
            DataTable rs = new DataTable();
            String cadena = "";

            cadena = @"SELECT p.IDPermiso, p.IDOpcion, p.IDRol, o.Opcion 
	            FROM Permisos p inner join Opciones o 
                on p.IDOpcion=o.IDOpcion where p.IDRol=" + IDR;
            try
            {
                rs = cs.Consultar(cadena);

            }
            catch (Exception )
            {
                rs = new DataTable();
            }
            return rs;
        }


        public static DataTable consultaProductos()
        {//Administrador
            DBOperacion cs = new DBOperacion();
            DataTable rs = new DataTable();
            /*String cadena = @"select p.IDProducto, p.NombreProducto , p.Marca, " +
                @"p.PrecioCompra, p.PrecioVenta, p.Existencias, p.FechaCompra, p.FechaVencimiento" +
                @", p.Presentacion, p.Alias, pr.Nombre 'Proveedor', c.NombreCategoria 'Categoria' from  Productos p inner join Categorias c " +
                @"on p.IDCategoria=c.IDCategoria inner join Proveedores pr on p.IDProveedor=pr.IDProveedor";*/
            String cadena = @"SELECT  p.NombreProducto ,p.IDProducto, p.Marca, 
                 p.PrecioVenta,p.Descuento, p.PrecioCompra,  p.IDCategoria, e.Existencias, c.NombreCategoria, pr.Nombre, p.IDProveedor,   p.fechaCompra, p.fechaVencimiento
                , p.Presentacion, p.Alias FROM  Productos p INNER JOIN Categorias c ON p.IDCategoria=c.IDCategoria 
                INNER JOIN Proveedores pr ON p.IDProveedor=pr.IDProveedor INNER JOIN Existencias e on p.IDProducto = e.IDPRoducto WHERE p.Estado=true;";

            try
            {
                rs = cs.Consultar(cadena);

            }
            catch (Exception e)
            {
                Console.WriteLine("error al consultar Productos" + e);
                rs = new DataTable();
            }
            return rs;
        }


        public static DataTable ConsultaProductosFactura()
        {//Usuario facturador
            DBOperacion cs = new DBOperacion();
            DataTable rs = new DataTable();
            String cadena = @"SELECT  p.IDProducto, p.NombreProducto 'Producto', p.Marca, 
									 p.PrecioVenta 'Precio', p.Descuento, p.PrecioCompra 'P_Costo', e.Existencias, p.Presentacion, p.Alias, c.NombreCategoria 'Categoria', pr.Nombre 'Proveedor', p.fechaCompra 'F_Compra', p.fechaVencimiento 'F_Vencimiento'
									 FROM  Productos p INNER JOIN Categorias c ON p.IDCategoria=c.IDCategoria 
									INNER JOIN Proveedores pr ON p.IDProveedor=pr.IDProveedor INNER JOIN Existencias e on p.IDProducto = e.IDPRoducto WHERE p.Estado=true;";
            try
            {
                rs = cs.Consultar(cadena);
            }
            catch (Exception e)
            {
                Console.WriteLine("error al consultar Productos" + e);
                rs = new DataTable();
            }
            return rs;
        }


        public static DataTable consultaProductos(String producto, int op = 0)
        {//Usuario facturador
            DBOperacion cs = new DBOperacion();
            DataTable rs = new DataTable();
            String cadena = "";
            if (op == 1) {//Admin en productos
                cadena = @"select p.IDProducto, p.NombreProducto , p.Marca, " +
                @"p.PrecioCompra, p.PrecioVenta, p.Existencias, p.FechaCompra, p.FechaVencimiento" +
                @", p.Presentacion, p.Alias, pr.Nombre 'Proveedor', c.NombreCategoria 'Categoria' from  Productos p inner join Categorias c " +
                @"on p.IDCategoria=c.IDCategoria inner join Proveedores pr on p.IDProveedor=pr.IDProveedor where NombreProducto like('%" + producto + @"%')";
            }
            else
            {
                cadena = @"select p.IDProducto, p.NombreProducto 'Producto', p.PrecioVenta 'Precio', " +
                @"p.Presentacion, p.Marca, p.Existencias, p.FechaVencimiento" +
                @", p.Alias from Productos p where p.NombreProducto like('%" + producto + @"%')";
            }
            try
            {
                rs = cs.Consultar(cadena);

            }
            catch (Exception e)
            {
                Console.WriteLine("error al consultar Productos" + e);
                rs = new DataTable();
            }
            return rs;
        }


        public static DataTable consultaProveedores(String clave = "", int i = 0)
        {
            DBOperacion cs = new DBOperacion();
            DataTable rs = new DataTable();
            String cadena = "";
            if (clave.Equals("")) {
                if (i != 0)
                {
                    cadena = @"Select IDProveedor, Nombre from Proveedores";
                }
                else
                {
                    cadena = @"select p.IDProveedor, p.Nombre , p.Correo , p.Telefono , p.Direccion" +
                        @", p.Codigo, p.Giro, p.Nombre2 , p.Apellido2, p.Telefono2, p.Correo2 , p.Cargo2 " +
                        ", p.Celular2  from Proveedores p";
                }

            } else {
                cadena = @"select p.IDProveedor, p.Nombre 'Proveedor', p.Correo , p.Telefono , p.Direccion" +
                    @", p.Codigo, p.Giro, p.Nombre2 'Nombre_R', p.Apellido2 'Apellido_R' ,p.Edad2 'Edad', p.Telefono2 'Telefono_R', p.Correo2 'Correo_R', p.Cargo2 'Cargo'" +
                    ", p.Celular2 'Celular_R' from Proveedores p where p.Nombre like('%" + clave + "%')";
            }
            try
            {
                rs = cs.Consultar(cadena);
            }
            catch (Exception e)
            {
                Console.WriteLine("error al consultar Productos" + e);
                rs = new DataTable();
            }
            return rs;
        }


        public static DataTable ConsultaCategorias(String clave = "", int i = 0)
        {
            DBOperacion cs = new DBOperacion();
            DataTable rs = new DataTable();
            String cadena = "";
            if (!clave.Equals(""))
            {
                cadena = @"Select c.IDCategoria, c.NombreCategoria 'Categoria', c.Descripcion,
	                (select count(*) from productos p where p.IDCategoria=c.IDCategoria) 'Productos' 
                    from Categorias c where c.NombreCategoria like('%" + clave + @"%')";
                /*cadena = @"Select c.IDCategoria, c.NombreCategoria 'Categoria', c.Descripcion"
                    + ", count(p.IDProducto) 'Productos' from Categorias c inner join Productos p " +
                    "on c.IDCategoria=p.IDCategoria where c.NombreCategoria like('%"+clave+@"%') group by Categoria ";*/
            }
            else
            {
                switch (i)
                {
                    case 1:
                        {
                            cadena = @"Select c.IDCategoria, c.NombreCategoria from Categorias c";
                            break;
                        }
                    case 2:
                        {
                            cadena = @"Select c.IDCategoria, c.NombreCategoria 'Categoria', c.Descripcion,
	                        (select count(*) from productos p where p.IDCategoria=c.IDCategoria) 'Productos' 
                            from Categorias c";
                            break;
                        }
                }
            }
            try
            {
                rs = cs.Consultar(cadena);

            }
            catch (Exception e)
            {
                Console.WriteLine("error al consultar Productos" + e);
                rs = new DataTable();
            }
            return rs;
        }


        public static DataTable consultaUsuarios()
        {
            DataTable Resultado = new DataTable();
            String Consulta;
            DataManager.CLS.DBOperacion oConsulta = new DataManager.CLS.DBOperacion();
            try
            {
                Consulta = @"Select u.IDUsuario, u.IDEmpleado, u.Usuario, u.Contrasena, u.IDRol from Usuarios u";
                Resultado = oConsulta.Consultar(Consulta);
            }
            catch (Exception e)
            {
                Console.WriteLine("error al consultar Usuarios" + e);
                Resultado = new DataTable();
            }

            return Resultado;
        }


        public static DataTable modificacionUsuario(String clave)
        {
            DataTable Resultado = new DataTable();
            String Consulta;
            DataManager.CLS.DBOperacion oConsulta = new DataManager.CLS.DBOperacion();
            try
            {
                Consulta = @"SELECT u.IDUsuario, u.IDEmpleado, u.Usuario, u.Contrasena, u.IDRol FROM "
                    + "Usuarios u WHERE u.Usuario = BINARY '" + clave + "'";
                Resultado = oConsulta.Consultar(Consulta);
            }
            catch (Exception e)
            {
                Console.WriteLine("error al consultar Usuarios" + e);
                Resultado = new DataTable();
            }

            return Resultado;
        }


        public static DataTable consultaUsuariosEmpleados()
        {
            DataTable Resultado = new DataTable();
            String Consulta;
            DataManager.CLS.DBOperacion oConsulta = new DataManager.CLS.DBOperacion();
            try
            {
                Consulta = @"Select u.IDUsuario, u.Contrasena, u.Usuario, r.Rol, e.Nombres," +
                    "e.Apellidos, e.Cargo from Usuarios u, Empleados e, Roles r where u.IDEmpleado=e.IDEmpleado" +
                    " and u.IDRol=r.IDRol";
                Resultado = oConsulta.Consultar(Consulta);
            }
            catch (Exception e)
            {
                Console.WriteLine("error al consultar Usuarios" + e);
                Resultado = new DataTable();
            }

            return Resultado;
        }


        public static DataTable ConsultaRoles()
        {
            DataTable Resultado = new DataTable();
            String Consulta;
            DBOperacion oConsulta = new DataManager.CLS.DBOperacion();
            try
            {
                Consulta = @"Select r.IDRol, r.Rol, r.Funcion from roles r";
                Resultado = oConsulta.Consultar(Consulta);
            }
            catch (Exception e)
            {
                Console.WriteLine("error al consultar Roles" + e);
                Resultado = new DataTable();
            }

            return Resultado;
        }


        public static DataTable AsignacionPermisos(String pIDRol)
        {
            DataTable Resultado = new DataTable();
            String Consulta;
            DBOperacion oConsulta = new DataManager.CLS.DBOperacion();
            try
            {
                Consulta = @"SELECT 
	                            IDOpcion, Opcion, a.IDClasificacion,b.Clasificacion,
                                IFNULL((SELECT IDPermiso FROM Permisos z WHERE z.IDRol=" + pIDRol + @"         AND z.IDOpcion=a.IDOpcion),0) 'IDPermiso',
                                IF(IFNULL((SELECT IDPermiso FROM Permisos z WHERE z.IDRol=" + pIDRol + @" AND z.IDOpcion=a.IDOpcion),0)>0,1,0) 'Asignado'
                            FROM Opciones a, Clasificaciones b
                            WHERE a.IDClasificacion=b.IDClasificacion";
                Resultado = oConsulta.Consultar(Consulta);
            }
            catch (Exception e)
            {
                Console.WriteLine("error al consultar Roles" + e);
                Resultado = new DataTable();
            }

            return Resultado;
        }


        public static DataTable ConsultarFactura(String pIDEmpleado)
        {
            DataTable Resultado = new DataTable();
            String Consulta;
            DBOperacion oConsulta = new DataManager.CLS.DBOperacion();
            try
            {
                Consulta = @"SELECT GestionFactura(" + pIDEmpleado + @") 'IDFactura';";
                Resultado = oConsulta.Consultar(Consulta);
            }
            catch (Exception e)
            {
                Console.WriteLine("error al consultar Factura" + e);
                Resultado = new DataTable();
            }

            return Resultado;
        }

        /// <summary>
        /// ----------------------------------------------------------- REPORTES ------------------------------------------------------------
        /// </summary>
        /// <returns></returns>
        public static DataTable ReporteProductos()
        {
            DataTable Resultado = new DataTable();
            String Consulta;
            DBOperacion oConsulta = new DataManager.CLS.DBOperacion();
            try
            {
                Consulta = @"select 
                             p.NombreProducto 'Producto', p.Marca, p.PrecioCompra 'Costo', p.PrecioVenta 'Precio', p.Descuento, p.Existencias, 
                             c.NombreCategoria 'Categoria', pr.Nombre 'Proveedor', p.fechaCompra 'F.Compra', p.fechaVencimiento 'F.Vencimiento', p.Presentacion
                             from Productos p INNER JOIN Categorias c ON p.IDCategoria=c.IDCategoria INNER JOIN Proveedores pr ON p.IDProveedor=pr.IDProveedor";
                Resultado = oConsulta.Consultar(Consulta);
            }
            catch (Exception e)
            {
                Console.WriteLine("error al consultar Factura" + e);
                Resultado = new DataTable();
            }

            return Resultado;
        }


        public static DataTable ConsultaFacturaVentas()
        {
            DataTable Resultado = new DataTable();
            String Consulta;
            DBOperacion oConsulta = new DataManager.CLS.DBOperacion();
            try
            {
                Consulta = @"SELECT f.IDFactura, time(o.Fecha) 'Hora', f.NumFactura, e.Nombres 'Empleado' 
                                FROM Ordenes o INNER JOIN Facturas f ON o.IDOrden=f.IDOrden 
                                INNER JOIN Empleados e ON O.IDEmpleado = e.IDEmpleado where date(o.Fecha)=date(now())
                                and f.Estado=true and f.Facturado=true order by Hora desc;";
                Resultado = oConsulta.Consultar(Consulta);
            }
            catch (Exception e)
            {
                Console.WriteLine("error al consultar Factura" + e);
                Resultado = new DataTable();
            }

            return Resultado;
        }

        public static DataTable ReporteFacturaDetalle(String pIDFactura)
        {
            DataTable Resultado = new DataTable();
            String Consulta;
            DBOperacion oConsulta = new DataManager.CLS.DBOperacion();
            try
            {
                Consulta = @"select fp.cantidad, p.NombreProducto 'Producto', p.PrecioVenta 'Precio', fp.Descuento, fp.subtotal, o.Fecha, " +
                            @"f.Cliente, e.Nombres 'Empleado' " +
                            @"from facturaproductos fp inner join facturas f on fp.idFactura=f.idfactura " +
                            @"inner join productos p on fp.idproducto=p.idproducto inner join Ordenes o on o.IDOrden=f.IDOrden " +
                            @"inner join Empleados e on o.IDEmpleado=e.IDEmpleado where f.IDFactura=" + pIDFactura;
                Resultado = oConsulta.Consultar(Consulta);
            }
            catch (Exception e)
            {
                Console.WriteLine("error al consultar Factura" + e);
                Resultado = new DataTable();
            }

            return Resultado;
        }


        public static DataTable ReporteNumFactura(String pNumFactura)
        {
            DataTable Resultado = new DataTable();
            String Consulta;
            DBOperacion oConsulta = new DataManager.CLS.DBOperacion();
            try
            {
                Consulta = @"select fp.cantidad, p.NombreProducto 'Producto', p.PrecioVenta 'Precio', fp.Descuento, fp.subtotal, o.Fecha, " +
                            @"f.Cliente, e.Nombres 'Empleado' " +
                            @"from facturaproductos fp inner join facturas f on fp.idFactura=f.idfactura " +
                            @"inner join productos p on fp.idproducto=p.idproducto inner join Ordenes o on o.IDOrden=f.IDOrden " +
                            @"inner join Empleados e on o.IDEmpleado=e.IDEmpleado where f.NumFactura=" + pNumFactura + " and f.Estado=true and f.Facturado=true";
                Resultado = oConsulta.Consultar(Consulta);
            }
            catch (Exception e)
            {
                Console.WriteLine("error al consultar Factura" + e);
                Resultado = new DataTable();
            }

            return Resultado;
        }

        public static DataTable ReporteF_Anio(String pAnio)
        {
            DataTable Resultado = new DataTable();
            String Consulta;
            DBOperacion oConsulta = new DataManager.CLS.DBOperacion();
            try
            {
                Consulta = @"SELECT 
                                case month(o.Fecha) 
                                WHEN 1 THEN 'Enero'
                                WHEN 2 THEN  'Febrero'
                                WHEN 3 THEN 'Marzo' 
                                WHEN 4 THEN 'Abril' 
                                WHEN 5 THEN 'Mayo'
                                WHEN 6 THEN 'Junio'
                                WHEN 7 THEN 'Julio'
                                WHEN 8 THEN 'Agosto'
                                WHEN 9 THEN 'Septiembre'
                                WHEN 10 THEN 'Octubre'
                                WHEN 11 THEN 'Noviembre'
                                WHEN 12 THEN 'Diciembre'
                                 END 'Mes',
                                count(f.idfactura) 'NumFacturas',
                                 sum((select sum(p.PrecioCompra*fp.Cantidad) from productos p 
                                 inner join facturaproductos fp on p.idproducto=fp.idproducto 
                                 where fp.idfactura=f.idfactura)) 'Costos', sum(f.Total) 'Ventas'
                                 from Ordenes o inner join facturas f on o.idorden=f.idorden where year(o.Fecha)="
                                 + pAnio + " group by Mes order by month(o.Fecha) asc";
                Resultado = oConsulta.Consultar(Consulta);
            }
            catch (Exception e)
            {
                Console.WriteLine("error al consultar Factura" + e);
                Resultado = new DataTable();
            }

            return Resultado;
        }


        public static DataTable Reporte_Movimiento_Productos(String fechaInicio, String fechaFin)
        {
            DataTable Resultado = new DataTable();
            String Consulta;
            DBOperacion oConsulta = new DataManager.CLS.DBOperacion();
            try
            {
                Consulta = @"select p.NombreProducto 'Producto',
					(select count(*) from facturaproductos fp inner join facturas f on fp.idfactura=f.idfactura where f.facturado=true
					and fp.idproducto=p.idproducto) 'CantFacturas',
                    (select sum(fp.cantidad) from facturaproductos fp inner join facturas f on fp.idfactura=f.idfactura where f.facturado=true
					and fp.idproducto=p.idproducto) 'CantUnidades',
					(select sum(fp.Descuento) from facturaproductos fp where fp.idproducto=p.idproducto) 'Descuentos',										
					(select sum(fp.Cantidad*p.PrecioCompra)from facturaproductos fp inner join facturas f on fp.idfactura=f.idfactura where f.facturado=true
					and fp.idproducto=p.idproducto) 'Costos',
                    (select sum(fp.SubTotal-fp.Descuento) from facturaproductos fp where fp.idproducto=p.idproducto) 'Totales'	
             from productos p inner join facturaproductos fp on p.idproducto=fp.idproducto inner join facturas f on fp.idfactura=f.idfactura 
             inner join ordenes o on f.idorden=o.idorden where date(o.fecha) between '"+fechaInicio+"' and '"+fechaFin+ "' group by Producto";
                Resultado = oConsulta.Consultar(Consulta);
            }
            catch (Exception e)
            {
                Console.WriteLine("error al consultar Factura" + e);
                Resultado = new DataTable();
            }

            return Resultado;
        }


        public static DataTable Reporte_Periodos(String fechaInicio, String fechaFin)
        {
            DataTable Resultado = new DataTable();
            String Consulta;
            DBOperacion oConsulta = new DataManager.CLS.DBOperacion();
            try
            {
                Consulta = @"select DATE_FORMAT(o.Fecha,'%d/%m/%Y') 'dia',
				                sum((select count(*) from facturas f where f.idorden=o.idorden)) 'facturas', 
                                sum((select sum(fp.cantidad) from facturaproductos fp inner join facturas f on fp.idfactura=f.idfactura
					                where f.idorden=o.idorden)) 'productos',
				                sum((select sum(fp.descuento) from facturaproductos fp inner join facturas f on fp.idfactura=f.idfactura where f.idorden=o.idorden)) 'descuentos',
                                sum((select sum(fp.Cantidad*p.PrecioCompra) from facturaproductos fp inner join productos p on fp.idproducto=p.idproducto
					                inner join facturas f on fp.idfactura=f.idfactura where f.idorden=o.idorden)) 'costos',
                                sum((select sum(fp.SubTotal-fp.Descuento) from facturaproductos fp inner join facturas f on fp.idfactura=f.idfactura where f.idorden=o.idorden)) 'totales'                
			                from ordenes o where date(o.fecha) between '" + fechaInicio+"' and '"+fechaFin+ "' group by dia;";
                Resultado = oConsulta.Consultar(Consulta);
            }
            catch (Exception e)
            {
                Console.WriteLine("error al consultar Factura" + e);
                Resultado = new DataTable();
            }

            return Resultado;
        }


        public static DataTable ReporteF_Mes(String pMes, String pAnio)
        {
            DataTable Resultado = new DataTable();
            String Consulta;
            DBOperacion oConsulta = new DataManager.CLS.DBOperacion();
            try
            {
                Consulta = @"select day(o.Fecha) 'Dia', count(f.IDFactura) 'Numfacturas', sum(
                                 (select sum(p.PrecioCompra*fp.Cantidad) from productos p 
                                   inner join facturaproductos fp on p.IDProducto=fp.IDProducto where fp.IDFactura=f.IDFactura)) 'Costos',
                                 sum(f.Total) 'Ventas'
                                from facturas f inner join Ordenes o on f.IDOrden=o.IDOrden  where month(o.Fecha)="+pMes+" and year(o.Fecha)="+pAnio+" group by Dia;";
                Resultado = oConsulta.Consultar(Consulta);
            }
            catch (Exception e)
            {
                Console.WriteLine("error al consultar Factura" + e);
                Resultado = new DataTable();
            }

            return Resultado;
        }


        public static DataTable ReporteF_Dia(String pFecha)
        {
            DataTable Resultado = new DataTable();
            String Consulta;
            DBOperacion oConsulta = new DataManager.CLS.DBOperacion();
            try
            {
                Consulta = @"SELECT f.NumFactura , Time(o.Fecha) 'Hora', e.Nombres 'Empleado',
                                (select sum(p.PrecioCompra*fp.Cantidad) 
                                    from productos p inner join facturaproductos
                                    fp on p.idproducto = fp.idproducto where fp.IDFactura=f.IDFactura) 
                                'Costos', f.Total
                                FROM Ordenes o INNER JOIN Facturas f ON o.IDOrden=f.IDOrden 
                                INNER JOIN Empleados e ON O.IDEmpleado = e.IDEmpleado WHERE DATE(o.Fecha) = date('"+pFecha+"');";
                Resultado = oConsulta.Consultar(Consulta);
            }
            catch (Exception e)
            {
                Console.WriteLine("error al consultar Factura" + e);
                Resultado = new DataTable();
            }

            return Resultado;
        }


        /*
         
        
        
        -- PROCEDIMIENTO ALMACENADO NOTIFICACIONES --------- MODIFICADO USANDOOOOOOOOOO
        DROP PROCEDURE IF EXISTS  ConsultaNotificaciones;
        DELIMITER $$
        CREATE PROCEDURE ConsultaNotificaciones()
            BEGIN
                DECLARE cont1 INT DEFAULT 0;
                DECLARE cont2 INT DEFAULT 0;
                DECLARE cont3 INT DEFAULT 0;
                DECLARE cont4 INT DEFAULT 0;	        
                SELECT COUNT(e.IDProducto) INTO cont1 FROM Existencias e INNER JOIN Productos p on e.IDProducto=p.IDProducto
                WHERE p.Estado=true AND e.Existencias > 1  AND e.Existencias <= 30;
                SELECT COUNT(e.IDProducto) INTO cont2 FROM Existencias e INNER JOIN Productos p on e.IDProducto=p.IDProducto
                WHERE p.Estado=true AND e.Existencias <= 1;
                SELECT COUNT(p.IDProducto) INTO cont3
                    FROM Productos p 
                    WHERE p.Estado=true AND p.FechaVencimiento BETWEEN NOW()
                    AND DATE_ADD(now(), INTERVAL 60 DAY);
                SELECT COUNT(p.IDProducto) INTO cont4
                    FROM Productos p 
                    WHERE p.Estado=true AND NOW() >= p.FechaVencimiento;
                SELECT cont1 'ProximosAgotar', cont2 'ProductosAgotados', 
                    cont3 'ProximosVencer', cont4 'ProductosVencidos'
                FROM DUAL;
            END; $$
            CALL ConsultaNotificaciones;
    
        -- PROCEDIMIENTO ALMACENADO RESPALDOS        
        DROP PROCEDURE IF EXISTS Respaldos;
        DELIMITER $$
        CREATE PROCEDURE Respaldos()
        READS SQL DATA
        BEGIN
            DECLARE Ruta VARCHAR(150) DEFAULT "0";
            DECLARE Usuario VARCHAR(50) DEFAULT "0";
            DECLARE Contrasena VARCHAR(50) DEFAULT "0";
            DECLARE BaseDatos VARCHAR(50) DEFAULT "0";
            DECLARE Estado BOOLEAN DEFAULT FALSE;
            DECLARE Opcion VARCHAR(50) DEFAULT "";
            DECLARE Duracion INT DEFAULT 0;
            DECLARE Duracion2 INT DEFAULT 0;
            DECLARE FechaInicio DATETIME ;    
            SELECT rO.Opcion, rO.Duracion, rO.FechaInicio INTO Opcion, Duracion, FechaInicio FROM RespaldoOpciones rO WHERE     IDRespaldoOpcion = 1;        
            CASE Opcion
                WHEN 'DIAS' THEN 
                    case Duracion
                        when 10 then -- semanual
                            if mod(day(now()),10)=0 then
                                set	Estado=true;
                            end if;									
                        when 20 then -- quincenal
                            if mod(day(now()),20)=0 then
                                set	Estado=true;
                            end if;
                        when 30 then -- mensual
                            if mod(day(now()),30)=0 then
                                set	Estado=true;
                            end if;				
                    end case; 
                WHEN 'MESES' THEN
                    SELECT TIMESTAMPDIFF(day, FechaInicio, now()) INTO Duracion2;            
                    if Duracion2=(Duracion*30) then
                        set	Estado=true;
                    end if;					
            END CASE;	       
            IF Estado THEN -- Este dia toca un respaldo		
                if ((exists (select r.Estado from Respaldos r where date(r.Fecha)=date(now())))) then 
                    -- si existe un respaldo hecho este dia: Estado = false			
                    set Estado = false;					
                else -- si no existe un respaldo
                    SELECT rO.Ruta, rO.Usuario, rO.Contrasena, rO.BaseDatos INTO Ruta, Usuario, Contrasena, BaseDatos
                        FROM RespaldoOpciones rO 
                        WHERE rO.IDRespaldoOpcion=1;
                end if;        
            END IF;
            SELECT Estado, Ruta, Usuario, Contrasena, BaseDatos;
        END; $$
        CALL Respaldos();
        
        
        -- TRIGER FACTURAS
        DROP TRIGGER IF EXISTS InsertarExistencias;
        DELIMITER //
        CREATE TRIGGER InsertarExistencias AFTER INSERT ON Productos
            FOR EACH ROW
        BEGIN
            INSERT INTO Existencias (IDProducto,Existencias) VALUES(NEW.IDProducto,NEW.Existencias);
        END; //

        -- TRIGER ACTUALIZAR EXISTENCIAS
        DROP TRIGGER IF EXISTS ActualizarExistencias;
        DELIMITER $$
        CREATE TRIGGER ActualizarExistencias
        AFTER INSERT ON FacturaProductos
        FOR EACH ROW	
        BEGIN
            DECLARE cant INT DEFAULT 0;
            SELECT e.Existencias INTO cant FROM Existencias e WHERE e.IDProducto = NEW.IDProducto;
            IF NEW.Cantidad <= cant THEN
                UPDATE EXISTENCIAS e SET e.Existencias = e.Existencias - NEW.Cantidad WHERE e.IDProducto = NEW.IDProducto; 
            ELSE 
                UPDATE EXISTENCIAS e SET e.Existencias = 0 WHERE e.IDProducto = NEW.IDProducto; 
            END IF;	
        END $$
         
         
        -- TRIGGER ACTUALIZAR LAS EXISTENCIAS AL ACTUALIZAR LOS DATOS DE LOS PRODUCTOS
        DROP TRIGGER IF EXISTS Productos_ActualizarExistencias;
        DELIMITER $$
        CREATE TRIGGER Productos_ActualizarExistencias
        AFTER UPDATE ON Productos
        FOR EACH ROW	
        BEGIN
	        DECLARE cant INT DEFAULT 0;
            SELECT e.Existencias INTO cant FROM Existencias e WHERE e.IDProducto = NEW.IDProducto;	
	        IF NEW.Existencias != cant THEN        
		        UPDATE EXISTENCIAS e SET e.Existencias = NEW.Existencias WHERE e.IDProducto = NEW.IDProducto;         	
	        END IF;    
        END $$
          
                   
        
        ------------   MODIFICADOOOOOOOOOOOOOOOOOOOOOOO USANDOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO
        DROP PROCEDURE IF EXISTS CerrarFactura;
        DELIMITER $$
        CREATE PROCEDURE CerrarFactura(IN idfactura INT, IN Cliente VARCHAR(50))
        READS SQL DATA
        BEGIN
	        DECLARE i INT DEFAULT 0;
            DECLARE idfactprod INT DEFAULT 0;
            DECLARE numProductos INT DEFAULT 0;
            DECLARE idP INT DEFAULT 0;
            DECLARE cant INT DEFAULT 0;
            DECLARE nompro VARCHAR(50);
            DECLARE idPro INT DEFAULT 0;
            DECLARE total DECIMAL(5,2) DEFAULT 0.0;
            DECLARE subtotal DECIMAL(5,2) DEFAULT 0.0;
            DECLARE precPro DECIMAL (5,2) DEFAULT 0.0;
            DECLARE descuento DECIMAL (5,2) DEFAULT 0.0;
            DECLARE descn DECIMAL (5,2) DEFAULT 0.0;                    
            DROP TEMPORARY TABLE IF EXISTS tempTotales;
            CREATE TEMPORARY TABLE tempTotales(
				idtempTotal INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
                Cantidad INT,
                NombreProducto VARCHAR(50),                
                PrecioVenta DECIMAL(5,2),
                Descuento DECIMAL(5,2)
            );
            SELECT MIN(f.IDFacturaProducto), COUNT(*) INTO idfactprod, numProductos FROM FacturaProductos f WHERE f.IDFactura=idfactura;
            WHILE i < numProductos DO		
		        SELECT fp.Cantidad, p.NombreProducto, fp.IDProducto, p.PrecioVenta, fp.Descuento INTO cant, nompro, idPro, precPro, descn
			        FROM facturaproductos fp INNER JOIN Productos p ON fp.IDProducto=p.IDProducto 
			        WHERE fp.IDFacturaProducto = idfactprod;                
		        SET subtotal = subtotal + (precPro*cant);
                SET descuento = descuento + descn;
                INSERT INTO tempTotales(Cantidad, NombreProducto, PrecioVenta, Descuento) VALUES(cant,nompro,precpro,descn);
		        UPDATE Existencias e SET e.Existencias = (e.Existencias - cant) WHERE e.IDProducto = idPro;	 
                SET idfactprod = idfactprod + 1;        
                SET i = i + 1;
            END WHILE;    
            UPDATE FACTURAS f SET f.Facturado=TRUE, f.Descuento=descuento, f.SubTotal=subtotal, f.Total = (subtotal-descuento), f.Cliente = Cliente, 
            f.NumFactura=idfactura+100
            WHERE f.IDFactura=idfactura;                                
			SELECT tt.Cantidad, tt.NombreProducto 'Producto', tt.PrecioVenta 'Precio', tt.Descuento, ROUND((tt.Cantidad*tt.PrecioVenta), 2 ) SubTotal 
            FROM tempTotales tt;             			
        END$$        
        call cerrarfactura(157,"Angelica garzona");        

             
         
        -- FUNCION CREAR FACTURA       
        DROP FUNCTION IF EXISTS GestionFactura;
        DELIMITER $$
        CREATE FUNCTION GestionFactura(IDUsuario INT)     
            RETURNS INT
            READS SQL DATA
        BEGIN
            DECLARE IDFactura INT DEFAULT 0;
            DECLARE IDOrden INT DEFAULT 0;
            INSERT INTO Ordenes (Fecha,IDEmpleado) VALUES(NOW(), IDUsuario);
            SET IDOrden = LAST_INSERT_ID();
            INSERT INTO FACTURAS(IDOrden, Facturado) VALUES(IDOrden,false);
            SET IDFactura = LAST_INSERT_ID();
            RETURN IDFactura;
        END; $$
        SELECT GestionFactura(27);
 


        -- No lo estoy utilizando 
       DROP PROCEDURE IF EXISTS  AnularFactura;
        DELIMITER $$
        CREATE PROCEDURE AnularFactura(IN idf INT)
            BEGIN               
                DECLARE idO INT DEFAULT 0;                
                select idorden into idO from Facturas f where f.idfactura=idf;
                delete fp.* from Facturas f inner join FacturaProductos fp on f.idfactura=fp.idfactura where f.idfactura=idf;
                delete from Facturas  where idfactura=idf;
                delete from Ordenes where idorden=idO;
            END; $$
            CALL AnularFactura(184);

        -- CARGAR PRODUCTOS PARA FACTURACION
        DROP PROCEDURE IF EXISTS  CargarProductos;
        DELIMITER $$
        CREATE PROCEDURE CargarProductos()
            BEGIN               
                SELECT  p.IDProducto, p.NombreProducto 'Producto', p.Marca, 
				 p.PrecioVenta 'Precio', p.Descuento, e.Existencias, p.Presentacion, p.Alias	
				 FROM  Productos p INNER JOIN Existencias e on p.IDProducto = e.IDProducto WHERE p.Estado=true
                and e.Existencias>0 and date(p.fechaVencimiento) > date(now());
            END; $$
         */


    }
}
