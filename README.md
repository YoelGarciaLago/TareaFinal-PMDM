# TareaFinal-PMDM

## Explicación Scripts 

### PlayerController.cs


#### Descripción
Este script gestiona el movimiento del jugador, los estados de animación, y las interacciones con el entorno, como recoger objetos o enfrentarse a enemigos.

 #### Características principales
- Movimiento: Control mediante teclas o acelerómetros (para móviles).
- Estados del jugador:
  -- Quieto: Jugador detenido, color rojo.
  --Moviéndose: Jugador en movimiento, color azul.
  --Volando: Jugador a una altura de ≥ 0.6, color verde.
- Interacciones:
  --PickUp: Recolecta objetos, aumenta el contador.
  --Enemy y Enemy2: Derrota al jugador si se contacta.
  --Muro: Se desactiva tras recolectar suficientes objetos.
  --Suelo: Si el jugador cae, muestra un mensaje de derrota.
- Cambio de nivel: Al recoger 9 objetos, pasa al siguiente nivel activando nuevos enemigos.
#### Funciones principales
- OnMove(InputValue movementValue): Captura la entrada de movimiento.
- FixedUpdate(): Mueve al jugador y cambia su estado en función de la altura o velocidad.
- OnTriggerEnter(Collider other): Maneja las interacciones con objetos etiquetados.
- SetCountText(): Actualiza el texto del contador y controla la transición de nivel.
- IsMoving(): Verifica si el jugador se está moviendo.
- IsIdle(): Comprueba si el jugador está quieto.
- IsAtCertainHeight(): Detecta si el jugador está a una altura de vuelo.
- currentActionState(string currentState): Cambia el color del jugador según su estado.






