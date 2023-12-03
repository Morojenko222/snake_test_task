public class InitializationReadyEvent : IGameEvent
{
	public bool IsReady;
	
	public void Dispose()
	{
		IsReady = default;
	}
}