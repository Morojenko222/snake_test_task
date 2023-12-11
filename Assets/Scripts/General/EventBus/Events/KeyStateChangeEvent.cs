using UnityEngine;

namespace Events
{
	public class KeyStateChangeEvent : IGameEvent
	{
		public readonly KeyCode Key;
		public readonly bool IsPressed;

		public KeyStateChangeEvent(KeyCode key, bool isPressed)
		{
			Key = key;
			IsPressed = isPressed;
		}
	}
}