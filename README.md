# 🧑‍💻 Módulo de Administración de Usuarios

Este proyecto implementa un módulo CRUD de usuarios como parte de una prueba técnica.

La solución está compuesta por:

- Backend en .NET 8 con arquitectura DDD + CQRS
- Frontend en Angular 17

---

# 🏗️ Tecnologías utilizadas

## Backend
- .NET 8
- Entity Framework Core
- SQL Server
- Domain Driven Design (DDD)
- CQRS
- Swagger

## Frontend
- Angular 17 (Standalone Components)
- Bootstrap 5
- SweetAlert2
- Google Material Icons

---

# 🚀 Funcionalidades

- Listado de usuarios paginado (10 por página)
- Creación de usuarios
- Edición de usuarios
- Eliminación lógica (soft delete)
- Gestión de múltiples direcciones
- Selección de dirección principal
- Agregado de nuevas direcciones en edición

---

# 🧠 Reglas de negocio

- FullName y Email son obligatorios
- Solo puede existir una dirección principal
- Si no se define una principal, se asigna automáticamente
- En edición:
  - No se modifican ni eliminan direcciones existentes
  - Solo se agregan nuevas
  - Se puede cambiar la dirección principal
- Eliminación lógica (IsActive = false)
- Solo se listan usuarios activos

---

# 📂 Estructura del proyecto

```text
/API    → Backend .NET 8
/Front  → Aplicación Angular 17
````

---

# ⚙️ Cómo ejecutar

## Backend

1. Configurar conexión en `appsettings.json`
2. Ejecutar:

```bash
dotnet run
```

Swagger disponible en:

```text
http://localhost:5227/swagger
```

---

## Frontend

1. Ir a carpeta Front:

```bash
cd Front
```

2. Instalar dependencias:

```bash
npm install
```

3. Ejecutar:

```bash
npm start
```

Aplicación disponible en:

```text
http://localhost:4200
```

---

# ⚠️ Consideraciones

* CORS configurado para `http://localhost:4200`
* Se deshabilitó `UseHttpsRedirection` para desarrollo local
* El servicio de envío de email no está disponible en entorno local, pero no bloquea la creación

---

# 🎯 Decisiones técnicas

* Se respetaron las reglas de negocio del backend en el frontend
* Se evitó DataTables ya que la paginación es responsabilidad del backend
* Se utilizó SweetAlert2 para confirmaciones
* Se separó la lógica de API en servicios

---

# 📌 Posibles mejoras

* Agregar metadata de paginación (totalCount)
* Separar componentes en Angular
* Validaciones más robustas en frontend
* Manejo global de errores HTTP

---

# 👤 Autor

Felipe León

````

---

# ⚠️ Notas

* El frontend depende del backend corriendo en localhost
* Se utilizan standalone components
* Se respeta la lógica de negocio definida en backend

```

---
