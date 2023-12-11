using System.Collections.Generic;
using Events;
using UnityEngine;

namespace TestTask222.UserInput
{
	public abstract class InputController
	{
		protected Dictionary<KeyCode, bool> keysPressStates;
		protected readonly EventBus eventBus;

		protected abstract void CheckKeyStateChanging(KeyCode keyCode);

		public InputController(EventBus eventBus)
		{
			this.eventBus = eventBus;
			
			Initialize();
		}

		public void Update()
		{
			UpdateArrowButtonsState();
		}

		protected void UpdateArrowState(KeyCode keyCode, bool newState)
		{
			eventBus.Publish(new KeyStateChangeEvent(keyCode, newState));
		}
		
		private void Initialize()
		{
			keysPressStates = new Dictionary<KeyCode, bool>();
		}

		private void UpdateArrowButtonsState()
		{
			CheckKeyStateChanging(KeyCode.LeftArrow);
			CheckKeyStateChanging(KeyCode.UpArrow);
			CheckKeyStateChanging(KeyCode.RightArrow);
			CheckKeyStateChanging(KeyCode.DownArrow);
		}
	}
}