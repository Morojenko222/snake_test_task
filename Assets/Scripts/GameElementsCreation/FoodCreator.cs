using System.Collections.Generic;
using Config;
using Events;
using State;
using UnityEngine;

namespace GameElementsCreation
{
	public class FoodCreator
	{
		private MainConfig _mainConfig;
		private EventBus _eventBus;
		private GameState _gameState;

		private TileCoordinate _foodCoordinate;
		
		public FoodCreator(MainConfig mainConfig, EventBus eventBus, GameState gameState)
		{
			_mainConfig = mainConfig;
			_eventBus = eventBus;
			_gameState = gameState;

			Initialize();
		}

		private void Initialize()
		{
			_eventBus.Subscribe<FirstSnakeKnotSpawnedEvent>(OnFirstElementSpawned);
			_eventBus.Subscribe<AteFoodEvent>(OnAteFoodEvent);
		}

		private void OnFirstElementSpawned(FirstSnakeKnotSpawnedEvent evt)
		{
			SpawnFood();
		}

		private void OnAteFoodEvent(AteFoodEvent evt)
		{
			SpawnFood();
		}

		private void SpawnFood()
		{
			TileCoordinate foodCoordinate = GenerateRandomFoodPosition();
			_foodCoordinate = foodCoordinate;
			_eventBus.Publish(new UpdateTileOccupationStateEvent(_foodCoordinate, TileOccupation.FoodTile));
		}

		private TileCoordinate GenerateRandomFoodPosition()
		{
			List<TileCoordinate> allowedCoordinates = GetAllowedCoordinates ();
			int randomIndex = Random.Range(0, allowedCoordinates.Count);

			return allowedCoordinates[randomIndex];
		}

		private List<TileCoordinate> GetAllowedCoordinates()
		{
			List<TileCoordinate> allowedCoordinates = new List<TileCoordinate>();
			uint mapSize = _mainConfig.GameParameters.MapSize;
			
			for (int x = 0; x < mapSize; x++)
			{
				for (int y = 0; y < mapSize; y++)
				{
					TileOccupation occupation = _gameState.GetTileOccupationState(x, y);

					if (occupation == TileOccupation.None)
					{
						allowedCoordinates.Add(new TileCoordinate(){X = x, Y = y});
					}
				}
			}

			return allowedCoordinates;
		}
	}
}