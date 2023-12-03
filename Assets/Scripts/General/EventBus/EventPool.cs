using System.Collections.Generic;

public class EventPool<T> where T : IGameEvent, new() {
	
	private Queue<T> _availableEvents;

	public EventPool()
	{
		_availableEvents = new Queue<T>();
	}

	public T Get() {
		if (_availableEvents.Count == 0) 
		{
			return new T();
		}

		return _availableEvents.Dequeue();
	}

	public void Release(T gameEvent) {
		gameEvent.Dispose();
		_availableEvents.Enqueue(gameEvent);
	}
}