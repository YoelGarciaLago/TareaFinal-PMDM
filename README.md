# TareaFinal-PMDM

## Explicación Scripts 

### PlayerController.cs


#### Descripción
Este script gestiona el movimiento del jugador, los estados de animación, y las interacciones con el entorno, como recoger objetos o enfrentarse a enemigos.

 #### Características principales
- Movimiento: Control mediante teclas o acelerómetros (para móviles).
- Estados del jugador:
    - Quieto: Jugador detenido, color rojo.
    - Moviéndose: Jugador en movimiento, color azul.
    - Volando: Jugador a una altura de ≥ 0.6, color verde.
- Interacciones:
    - PickUp: Recolecta objetos, aumenta el contador.
    - Enemy y Enemy2: Derrota al jugador si se contacta.
    - Muro: Se desactiva tras recolectar suficientes objetos.
    - Suelo: Si el jugador cae, muestra un mensaje de derrota.
- Cambio de nivel: Al recoger 9 objetos, pasa al siguiente nivel activando nuevos enemigos.
#### Funciones principales
- ´´´OnMove(InputValue movementValue)´´´: Captura la entrada de movimiento.
- ´´´FixedUpdate()´´´: Mueve al jugador y cambia su estado en función de la altura o velocidad.
- ´´´OnTriggerEnter(Collider other)´´´: Maneja las interacciones con objetos etiquetados.
- ´´´SetCountText()´´´: Actualiza el texto del contador y controla la transición de nivel.
- ´´´IsMoving()´´´: Verifica si el jugador se está moviendo.
- ´´´IsIdle()´´´: Comprueba si el jugador está quieto.
- ´´´IsAtCertainHeight()´´´: Detecta si el jugador está a una altura de vuelo.
- ´´´currentActionState(string currentState)´´´: Cambia el color del jugador según su estado.
#### Código destacable
```
AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        string currentStateName = stateInfo.IsName("Quieto") ? "Quieto" :
          stateInfo.IsName("Moviendose") ? "Moviendose" :
          stateInfo.IsName("Volando") ? "Volando" :
          "Estado Desconocido";
```
*Obtiene los nombres de los estados del AnimatorController*

```
foreach (GameObject wazaaa in enemys)
            {
                UnityEngine.AI.NavMeshAgent agent = wazaaa.GetComponent<UnityEngine.AI.NavMeshAgent>();
                if (agent != null)
                {
                    agent.enabled = false; 
                }
            }
            level++;
            if (level == 2)
            {
                count = 0;
                countText.text = "Count: " + count.ToString();
                transform.position = new Vector3(449.65f, 1.667f, -47.43f);
                foreach (var enemy in enemysLvl2)
                {
                    enemy.SetActive(true);
                    UnityEngine.AI.NavMeshAgent agent = enemy.GetComponent<UnityEngine.AI.NavMeshAgent>();
                    if (agent != null)
                    {
                        agent.enabled = true; 
                    }
                }
                
            }
```
*Esto es lo esencial para moverse por los niveles sin tener errores por parte de los agentes*
Los desactivas para que cuando te teletransportes no den errores al no poder acceder al jugador transportado por el otro NavMeshSurface y activas los del Lvl2.
```
void currentActionState(string currentState)
    {
        switch (currentState)
        {
            case "Quieto":
                colores.material.color = Color.red;
                break;
            case "Moviendose":
                colores.material.color = Color.blue;
                break;
            case "Volando":
                colores.material.color = Color.green;
                break;
        }
    }
```
*Dependiendo del state actual en el que se encuentre el jugador, pasa una cosa u otra*

### CameraController.cs

#### Descripción
Este script controla el comportamiento de la cámara, permitiendo alternar entre tres modos de vista: tercera persona, primera persona y vista cenital.

#### Modos de cámara
- Primera persona:
  - La cámara se coloca justo sobre el jugador a una altura específica.
  - Control de la cámara con el ratón para mirar alrededor.
- Tercera persona:
  - La cámara sigue al jugador desde una posición fija con un offset.
- Cenital:
  - La cámara se coloca por encima del jugador, ligeramente desplazada para ver desde un ángulo.
  - La cámara rota automáticamente alrededor del jugador en el eje Y.
#### Características principales
- Cambios de cámara:
  - Tecla F: Alterna entre primera y tercera persona.
  - Tecla C: Activa o desactiva la vista cenital.
- Control del cursor: Bloqueo y desbloqueo del cursor según el modo de cámara.
- Movimiento en primera persona: Permite mover al jugador usando el teclado con control de velocidad.
#### Funciones principales
- ```Start()```:
  - Calcula el offset inicial de la cámara.
  - Bloquea el cursor al iniciar en tercera o primera persona.
- ```Update()```:
  - Cambia entre modos de cámara al presionar F o C.
  - Controla la rotación de la cámara en el modo de primera persona.
- ```LateUpdate()```:
  - Primera persona: Ajusta la posición de la cámara sobre el jugador.
  - Tercera persona: Mantiene la posición relativa inicial respecto al jugador.
  - Cenital: Coloca la cámara sobre el jugador y rota sobre el eje Y.
- ```HandleFirstPersonMovement()```:
  - Gestiona el movimiento del jugador en modo primera persona usando el teclado.

#### Código destacable
```
if (Input.GetKeyDown(KeyCode.F))
        {
            isFirstPerson = !isFirstPerson;
            isCenital = false;  // Asegurarse de que no esté en modo cenital
            Cursor.lockState = isFirstPerson ? CursorLockMode.Locked : CursorLockMode.None;
        }

        // Cambiar al modo cenital cuando se presione la tecla C
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCenital = !isCenital;
            isFirstPerson = false;  // Asegurarse de que no esté en modo primera persona
            Cursor.lockState = CursorLockMode.None;  // Desbloquear el cursor para vista cenital
        }

        if (isFirstPerson)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            yRotation += mouseX;

            transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        }
```
*Código que controla el comportamiento de la cámara*

Con las 2 primeras condiciones miras si, dentro del ```Update()```, se pulsó la tecla, lo que activa las características de cada tipo de cámara y se las pasa al objeto ***(en caso de la 1ra, se pone un booleano a su contrario para comprobarlo luego)***.    
La 3ra condición es la comprobación de si se está usando la 1ra persona y, si es así, pone las características de este tipo de cámara.

```
float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 forward = transform.forward;
        Vector3 right = transform.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 movementDirection = (forward * moveVertical + right * moveHorizontal).normalized;
        playerRigidbody.velocity = movementDirection * movementSpeed + new Vector3(0, playerRigidbody.velocity.y, 0);
```
*Maneja cómo se mueve la cámara en 1ra persona junto al jugador* ***(simplemente para que no haya errores en el movimiento de la cámara y el jugador)***.

```
 if (isFirstPerson)
        {
            transform.position = player.transform.position + new Vector3(0, firstPersonHeight, 0);
        }
        else if (isCenital)
        {
            Vector3 cenitalPosition = player.transform.position + new Vector3(0, cenitalHeight, -cenitalDistance);
            transform.position = cenitalPosition;

            transform.RotateAround(player.transform.position, Vector3.up, rotationSpeed * Time.deltaTime);

            transform.LookAt(player.transform.position);
        }
        else
        {
            transform.position = player.transform.position + offset;
            transform.LookAt(player.transform.position);
        }
```
*En el de la 1ra persona, pone el movimiento al unísono al del jugador. Si es la cenital, opne las características explicadas en el 1er script pero con las características de la cámara cenital* ***(Mirad el 1er código destacable, más concretamente el análisis de la 3ra condición)***. Si no, pone la cámara normal y que siga al jugador.


### RampTrigger Script
#### Descripción
Este script aplica una fuerza a un objeto con el tag "Player" cuando entra en un área de trigger (por ejemplo, una rampa). La fuerza puede configurarse para aplicarse en relación al sistema de coordenadas de la rampa, simulando un impulso direccional.

#### Características principales
- Detección de colisión: Aplica fuerza a objetos que entren en el trigger si tienen el tag "Player".
- Aplicación de fuerza:
  - La fuerza aplicada puede configurarse para alinearse con el sistema de coordenadas del objeto (rampa).
  - Control configurable de la magnitud del impulso desde el inspector de Unity.
  - Depuración: Mensajes de consola para advertir si el objeto no tiene un Rigidbody o si no se maneja la colisión con otros objetos.
- Funciones principales
- ```OnTriggerEnter(Collider other)```:
  - Detecta cuando un objeto entra en el área de trigger.
  - Verifica si el objeto tiene el tag "Player".
  - Si tiene un Rigidbody, aplica la fuerza configurada como impulso.
  - Convierte la fuerza al sistema de coordenadas de la rampa si aplicarFuerzaRelativaRampa es true.
- Parámetros configurables
  - impulseForce: Vector configurable que define la magnitud y dirección del impulso aplicado al objeto.
  - aplicarFuerzaRelativaRampa: Determina si la fuerza se aplica en relación a la orientación de la rampa.

*Hay que tener cuidado para donde se pone la fuerza del impulso, ya que es fácil confundirse de coordenadas y que el impulso no salga a la dirección deseada*. 
 
### EnemyMovement & EnemyMovementLvl2
#### Descripción
Ambos scripts controlan el movimiento de enemigos en un juego utilizando el sistema de navegación de Unity (NavMeshAgent). Los enemigos se mueven automáticamente hacia un objetivo (el jugador). Aunque ambos scripts comparten funcionalidad básica, hay algunas diferencias clave en el manejo del NavMesh y en la forma de moverse hacia el jugador.

#### Funciones principales compartidas
- Movimiento hacia el jugador: Ambos scripts utilizan SetDestination para mover al enemigo hacia la posición del jugador.
- Verificación del NavMesh: Ambos scripts verifican si el agente está en un NavMesh antes de establecer el destino para evitar errores.
- Control de habilitación del NavMeshAgent: Controlan el estado de NavMeshAgent según si el enemigo está en el NavMesh o no.
#### Diferencias clave
1. EnemyMovement
Uso de una corrutina:
  - ```CheckAndMoveToTarget()``` es una corrutina que espera hasta que el agente esté en el NavMesh antes de establecer el destino del jugador.
  - Se verifica cada 0.5 segundos si el agente está en el NavMesh.
- Se deshabilita el NavMeshAgent si el agente no está en el NavMesh.
- Mayor manejo de errores:
  - Si el agente no está en el NavMesh, se desactiva el agente para evitar movimientos indeseados.
2. EnemyMovementLvl2
- Sin corrutina: Este script no utiliza una corrutina para verificar si el agente está en el NavMesh. La verificación se hace directamente en cada Update.
- Mensajes de advertencia: Se muestra un mensaje de advertencia (Debug.LogWarning) si el agente no está en el NavMesh.   

*La necesidad de diferencia radica en la teletransportación que se hace al cambiar de nivel. Usé scripts diferentes para poder depurar mejor los comportamientos de los agentes y poder manejar mejor los errores.* 

### Rotator Script
#### Descripción
Este script controla la rotación continua de un objeto en Unity. La rotación se aplica en los tres ejes (X, Y, Z) y es constante en cada fotograma (Update). Se utiliza para dar un efecto visual dinámico a objetos, como monedas o pickups en un juego.

#### Funciones principales
1. ```Update()```
Rotación constante:
- Rota el objeto según un vector de (15, 30, 45) multiplicado por Time.deltaTime.
- Esto garantiza que la rotación sea suave e independiente de la tasa de fotogramas (FPS).
- La rotación afecta los tres ejes:
  - 15°/s en el eje X
  - 30°/s en el eje Y
  - 45°/s en el eje Z
