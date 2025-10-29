# Kaits_Demo
KaitsDemo: Proyecto de gestión de pedidos con **Frontend en React** y **Backend en .NET 6/7 WebAPI**, conectado a una base de datos SQL.
---

## Estructura de Carpetas
BD/
├── create_db.sql
├── seed_clientes.sql
└── seed_productos.sql

KaitsDemo/
├── Kaits.Application
├── Kaits.Domain-Entities
├── Kaits.Infrastructure
├── Kaits.WebAPI
└── kaits-frontend/

---
## Instrucciones para Ejecutar el Proyecto
### 1. Configurar Base de Datos
1. Crear la base de datos ejecutando `create_db.sql`.
2. Poblar las tablas iniciales con: seed_clientes.sql y seed_productos.sql

### 2. Ejecutar Backend (.NET WebAPI)
1. Abrir la solución en Visual Studio 2022 o VS Code.
2. Restaurar paquetes NuGet: dotnet restore
3. Construir el proyecto: dotnet build
4. Ejecutar la WebAPI: dotnet run --project KaitsDemo
5. La API quedará disponible en `https://localhost:7216`.

### 3. Ejecutar Frontend (React)
1. Abrir la carpeta `kaits-frontend`.
2. Instalar dependencias: npm install
3. Ejecutar la aplicación: npm start
4. La app estará disponible en `http://localhost:3000`.
---

## Arquitectura y Decisiones Técnicas

* **Backend (.NET WebAPI)**

  * **Arquitectura Limpia (Clean Architecture):**
    * `Kaits.Domain` → Entidades del dominio (`Cliente`, `Producto`, `Pedido`, `PedidoDetalle`).
    * `Kaits.Application.Interfaces` → Interfaces de acceso a datos (`IKaitsDbContext`).
    * `Kaits.Infrastructure.Persistence` → Implementación concreta del DbContext con EF Core.
    * `Kaits.WebAPI.Controllers` → Exposición de endpoints REST.
	
  * **Decisiones técnicas:**
    * Uso de Entity Framework Core para ORM.
    * Separación de capas para escalabilidad y mantenibilidad.
    * DTOs opcionales para el intercambio de datos entre capas.

* **Frontend (React + TypeScript)**
  * Componentes funcionales con Hooks (`useState`, `useEffect`).
  * Consumo de WebAPI mediante Axios.
  * Diseño responsivo usando **TailwindCSS**.
  * Toast notifications con `react-hot-toast` para mensajes de éxito/error.
  * Arquitectura simple de componentes:
    * `ClienteSelect.tsx`
    * `ProductoSelect.tsx`
    * `PedidoForm.tsx`
---

## Paquetes NuGet y Librerías Principales
### Backend
* `Microsoft.EntityFrameworkCore`
* `Microsoft.EntityFrameworkCore.SqlServer`
* `Microsoft.EntityFrameworkCore.Tools`
* `Swashbuckle.AspNetCore` (Swagger)
* `System.ComponentModel.Annotations` (para validaciones)

### Frontend
* `react` + `react-dom` + `react-scripts`
* `typescript`
* `axios`
* `react-hot-toast`
* `tailwindcss`
* `postcss` y `autoprefixer`
