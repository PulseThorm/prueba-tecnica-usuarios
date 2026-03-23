# 🔧 Backend - API Usuarios

API REST desarrollada en .NET 8 para la gestión de usuarios.

---

# 🏗️ Tecnologías

- .NET 8
- Entity Framework Core
- SQL Server
- DDD
- CQRS

---

# 🚀 Funcionalidades

- GET usuarios paginados
- POST crear usuario
- PUT editar usuario
- DELETE eliminación lógica

---

# 🧠 Reglas importantes

- Solo una dirección principal por usuario
- No se editan ni eliminan direcciones existentes
- Solo se agregan nuevas direcciones en edición
- Eliminación lógica (IsActive)

---

# ⚙️ Ejecución

1. Configurar `appsettings.json`
2. Ejecutar:

```bash
dotnet run
````

Swagger:

```text
http://localhost:5227/swagger
```

---

# ⚠️ Notas

* CORS habilitado para frontend Angular
* Servicio de email simulado (no disponible en local)

````

---
