using Config;
using Controllers;
using GameElementsCreation;
using Movement;
using State;
using TestTask222.UserInput;
using Ticks;
using Views;

public class GameBootstrap
{
	private EventBus _eventBus;
	private InputController _inputController;
	private MapLifetimeController _mapLifetimeController;
	private GameFlowController _gameFlowController;
	private TickUpdater _tickUpdater;
	private MovementCommander _movementCommander;
	private GameState _gameState;
	private SnakeKnotsCreator _knotsCreator;
	private FoodCreator _foodCreator;
	private SnakeMover _snakeMover;

	private MainConfig _mainConfig;
	private SceneView _sceneView;

	public GameBootstrap(MainConfig mainConfig, SceneView sceneView)
	{
		_mainConfig = mainConfig;
		_sceneView = sceneView;
	}

	public void Start()
	{
		Initialize();
		_gameFlowController.StartNewGame();
	}

	public void Update()
	{
		_inputController.Update();
	}

	public void Destroy()
	{
		_eventBus.Dispose();
	}

	private void Initialize()
	{
		_eventBus = new EventBus();
		_gameState = new GameState(_mainConfig, _eventBus);
		
		_inputController = new EditorInputController(_eventBus);
		_mapLifetimeController = new MapLifetimeController(_eventBus, _mainConfig, _sceneView);

		_gameFlowController = new GameFlowController(_eventBus);
		_tickUpdater = new TickUpdater(_mainConfig, _eventBus, _sceneView.CoroutinesHelper);
		_movementCommander = new MovementCommander(_eventBus);
		_knotsCreator = new SnakeKnotsCreator(_mainConfig, _eventBus);
		_snakeMover = new SnakeMover(_mainConfig, _eventBus, _gameState);
		_foodCreator = new FoodCreator(_mainConfig, _eventBus, _gameState);
	}
}