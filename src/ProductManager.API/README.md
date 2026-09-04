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