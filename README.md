# 📦 ProductManager - Solución Fullstack

Sistema completo para la gestión de productos desarrollado con una arquitectura por capas en el backend utilizando **.NET 10 Web API** y un cliente web en **Angular 18+**

---

## 📋 Requisitos Previos

Antes de comenzar, asegúrate de tener instalados en tu máquina los siguientes componentes:

* **.NET 10 SDK** 
* **Node.js** (v18.x o v20.x LTS) y **npm**.
* **Angular CLI** instalado globalmente (`npm install -g @angular/cli`).
* **SQL Server** local (SQL Server Express, LocalDB o una instancia accesible).

---

## 🚀 Guía de Ejecución Paso a Paso 

### 1. Clonar o descargar el repositorio
Abre una terminal y navega hasta la carpeta raíz donde se encuentra el proyecto:
```bash
git clone <URL_DEL_REPOSITORIO>
cd ProductManager
```

---

### 2. Configurar y levantar el Backend (.NET)

1. **Configurar la cadena de conexión:**
   Abre el archivo `src/ProductManager.API/appsettings.json` y verifica que la propiedad `DefaultConnection` apunte a tu servidor de SQL Server local:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=ProductsDb;Trusted_Connection=True;TrustServerCertificate=True;"
     }
   }
   ```
   *(Si utilizas LocalDB, puedes usar `Server=(localdb)\\mssqllocaldb;Database=ProductsDb;Trusted_Connection=True;TrustServerCertificate=True;`)*.

2. **Restaurar paquetes de la solución:**
   En la terminal, dentro de la raíz de la solución, ejecuta:
   ```bash
   dotnet restore
   ```

3. **Ejecutar la API:**
   Inicia la aplicación Web API ejecutando:
   ```bash
   dotnet run --project src/ProductManager.API
   ```

   > **Nota:** Al arrancar la aplicación, el método `dbContext.Database.Migrate()` configurado en `Program.cs` creará automáticamente la base de datos `ProductsDb` y aplicará las migraciones de Entity Framework Core pendientes.

4. **Verificar la API en el navegador:**
   Una vez iniciada la consola, abre tu navegador e ingresa a la documentación interactiva:
   * **Swagger UI:** `http://localhost:5000/swagger` (o el puerto expuesto en consola).

---

### 3. Configurar y levantar el Frontend (Angular)

1. **Navegar a la carpeta del proyecto Angular:**
   Abre una segunda terminal (dejando la API corriendo) y ejecuta:
   ```bash
   cd ProductManager-UI
   ```

2. **Instalar dependencias de Node:**
   ```bash
   npm install
   ```

3. **Verificar la URL del backend:**
   Asegúrate de que la variable `apiUrl` en el archivo `src/app/services/product.service.ts` coincida con la ruta y puerto donde corre tu Web API:
   ```typescript
   private apiUrl = 'http://localhost:5000/api/products';
   ```

4. **Iniciar el servidor de desarrollo:**
   ```bash
   ng serve --open
   ```

5. **Acceder a la aplicación:**
   Se abrirá automáticamente el navegador en `http://localhost:4200` desde donde podrás listar, crear y eliminar productos.

---

## 🔄 Propuesta de Pipeline CI/CD (Azure DevOps)

Para integrar y desplegar automáticamente los cambios de este proyecto hacia la nube, se plantea la siguiente arquitectura en Azure DevOps (`azure-pipelines.yml`):

### 1. Integración Continua (CI)
* **Disparador:** PRs o Commits hacia la rama `main`.
* **Pasos:**
  1. El pipeline restaura los paquetes de NuGet y compila la solución en .NET 10.
  2. Corre los tests unitarios para verificar que nada se rompa.
  3. Arma la imagen de Docker del backend usando el Dockerfile del proyecto.
  4. Sube esa imagen lista a un registro como Azure Container Registry (ACR) con la etiqueta de la versión.

### 2. Despliegue Continuo (CD)
* **Disparador:** Finalización exitosa de la etapa de CI.
* **Pasos:**
  1. Se ejecutan las migraciones pendientes en la base de datos usando dotnet ef database update.
  2. Se toma la nueva imagen guardada en ACR y se despliega directamente en un Azure App Service (o Azure Container Apps).
  3. Se hace una prueba rápida a la URL de la API para confirmar que está respondiendo bien.