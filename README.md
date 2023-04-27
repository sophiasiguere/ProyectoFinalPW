<h1> Documentación </h1> 
<br>
DESCRIPCIÓN
<br>
Una página web donde los dueños de una empresa puedan llevar registro de sus cotizaciones y pedidos realizados por sus clientes, guardando los datos requeridos para llevar un mejor control.
<br>
<br>
REQUERIMIENTOS:
<br>
  1. CRUD de pedidos: Existe una sección donde se ingresaran los campos que son requeridos al momento de solicitar un pedido de bolsas, en estos solo se guardará el detalle del pedido y el número de orden (el cliente) a la que pertenecen.
  <br>
  2. CRUD de cotizaciones: En esta sección se coloca la información del cliente y la fecha del pedido. 
  <br>
  3. CRUD de usuarios: En esta sección se encuentra la información de los usuarios, en donde tienen la posibilidad de tener 2 roles, una es vendedor y la otra es administrador. 
  <br>
<br>
<br>
DIAGRAMA DE ENTIDAD RELACIÓN
 <img width="1000" alt="diagrama" src="https://user-images.githubusercontent.com/59983672/228640861-5736a79b-d7b6-487e-b746-60b7d80425fd.PNG">
<br>
<br>
Query utilizado: 
<br>

```
CREATE TABLE Rol (
  id INT AUTO_INCREMENT PRIMARY KEY,
  nombre VARCHAR(50) NOT NULL
);

CREATE TABLE Usuarios (
  id INT AUTO_INCREMENT PRIMARY KEY,
  nombre VARCHAR(50) NOT NULL,
  usuario VARCHAR(50) NOT NULL,
  correo VARCHAR(50) NOT NULL,
  telefono VARCHAR(50) NOT NULL,
  contrasena VARCHAR(50) NOT NULL,
  id_rol INT NOT NULL,
  FOREIGN KEY (id_rol) REFERENCES Rol(id)
);

INSERT INTO Rol (nombre) VALUES ('administrador'), ('vendedor');

CREATE TABLE Cotizacion (
  id INT AUTO_INCREMENT NOT NULL PRIMARY KEY,
  correoCliente VARCHAR(50) NOT NULL,
  telefonoCliente VARCHAR(50) NOT NULL,
  fechaCotizacion DATE NOT NULL
);

CREATE TABLE Pedido (
  id INT AUTO_INCREMENT PRIMARY KEY,
  NoPedido INT NOT NULL,
  tamañoAltura INT NOT NULL,
  tamañoHorizonte INT NOT NULL,
  tamañoExtension INT NOT NULL,
  colorPantone VARCHAR(50) NOT NULL,
  Material VARCHAR(50) NOT NULL,
  calibre VARCHAR(50) NOT NULL,
  logo VARCHAR(50) NOT NULL,
  FOREIGN KEY (NoPedido) REFERENCES Cotizacion(id)
);

```
