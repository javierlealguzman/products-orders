# Products Api

Proyecto chico full stack siguiente arquitectura limpia para la creacion de apis y consumo de estas desde un cliente blazor.

---

## Tecnologías

- .Net 8
- Blazor WebAssembly

---

## Instalación

Clona el repositorio:

```bash
git clone https://github.com/javierlealguzman/products-orders.git
```
Dentro del repositorio se encontraran 2 folders, backend y frontend con sus respectivos proyectos.

Dentro de backend, modifica el archivo appsettings.json para agregar las urls necesarias para los proveedores externos asi como los headers requeridos

```bash
"Providers": {
    "CazaPagos": {
      "Url": "<url-de-tu-cliente-aqui>",
      "Headers": {
        "x-api-key": "<header-de-tu-cliente-aqui>"
      }
    },
    "PagoFacil": {
      "Url": "<url-de-tu-cliente-aqui>",
      "Headers": {
        "x-api-key": "<header-de-tu-cliente-aqui>"
      }
    }
  }
```

Esta es toda la configuracion necesaria para que puedas correr ambas aplicaciones sin ningun problema.

## Autenticacion
El cliente que esta construido en blazor requiere de un usuario para poder iniciar sesion y posteriormente navegar a travez de sus diferentes paginas.

Utiliza el siguiente usuario y contraseña para iniciar sesion:
- Usuario: admin@system.com
- Contraseña: admin@123

Tambien podras encontrar los datos en el seeder que se encuentra dentro del proyecto backend.
