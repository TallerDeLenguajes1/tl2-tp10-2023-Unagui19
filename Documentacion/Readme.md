# Documentación Proyecto Final

## Funciones de los usuarios según su rol:

### Admin

El usuario “admin” con relación a:


#### -Usuarios

•Incluso, puede eliminarse a sí mismo, siempre y cuando no sea el único usuario administrador.
•Puede crear usuarios a su antojo.
•No puede modificar su rol de usuario. Para que éste cambie, debe hacerlo otro administrador.

#### -Tableros
•Puede ver, modificar y eliminar cualquier tablero.
•Puede crear tableros y elegir al usuario al que va a estar asociado el mismo

#### -Tareas
•Puede ver, modificar y eliminar cualquier tarea.
•Puede crear una tarea y asignarla a cualquier tablero independientemente de si es propio o no. También puede optar por no asignarle un tablero.

### -Operador
El usuario “Operador” con relación a:

#### -Usuarios
•Puede ver todos los usuarios, pero solo modificar y eliminarse a sí mismo.
•Solo puede modificarse el nombre y la contraseña, no su rol.
•No puede crear usuarios.

#### -Tableros

•Puede ver solo los tableros de los cuales es propietario, es decir que creó y, aquellos que fueron creados por otro usuario con rol de “admin” y se le asigno al usuario operador.
•No tiene la facultad de modificar o eliminar los tableros de los cuales no es propietario.
•Puede crear tableros, los cuales quedan asociados a su usuario (es decir, es el propietario del tablero).
•Puede modificar y eliminar los tableros que creó.


#### -Tareas
•Puede ver todas las tareas: tareas propias (son aquellas tareas que están asociadas a algún tablero perteneciente al usuario), tareas asignadas (aquellas que tienen lo tienen como usuario asignado) y tareas no asignadas (aquellas que no tienen relación con el usuario)

•Los permisos que posee el operador, además de la lectura, de las tareas según su tipo son: 
- Tareas propias: modificación y eliminación de esta.
- Tarea asignada: únicamente puede modificar su estado.
- Tarea no asignada: solo lectura.

•Puede crear tareas y asociarlas a cualquier tablero del cual sea propietario.
•A las tareas puede o no asignarle un usuario.


