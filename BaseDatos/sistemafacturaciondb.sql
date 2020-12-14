-- phpMyAdmin SQL Dump
-- version 5.0.2
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 07-12-2020 a las 23:26:24
-- Versión del servidor: 10.4.14-MariaDB
-- Versión de PHP: 7.4.9

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `sistemafacturaciondb`
--

DELIMITER $$
--
-- Procedimientos
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `AnularFactura` (IN `idf` INT)  BEGIN               
                DECLARE idO INT DEFAULT 0;                
                select idorden into idO from Facturas f where f.idfactura=idf;
                delete fp.* from Facturas f inner join FacturaProductos fp on f.idfactura=fp.idfactura where f.idfactura=idf;
                delete from Facturas  where idfactura=idf;
                delete from Ordenes where idorden=idO;
            END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `CargarProductos` ()  BEGIN               
                SELECT  p.IDProducto, p.NombreProducto 'Producto', p.Marca, 
				 p.PrecioVenta 'Precio', p.Descuento, e.Existencias, p.Presentacion, p.Alias	
				 FROM  Productos p INNER JOIN Existencias e on p.IDProducto = e.IDProducto WHERE p.Estado=true
                and e.Existencias>0 and date(p.fechaVencimiento) > date(now());
            END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `CerrarFactura` (IN `idfactura` INT, IN `Cliente` VARCHAR(50))  READS SQL DATA
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

CREATE DEFINER=`root`@`localhost` PROCEDURE `ConsultaNotificaciones` ()  BEGIN
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
            END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `Respaldos` ()  READS SQL DATA
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
    SELECT rO.Opcion, rO.Duracion, rO.FechaInicio INTO Opcion, Duracion, FechaInicio FROM RespaldoOpciones rO WHERE IDRespaldoOpcion=1;        
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
END$$

--
-- Funciones
--
CREATE DEFINER=`root`@`localhost` FUNCTION `GestionFactura` (`IDUsuario` INT) RETURNS INT(11) READS SQL DATA
BEGIN
	DECLARE IDFactura INT DEFAULT 0;
    DECLARE IDOrden INT DEFAULT 0;
    INSERT INTO Ordenes (Fecha,IDEmpleado) VALUES(NOW(), IDUsuario);
    SET IDOrden = LAST_INSERT_ID();
    INSERT INTO FACTURAS(IDOrden, Facturado) VALUES(IDOrden,false);
    SET IDFactura = LAST_INSERT_ID();
    RETURN IDFactura;
END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `categorias`
--

CREATE TABLE `categorias` (
  `IDCategoria` int(10) UNSIGNED NOT NULL,
  `NombreCategoria` varchar(75) NOT NULL,
  `Descripcion` varchar(500) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `categorias`
--

INSERT INTO `categorias` (`IDCategoria`, `NombreCategoria`, `Descripcion`) VALUES
(1, 'Lacteos ', 'Productos en general cremas, quesos '),
(2, 'Embutidos', 'Productos en general jamon,chorizos, salchichas, mortadela'),
(3, 'Granos basicos', 'Productos en general: maiz, arroz, frijoles'),
(4, 'Calzado', 'Calzados tipo Sport y de vestir para hombres y mujeres'),
(5, 'Productos plasticos', 'Productos en general: vasos, platos, termos, jarros, cucharas, guacales, cantaros, barriles, mesas, sillas, bancos, etc'),
(37, 'bebidas', 'bebida'),
(38, 'atriculos de limpieza', 'productos en general: rinso, jabon de platos, lejia'),
(39, 'articulos de cuidado personal', 'productos en general: shampoo, jabon de baño, desodorante, cepillo de diente'),
(40, 'bakery', 'productos en general: quesadilla, pinguinos,margaritas'),
(41, 'refrigerados y congelados', 'productos en general: alitas de pollo, yogurt,queso capa'),
(43, 'comida de perro y gato', 'producto en general: pedigree,alimiau,dogui'),
(44, 'abarrotes', 'producto en general: sopas,arroz, galletas,sazonadores');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `clasificaciones`
--

CREATE TABLE `clasificaciones` (
  `IDClasificacion` int(11) NOT NULL,
  `Clasificacion` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `clasificaciones`
--

INSERT INTO `clasificaciones` (`IDClasificacion`, `Clasificacion`) VALUES
(1, 'Sistema');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `empleados`
--

CREATE TABLE `empleados` (
  `IDEmpleado` int(10) UNSIGNED NOT NULL,
  `Nombres` varchar(50) NOT NULL,
  `Apellidos` varchar(50) NOT NULL,
  `Correo` varchar(100) DEFAULT NULL,
  `EstudiosAcademicos` varchar(150) DEFAULT NULL,
  `Salario` decimal(7,2) DEFAULT NULL,
  `FechaIngreso` date DEFAULT NULL,
  `Direccion` varchar(500) DEFAULT NULL,
  `NumeroCelular` varchar(9) NOT NULL,
  `Cargo` varchar(100) DEFAULT NULL,
  `DUI` varchar(10) DEFAULT NULL,
  `NIT` varchar(17) DEFAULT NULL,
  `NUP` varchar(12) DEFAULT NULL,
  `NumeroTelefono` varchar(9) DEFAULT NULL,
  `Edad` int(2) NOT NULL,
  `Estado` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `empleados`
--

INSERT INTO `empleados` (`IDEmpleado`, `Nombres`, `Apellidos`, `Correo`, `EstudiosAcademicos`, `Salario`, `FechaIngreso`, `Direccion`, `NumeroCelular`, `Cargo`, `DUI`, `NIT`, `NUP`, `NumeroTelefono`, `Edad`, `Estado`) VALUES
(27, 'Jorge Alberto', 'Perez Nolasco', 'nolascojorge39@gmail.com', 'Ingeniero en sistemas', '5000.00', '2020-03-10', 'final tercera calle poniente barrio la trinidad, Nahuizalco, Sonsonate. El salvador d4', '70032797', 'Gerente general', '04921719-9', '0308-130293-102-0', '548980005215', '24848464', 25, 1),
(28, 'Angelica Maria', 'Flores de Perez!', 'nolascojorge39@gmail.com', 'Ingeniero en sistemas', '5000.00', '2020-03-10', 'final tercera calle poniente barrio la trinidad, Nahuizalco, Sonsonate. El salvador', '70032797', 'Gerente general', '04921719-9', '0308-130293-102-0', '548980005215', '24848464', 22, 0),
(45, 'Mario humberto', 'Moran najarro', 'mario@gmail.com', 'Ingeniero', '534.00', '2020-12-07', 'Sonsonate, sonsonate', '123456789', 'Cajero', '123456789', '123456789', '123456789', '123456789', 23, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `existencias`
--

CREATE TABLE `existencias` (
  `IDExistencias` int(11) NOT NULL,
  `IDProducto` int(10) UNSIGNED NOT NULL,
  `Existencias` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `existencias`
--

INSERT INTO `existencias` (`IDExistencias`, `IDProducto`, `Existencias`) VALUES
(3, 39, 14),
(4, 40, 24),
(5, 41, 25),
(6, 42, 15),
(7, 43, 120),
(8, 44, 175),
(9, 45, 14),
(10, 46, 23),
(11, 47, 19),
(13, 48, 18),
(14, 49, 55),
(15, 50, 36),
(16, 51, 34),
(17, 52, 19),
(18, 53, 10),
(19, 54, 10),
(20, 55, 55),
(21, 56, 23),
(22, 57, 12),
(23, 58, 12),
(24, 59, 24),
(25, 60, 35),
(26, 61, 24),
(27, 62, 23),
(28, 63, 10),
(29, 64, 24),
(30, 65, 24),
(31, 66, 35),
(32, 67, 24),
(33, 68, 40),
(34, 69, 36),
(35, 70, 36),
(36, 71, 36),
(37, 72, 36),
(38, 73, 12),
(39, 74, 15),
(40, 75, 48),
(41, 76, 38),
(42, 77, 35),
(43, 78, 60),
(44, 79, 26),
(45, 80, 36),
(46, 81, 20),
(47, 82, 20),
(48, 83, 25),
(49, 84, 30),
(50, 85, 30),
(51, 86, 25),
(52, 87, 24),
(53, 88, 25),
(54, 89, 15),
(55, 90, 20),
(56, 91, 25),
(57, 92, 25),
(58, 93, 24),
(59, 94, 15);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `facturaproductos`
--

CREATE TABLE `facturaproductos` (
  `IDFacturaProducto` int(10) UNSIGNED NOT NULL,
  `IDProducto` int(10) UNSIGNED NOT NULL,
  `IDFactura` int(10) UNSIGNED NOT NULL,
  `Cantidad` int(10) UNSIGNED DEFAULT NULL,
  `Descuento` decimal(5,2) DEFAULT NULL,
  `SubTotal` decimal(6,2) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `facturaproductos`
--

INSERT INTO `facturaproductos` (`IDFacturaProducto`, `IDProducto`, `IDFactura`, `Cantidad`, `Descuento`, `SubTotal`) VALUES
(224, 39, 150, 3, '6.75', '75.00'),
(225, 42, 150, 15, '0.00', '11.25'),
(226, 41, 151, 1, '0.00', '0.35'),
(227, 42, 151, 3, '0.00', '2.25'),
(228, 41, 152, 5, '0.00', '1.75'),
(229, 43, 152, 7, '0.00', '7.00'),
(230, 41, 152, 1, '0.00', '0.35'),
(231, 42, 155, 1, '0.05', '0.75'),
(232, 41, 156, 1, '0.00', '0.35'),
(233, 41, 157, 1, '0.00', '0.35'),
(234, 41, 157, 1, '0.00', '0.35'),
(235, 43, 158, 10, '0.00', '10.00'),
(236, 42, 158, 5, '0.25', '3.75'),
(237, 43, 159, 1, '0.00', '1.00'),
(238, 43, 160, 5, '0.00', '5.00'),
(239, 40, 160, 1, '0.15', '0.45'),
(240, 44, 161, 2, '0.00', '5.00'),
(241, 48, 161, 5, '0.50', '12.50'),
(242, 52, 161, 7, '0.00', '11.20'),
(243, 44, 171, 1, '0.00', '2.50'),
(244, 43, 171, 1, '0.00', '1.00'),
(245, 40, 172, 1, '0.15', '0.45'),
(246, 42, 172, 1, '0.05', '0.75'),
(247, 42, 173, 2, '0.10', '1.50'),
(248, 48, 173, 3, '0.30', '7.50'),
(249, 43, 174, 2, '0.00', '2.00'),
(250, 42, 174, 3, '0.15', '2.25'),
(251, 43, 174, 4, '0.00', '4.00'),
(252, 52, 174, 3, '0.00', '4.80'),
(253, 51, 174, 2, '0.20', '2.90'),
(254, 42, 176, 1, '0.05', '0.75'),
(255, 48, 176, 7, '0.70', '17.50'),
(256, 52, 176, 2, '0.00', '3.20'),
(257, 39, 176, 5, '11.25', '125.00'),
(258, 52, 177, 5, '0.00', '8.00'),
(259, 51, 177, 2, '0.20', '2.90'),
(260, 41, 178, 3, '0.00', '1.05'),
(261, 43, 178, 5, '0.00', '5.00'),
(262, 52, 179, 4, '0.00', '6.40'),
(263, 51, 179, 3, '0.30', '4.35'),
(264, 48, 179, 6, '0.60', '15.00'),
(265, 39, 180, 1, '0.00', '25.00'),
(266, 41, 180, 1, '0.00', '0.35'),
(267, 52, 180, 1, '0.00', '1.60'),
(268, 51, 180, 1, '0.10', '1.45'),
(269, 44, 180, 1, '0.00', '2.50'),
(270, 41, 181, 1, '0.00', '0.35'),
(271, 42, 181, 1, '0.05', '0.75'),
(272, 39, 181, 1, '0.00', '25.00'),
(273, 39, 182, 1, '0.00', '25.00'),
(274, 41, 182, 1, '0.00', '0.35'),
(282, 44, 185, 5, '0.00', '12.50'),
(283, 45, 185, 1, '0.00', '1.75'),
(284, 42, 185, 1, '0.05', '0.75'),
(285, 45, 186, 1, '0.00', '1.75'),
(286, 41, 186, 1, '0.00', '0.35'),
(287, 43, 186, 1, '0.00', '1.00'),
(288, 48, 187, 1, '0.10', '2.50'),
(289, 52, 187, 2, '0.00', '3.20'),
(290, 51, 187, 1, '0.10', '1.45'),
(291, 41, 187, 1, '0.00', '0.35'),
(292, 45, 187, 1, '0.00', '1.75'),
(293, 44, 187, 5, '0.00', '12.50'),
(294, 44, 188, 5, '0.00', '12.50'),
(295, 45, 188, 1, '0.00', '1.75'),
(296, 48, 188, 3, '0.30', '7.50'),
(297, 51, 188, 4, '0.40', '5.80'),
(298, 44, 189, 1, '0.00', '2.50'),
(299, 45, 189, 1, '0.00', '1.75'),
(300, 39, 189, 1, '0.00', '25.00'),
(301, 41, 189, 1, '0.00', '0.35'),
(302, 42, 189, 3, '0.15', '2.25'),
(303, 43, 189, 1, '0.00', '1.00'),
(304, 48, 189, 1, '0.10', '2.50'),
(305, 52, 189, 1, '0.00', '1.60'),
(306, 44, 190, 2, '0.00', '5.00'),
(307, 45, 190, 1, '0.00', '1.75'),
(308, 39, 190, 1, '0.00', '25.00'),
(309, 41, 190, 1, '0.00', '0.35'),
(310, 42, 190, 1, '0.05', '0.75'),
(311, 43, 190, 1, '0.00', '1.00'),
(312, 48, 190, 1, '0.10', '2.50'),
(313, 51, 190, 1, '0.10', '1.45'),
(314, 48, 190, 1, '0.10', '2.50'),
(315, 52, 190, 1, '0.00', '1.60'),
(316, 45, 191, 1, '0.00', '1.75'),
(317, 39, 191, 1, '0.00', '25.00'),
(318, 41, 191, 1, '0.00', '0.35'),
(319, 42, 191, 1, '0.05', '0.75'),
(320, 43, 191, 1, '0.00', '1.00'),
(321, 48, 191, 4, '0.40', '10.00'),
(322, 52, 191, 1, '0.00', '1.60'),
(323, 51, 191, 1, '0.10', '1.45'),
(324, 41, 191, 1, '0.00', '0.35'),
(325, 42, 191, 1, '0.05', '0.75'),
(326, 45, 193, 2, '0.00', '3.50'),
(327, 44, 193, 4, '0.00', '10.00'),
(328, 39, 193, 1, '0.00', '25.00'),
(329, 41, 193, 1, '0.00', '0.35'),
(330, 42, 193, 1, '0.05', '0.75'),
(331, 43, 193, 1, '0.00', '1.00'),
(332, 48, 193, 1, '0.10', '2.50'),
(333, 52, 193, 1, '0.00', '1.60'),
(334, 51, 193, 1, '0.10', '1.45'),
(335, 44, 193, 1, '0.00', '2.50'),
(336, 45, 193, 1, '0.00', '1.75'),
(337, 44, 196, 1, '0.00', '2.50'),
(338, 45, 196, 1, '0.00', '1.75'),
(339, 39, 196, 1, '0.00', '25.00'),
(340, 41, 196, 1, '0.00', '0.35'),
(341, 42, 196, 1, '0.05', '0.75'),
(342, 43, 196, 1, '0.00', '1.00'),
(343, 48, 196, 1, '0.10', '2.50'),
(344, 52, 196, 1, '0.00', '1.60'),
(345, 51, 196, 13, '1.30', '18.85'),
(346, 48, 196, 1, '0.10', '2.50'),
(347, 42, 197, 1, '0.05', '0.75'),
(348, 43, 197, 1, '0.00', '1.00'),
(349, 48, 197, 1, '0.10', '2.50'),
(350, 44, 199, 1, '0.00', '2.50'),
(351, 39, 199, 1, '0.00', '25.00'),
(352, 41, 199, 1, '0.00', '0.35'),
(353, 42, 199, 5, '0.25', '3.75'),
(354, 43, 199, 7, '0.00', '7.00'),
(355, 39, 200, 3, '0.00', '75.00'),
(356, 41, 200, 1, '0.00', '0.35'),
(357, 52, 200, 1, '0.00', '1.60'),
(358, 44, 200, 2, '0.00', '5.00'),
(359, 43, 200, 1, '0.00', '1.00'),
(360, 48, 200, 3, '0.30', '7.50'),
(361, 51, 200, 4, '0.40', '5.80'),
(362, 44, 200, 1, '0.00', '2.50'),
(363, 45, 200, 1, '0.00', '1.75'),
(364, 39, 200, 1, '0.00', '25.00'),
(365, 43, 201, 1, '0.00', '1.00'),
(366, 44, 201, 1, '0.00', '2.50'),
(367, 39, 201, 3, '0.00', '75.00'),
(368, 42, 201, 4, '0.20', '3.00'),
(369, 48, 201, 7, '0.70', '17.50'),
(370, 52, 201, 5, '0.00', '8.00'),
(371, 51, 201, 1, '0.10', '1.45'),
(372, 39, 201, 1, '0.00', '25.00'),
(373, 45, 201, 1, '0.00', '1.75'),
(374, 39, 201, 1, '0.00', '25.00'),
(375, 40, 202, 1, '0.15', '0.45'),
(376, 41, 202, 1, '0.00', '0.35'),
(377, 39, 203, 1, '0.00', '25.00'),
(378, 42, 203, 3, '0.15', '2.25'),
(379, 43, 203, 1, '0.00', '1.00'),
(380, 44, 203, 1, '0.00', '2.50'),
(381, 46, 203, 1, '0.00', '0.35'),
(382, 47, 203, 1, '0.00', '1.10'),
(383, 48, 203, 1, '0.10', '2.50'),
(384, 46, 203, 1, '0.00', '0.35'),
(385, 39, 203, 1, '0.00', '25.00'),
(386, 43, 203, 1, '0.00', '1.00'),
(387, 44, 204, 1, '0.00', '2.50'),
(388, 43, 205, 1, '0.00', '1.00'),
(389, 47, 205, 1, '0.00', '1.10'),
(390, 43, 206, 1, '0.00', '1.00'),
(391, 44, 206, 3, '0.00', '7.50'),
(392, 47, 206, 3, '0.00', '3.30'),
(393, 42, 207, 1, '0.05', '0.75'),
(394, 44, 207, 1, '0.00', '2.50'),
(395, 44, 208, 1, '0.00', '2.50'),
(396, 47, 208, 1, '0.00', '1.10'),
(397, 42, 208, 1, '0.05', '0.75'),
(398, 43, 209, 1, '0.00', '1.00'),
(399, 44, 209, 1, '0.00', '2.50'),
(400, 46, 209, 1, '0.00', '0.35'),
(401, 40, 209, 1, '0.00', '0.45'),
(402, 57, 209, 1, '0.00', '0.60'),
(403, 52, 209, 1, '0.00', '1.60'),
(404, 62, 209, 1, '0.00', '1.15'),
(405, 65, 209, 1, '0.00', '0.15'),
(406, 66, 209, 1, '0.00', '0.25'),
(407, 76, 209, 1, '0.00', '1.00'),
(408, 66, 210, 1, '0.00', '0.25'),
(409, 78, 210, 1, '0.00', '3.15'),
(410, 86, 210, 1, '0.00', '0.25'),
(411, 88, 210, 1, '0.00', '1.00'),
(412, 91, 210, 1, '0.00', '0.65'),
(413, 93, 210, 1, '0.00', '1.65'),
(414, 94, 210, 1, '0.00', '1.25'),
(415, 77, 210, 1, '0.00', '1.00'),
(416, 75, 210, 1, '0.00', '0.25'),
(417, 85, 210, 1, '0.00', '0.05'),
(418, 94, 211, 1, '0.00', '1.25'),
(419, 93, 211, 1, '0.00', '1.65'),
(420, 92, 211, 1, '0.00', '0.65'),
(421, 91, 211, 1, '0.00', '0.65'),
(422, 90, 211, 1, '0.00', '0.15'),
(423, 89, 211, 1, '0.00', '0.45'),
(424, 88, 211, 1, '0.00', '1.00'),
(425, 87, 211, 1, '0.00', '0.25'),
(426, 86, 211, 1, '0.00', '0.25'),
(427, 85, 211, 1, '0.00', '0.05'),
(428, 39, 212, 1, '0.00', '25.00'),
(429, 41, 212, 1, '0.00', '0.35'),
(430, 43, 212, 1, '0.00', '1.00'),
(431, 45, 212, 1, '0.00', '1.75'),
(432, 47, 212, 1, '0.00', '1.10'),
(433, 48, 212, 1, '0.00', '2.50'),
(434, 42, 213, 1, '0.00', '0.75'),
(435, 47, 213, 1, '0.00', '1.10'),
(436, 56, 213, 1, '0.00', '1.60'),
(437, 58, 213, 1, '0.00', '0.25'),
(438, 63, 213, 1, '0.00', '1.80'),
(439, 61, 213, 1, '0.00', '1.00'),
(440, 59, 214, 1, '0.00', '1.70'),
(441, 64, 214, 1, '0.00', '1.00'),
(442, 63, 214, 1, '0.00', '1.80'),
(443, 66, 214, 1, '0.00', '0.25'),
(444, 67, 214, 1, '0.00', '0.65'),
(445, 56, 215, 1, '0.00', '1.60'),
(446, 65, 215, 1, '0.00', '0.15'),
(447, 64, 215, 1, '0.00', '1.00'),
(448, 73, 215, 1, '0.00', '1.60'),
(449, 74, 215, 1, '0.00', '2.00'),
(450, 67, 216, 1, '0.00', '0.65'),
(451, 64, 216, 1, '0.00', '1.00'),
(452, 72, 216, 1, '0.00', '0.15'),
(453, 66, 216, 1, '0.00', '0.25'),
(454, 73, 216, 1, '0.00', '1.60'),
(455, 73, 216, 1, '0.00', '1.60'),
(456, 56, 217, 2, '0.00', '3.20'),
(457, 42, 218, 1, '0.00', '0.75'),
(458, 47, 218, 1, '0.00', '1.10'),
(459, 42, 218, 1, '0.00', '0.75'),
(460, 47, 218, 1, '0.00', '1.10'),
(461, 52, 218, 1, '0.00', '1.60'),
(462, 56, 218, 1, '0.00', '1.60'),
(463, 48, 218, 1, '0.00', '2.50'),
(464, 46, 218, 6, '0.00', '2.10'),
(465, 59, 219, 1, '0.00', '1.70'),
(466, 61, 219, 1, '0.00', '1.00'),
(467, 60, 219, 7, '0.00', '7.00'),
(468, 62, 219, 2, '0.00', '2.30'),
(469, 64, 219, 2, '0.00', '2.00'),
(470, 63, 219, 4, '0.00', '7.20'),
(471, 64, 220, 2, '0.00', '2.00'),
(472, 65, 220, 3, '0.00', '0.45'),
(473, 52, 220, 1, '0.00', '1.60'),
(474, 58, 220, 4, '0.00', '1.00'),
(475, 59, 220, 1, '0.00', '1.70'),
(476, 41, 221, 1, '0.00', '0.35'),
(477, 43, 221, 1, '0.00', '1.00'),
(478, 46, 221, 2, '0.00', '0.70'),
(479, 51, 221, 2, '0.00', '2.90'),
(480, 47, 221, 1, '0.00', '1.10'),
(481, 56, 221, 4, '0.00', '6.40'),
(482, 40, 221, 2, '0.00', '0.90'),
(483, 42, 222, 1, '0.00', '0.75'),
(484, 69, 222, 1, '0.00', '0.25'),
(485, 68, 222, 2, '0.00', '0.30'),
(486, 70, 222, 2, '0.00', '0.30'),
(487, 69, 222, 2, '0.00', '0.50'),
(488, 69, 223, 1, '0.00', '0.25'),
(489, 67, 223, 1, '0.00', '0.65'),
(490, 64, 223, 1, '0.00', '1.00'),
(491, 71, 223, 1, '0.00', '0.15'),
(492, 72, 223, 1, '0.00', '0.15'),
(493, 74, 223, 1, '0.00', '2.00'),
(494, 73, 224, 3, '0.00', '4.80'),
(495, 66, 224, 1, '0.00', '0.25'),
(496, 74, 224, 2, '0.00', '4.00'),
(497, 75, 224, 2, '0.00', '0.50'),
(498, 77, 224, 1, '0.00', '1.00'),
(499, 78, 224, 3, '0.00', '9.45'),
(500, 79, 224, 4, '0.00', '1.00'),
(501, 80, 224, 1, '0.00', '0.25'),
(502, 71, 224, 1, '0.00', '0.15'),
(503, 76, 225, 1, '0.00', '1.00'),
(504, 77, 225, 3, '0.00', '3.00'),
(505, 78, 225, 1, '0.00', '3.15'),
(506, 83, 225, 1, '0.00', '0.45'),
(507, 82, 225, 1, '0.00', '0.70'),
(508, 81, 225, 1, '0.00', '0.60'),
(509, 85, 225, 1, '0.00', '0.05'),
(510, 86, 225, 1, '0.00', '0.25'),
(511, 87, 225, 1, '0.00', '0.25'),
(512, 88, 225, 1, '0.00', '1.00'),
(513, 90, 226, 1, '0.00', '0.15'),
(514, 91, 226, 1, '0.00', '0.65'),
(515, 92, 226, 1, '0.00', '0.65'),
(516, 93, 226, 1, '0.00', '1.65'),
(517, 94, 226, 2, '0.00', '2.50'),
(518, 88, 227, 1, '0.00', '1.00'),
(519, 84, 227, 1, '0.00', '1.25'),
(520, 92, 227, 1, '0.00', '0.65'),
(521, 83, 227, 1, '0.00', '0.45'),
(522, 82, 227, 1, '0.00', '0.70'),
(523, 93, 227, 1, '0.00', '1.65'),
(524, 75, 227, 1, '0.00', '0.25'),
(525, 70, 228, 1, '0.00', '0.15'),
(526, 67, 228, 1, '0.00', '0.65'),
(527, 64, 228, 1, '0.00', '1.00'),
(528, 56, 228, 1, '0.00', '1.60'),
(529, 47, 228, 1, '0.00', '1.10'),
(530, 60, 228, 1, '0.00', '1.00'),
(531, 42, 228, 1, '0.00', '0.75'),
(532, 40, 228, 1, '0.00', '0.45'),
(533, 56, 228, 1, '0.00', '1.60'),
(534, 41, 229, 1, '0.00', '0.35'),
(535, 48, 229, 1, '0.00', '2.50'),
(536, 44, 229, 1, '0.00', '2.50'),
(537, 51, 229, 1, '0.00', '1.45'),
(538, 52, 229, 2, '0.00', '3.20'),
(539, 56, 229, 1, '0.00', '1.60'),
(540, 57, 229, 1, '0.00', '0.60'),
(541, 58, 229, 1, '0.00', '0.25'),
(542, 59, 229, 1, '0.00', '1.70'),
(543, 61, 229, 1, '0.00', '1.00'),
(544, 48, 230, 1, '0.00', '2.50'),
(545, 58, 230, 1, '0.00', '0.25'),
(546, 59, 230, 1, '0.00', '1.70'),
(547, 63, 230, 1, '0.00', '1.80'),
(548, 66, 230, 1, '0.00', '0.25'),
(549, 67, 230, 1, '0.00', '0.65'),
(550, 60, 231, 1, '0.00', '1.00'),
(551, 66, 231, 1, '0.00', '0.25'),
(552, 62, 231, 1, '0.00', '1.15'),
(553, 69, 231, 1, '0.00', '0.25'),
(554, 73, 231, 1, '0.00', '1.60'),
(555, 76, 231, 1, '0.00', '1.00'),
(556, 79, 232, 1, '0.00', '0.25'),
(557, 77, 232, 1, '0.00', '1.00'),
(558, 82, 232, 1, '0.00', '0.70'),
(559, 85, 232, 1, '0.00', '0.05'),
(560, 88, 232, 1, '0.00', '1.00'),
(561, 86, 232, 1, '0.00', '0.25'),
(562, 86, 233, 1, '0.00', '0.25'),
(563, 83, 233, 1, '0.00', '0.45'),
(564, 91, 233, 1, '0.00', '0.65'),
(565, 93, 233, 1, '0.00', '1.65'),
(566, 94, 233, 1, '0.00', '1.25'),
(567, 69, 233, 1, '0.00', '0.25'),
(568, 63, 233, 1, '0.00', '1.80'),
(569, 65, 233, 1, '0.00', '0.15'),
(570, 70, 233, 1, '0.00', '0.15'),
(571, 73, 233, 1, '0.00', '1.60'),
(572, 73, 234, 1, '0.00', '1.60'),
(573, 75, 234, 1, '0.00', '0.25'),
(574, 69, 234, 1, '0.00', '0.25'),
(575, 65, 234, 1, '0.00', '0.15'),
(576, 73, 234, 1, '0.00', '1.60'),
(577, 78, 234, 1, '0.00', '3.15'),
(578, 76, 235, 1, '0.00', '1.00'),
(579, 77, 235, 1, '0.00', '1.00'),
(580, 71, 235, 1, '0.00', '0.15'),
(581, 84, 235, 1, '0.00', '1.25'),
(582, 88, 235, 1, '0.00', '1.00'),
(583, 89, 235, 1, '0.00', '0.45'),
(584, 93, 235, 1, '0.00', '1.65'),
(585, 93, 236, 1, '0.00', '1.65'),
(586, 88, 236, 1, '0.00', '1.00'),
(587, 73, 236, 1, '0.00', '1.60'),
(588, 59, 236, 1, '0.00', '1.70'),
(589, 42, 236, 1, '0.00', '0.75'),
(590, 91, 236, 1, '0.00', '0.65'),
(591, 44, 237, 1, '0.00', '2.50'),
(592, 51, 237, 1, '0.00', '1.45'),
(593, 64, 237, 1, '0.00', '1.00'),
(594, 73, 237, 1, '0.00', '1.60'),
(595, 85, 237, 1, '0.00', '0.05'),
(596, 94, 237, 1, '0.00', '1.25'),
(597, 78, 237, 1, '0.00', '3.15'),
(598, 66, 238, 1, '0.00', '0.25'),
(599, 52, 238, 1, '0.00', '1.60'),
(600, 45, 238, 1, '0.00', '1.75'),
(601, 59, 238, 1, '0.00', '1.70'),
(602, 87, 238, 1, '0.00', '0.25'),
(603, 57, 238, 1, '0.00', '0.60'),
(604, 87, 240, 1, '0.00', '0.25'),
(605, 92, 240, 1, '0.00', '0.65'),
(606, 83, 240, 1, '0.00', '0.45'),
(607, 72, 240, 1, '0.00', '0.15'),
(608, 62, 240, 1, '0.00', '1.15'),
(609, 46, 240, 1, '0.00', '0.35'),
(610, 44, 240, 1, '0.00', '2.50'),
(611, 40, 240, 1, '0.00', '0.45'),
(612, 94, 240, 1, '0.00', '1.25'),
(613, 92, 240, 1, '0.00', '0.65'),
(614, 66, 241, 1, '0.00', '0.25'),
(615, 78, 241, 1, '0.00', '3.15'),
(616, 43, 242, 3, '0.30', '3.00'),
(617, 46, 242, 5, '0.20', '1.75'),
(618, 39, 243, 3, '7.50', '75.00'),
(619, 43, 244, 2, '0.20', '2.00'),
(620, 46, 244, 1, '0.04', '0.35'),
(621, 42, 244, 3, '0.24', '2.25'),
(622, 51, 244, 1, '0.15', '1.45'),
(623, 56, 244, 1, '0.16', '1.60'),
(624, 39, 244, 1, '2.50', '25.00'),
(625, 62, 244, 1, '0.12', '1.15'),
(626, 60, 244, 1, '0.10', '1.00'),
(627, 66, 244, 1, '0.03', '0.25'),
(628, 77, 244, 1, '0.10', '1.00'),
(629, 47, 245, 1, '0.11', '1.10'),
(630, 40, 245, 1, '0.05', '0.45'),
(631, 51, 245, 1, '0.15', '1.45');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `facturas`
--

CREATE TABLE `facturas` (
  `IDFactura` int(10) UNSIGNED NOT NULL,
  `IDOrden` int(10) UNSIGNED NOT NULL,
  `Facturado` tinyint(1) DEFAULT NULL,
  `Descuento` decimal(5,2) DEFAULT NULL,
  `Total` decimal(6,2) DEFAULT NULL,
  `SubTotal` decimal(6,2) DEFAULT NULL,
  `Cliente` varchar(50) DEFAULT NULL,
  `NumFactura` varchar(10) NOT NULL DEFAULT '0',
  `Estado` tinyint(4) DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `facturas`
--

INSERT INTO `facturas` (`IDFactura`, `IDOrden`, `Facturado`, `Descuento`, `Total`, `SubTotal`, `Cliente`, `NumFactura`, `Estado`) VALUES
(150, 152, 1, '6.75', '79.50', '86.25', NULL, '250', 0),
(151, 153, 1, '0.00', '2.60', '2.60', NULL, '251', 0),
(152, 154, 1, '0.00', '9.10', '9.10', 'Carmen Perez', '252', 0),
(155, 157, 1, '0.05', '0.70', '0.75', NULL, '255', 0),
(156, 158, 1, '0.00', '0.35', '0.35', NULL, '256', 0),
(157, 159, 1, '0.00', '0.70', '0.70', 'Angelica garzona', '257', 1),
(158, 160, 1, '0.25', '13.50', '13.75', 'Angye de Perez', '258', 1),
(159, 161, 1, '0.00', '1.00', '1.00', 'juan perez', '259', 1),
(160, 162, 1, '0.15', '5.30', '5.45', 'mario perez', '260', 1),
(161, 163, 1, '0.50', '28.20', '28.70', 'Maria garzona', '261', 0),
(171, 165, 0, NULL, NULL, NULL, NULL, '271', 0),
(172, 166, 0, NULL, NULL, NULL, NULL, '272', 0),
(173, 167, 1, '0.40', '8.60', '9.00', 'Angelica garzona', '273', 0),
(174, 168, 1, '0.35', '15.60', '15.95', 'flor nolasco', '274', 0),
(175, 169, 0, NULL, NULL, NULL, NULL, '0', 1),
(176, 170, 1, '12.00', '134.45', '146.45', 'angelica de perez', '276', 1),
(177, 171, 1, '0.20', '10.70', '10.90', 'Angye garzona de perez', '277', 1),
(178, 172, 1, '0.00', '6.05', '6.05', 'Dina nolasco', '278', 1),
(179, 173, 1, '0.90', '24.85', '25.75', 'jorge alberto nolasco', '279', 1),
(180, 174, 1, '0.10', '30.80', '30.90', 'manuel perez', '280', 1),
(181, 175, 1, '0.05', '26.05', '26.10', 'gabriela perez', '281', 1),
(182, 176, 1, '0.00', '25.35', '25.35', 'Jorge cortez', '282', 1),
(183, 177, 1, '0.00', '35.35', '35.35', 'Carolina nolasco', '283', 1),
(185, 179, 1, '0.05', '14.95', '15.00', 'maria flores de perez', '285', 1),
(186, 180, 1, '0.00', '3.10', '3.10', 'alberto perez', '286', 1),
(187, 181, 1, '0.20', '21.55', '21.75', 'alvaro cortez', '287', 1),
(188, 182, 1, '0.70', '26.85', '27.55', 'juan cortez', '288', 1),
(189, 183, 1, '0.25', '36.70', '36.95', 'carmen perez', '289', 1),
(190, 184, 1, '0.35', '41.55', '41.90', 'veronica perez', '290', 1),
(191, 185, 1, '0.60', '42.40', '43.00', 'elena perez', '291', 1),
(192, 186, 0, NULL, NULL, NULL, NULL, '0', 1),
(193, 187, 1, '0.25', '50.15', '50.40', 'beatriz nolasco', '293', 1),
(194, 188, 0, NULL, NULL, NULL, NULL, '0', 1),
(195, 189, 0, NULL, NULL, NULL, NULL, '0', 1),
(196, 190, 1, '1.55', '55.25', '56.80', 'angelica garzona', '296', 1),
(197, 191, 1, '0.15', '4.10', '4.25', 'angelica garzona de perez', '297', 1),
(198, 192, 0, NULL, NULL, NULL, NULL, '0', 1),
(199, 193, 1, '0.25', '38.35', '38.60', 'jose perez', '299', 1),
(200, 194, 1, '0.70', '124.80', '125.50', 'juana perez', '300', 1),
(201, 195, 1, '1.00', '159.20', '160.20', 'juana perez', '301', 1),
(202, 196, 1, '0.15', '0.65', '0.80', 'juan perez', '302', 1),
(203, 197, 1, '0.25', '60.80', '61.05', 'jorge nolasco', '303', 1),
(204, 198, 1, '0.00', '2.50', '2.50', 'fgad', '304', 1),
(205, 199, 1, '0.00', '2.10', '2.10', 'angie de perez', '305', 1),
(206, 200, 1, '0.00', '11.80', '11.80', 'wiliam garzona', '306', 1),
(207, 201, 1, '0.05', '3.20', '3.25', 'wil garzona', '307', 1),
(208, 202, 1, '0.05', '4.30', '4.35', 'angye', '308', 1),
(209, 203, 1, '0.00', '9.05', '9.05', 'angelica de perez', '309', 1),
(210, 204, 1, '0.00', '9.50', '9.50', 'jorge perez', '310', 1),
(211, 205, 1, '0.00', '6.35', '6.35', 'william', '311', 1),
(212, 206, 1, '0.00', '31.70', '31.70', 'nestor', '312', 1),
(213, 207, 1, '0.00', '6.50', '6.50', 'ana', '313', 1),
(214, 208, 1, '0.00', '5.40', '5.40', 'maria', '314', 1),
(215, 209, 1, '0.00', '6.35', '6.35', 'ceci', '315', 1),
(216, 210, 1, '0.00', '5.25', '5.25', 'caro', '316', 1),
(217, 211, 1, '0.00', '3.20', '3.20', 'maria garzona', '317', 1),
(218, 212, 1, '0.00', '11.50', '11.50', 'dalia', '318', 1),
(219, 213, 1, '0.00', '21.20', '21.20', 'bea', '319', 1),
(220, 214, 1, '0.00', '6.75', '6.75', 'maria', '320', 1),
(221, 215, 1, '0.00', '13.35', '13.35', 'victoria', '321', 1),
(222, 216, 1, '0.00', '2.10', '2.10', 'rosa', '322', 1),
(223, 217, 1, '0.00', '4.20', '4.20', 'antonio', '323', 1),
(224, 218, 1, '0.00', '21.40', '21.40', 'alexander', '324', 1),
(225, 219, 1, '0.00', '10.45', '10.45', 'rafael', '325', 1),
(226, 220, 1, '0.00', '5.60', '5.60', 'carmen', '326', 1),
(227, 221, 1, '0.00', '5.95', '5.95', 'mayra', '327', 1),
(228, 222, 1, '0.00', '8.30', '8.30', 'flor', '328', 1),
(229, 223, 1, '0.00', '15.15', '15.15', 'maria jose', '329', 1),
(230, 224, 1, '0.00', '7.15', '7.15', 'celina', '330', 1),
(231, 225, 1, '0.00', '5.25', '5.25', 'elizabeth', '331', 1),
(232, 226, 1, '0.00', '3.25', '3.25', 'dina', '332', 1),
(233, 227, 1, '0.00', '8.20', '8.20', 'valeria', '333', 1),
(234, 228, 1, '0.00', '7.00', '7.00', 'isabel', '334', 1),
(235, 229, 1, '0.00', '6.50', '6.50', 'raquel', '335', 1),
(236, 230, 1, '0.00', '7.35', '7.35', 'miriam', '336', 1),
(237, 231, 1, '0.00', '11.00', '11.00', 'marcos', '337', 1),
(238, 232, 1, '0.00', '6.15', '6.15', 'fabiola', '338', 1),
(239, 233, 0, NULL, NULL, NULL, NULL, '0', 1),
(240, 234, 1, '0.00', '7.85', '7.85', 'ileana', '340', 1),
(241, 235, 1, '0.00', '3.40', '3.40', 'angye', '341', 1),
(242, 236, 1, '0.50', '4.25', '4.75', 'dina beatriz', '342', 1),
(243, 237, 1, '7.50', '67.50', '75.00', 'angelica garzona', '343', 0),
(244, 238, 1, '3.64', '32.41', '36.05', 'angye', '344', 1),
(245, 239, 1, '0.31', '2.69', '3.00', 'angie', '345', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `opciones`
--

CREATE TABLE `opciones` (
  `IDOpcion` int(11) NOT NULL,
  `Opcion` varchar(50) NOT NULL,
  `IDClasificacion` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `opciones`
--

INSERT INTO `opciones` (`IDOpcion`, `Opcion`, `IDClasificacion`) VALUES
(1, 'GESTION DE EMPLEADOS', 1),
(2, 'GESTION DE ROLES', 1),
(3, 'GESTION DE PERMISOS', 1),
(4, 'GESTION DE USUARIOS', 1),
(5, 'GESTION DE PRODUCTOS', 1),
(6, 'GESTION DE CATEGORIAS', 1),
(7, 'GESTION DE PROVEEDORES', 1),
(8, 'MODIFICACION DE USUARIO', 1),
(9, 'NOTIFICACIONES', 1),
(10, 'RESPALDO_DB', 1),
(11, 'FACTURACION', 1),
(12, 'REPORTES', 1),
(13, 'INVENTARIOS', 1),
(14, 'Inventario productos', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `ordenes`
--

CREATE TABLE `ordenes` (
  `IDOrden` int(10) UNSIGNED NOT NULL,
  `Fecha` datetime NOT NULL,
  `IDEmpleado` int(10) UNSIGNED NOT NULL,
  `estado` tinyint(1) DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `ordenes`
--

INSERT INTO `ordenes` (`IDOrden`, `Fecha`, `IDEmpleado`, `estado`) VALUES
(152, '2020-06-11 11:14:59', 27, 1),
(153, '2020-10-27 22:58:34', 27, 1),
(154, '2020-11-09 21:42:38', 27, 1),
(157, '2020-11-09 21:56:19', 27, 1),
(158, '2020-11-10 10:47:04', 27, 1),
(159, '2020-11-10 11:02:31', 27, 1),
(160, '2020-11-10 11:51:06', 27, 1),
(161, '2020-11-10 12:06:23', 27, 1),
(162, '2020-11-10 14:51:45', 27, 1),
(163, '2020-11-13 10:15:59', 27, 1),
(165, '2020-11-13 11:27:15', 27, 1),
(166, '2020-11-13 11:31:33', 27, 1),
(167, '2020-11-13 12:27:27', 27, 1),
(168, '2020-11-13 12:34:14', 27, 1),
(169, '2020-11-14 20:36:11', 27, 1),
(170, '2020-11-17 13:35:25', 27, 1),
(171, '2020-11-23 11:41:24', 27, 1),
(172, '2020-11-23 11:51:05', 27, 1),
(173, '2020-11-23 11:52:13', 27, 1),
(174, '2020-11-23 12:38:20', 27, 1),
(175, '2020-11-23 16:52:14', 27, 1),
(176, '2020-11-23 22:31:21', 27, 1),
(177, '2020-11-24 11:10:15', 27, 1),
(179, '2020-11-25 09:14:17', 27, 1),
(180, '2020-11-25 09:19:41', 27, 1),
(181, '2020-11-25 09:23:25', 27, 1),
(182, '2020-11-25 09:26:11', 27, 1),
(183, '2020-11-25 09:36:22', 27, 1),
(184, '2020-11-25 09:40:10', 27, 1),
(185, '2020-11-25 09:46:07', 27, 1),
(186, '2020-11-25 09:48:52', 27, 1),
(187, '2020-11-25 09:54:52', 27, 1),
(188, '2020-11-25 10:01:17', 27, 1),
(189, '2020-11-25 10:04:03', 27, 1),
(190, '2020-11-25 10:09:34', 27, 1),
(191, '2020-11-25 10:24:34', 27, 1),
(192, '2020-11-25 10:25:42', 27, 1),
(193, '2020-11-25 10:31:24', 27, 1),
(194, '2020-11-25 10:35:19', 27, 1),
(195, '2020-11-25 10:39:07', 27, 1),
(196, '2020-11-25 11:25:35', 27, 1),
(197, '2020-11-25 15:01:08', 27, 1),
(198, '2020-11-25 15:03:21', 27, 1),
(199, '2020-11-25 15:07:43', 27, 1),
(200, '2020-11-25 15:11:11', 27, 1),
(201, '2020-11-25 15:15:42', 27, 1),
(202, '2020-11-25 15:21:22', 27, 1),
(203, '2020-11-26 16:44:32', 27, 1),
(204, '2020-11-26 16:46:57', 27, 1),
(205, '2020-11-26 16:48:23', 27, 1),
(206, '2020-11-26 16:49:25', 27, 1),
(207, '2020-11-26 16:50:12', 27, 1),
(208, '2020-11-26 16:51:06', 27, 1),
(209, '2020-11-26 16:51:48', 27, 1),
(210, '2020-11-26 16:53:49', 27, 1),
(211, '2020-11-27 15:32:13', 27, 1),
(212, '2020-11-27 15:45:19', 27, 1),
(213, '2020-11-27 15:48:08', 27, 1),
(214, '2020-11-27 15:50:42', 27, 1),
(215, '2020-11-27 15:55:29', 27, 1),
(216, '2020-11-27 15:59:26', 27, 1),
(217, '2020-11-27 16:02:14', 27, 1),
(218, '2020-11-27 16:03:20', 27, 1),
(219, '2020-11-27 16:07:33', 27, 1),
(220, '2020-11-27 16:09:41', 27, 1),
(221, '2020-11-27 16:11:27', 27, 1),
(222, '2020-11-27 16:12:12', 27, 1),
(223, '2020-11-27 16:13:22', 27, 1),
(224, '2020-11-27 16:14:29', 27, 1),
(225, '2020-11-27 16:17:12', 27, 1),
(226, '2020-11-27 16:18:46', 27, 1),
(227, '2020-11-27 16:23:48', 27, 1),
(228, '2020-11-27 17:06:18', 27, 1),
(229, '2020-11-27 17:09:00', 27, 1),
(230, '2020-11-27 17:12:49', 27, 1),
(231, '2020-11-27 17:14:15', 27, 1),
(232, '2020-11-27 17:14:42', 27, 1),
(233, '2020-11-27 17:17:19', 27, 1),
(234, '2020-11-27 17:17:44', 27, 1),
(235, '2020-12-04 11:18:52', 27, 1),
(236, '2020-12-04 12:17:48', 27, 0),
(237, '2020-12-07 13:50:31', 27, 0),
(238, '2020-12-07 13:57:56', 27, 0),
(239, '2020-12-07 14:02:10', 27, 0);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `permisos`
--

CREATE TABLE `permisos` (
  `IDPermiso` int(11) NOT NULL,
  `IDOpcion` int(11) NOT NULL,
  `IDRol` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `permisos`
--

INSERT INTO `permisos` (`IDPermiso`, `IDOpcion`, `IDRol`) VALUES
(4, 4, 1),
(5, 1, 2),
(7, 9, 1),
(8, 5, 1),
(9, 6, 1),
(10, 7, 1),
(12, 10, 1),
(13, 2, 1),
(14, 1, 1),
(15, 3, 1),
(19, 8, 1),
(24, 2, 5),
(25, 11, 1),
(26, 12, 1),
(27, 13, 1),
(32, 14, 1),
(34, 5, 2),
(35, 6, 2),
(36, 7, 2),
(37, 12, 2),
(38, 13, 2);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `productos`
--

CREATE TABLE `productos` (
  `IDProducto` int(10) UNSIGNED NOT NULL,
  `NombreProducto` varchar(200) NOT NULL,
  `Marca` varchar(75) DEFAULT NULL,
  `PrecioVenta` decimal(5,2) NOT NULL,
  `IDCategoria` int(10) UNSIGNED NOT NULL,
  `Existencias` int(11) NOT NULL,
  `IDProveedor` int(10) UNSIGNED NOT NULL,
  `PrecioCompra` decimal(5,2) NOT NULL,
  `fechaCompra` datetime NOT NULL,
  `fechaVencimiento` datetime DEFAULT NULL,
  `Presentacion` varchar(30) DEFAULT NULL,
  `Alias` varchar(50) DEFAULT NULL,
  `Descuento` decimal(5,2) DEFAULT NULL,
  `Estado` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `productos`
--

INSERT INTO `productos` (`IDProducto`, `NombreProducto`, `Marca`, `PrecioVenta`, `IDCategoria`, `Existencias`, `IDProveedor`, `PrecioCompra`, `fechaCompra`, `fechaVencimiento`, `Presentacion`, `Alias`, `Descuento`, `Estado`) VALUES
(39, 'Maiz nacional', 'nacional', '25.00', 3, 18, 1, '15.00', '2020-06-02 00:00:00', '2021-04-01 00:00:00', '1 quintal', 'Maiz', '2.50', 1),
(40, 'Arroz precocido', 'Nacional', '0.45', 3, 25, 1, '0.25', '2020-06-02 00:00:00', '2022-06-01 00:00:00', '1 libra', 'Arroz precocido', '0.05', 1),
(41, 'Arroz blanco', 'Dany', '0.35', 3, 25, 1, '0.20', '2020-06-02 00:00:00', '2022-06-01 00:00:00', '1 libra', 'arroz blanco', '0.04', 1),
(42, 'Frijol rojo', 'Dany', '0.75', 3, 18, 1, '0.50', '2020-06-02 00:00:00', '2022-06-01 00:00:00', '1 libra', 'frijol rojo', '0.08', 1),
(43, 'Frijol negro', 'Dany', '1.00', 3, 125, 1, '0.75', '2020-06-02 00:00:00', '2022-10-01 00:00:00', '1 libra', 'frijol negro', '0.10', 1),
(44, 'Queso duro ', 'nacional', '2.50', 1, 175, 4, '2.00', '2020-06-02 00:00:00', '2022-01-01 00:00:00', '1 libra', 'queso duro', '0.25', 1),
(45, 'Crema ', 'nacional', '1.75', 1, 14, 4, '1.00', '2020-06-02 00:00:00', '2021-01-25 00:00:00', '1 libra', 'crema', '0.18', 1),
(46, 'Margarina', 'Mirasol', '0.35', 3, 29, 1, '0.15', '2020-06-02 00:00:00', '2021-10-01 00:00:00', '1 barra', 'margarina', '0.04', 1),
(47, 'Aceite ', 'El dorado', '1.10', 3, 20, 1, '0.75', '2020-06-02 00:00:00', '2021-06-03 00:00:00', '500 ml', 'aceite el dorado', '0.11', 1),
(48, 'Azucar', 'Del cañal', '2.50', 3, 18, 1, '2.00', '2020-06-02 00:00:00', '2021-05-01 00:00:00', '5 libras', 'azucar el cañal', '0.25', 1),
(49, 'asfasdf', 'asdf', '0.00', 1, 55, 4, '0.00', '2020-06-06 00:00:00', '2020-07-12 00:00:00', 'asdf', 'asdf', '0.00', 0),
(50, 'eete', 'tete', '0.00', 1, 36, 1, '0.25', '2020-06-06 00:00:00', '2020-06-06 00:00:00', 'asfdf', 'asfasd', '0.00', 0),
(51, 'galletas ', 'picnic', '1.45', 3, 36, 4, '1.00', '2020-06-11 00:00:00', '2021-01-30 00:00:00', '12 unidades', 'picnic', '0.15', 1),
(52, 'harina ', 'molsa', '1.60', 3, 19, 1, '1.35', '2020-06-11 00:00:00', '2021-01-01 00:00:00', '1 libra', 'harina', '0.16', 1),
(53, 'asdfsaf', 'asdf', '10.00', 3, 10, 1, '10.00', '2020-06-11 00:00:00', '2020-06-11 00:00:00', 'sdf', 'asdfdasas', '1.00', 0),
(54, 'prueba', 'asdfadsf', '55.00', 3, 10, 1, '22.00', '2020-06-11 00:00:00', '2020-06-11 00:00:00', 'asdf', '', '5.50', 0),
(55, 'af', 'asdf', '55.00', 3, 55, 4, '14.00', '2020-06-11 00:00:00', '2020-06-11 00:00:00', 'asdfa', 'asdf', '5.50', 0),
(56, 'alitas de pollo', 'sello de oro', '1.60', 41, 24, 1, '1.38', '2020-11-25 00:00:00', '2021-01-01 00:00:00', '226gr', 'alitas', '0.16', 1),
(57, 'yogurt liquido', 'yes', '0.60', 41, 12, 1, '0.40', '2020-11-25 00:00:00', '2021-01-20 00:00:00', '200 ml', 'yogurt', '0.06', 1),
(58, 'chocolatina salud', 'salud', '0.25', 41, 12, 1, '0.20', '2020-12-01 00:00:00', '2021-01-30 00:00:00', '200 ml', 'chocolatina', '0.03', 1),
(59, 'pollo indio nuggets', 'pollo indio', '1.70', 41, 24, 1, '1.59', '2020-11-25 00:00:00', '2021-02-01 00:00:00', '460 gr', 'nuggets', '0.17', 1),
(60, 'alimento alimiau adulto gato', 'pollo indio', '1.00', 43, 36, 4, '0.86', '2020-11-25 00:00:00', '2021-02-01 00:00:00', '455 gr', 'alimiau', '0.10', 1),
(61, 'alimento dogui adulto', 'cargill', '1.00', 43, 24, 4, '0.92', '2020-11-25 00:00:00', '2021-02-01 00:00:00', '1 lb', 'dogui', '0.10', 1),
(62, 'alimento pedigree adulto', 'caligri', '1.15', 43, 24, 4, '0.90', '2020-11-25 00:00:00', '2021-03-26 00:00:00', '1 lb', 'pedigree', '0.12', 1),
(63, 'longanizas', 'dany', '1.80', 2, 10, 1, '1.50', '2020-11-25 00:00:00', '2020-12-11 00:00:00', '1 docena', 'longanizas', '0.18', 1),
(64, 'papel higienico', 'scott', '1.00', 38, 24, 4, '0.80', '2020-11-25 00:00:00', '2021-08-01 00:00:00', '4 rollos', 'papel ', '0.10', 1),
(65, 'margarita', 'lido', '0.15', 40, 24, 1, '0.10', '2020-11-25 00:00:00', '2021-03-01 00:00:00', '25 unidades', 'margarita', '0.02', 1),
(66, 'mufin de naranja', 'lido', '0.25', 40, 36, 4, '0.15', '2020-11-25 00:00:00', '2021-02-01 00:00:00', '220 gr', 'mufin', '0.03', 1),
(67, ' sopa de camaron vaso', 'maruchan', '0.65', 3, 24, 4, '0.48', '2020-11-25 00:00:00', '2021-04-01 00:00:00', '64 gr', 'maruchan', '0.07', 1),
(68, 'sabrosador de pollo', 'continental', '0.15', 44, 40, 1, '0.10', '2020-11-25 00:00:00', '2021-04-01 00:00:00', '10 gr', 'consome', '0.02', 1),
(69, 'galleta oreo original', 'abisco', '0.25', 44, 36, 1, '0.22', '2020-11-25 00:00:00', '2021-04-03 00:00:00', '432 gr', 'oreo', '0.03', 1),
(70, 'sabrosador costilla res', 'continental', '0.15', 44, 36, 1, '0.10', '2020-11-26 00:00:00', '2021-02-22 00:00:00', '10 gr', 'consome de costilla', '0.02', 1),
(71, 'sabrosador de carne', 'continental', '0.15', 44, 36, 1, '0.10', '2020-11-26 00:00:00', '2021-02-26 00:00:00', '10 gr', 'consome de carne', '0.02', 1),
(72, 'sabrosador de camaron', 'continental', '0.15', 44, 36, 1, '0.10', '2020-11-26 00:00:00', '2021-02-27 00:00:00', '10 gr', 'consome de camaron', '0.02', 1),
(73, 'pechuga con hueso', 'selectos', '1.60', 41, 12, 1, '1.49', '2020-11-26 00:00:00', '2020-12-01 00:00:00', '1 lb', 'pechuga', '0.16', 1),
(74, 'queso procesado amarillo', 'lactosa', '2.00', 41, 15, 1, '1.60', '2020-11-26 00:00:00', '2021-02-18 00:00:00', '170 gr', 'queso craft', '0.20', 1),
(75, 'petit manzana', 'petit', '0.25', 37, 48, 1, '2.50', '2020-12-01 00:00:00', '2021-04-01 00:00:00', '150 ml', 'jugo de manzana', '0.03', 1),
(76, 'salutari toronja', 'pepsi', '1.00', 37, 38, 4, '10.30', '2020-12-01 00:00:00', '2021-05-01 00:00:00', '1.5 lt', 'salutari', '0.10', 1),
(77, 'frijoles molidos', 'la chula', '1.00', 44, 36, 4, '0.69', '2020-11-29 00:00:00', '2021-05-13 00:00:00', '480 gr', 'frijoles', '0.10', 1),
(78, 'carton de huevos', 'la catalana', '3.15', 3, 60, 4, '2.85', '2020-11-29 00:00:00', '2020-12-31 00:00:00', '950 gr', 'huevos', '0.32', 1),
(79, 'cafe musun', 'musun', '0.25', 44, 26, 4, '3.92', '2020-11-29 00:00:00', '2021-02-22 00:00:00', '66 uni', 'musun', '0.03', 1),
(80, 'riko cafe display', 'riko', '0.25', 44, 36, 4, '3.04', '2020-11-26 00:00:00', '2021-01-01 00:00:00', '48 gr', 'riko', '0.03', 1),
(81, 'jabon ole miel', 'ole', '0.60', 39, 20, 4, '0.43', '2020-11-26 00:00:00', '2021-03-15 00:00:00', '150 gr', 'jabon de baño', '0.06', 1),
(82, 'jabon lilac dulce', 'lilac', '0.70', 39, 20, 4, '0.50', '2020-11-26 00:00:00', '2021-03-15 00:00:00', '165 gr', 'lilac', '0.07', 1),
(83, 'cepillo de ropa', 'festa', '0.45', 38, 25, 4, '0.30', '2020-11-26 00:00:00', '2021-01-01 00:00:00', 'de mano', 'cepillo', '0.05', 1),
(84, 'ego gel', 'ego', '1.25', 39, 30, 4, '1.11', '2020-11-26 00:00:00', '2021-02-01 00:00:00', '200 ml', 'gelatina de pelo', '0.13', 1),
(85, 'curitas super', 'aid', '0.05', 39, 30, 4, '0.98', '2020-11-26 00:00:00', '2021-02-01 00:00:00', '70', 'curitas', '0.01', 1),
(86, 'quesadilla', 'sinai', '0.25', 40, 25, 4, '0.15', '2020-11-26 00:00:00', '2021-02-21 00:00:00', '378 gr', 'quesadilla', '0.03', 1),
(87, 'pan concha', 'sinai', '0.25', 40, 24, 4, '0.15', '2020-11-26 00:00:00', '2021-02-21 00:00:00', '48 gr', 'concha', '0.03', 1),
(88, 'crema dental', 'colgate', '1.00', 39, 25, 4, '0.87', '2020-11-26 00:00:00', '2021-03-29 00:00:00', '150 ml', 'pasta de diente', '0.10', 1),
(89, 'ajo red ', 'sasson', '0.45', 38, 15, 4, '0.30', '2020-11-26 00:00:00', '2020-12-30 00:00:00', '456 gr', 'ajo', '0.05', 1),
(90, 'club extra galleta', 'club', '0.15', 44, 20, 1, '1.39', '2020-11-26 00:00:00', '2021-02-18 00:00:00', '300 gr', 'gallate saladina', '0.02', 1),
(91, 'crema tomate', 'maggi', '0.65', 44, 25, 4, '0.58', '2020-11-26 00:00:00', '2021-01-01 00:00:00', '76 gr', 'crema', '0.07', 1),
(92, 'crema esparragos', 'maggi', '0.65', 44, 25, 4, '0.50', '2020-11-26 00:00:00', '2021-01-01 00:00:00', '76 gr', 'crema', '0.07', 1),
(93, 'orisol oliva light aceite', 'orisol', '1.65', 44, 24, 1, '1.45', '2020-11-26 00:00:00', '2021-01-01 00:00:00', '150 ml', 'aceite oliva', '0.17', 1),
(94, 'pan blanco', 'monarca', '1.25', 44, 15, 1, '0.95', '2020-11-26 00:00:00', '2021-01-01 00:00:00', '540 gr', 'pan de caja', '0.13', 1);

--
-- Disparadores `productos`
--
DELIMITER $$
CREATE TRIGGER `InsertarExistencias` AFTER INSERT ON `productos` FOR EACH ROW BEGIN
	INSERT INTO Existencias (IDProducto,Existencias) VALUES(NEW.IDProducto,NEW.Existencias);
END
$$
DELIMITER ;
DELIMITER $$
CREATE TRIGGER `Productos_ActualizarExistencias` AFTER UPDATE ON `productos` FOR EACH ROW BEGIN
	DECLARE cant INT DEFAULT 0;
    SELECT e.Existencias INTO cant FROM Existencias e WHERE e.IDProducto = NEW.IDProducto;	
	IF NEW.Existencias != cant THEN        
		UPDATE EXISTENCIAS e SET e.Existencias = NEW.Existencias WHERE e.IDProducto = NEW.IDProducto;         	
	END IF;    
END
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `proveedores`
--

CREATE TABLE `proveedores` (
  `IDProveedor` int(10) UNSIGNED NOT NULL,
  `Nombre` varchar(50) NOT NULL,
  `Correo` varchar(100) NOT NULL,
  `Telefono` varchar(15) NOT NULL,
  `Direccion` varchar(400) NOT NULL,
  `Codigo` varchar(25) NOT NULL,
  `Giro` varchar(150) NOT NULL,
  `Telefono2` varchar(15) NOT NULL,
  `Correo2` varchar(100) NOT NULL,
  `Nombre2` varchar(75) NOT NULL,
  `Apellido2` varchar(75) NOT NULL,
  `Cargo2` varchar(75) NOT NULL,
  `Celular2` varchar(15) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `proveedores`
--

INSERT INTO `proveedores` (`IDProveedor`, `Nombre`, `Correo`, `Telefono`, `Direccion`, `Codigo`, `Giro`, `Telefono2`, `Correo2`, `Nombre2`, `Apellido2`, `Cargo2`, `Celular2`) VALUES
(1, 'Alimentos basicos_01', 'correo@gmail.com', '70032797', 'Nahuizalco, Sonsonate', '84545', 'Granos basicos, productos del hogar', '24848464', 'angelica.garzona@gmail.com', 'Angelica maria', 'flores garzona', 'Gerente de ventas', '70032797'),
(4, 'Angelica food_01', 'afadsf@gasd.com', '25555555', 'nahuizalco, sonsonate, barrio la trinidadnahuizalco, sonsonate, barrio la trinidadnahuizalco, sonsonate, barrio la trinidad', 'asdf33', 'alimentos y bebidas', '22225555', 'adfasfd@gmail.com', 'jorge alberto', 'perez nolasco', 'Ingeniero en sistemas', '70032797');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `respaldoopciones`
--

CREATE TABLE `respaldoopciones` (
  `IDRespaldoOpcion` int(11) NOT NULL,
  `Opcion` enum('DIAS','MESES') DEFAULT NULL,
  `FechaInicio` datetime NOT NULL,
  `Duracion` int(11) NOT NULL,
  `Ruta` varchar(300) NOT NULL,
  `Usuario` varchar(50) NOT NULL,
  `Contrasena` varchar(50) NOT NULL,
  `BaseDatos` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `respaldoopciones`
--

INSERT INTO `respaldoopciones` (`IDRespaldoOpcion`, `Opcion`, `FechaInicio`, `Duracion`, `Ruta`, `Usuario`, `Contrasena`, `BaseDatos`) VALUES
(1, 'DIAS', '2020-06-11 09:18:10', 30, 'C: Users Alberto Desktop ', 'Admin', 'jorge_perez100', 'sistemafacturaciondb');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `respaldos`
--

CREATE TABLE `respaldos` (
  `IDRespaldo` int(11) NOT NULL,
  `Fecha` datetime NOT NULL,
  `Estado` tinyint(1) NOT NULL,
  `IDRespaldoOpcion` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `respaldos`
--

INSERT INTO `respaldos` (`IDRespaldo`, `Fecha`, `Estado`, `IDRespaldoOpcion`) VALUES
(9, '2020-06-04 12:16:00', 1, 1),
(10, '2020-02-04 12:22:59', 1, 1),
(11, '2020-10-30 12:27:34', 1, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `roles`
--

CREATE TABLE `roles` (
  `IDRol` int(11) NOT NULL,
  `Rol` varchar(45) NOT NULL,
  `Funcion` varchar(700) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `roles`
--

INSERT INTO `roles` (`IDRol`, `Rol`, `Funcion`) VALUES
(1, 'Administrador', 'El rol Administrador realiza la funcion '),
(2, 'Vendedor', 'El rol Vendedor realiza la funcion '),
(5, 'Bodeguero', 'Realizar el ingreso de nuevos productos al sistema');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuarios`
--

CREATE TABLE `usuarios` (
  `IDUsuario` int(10) UNSIGNED NOT NULL,
  `IDEmpleado` int(10) UNSIGNED NOT NULL,
  `Usuario` varchar(45) NOT NULL,
  `Contrasena` varchar(250) NOT NULL,
  `IDRol` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `usuarios`
--

INSERT INTO `usuarios` (`IDUsuario`, `IDEmpleado`, `Usuario`, `Contrasena`, `IDRol`) VALUES
(64, 27, 'admin', '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', 1),
(66, 28, 'User', '1a750451c6e403b693adb251a3f7a29b6aa97cd4d561d9aff9accc7f3523a241', 2);

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `categorias`
--
ALTER TABLE `categorias`
  ADD PRIMARY KEY (`IDCategoria`);

--
-- Indices de la tabla `clasificaciones`
--
ALTER TABLE `clasificaciones`
  ADD PRIMARY KEY (`IDClasificacion`);

--
-- Indices de la tabla `empleados`
--
ALTER TABLE `empleados`
  ADD PRIMARY KEY (`IDEmpleado`);

--
-- Indices de la tabla `existencias`
--
ALTER TABLE `existencias`
  ADD PRIMARY KEY (`IDExistencias`),
  ADD KEY `IDProducto` (`IDProducto`);

--
-- Indices de la tabla `facturaproductos`
--
ALTER TABLE `facturaproductos`
  ADD PRIMARY KEY (`IDFacturaProducto`),
  ADD KEY `fk_facturaProductos_productos1_idx` (`IDProducto`),
  ADD KEY `fk_facturaProductos_facturas1_idx` (`IDFactura`);

--
-- Indices de la tabla `facturas`
--
ALTER TABLE `facturas`
  ADD PRIMARY KEY (`IDFactura`),
  ADD KEY `fk_Facturas_Ordenes1_idx` (`IDOrden`);

--
-- Indices de la tabla `opciones`
--
ALTER TABLE `opciones`
  ADD PRIMARY KEY (`IDOpcion`),
  ADD KEY `IDClasificacion` (`IDClasificacion`);

--
-- Indices de la tabla `ordenes`
--
ALTER TABLE `ordenes`
  ADD PRIMARY KEY (`IDOrden`),
  ADD KEY `fk_Ordenes_Empleados1_idx` (`IDEmpleado`);

--
-- Indices de la tabla `permisos`
--
ALTER TABLE `permisos`
  ADD PRIMARY KEY (`IDPermiso`),
  ADD KEY `IDOpcion` (`IDOpcion`),
  ADD KEY `IDRol` (`IDRol`);

--
-- Indices de la tabla `productos`
--
ALTER TABLE `productos`
  ADD PRIMARY KEY (`IDProducto`),
  ADD KEY `fk_Productos_Categorias_idx` (`IDCategoria`),
  ADD KEY `fk_Productos_Proveedores1_idx` (`IDProveedor`);

--
-- Indices de la tabla `proveedores`
--
ALTER TABLE `proveedores`
  ADD PRIMARY KEY (`IDProveedor`);

--
-- Indices de la tabla `respaldoopciones`
--
ALTER TABLE `respaldoopciones`
  ADD PRIMARY KEY (`IDRespaldoOpcion`);

--
-- Indices de la tabla `respaldos`
--
ALTER TABLE `respaldos`
  ADD PRIMARY KEY (`IDRespaldo`),
  ADD KEY `IDRespaldoOpcion` (`IDRespaldoOpcion`);

--
-- Indices de la tabla `roles`
--
ALTER TABLE `roles`
  ADD PRIMARY KEY (`IDRol`);

--
-- Indices de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  ADD PRIMARY KEY (`IDUsuario`),
  ADD KEY `fk_Usuarios_Empleados1_idx` (`IDEmpleado`),
  ADD KEY `fk_Usuarios_Roles1_idx` (`IDRol`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `categorias`
--
ALTER TABLE `categorias`
  MODIFY `IDCategoria` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=45;

--
-- AUTO_INCREMENT de la tabla `clasificaciones`
--
ALTER TABLE `clasificaciones`
  MODIFY `IDClasificacion` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de la tabla `empleados`
--
ALTER TABLE `empleados`
  MODIFY `IDEmpleado` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=46;

--
-- AUTO_INCREMENT de la tabla `existencias`
--
ALTER TABLE `existencias`
  MODIFY `IDExistencias` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=60;

--
-- AUTO_INCREMENT de la tabla `facturaproductos`
--
ALTER TABLE `facturaproductos`
  MODIFY `IDFacturaProducto` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=632;

--
-- AUTO_INCREMENT de la tabla `facturas`
--
ALTER TABLE `facturas`
  MODIFY `IDFactura` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=246;

--
-- AUTO_INCREMENT de la tabla `opciones`
--
ALTER TABLE `opciones`
  MODIFY `IDOpcion` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;

--
-- AUTO_INCREMENT de la tabla `ordenes`
--
ALTER TABLE `ordenes`
  MODIFY `IDOrden` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=240;

--
-- AUTO_INCREMENT de la tabla `permisos`
--
ALTER TABLE `permisos`
  MODIFY `IDPermiso` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=39;

--
-- AUTO_INCREMENT de la tabla `productos`
--
ALTER TABLE `productos`
  MODIFY `IDProducto` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=95;

--
-- AUTO_INCREMENT de la tabla `proveedores`
--
ALTER TABLE `proveedores`
  MODIFY `IDProveedor` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=110;

--
-- AUTO_INCREMENT de la tabla `respaldos`
--
ALTER TABLE `respaldos`
  MODIFY `IDRespaldo` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT de la tabla `roles`
--
ALTER TABLE `roles`
  MODIFY `IDRol` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT de la tabla `usuarios`
--
ALTER TABLE `usuarios`
  MODIFY `IDUsuario` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=71;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `existencias`
--
ALTER TABLE `existencias`
  ADD CONSTRAINT `existencias_ibfk_1` FOREIGN KEY (`IDProducto`) REFERENCES `productos` (`IDProducto`);

--
-- Filtros para la tabla `facturaproductos`
--
ALTER TABLE `facturaproductos`
  ADD CONSTRAINT `fk_facturaProductos_facturas1` FOREIGN KEY (`IDFactura`) REFERENCES `facturas` (`IDFactura`),
  ADD CONSTRAINT `fk_facturaProductos_productos1` FOREIGN KEY (`IDProducto`) REFERENCES `productos` (`IDProducto`);

--
-- Filtros para la tabla `facturas`
--
ALTER TABLE `facturas`
  ADD CONSTRAINT `fk_Facturas_Ordenes1` FOREIGN KEY (`IDOrden`) REFERENCES `ordenes` (`IDOrden`);

--
-- Filtros para la tabla `opciones`
--
ALTER TABLE `opciones`
  ADD CONSTRAINT `opciones_ibfk_1` FOREIGN KEY (`IDClasificacion`) REFERENCES `clasificaciones` (`IDClasificacion`);

--
-- Filtros para la tabla `ordenes`
--
ALTER TABLE `ordenes`
  ADD CONSTRAINT `fk_Ordenes_Empleados1` FOREIGN KEY (`IDEmpleado`) REFERENCES `empleados` (`IDEmpleado`);

--
-- Filtros para la tabla `permisos`
--
ALTER TABLE `permisos`
  ADD CONSTRAINT `permisos_ibfk_1` FOREIGN KEY (`IDOpcion`) REFERENCES `opciones` (`IDOpcion`),
  ADD CONSTRAINT `permisos_ibfk_2` FOREIGN KEY (`IDRol`) REFERENCES `roles` (`IDRol`);

--
-- Filtros para la tabla `productos`
--
ALTER TABLE `productos`
  ADD CONSTRAINT `fk_Productos_Categorias` FOREIGN KEY (`IDCategoria`) REFERENCES `categorias` (`IDCategoria`),
  ADD CONSTRAINT `fk_Productos_Proveedores1` FOREIGN KEY (`IDProveedor`) REFERENCES `proveedores` (`IDProveedor`);

--
-- Filtros para la tabla `respaldos`
--
ALTER TABLE `respaldos`
  ADD CONSTRAINT `respaldos_ibfk_1` FOREIGN KEY (`IDRespaldoOpcion`) REFERENCES `respaldoopciones` (`IDRespaldoOpcion`);

--
-- Filtros para la tabla `usuarios`
--
ALTER TABLE `usuarios`
  ADD CONSTRAINT `fk_Usuarios_Empleados1` FOREIGN KEY (`IDEmpleado`) REFERENCES `empleados` (`IDEmpleado`),
  ADD CONSTRAINT `fk_Usuarios_Roles1` FOREIGN KEY (`IDRol`) REFERENCES `roles` (`IDRol`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
