using System.Collections;
using Config;
using UnityEngine;

namespace Ticks
{
	public class TickUpdater
	{
		private MainConfig _mainConfig;
		private EventBus _eventBus;
		private CoroutinesHelper _coroutinesHelper;

		private WaitForSeconds _waitPeriod;
		private Coroutine _tickCoroutine;

		private GameTickEvent _gameTickEvent;
		
		public TickUpdater(MainConfig mainConfig, EventBus eventBus, CoroutinesHelper coroutinesHelper)
		{
			_mainConfig = mainConfig;
			_eventBus = eventBus;
			_coroutinesHelper = coroutinesHelper;

			Initialize();
		}

		private void Initialize()
		{
			_waitPeriod = new WaitForSeconds(_mainConfig.GameParameters.TickSeconds);
			_gameTickEvent = new GameTickEvent();
			
			_eventBus.Subscribe<CleaningBeforeNewGameEvent>(Cleaning);
			_eventBus.Subscribe<StartNewGameEvent> (Run);
			
		}

		private void Run(StartNewGameEvent _)
		{
			_tickCoroutine = _coroutinesHelper.StartCoroutine(RunInternal());
		}

		private IEnumerator RunInternal()
		{
			while (true)
			{
				yield return _waitPeriod;

				_eventBus.Publish(_gameTickEvent);
			}
		}

		private void Cleaning(CleaningBeforeNewGameEvent _)
		{
			if (_tickCoroutine != null)
			{
				_coroutinesHelper.StopCoroutine(_tickCoroutine);
				_tickCoroutine = null;
			}
		}
	}
}