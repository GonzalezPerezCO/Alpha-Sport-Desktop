# PRUEBAS UNITARIAS


## CAPA DE CONEXIÓN
- Sí, la conexión está abierta, al abrirla de nuevo debe fallar.
- Sí, la conexión está abierta y con los datos correctos, debe permitir hacer consultas.
- Sí, la conexión está sin iniciar, debe fallar al hacer consultas.
- Sí, la conexión está cerrada, debe fallar al hacer consultas.
- Sí, la conexión está cerrada, debe fallar si intenta cerrarla.

## GIMNASIO

- Sí, el estudiante no está registrado, no debe poder crear horario.
- Sí, el estudiante está bloqueado, no debe poder crear horario.
- Sí, el estudiante no tiene horario debe poder agregar máximo tres dias diferentes.
- Sí, el estudiante tiene horario debe poder ver que días tiene su horario.
- Sí, el estudiante acumula 3 fallas consecutivas, debe quedar bloqueado.
- Si, el estudiante no acepta el reglamento, no debe poder crear horario.

## DEPORTES



## CASILLEROS
Si, el estudiante tiene un casillero registrado, no debe poder registrar otro.
Si, el estudiante no tiene casillero registrado, puede elegir uno de la sección de hombres.
