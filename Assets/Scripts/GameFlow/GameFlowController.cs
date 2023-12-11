using Events;

public class GameFlowController
{
	private EventBus _eventBus;

	public GameFlowController(EventBus eventBus)
	{
		_eventBus = eventBus;

		Initialize();
	}

	private void Initialize()
	{
		_eventBus.Subscribe<SnakeCrashEvent> (OnSnakeCrashEvent);
	}

	public void StartNewGame()
	{
		_eventBus.Publish(new CleaningBeforeNewGameEvent());
		_eventBus.Publish(new StartNewGameEvent());
	}

	private void OnSnakeCrashEvent(SnakeCrashEvent _)
	{
		StartNewGame();
	}
}
