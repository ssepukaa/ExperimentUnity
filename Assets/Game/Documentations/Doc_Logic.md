# Проект: Game Controller

## Структура проекта

### Папки и файлы

- **Assets/Game/Scripts/Boot**
  - `BootCore.cs`: Синглтон, обеспечивающий сохранение объекта BootCore при загрузке новой сцены.

- **Assets/Game/Scripts/Data**
  - `GameConfig.cs`: ScriptableObject, содержащий настройки игры, включая конфигурации уровней.

- **Assets/Game/Scripts/Editor**
  - `ScriptsCollector.cs`: Скрипт для объединения всех скриптов в один файл.

- **Assets/Game/Scripts/GameC**
  - `GameController.cs`: Основной контроллер игры, управляющий состояниями игры и контроллерами.
  - `GameModel.cs`: Модель игры, наследующая от BaseModel.
  - `IGame.cs`: Интерфейс для игры.

- **Assets/Game/Scripts/GameC/GameServices**
  - `BaseGameService.cs`: Абстрактный базовый класс для игровых сервисов.
  - `ControllerFactoryService.cs`: Сервис для создания и управления контроллерами.
  - `LevelGenerationService.cs`: Сервис для генерации начальных моделей уровней.
  - `RandomService.cs`: Сервис для генерации случайных чисел.
  - `SaveLoadService.cs`: Сервис для сохранения и загрузки данных игры.
  - `IGameService.cs`: Интерфейс для игровых сервисов.

- **Assets/Game/Scripts/GameC/GameStates**
  - `BaseGameState.cs`: Абстрактный базовый класс для состояний игры.
  - `GameplayGameState.cs`: Состояние игрового процесса.
  - `LoadingGameState.cs`: Состояние загрузки игры, в котором происходит загрузка или генерация моделей.
  - `MenuGameState.cs`: Состояние меню игры.
  - `NewGameState.cs`: Состояние новой игры.
  - `PausedGameState.cs`: Состояние паузы в игре.

- **Assets/Game/Scripts/MicrobeC**
  - `MicrobeController.cs`: Контроллер микроба.
  - `MicrobeModel.cs`: Модель микроба, наследующая от BaseModel.
  - `MicrobeView.cs`: View микроба, наследующий от BaseView.

- **Assets/Game/Scripts/PlayerC**
  - `PlayerController.cs`: Контроллер игрока.
  - `PlayerModel.cs`: Модель игрока, наследующая от BaseModel.
  - `PlayerView.cs`: View игрока, наследующий от BaseView.

- **Assets/Game/Scripts/Bases/BaseControllers**
  - `BaseController.cs`: Абстрактный базовый класс для всех контроллеров, реализующий интерфейс IController.
  - `IController.cs`: Интерфейс для контроллеров.

- **Assets/Game/Scripts/Bases/BaseModels**
  - `BaseModel.cs`: Абстрактный базовый класс для всех моделей, реализующий интерфейс IModel.
  - `IModel.cs`: Интерфейс для моделей.

- **Assets/Game/Scripts/Bases/BaseViews**
  - `BaseView.cs`: Абстрактный базовый класс для всех представлений (View), реализующий интерфейс IView.
  - `IView.cs`: Интерфейс для представлений.

- **Assets/Game/Scripts/Bases/Interfaces**
  - `IFactory.cs`: Интерфейс фабрики.
  - `IFixedUpdatable.cs`: Интерфейс для объектов, обновляемых в FixedUpdate.
  - `IGameState.cs`: Интерфейс для состояний игры.
  - `ILateUpdatable.cs`: Интерфейс для объектов, обновляемых в LateUpdate.
  - `IUpdatable.cs`: Интерфейс для объектов, обновляемых в Update.

## Алгоритм работы приложения

### 1. BootCore.Awake()
- Класс `BootCore` является точкой входа инициализации приложения.
- Проверка наличия существующего экземпляра `BootCore`. Если нет - создание и сохранение, если есть - уничтожение нового объекта.

### 2. GameController.Awake()
- Класс `GameController` инициализируется.
- Проверка наличия существующего экземпляра `GameController`. Если нет - создание и сохранение, если есть - уничтожение нового объекта.

### 3. GameController.Start()
- Метод `Start()` инициализирует состояния игры и сервисы.
- `InitializeGameStates()`: Инициализация состояний игры (загрузка, игровой процесс, пауза).
- `InitializeGameServices()`: Инициализация игровых сервисов (сохранение/загрузка, генерация уровней и т.д.).
- `ChangeGameState<LoadingGameState>(): Переход в состояние загрузки игры.

### 4. LoadingGameState.Enter()
- Вход в состояние загрузки игры.
- Вызов метода `LoadOrGenerateModels()`: Загрузка сохраненных данных или генерация новых моделей.

### 5. LoadingGameState.LoadOrGenerateModels()
- Попытка загрузки моделей с использованием `SaveLoadService.Load()`.
- Если модели не загружены (нет сохранений), вызов `LevelGenerationService.GenerateInitialModels()` для генерации новых моделей.
- Создание новых контроллеров на основе загруженных или сгенерированных моделей: `GameController.CreateNewListControllers(models)`.
- Переход в состояние игрового процесса: `GameController.ChangeGameState<GameplayGameState>()`.

### 6. GameplayGameState.Enter()
- Вход в состояние игрового процесса.

### 7. GameController.Update()
- Метод `Update()` вызывается каждый кадр.
- Выполнение логики текущего состояния игры: `_currentGameState.Execute()`.
- Вызов `RunControllers()`: Обновление всех контроллеров, поддерживающих интерфейс `IUpdatable`.

### 8. GameController.RunControllers()
- Получение всех контроллеров, поддерживающих `IUpdatable`, из `ControllerFactoryService`.
- Вызов метода `Run()` у каждого из этих контроллеров.

### 9. GameController.FixedUpdate()
- Метод `FixedUpdate()` вызывается каждый фиксированный кадр.
- Вызов `FixedRunControllers()`: Обновление всех контроллеров, поддерживающих интерфейс `IFixedUpdatable`.

### 10. GameController.FixedRunControllers()
- Получение всех контроллеров, поддерживающих `IFixedUpdatable`, из `ControllerFactoryService`.
- Вызов метода `RunFixed()` у каждого из этих контроллеров.

### 11. GameController.LateUpdate()
- Метод `LateUpdate()` вызывается после метода `Update()`.
- Вызов `LateRunControllers()`: Обновление всех контроллеров, поддерживающих интерфейс `ILateUpdatable`.

### 12. GameController.LateRunControllers()
- Получение всех контроллеров, поддерживающих `ILateUpdatable`, из `ControllerFactoryService`.
- Вызов метода `RunLate()` у каждого из этих контроллеров.

## Пример сценария
- Вызов метода `GameController.ChangeGameState<PausedGameState>()` для перехода в состояние паузы.
- **PausedGameState.Enter()**
  - Вход в состояние паузы.
  - Остановка времени в игре: `Time.timeScale = 0`.

- **PausedGameState.Exit()**
  - Выход из состояния паузы.
  - Восстановление времени в игре: `Time.timeScale = 1`.

## Обработка сохранения и загрузки
- **SaveLoadService.Save(List<IController> controllers)**
  - Сохранение текущих моделей всех контроллеров в JSON-файл.

- **SaveLoadService.Load()**
  - Загрузка моделей из JSON-файла и восстановление их в виде списка моделей.
