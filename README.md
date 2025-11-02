# 🐾 Proyecto Veterinaria 

## 👨‍💻 Integrantes
- **Juan José Muñoz Agudelo**  
- **Federico Ospina**  
- **Juan Manuel Zapata Urrego**

---

## ⚙️ Ejecución del Proyecto
Despues de clonar el repsitorio,

Para poder ejecutar con éxito el proyecto, debe **cambiar la cadena de conexión** según su servidor de **SQL Server**.

En este caso se utilizó la siguiente conexión:

"ConnectionStrings": {
    "DefaultConnection": "Server=LAPTOP-8E5R31UH\\SQLEXPRESS;Database=VeterinariaDB;Trusted_Connection=True;TrustServerCertificate=True;"
}

Luego, desde la **Consola del Administrador de Paquetes** en Visual Studio, ejecute el siguiente comando:

Update-Database 

Este comando generará la base de datos **VeterinariaDB** en la computadora local mediante **Entity Framework**, permitiendo correr el proyecto de manera satisfactoria.

