# 🎨 Frontend - Administración de Usuarios

Aplicación frontend desarrollada en Angular 17 para consumir la API de usuarios.

---

# 🏗️ Tecnologías

- Angular 17
- Bootstrap 5
- SweetAlert2
- TypeScript

---

# 🚀 Funcionalidades

- Listado de usuarios con paginación
- Creación de usuarios con direcciones dinámicas
- Edición de usuario
- Eliminación con confirmación
- Manejo de dirección principal

---

# ⚙️ Ejecución

1. Instalar dependencias:

```bash
npm install
````

2. Ejecutar:

```bash
npm start
```

Aplicación disponible en:

```text
http://localhost:4200
```

---

# 🔗 Configuración de API

Archivo:

```text
src/app/services/user.service.ts
```

```ts
private apiUrl = 'http://localhost:5227/api/User';
```

---
