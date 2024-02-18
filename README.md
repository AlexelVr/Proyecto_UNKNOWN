## Presentacion

Este proyecto individual ha sido realizado durante el primer año del Grado Superior de Animación 3D y videojuegos.    

## Contenido del repositorio

Este repositorio contiene los scripts principales de dicho proyecto indidvual.
A continuación, se explicará brevemente en qué consiste cada script, junto con todas las funciones, componentes y variables.
Para conservar el orden dentro del proyecto se han creado varias carpetas, de manera que los scripts se puedan clasificar según su finalidad.

## ENEMY: 

*EnemyMovement:* El script de “EnemyMovement” consiste en un movimiento que utiliza el componente RigidBody para desplazarse en el plano XY. En cuanto a las variables utilizadas, he decidido crear “Headers” para clasificarlas según su uso. Las variables que he dejado públicas són variables que se asignan desde el inspector a conveniencia, cosa que implica más versatilidad para poder utilizar el mismo Script para más enemigos. Se pueden retocar parámetros como la fuerza de salto o la aceleración y deceleración del enemigo.

[EnemyMovement](Scripts/Enemy/EnemyMovement.cs) 

*EnemyStateController:* El script de “EnemyStateController” se asemeja a  una máquina de estados que funciona con ayuda del Animator. El enemigo cuenta con tres estados funcionales: Idle, Patrol y Chase.

El enemigo funciona de la siguiente manera: cuando se inicia el ejecutable, el enemigo empieza desde el estado de Idle durante un tiempo determinado  (que se puede configurar en el Inspector) y cuando se alcanza ese tiempo, se cambia al estado de Patrol y el enemigo empieza a moverse. Para cambiar de estado, se utiliza la función “ChangeState”, que cambia el estado actual por uno nuevo. Estando en el estado de Patrol, el enemigo avanzará hasta la pared o abismo que haya en la dirección que avanzó (normalmente izquierda).

El enemigo puede detectar las paredes y abismos mediante un sistema de Raycasts que funciona de la siguiente manera: un Raycast apuntando al suelo desplazado X unidades del personaje en la dirección que esté mirando, un Raycast apuntando desde el personaje hasta X unidades hacia delante situado justo a la altura que puede saltar y otro Raycast paralelo a este situado un poco más arriba (de manera que si se activan los dos, no puede saltar, pero si se activa solamente uno, puede saltar).

Por último, el script cuenta con un gizmos que genera un área circular que detecta al jugador para cambiar de estado a Chase, en el que persigue al jugador hasta que salga de ese rango. En el momento en el que sale del rango, un contador se inicia hasta que transcurren X segundos (configurable en el inspector). Durante ese tiempo, el enemigo estaŕa buscando al jugador en la última dirección que se guardó en la variable “setDirection”. Una vez pase ese tiempo, el estado de enemigo regresará a Patrol.

El script de “ReturnChase” sirve para cambiar al estado de Chase, y está separado del script principal porque fué lo último que añadí, pero podría estar perfectamente dentro del script de “EnemyStateController”.

[EnemyStateController](Scripts/Enemy/EnemyStateController.cs)

[ReturnChase](Scripts/Enemy/ReturnChase.cs)

## PLAYER: 

*PlayerData:* El script de “PlayerData” almacena todos los parámetros relacionados con el jugador que puedan variar en el gameplay de un videojuego. Algunos ejemplos serían la vida, la resistencia, los coleccionables, etc. Además de eso, también se encarga de gestionar los puntos de control y el “respawn” del jugador, de manera que cuando la vida sea 0 o inferior, el jugador regrese al último punto de control por el que haya pasado.

[PlayerData](Scripts/Player/PlayerData.cs)

*PlayerMovement:* El script de “PlayerMovement” es el script más complejo de este proyecto. Este script se encarga de gestionar el movimiento del jugador utilizando el componente RigidBody, y este se realizará mediante Inputs (botones) que se pueden cambiar en cualquier momento (programación / Input Manager). El personaje es capaz de caminar, saltar y ejecutar acelerones presionando cierta tecla. En cuanto a las variables utilizadas, al ser demasiadas y para conservar el orden y limpieza en el script, se han separado de la misma forma que el Enemigo (mediante “Headers”). La mayoría de variables que son públicas se pueden cambiar en el inspector a conveniencia, aunque también se le pueden asignar valores que sean predeterminados por programación.
 
Se puede observar que la función de “Update” ejecuta varias funciones a la vez. Esto se hace con el objetivo de facilitar la lectura del código al programa, así como también para estructurar el código.
La función de “Start” se asegura de que al comenzar el juego, el personaje esté mirando hacia la derecha y de que la gravedad se aplica correctamente. 
La función de “GetInputs” se encarga de asignar acciones a las correspondientes teclas (o botones si hubiera).
La función de “SetAnimatorParameters” sirve para que el “Animator” pueda ejecutar las animaciones del personaje mediante condiciones. 
La función “Movement” se utiliza para juntar el movimiento horizontal y el vertical, además de asignar una dirección para aplicar la velocidad sobre el Rigidbody.
Las funciones de “Direction” y “Flip” se encargan de que los sprites se den la vuelta cuando el jugador cambia la dirección en la que se dirige.
La función de “HorizonalMovement” permite que el jugador se pueda desplazar en el eje Horizontal a una velocidad determinada por dos variables: “playerAcceleration” y “playerDeceleration” (pueden ser cambiadas en el inspector). 
La función de VerticalMovement” permite al jugador desplazarse en el eje Vertical al ejecutar un salto. Este salto varía dependiendo del tiempo que el jugador pulse la tecla de salto, y depende de dos variables: jumpForce e isGrounded (ambas pueden ser asiganadas en el inspector o mediante programación). La variable jumpForce determina (como su nombre indica) la fuerza de salto del jugador, mientras que la variable isGrounded comprueba si el jugador está en contacto con el suelo y, en el caso de que no esté en el suelo, no se podrá ejecutar la acción de saltar (para que no se pueda saltar infinitamente). La fuerza de gravedad se encargará de que el personaje vuelva al suelo. 
Por último, la función de “GroundCheck” generará un círculo cuya función es detectar la layer “Ground” que tiene el suelo. Como este círculo no es visible, la función “OnDrawGizmosSelected” se encargará de dibujar un círculo colocado en el mismo punto, solucionando así el problema.

[PlayerMovement](Scripts/Player/PlayerMovement.cs)

## SCENE SETTINGS:

*CameraFollow:* El script de “CameraFollow” se encarga de hacer que la cámara siga al jugador. Para ello, se han utilizado dos vectores: “targetPosition” y “followPosition” . El vector “targetPosition” siempre apunta a la posición del jugador, mientras que el vector ”followPosition” utiliza un Slerp para que la posición de la cámara se aproxime a la del jugador mediante un movimiento más suavizado.

[CameraFollow](Scripts/SceneSettings/CameraFollow.cs)

*SceneController:* El script de SceneController” se encarga de ejecutar las diferentes escenas que se puedan plantear en el diseño de Interfaz (UX y UI). En este caso, se haría uso de la herramienta “SceneManager” para controlar dicho cambio de escena, pasando únicamente el nombre de la escena a este script.

[SceneController](Scripts/SceneSettings/SceneController.cs)

*FinalScene:* El script de “FinalScene” se encarga de ejecutar una escena añadida al final del juego. El contenido de este script se puede añadir al “SceneController” perfectamente.

[FinalScene](Scripts/SceneSettings/FinalScene.cs)

## WORLD MECHANICS: 

*-----Collectables-----*

*CoinSystem:* El script de “CoinSystem” se le asigna a la moneda que tenga el juego. Se pueden hacer varias monedas con distinto valor cambiando la variable “coinValue” desde el inspector.

[CoinSystem](Scripts/WorldMechanics/Collectionables/CoinSystem)

*KeySystem:* El script de “KeySystem” funciona de manera similar al de “CoinSystem”, de manera que un contador aumenta según el valor asignado a la variable “keyValue”.

[KeySystem](Scripts/WorldMechanics/Collectionables/KeySystem)

*-----DetectionSystem------*

*Checkpoint:* El script de “Checkpoint” se le asigna a los puntos de control del mapa. Funciona de manera que cuando el jugador entra a un “trigger” (zona de detección).

[Checkpoint](Scripts/WorldMechanics/DetectionSystem/Checkpoint)

*TriggerDetect:* El script de “TriggerDetect” se le asigna a las puertas, de manera que cuando el jugador haya recolectado X cantidad de un objeto en concreto y entre en contacto con dicha zona, la puerta desaparece.

[TriggerDetect](Scripts/WorldMechanics/DetectionSystem/TriggerDetect)

*-----Hazards-----*

*TriggerDamage:* El script de “TriggerDamage” se le asigna a los objetos que tengan la capacidad de dañar al jugador. Este script consta de una variable que determina si el daño es constante o por el contrario solo resta X puntos de vida. Por lo tanto, si se decide no activar esta “bool” en el inspector, el objeto hará X puntos de daño más un daño adicional que será constante y se hará cada segundo (Time.deltaTime).

[TriggerDamage](Scripts/WorldMechanics/Hazards/TriggerDamage)

*-----Mechanism-----*

*PlatformSystem:* El script de “PlatformSystem” se encarga del movimiento de las plataformas. En el inspector se deberán asignar un objeto (que sirva como plataforma) y dos puntos, de manera que la plataforma viajará desde el punto A hacia el punto B y viceversa (este ciclo se repite constantemente). Al ejecutar el juego, se asigna un “target” (que será uno de los puntos) y  la función “PlatformMovement” se encargará de calcular el vector que debe seguir para llegar a dicho punto. Utilizando “MoveTowards”, la plataforma se irá desplazando hasta llegar al “target”. Al llegar al objetivo, el “target” cambia al otro punto, con lo cual la plataforma se dirigirá a su siguiente destino.

[PlatformSystem](Scripts/WorldMechanics/Mechanism/PlatformSystem)

## Licencia
MIT
