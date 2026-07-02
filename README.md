# Labyrinth Runner

**Время на разработку:** _5 часов в сумме_
[Releases](https://github.com/sozooo/labyrinth-runner/releases) содержит исходный код и запускаемый билд игры.

## Управление

- **WASD** — передвижение
- **Mouse** — поворот камеры

## Зависимости

- [Extenject](https://github.com/Mathijs-Bakker/Extenject) — DI-контейнер
- [MessagePipe](https://github.com/Cysharp/MessagePipe) — in-process pub/sub
- [GameStateMachine](https://github.com/sozooo/game-state-machine) — управление состояниями игры
  (включает [C-Sharp-Promise](https://github.com/Real-Serious-Games/C-Sharp-Promise))

## Архитектура

Проект разделён на 4 asmdef-сборки:
- **Application.Core** — конфиги, интерфейсы сервисов, сообщения (0 зависимостей)
- **Application.Gameplay** — игрок, враги, бриллианты, триггеры (→ Core)
- **Application.GameFlow** — состояния игры, UI (→ Core, Gameplay)
- **Infrastructure** — установщики, InputSystem, SceneLoader (→ всё)

### Ключевые классы

- `BootstrapInstaller` + подустановщики — DI-композиция (Extenject)
- `GameStateMachine` — управление состояниями: `InitState → MenuState → GameplayState → WinState/LoseState`
- `PlayerMovement` — движение через `CharacterController.Move()`
- `PlayerCamera` — следование камеры за игроком
- `EnemyBehaviour` + `EnemyStateMachine` — FSM врага (Chase/Patrol/Search/Idle)
- `UISwitcher` — переключение UI-панелей

### Логика победы/поражения

`Application.Gameplay.Trigger.ExitTrigger` → публикует `ExitReachedMessage`

`Application.Gameplay.Enemy.EnemyBehaviour` → публикует `PlayerDiedMessage`

`Application.GameFlow.States.GameplayState` подписан на оба сообщения и переключает:
- `WinState` → показ `WinPanel`
- `LoseState` → показ `LosePanel`

### Логика сбора бриллиантов

- `DiamondSpawner` — случайный спавн (Fisher-Yates shuffle)
- `DiamondCollector` — триггер подбора, увеличение счётчика
- `DiamondUICounter` — обновление UI (TextMeshPro)
- Когда все собраны — `Door` открывается через `DoorOpener`

### Логика врагов

- `EnemySpawner` — случайный спавн (Fisher-Yates)
- `EnemyBehaviour` — создаёт состояния через `IStateFactory` (DI, без Activator)
- Состояния:
  - `EnemyChaseState` — преследование + `PushAway` игрока
  - `EnemyPatrolState` — патруль по точкам
  - `EnemySearchState` — поиск игрока
  - `EnemyIdleState` — ожидание
