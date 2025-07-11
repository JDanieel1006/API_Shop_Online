# API Shop Online

API desarrollada en **ASP.NET Core** con **Entity Framework Core** para la gestión de una tienda en línea.

## Características principales

- **Gestión de Tiendas, Artículos y Clientes**  
- **Relación tienda-artículo y cliente-artículo**  
- **Control y registro de ventas con detalle por producto**  
- **Autenticación y autorización por JWT**  
- **Soporte para subida de imágenes y manejo de archivos**
- **Roles de usuario y administrador**
- **DTOs y AutoMapper para seguridad y claridad en la transferencia de datos**
- **CORS habilitado para desarrollo frontend (Angular/React/Vue)**

---

## Estructura del Proyecto

- `/Models`: Modelos de base de datos (Store, Article, Customer, Sale, etc)
- `/Dto`: Objetos de transferencia de datos (DTOs)
- `/Services`: Lógica de negocio, servicios y contratos (interfaces)
- `/Controllers`: Endpoints RESTful
- `/Migrations`: Migraciones de Entity Framework Core

---

## Requisitos

- [.NET 8 SDK o superior](https://dotnet.microsoft.com/download)
- SQL Server (puedes usar SQL Server Express para pruebas)
- Node.js y Angular (opcional, si consumes desde frontend)

---

## Configuración

1. **Clona el repositorio:**
   ```bash
   git clone https://github.com/tu-usuario/API_Shop_Online.git
   cd API_Shop_Online
   
## Usuario administrador por defecto

Para probar la autenticación puedes usar el siguiente usuario:

- **Usuario:** `admin@email.com`
- **Password:** `admin123`

> Si el usuario no existe, agrégalo ejecutando este script SQL:

```sql
INSERT INTO Users (Name, Email, Password)
VALUES ('Admin', 'admin@email.com', 'admin123');
