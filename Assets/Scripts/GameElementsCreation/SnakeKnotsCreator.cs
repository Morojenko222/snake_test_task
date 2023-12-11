using Config;
using Events;
using State;
using Random = UnityEngine.Random;

namespace GameElementsCreation
{
	public class SnakeKnotsCreator
	{
		private MainConfig _mainConfig;
		private EventBus _eventBus;
		
		public SnakeKnotsCreator(MainConfig mainConfig, EventBus eventBus)
		{
			_mainConfig = mainConfig;
			_eventBus = eventBus;

			Initialize();
		}

		private void Initialize()
		{
			_eventBus.Subscribe<MapCreatedEvent>(OnMapCreated);
		}

		private void OnMapCreated(MapCreatedEvent evt)
		{
			CreateFirstElement();
		}

		private void CreateFirstElement()
		{
			int mapSize = (int) _mainConfig.GameParameters.MapSize;

			int x = Random.Range(0, mapSize);
			int y = Random.Range(0, mapSize);
			TileCoordinate coordinate = new TileCoordinate { X = x, Y = y };
			
			_eventBus.Publish(new UpdateTileOccupationStateEvent(coordinate, TileOccupation.SnakeTile));
			_eventBus.Publish(new FirstSnakeKnotSpawnedEvent(coordinate));
		}
	}
}