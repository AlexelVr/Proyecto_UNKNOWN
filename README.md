## Presentacion

Este proyecto individual ha sido realizado durante el primer año del Grado Superior de Animación 3D y videojuegos.    

## Contenido del repositorio

Este repositorio contiene los scripts principales de dicho proyecto indidvual.
A continuación, se explicará brevemente en qué consiste cada script, junto con todas las funciones, componentes y variables.
Para conservar el orden dentro del proyecto se han creado varias carpetas, de manera que los scripts se puedan clasificar según su finalidad.

## ENEMY: Esta carpeta incluye los scripts relacionados con la configuración del enemigo.

EnemyMovement: El script de “EnemyMovement” consiste en un movimiento que utiliza el componente RigidBody para desplazarse en el plano XY. En cuanto a las variables utilizadas, he decidido crear “Headers” para clasificarlas según su uso. Las variables que he dejado públicas són variables que se asignan desde el inspector a conveniencia, cosa que implica más versatilidad para poder utilizar el mismo Script para más enemigos. Se pueden retocar parámetros como la fuerza de salto o la aceleración y deceleración del enemigo.
[EnemyMovement](Scripts/Enemy/EnemyMovement.cs) 
EnemyStateController: El script de “EnemyStateController” se asemeja a  una máquina de estados que funciona con ayuda del Animator. El enemigo cuenta con tres estados funcionales: Idle, Patrol y Chase.

El enemigo funciona de la siguiente manera: cuando se inicia el ejecutable, el enemigo empieza desde el estado de Idle durante un tiempo determinado  (que se puede configurar en el Inspector) y cuando se alcanza ese tiempo, se cambia al estado de Patrol y el enemigo empieza a moverse. Para cambiar de estado, se utiliza la función “ChangeState”, que cambia el estado actual por uno nuevo. Estando en el estado de Patrol, el enemigo avanzará hasta la pared o abismo que haya en la dirección que avanzó (normalmente izquierda).

El enemigo puede detectar las paredes y abismos mediante un sistema de Raycasts que funciona de la siguiente manera: un Raycast apuntando al suelo desplazado X unidades del personaje en la dirección que esté mirando, un Raycast apuntando desde el personaje hasta X unidades hacia delante situado justo a la altura que puede saltar y otro Raycast paralelo a este situado un poco más arriba (de manera que si se activan los dos, no puede saltar, pero si se activa solamente uno, puede saltar).

Por último, el script cuenta con un gizmos que genera un área circular que detecta al jugador para cambiar de estado a Chase, en el que persigue al jugador hasta que salga de ese rango. En el momento en el que sale del rango, un contador se inicia hasta que transcurren X segundos (configurable en el inspector). Durante ese tiempo, el enemigo estaŕa buscando al jugador en la última dirección que se guardó en la variable “setDirection”. Una vez pase ese tiempo, el estado de enemigo regresará a Patrol.

El script de “ReturnChase” sirve para cambiar al estado de Chase, y está separado del script principal porque fué lo último que añadí, pero podría estar perfectamente dentro del script de “EnemyStateController”.

[EnemyStateController](Scripts/Enemy/EnemyStateController.cs)

[ReturnChase](Scripts/Enemy/ReturnChase.cs)

## PLAYER: Esta carpeta incluye los scripts relacionados con la configuración del jugador.

PlayerData:
[PlayerData](Scripts/Player/PlayerData.cs)
PlayerMovement:
[PlayerMovement](Scripts/Player/PlayerMovement.cs)

## SCENE SETTINGS: Esta carpeta incluye los scripts relacionados con la configuración de las escenas.

CameraFollow:
[CameraFollow](Scripts/SceneSettings/CameraFollow.cs)
SceneController:
[SceneController](Scripts/SceneSettings/SceneController.cs)
FinalScene:
[FinalScene](Scripts/SceneSettings/FinalScene.cs)

## WORLD MECHANICS: Esta carpeta incluye los scripts relacionados con los elementos del escenario.

-----Collectables-----

CoinSystem:
[CoinSystem](Scripts/WorldMechanics/Collectionables/CoinSystem)
KeySystem:
[KeySystem](Scripts/WorldMechanics/Collectionables/KeySystem)

-----DetectionSystem------

Checkpoint:
[Checkpoint](Scripts/WorldMechanics/DetectionSystem/Checkpoint)
TriggerDetect:
[TriggerDetect](Scripts/WorldMechanics/DetectionSystem/TriggerDetect)

-----Hazards-----

TriggerDamage:
[TriggerDamage](Scripts/WorldMechanics/Hazards/TriggerDamage)

-----Mechanism-----

PlatformSystem:
[PlatformSystem](Scripts/WorldMechanics/Mechanism/PlatformSystem)

## Unknown

CONTENIDO DEL VIDEOJUEGO (Resumen Wiki)

## Licencia
MIT
