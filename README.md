<p align="center">
  <a href="https://dotnet.microsoft.com/" target="_blank">
    <img src="https://upload.wikimedia.org/wikipedia/commons/e/ee/.NET_Core_Logo.svg" width="120" alt=".NET Logo" />
  </a>
</p>

<h1 align="center">🛍️ ApiEcommerce - Backend .NET 8</h1>

<p align="center">
  Backend RESTful desarrollado con <strong>ASP.NET Core 8</strong> y <strong>Entity Framework Core</strong>,  
  que incluye autenticación JWT, patrón Repository, y persistencia en SQL Server 2022 usando Docker.
</p>

---

## 🧩 Tecnologías principales

- **.NET 8.0**
- **Entity Framework Core 8**
- **SQL Server 2022 (Docker)**
- **JWT Authentication**
- **Patrón Repository**
- **Swagger / OpenAPI**
- **Docker Compose**

---

## 🐳 Estructura general

ApiEcommerce/
│
├── ApiEcommerce.csproj
├── Controllers/
├── Models/
├── Repository/
│ ├── ICalificacionRepository.cs
│ └── CalificacionRepository.cs
├── Data/
│ └── ApplicationDbContext.cs
├── appsettings.json
├── docker-compose.yml
└── README.md

## ⚙️ Configuración inicial

### 1️⃣ Requisitos previos

Asegúrate de tener instalado:

- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [.NET SDK 8.0+](https://dotnet.microsoft.com/download)
- [Entity Framework CLI](https://learn.microsoft.com/ef/core/cli/dotnet)
  ```bash
  dotnet tool install --global dotnet-ef

	```
### Este proyecto incluye un archivo docker-compose.yml que levanta un contenedor con SQL Server 2022 listo para usar.
```bash
	docker compose up -d
```

### Esto descargará y levantará una imagen de SQL Server 2022, normalmente accesible en:

localhost:1433
usuario: sa
contraseña: Your_password123

### Puedes verificar que el contenedor está corriendo con:
```bash
docker ps
```
🧱 Migraciones de base de datos

Una vez que el contenedor esté corriendo, puedes aplicar las migraciones de Entity Framework Core:

🔹 Crear una migración (opcional)

Si haces cambios en tus modelos:
```bash
dotnet ef migrations add InitialCreate --project ApiEcommerce
```
🔹 Aplicar las migraciones

Para crear las tablas en la base de datos SQL Server del contenedor:
```bash
dotnet ef database update --project ApiEcommerce
```
✅ Esto ejecutará todas las migraciones y creará las tablas en la BD configurada en

⚡ Ejecutar la API

Inicia el servidor localmente (usando el perfil Development):
```bash
dotnet run --launch-profile https
```

Por defecto, la API estará disponible en:

http://localhost:5176/

Swagger UI: http://localhost:5176/swagger/index.html

🔐 Autenticación y roles

## El sistema usa JWT Tokens para la autenticación.
Los roles principales son:

Admin → Acceso al dashboard de métricas.

Votante → Puede responder una sola vez la encuesta NPS.

Los tokens incluyen los claims id, username, y role.

## 🧪 Endpoints principales
Método	Ruta	Descripción
POST	/api/v1/Auth/login	Inicia sesión y devuelve un JWT

POST 	/api/v1/Auth/Users  registrar usuario y pass

POST	/api/v1/Encuesta/calificar	Registra una calificación (solo rol Votante)

GET	/api/v1/Encuesta/yaRespondio	Verifica si el usuario ya respondió

GET	/api/v1/Encuesta	Devuelve encuestas (uso interno o admin)