using Config;
using Events;
using UnityEngine;

namespace Movement
{
	public class MovementCommander
	{
		private EventBus _eventBus;
		
		public MovementCommander(EventBus eventBus)
		{
			_eventBus = eventBus;
			
			Initialize();
		}

		private void Initialize()
		{
			_eventBus.Subscribe<KeyStateChangeEvent>(OnKeyStateUpdateEvent);
		}

		private void OnKeyStateUpdateEvent(KeyStateChangeEvent keyState)
		{
			KeyCode key = keyState.Key;
			bool isArrowKey = key == KeyCode.UpArrow
			                  || key == KeyCode.RightArrow
			                  || key == KeyCode.DownArrow
			                  || key == KeyCode.LeftArrow;

			if (!isArrowKey)
			{
				return;
			}

			if (keyState.IsPressed)
			{
				MovementDirection direction = GetMovementDirectionByKey(key);
				_eventBus.Publish(new UpdateDirectionCommandEvent(direction));
			}
		}

		private MovementDirection GetMovementDirectionByKey(KeyCode key)
		{
			switch (key)
			{
				case KeyCode.UpArrow:
					return MovementDirection.Up;
				case KeyCode.RightArrow:
					return MovementDirection.Right;
				case KeyCode.DownArrow:
					return MovementDirection.Down;
				case KeyCode.LeftArrow:
					return MovementDirection.Left;
				default:
					return default;
			}
		}
	}
}