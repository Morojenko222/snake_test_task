using System;
using Events;
using UnityEngine;

namespace TestTask222.UserInput
{
	public class EditorInputController : InputController
	{
		public EditorInputController(EventBus eventBus) : base(eventBus)
		{
		}

		protected override void CheckKeyStateChanging(KeyCode keyCode)
		{
			keysPressStates.TryAdd(keyCode, false);
			
			bool newState = Input.GetKey(keyCode);
			bool currentState = keysPressStates[keyCode];

			if (currentState != newState)
			{
				keysPressStates[keyCode] = newState;
				UpdateArrowState(keyCode, newState);
			}
		}
	}
}