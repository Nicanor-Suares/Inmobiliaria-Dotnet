-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Apr 15, 2023 at 10:31 PM
-- Server version: 10.4.22-MariaDB
-- PHP Version: 7.3.33

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `inmobiliaria`
--

-- --------------------------------------------------------

--
-- Table structure for table `contrato`
--

CREATE TABLE `contrato` (
  `idContrato` int(11) NOT NULL,
  `fechaInicio` date NOT NULL,
  `fechaFin` date NOT NULL,
  `activo` tinyint(1) NOT NULL,
  `idInquilino` int(11) NOT NULL,
  `idInmueble` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `contrato`
--

INSERT INTO `contrato` (`idContrato`, `fechaInicio`, `fechaFin`, `activo`, `idInquilino`, `idInmueble`) VALUES
(15, '2022-07-15', '2022-08-31', 1, 6, 5),
(16, '2023-03-25', '2023-03-25', 1, 1, 1),
(18, '2023-04-14', '2023-09-07', 1, 5, 8);

-- --------------------------------------------------------

--
-- Table structure for table `inmueble`
--

CREATE TABLE `inmueble` (
  `direccion` varchar(255) NOT NULL,
  `disponibilidad` tinyint(1) NOT NULL,
  `precio` double NOT NULL,
  `idInmueble` int(11) NOT NULL,
  `propietarioId` int(11) DEFAULT NULL,
  `idTipo` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `inmueble`
--

INSERT INTO `inmueble` (`direccion`, `disponibilidad`, `precio`, `idInmueble`, `propietarioId`, `idTipo`) VALUES
('Feliziani 1234', 1, 5000000, 1, 8, 3),
('Gdor. Elias Adre', 1, 24000000, 2, 4, 3),
('Rivadavia 75', 1, 5000010, 3, 1, 1),
('Aristides', 1, 200000, 4, 6, 2),
('Villanueva', 1, 100000, 5, 7, 3),
('Godoy Cruz', 1, 10000, 6, 7, 2),
('Conca', 1, 5000, 8, 6, 2);

-- --------------------------------------------------------

--
-- Table structure for table `inquilino`
--

CREATE TABLE `inquilino` (
  `idInquilino` int(11) NOT NULL,
  `nombre` varchar(255) NOT NULL,
  `apellido` varchar(255) NOT NULL,
  `dni` int(11) NOT NULL,
  `telefono` varchar(45) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `inquilino`
--

INSERT INTO `inquilino` (`idInquilino`, `nombre`, `apellido`, `dni`, `telefono`) VALUES
(1, 'agustin', 'alunda', 39291609, NULL),
(2, 'Juan', 'Sosa', 4789876, NULL),
(4, 'Roberto', 'Orozco', 14657483, '645645645'),
(5, 'Nicanor', 'Suares', 42991424, NULL),
(6, 'Luis', 'Mercado', 2000442, NULL),
(7, 'Edgardo', 'Suares', 37889656, '132156454');

-- --------------------------------------------------------

--
-- Table structure for table `pago`
--

CREATE TABLE `pago` (
  `idPago` int(11) NOT NULL,
  `idContrato` int(11) NOT NULL,
  `fechaPago` datetime NOT NULL,
  `monto` int(11) NOT NULL,
  `nroPago` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `pago`
--

INSERT INTO `pago` (`idPago`, `idContrato`, `fechaPago`, `monto`, `nroPago`) VALUES
(2, 15, '2023-04-15 16:52:00', 5000, 1);

-- --------------------------------------------------------

--
-- Table structure for table `propietario`
--

CREATE TABLE `propietario` (
  `idPropietario` int(11) NOT NULL,
  `nombre` varchar(255) NOT NULL,
  `apellido` varchar(255) NOT NULL,
  `dni` varchar(45) NOT NULL,
  `direccion` varchar(255) NOT NULL,
  `telefono` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `propietario`
--

INSERT INTO `propietario` (`idPropietario`, `nombre`, `apellido`, `dni`, `direccion`, `telefono`) VALUES
(1, 'Nicolas', 'Toledo', '36584795', 'Tucuman 54', 236569685),
(3, 'Juan', 'Soto', '45654432', 'San Juan 543', 254768908),
(4, 'Nicanor', 'Suares', '40985675', 'calle falsa 123', 266587694),
(5, 'Agustin', 'Alunda', '39291609', 'Almirante Brown 664', 236456352),
(6, 'Maria', 'Perez', '457896', 'calle 1234', 12345),
(7, 'Juanjo', 'Saez', '2234555', 'su casa', 123453455),
(8, 'Anastasia', 'Lacoste', '47196929', 'Feliziani 123', 21341234);

-- --------------------------------------------------------

--
-- Table structure for table `tipo_inmueble`
--

CREATE TABLE `tipo_inmueble` (
  `idTipo` int(11) NOT NULL,
  `tipo` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `tipo_inmueble`
--

INSERT INTO `tipo_inmueble` (`idTipo`, `tipo`) VALUES
(1, 'departamento'),
(2, 'monoambiente'),
(3, 'casa'),
(4, 'comercio');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `contrato`
--
ALTER TABLE `contrato`
  ADD PRIMARY KEY (`idContrato`),
  ADD KEY `inquilinoContrato` (`idInquilino`),
  ADD KEY `propiedadContrato` (`idInmueble`);

--
-- Indexes for table `inmueble`
--
ALTER TABLE `inmueble`
  ADD PRIMARY KEY (`idInmueble`),
  ADD KEY `propietarioInmueble` (`propietarioId`),
  ADD KEY `idTipo` (`idTipo`);

--
-- Indexes for table `inquilino`
--
ALTER TABLE `inquilino`
  ADD PRIMARY KEY (`idInquilino`),
  ADD UNIQUE KEY `dni` (`dni`);

--
-- Indexes for table `pago`
--
ALTER TABLE `pago`
  ADD PRIMARY KEY (`idPago`),
  ADD KEY `idContrato` (`idContrato`);

--
-- Indexes for table `propietario`
--
ALTER TABLE `propietario`
  ADD PRIMARY KEY (`idPropietario`),
  ADD UNIQUE KEY `dni` (`dni`);

--
-- Indexes for table `tipo_inmueble`
--
ALTER TABLE `tipo_inmueble`
  ADD PRIMARY KEY (`idTipo`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `contrato`
--
ALTER TABLE `contrato`
  MODIFY `idContrato` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;

--
-- AUTO_INCREMENT for table `inmueble`
--
ALTER TABLE `inmueble`
  MODIFY `idInmueble` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `inquilino`
--
ALTER TABLE `inquilino`
  MODIFY `idInquilino` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `pago`
--
ALTER TABLE `pago`
  MODIFY `idPago` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT for table `propietario`
--
ALTER TABLE `propietario`
  MODIFY `idPropietario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `tipo_inmueble`
--
ALTER TABLE `tipo_inmueble`
  MODIFY `idTipo` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `contrato`
--
ALTER TABLE `contrato`
  ADD CONSTRAINT `contrato_ibfk_1` FOREIGN KEY (`idInmueble`) REFERENCES `inmueble` (`idInmueble`),
  ADD CONSTRAINT `contrato_ibfk_3` FOREIGN KEY (`idInquilino`) REFERENCES `inquilino` (`idInquilino`);

--
-- Constraints for table `inmueble`
--
ALTER TABLE `inmueble`
  ADD CONSTRAINT `inmueble_ibfk_1` FOREIGN KEY (`propietarioId`) REFERENCES `propietario` (`idPropietario`),
  ADD CONSTRAINT `inmueble_ibfk_2` FOREIGN KEY (`idTipo`) REFERENCES `tipo_inmueble` (`idTipo`);

--
-- Constraints for table `pago`
--
ALTER TABLE `pago`
  ADD CONSTRAINT `idContrato` FOREIGN KEY (`idContrato`) REFERENCES `contrato` (`idContrato`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
