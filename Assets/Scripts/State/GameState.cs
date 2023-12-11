using Config;
using Events;

namespace State
{
	public class GameState
	{
		private MainConfig _mainConfig;
		private EventBus _eventBus;
		
		private TileOccupation[,] _map;
		private MapStateUpdatedEvent _tileUpdateEvent;
		
		public GameState(MainConfig mainConfig, EventBus eventBus)
		{
			_mainConfig = mainConfig;
			_eventBus = eventBus;
			
			Initialize();
		}

		private void Initialize()
		{
			uint mapSize = _mainConfig.GameParameters.MapSize;
			_map = new TileOccupation[mapSize,mapSize];
			_tileUpdateEvent = new MapStateUpdatedEvent(default, default);
			
			_eventBus.Subscribe<UpdateTileOccupationStateEvent>(UpdateTileOccupationState);
			_eventBus.Subscribe<MapCreatedEvent>(CleanMapState);
		}

		public TileOccupation GetTileOccupationState(int x, int y)
		{
			return _map[x, y];
		}

		private void UpdateTileOccupationState(UpdateTileOccupationStateEvent updateState)
		{
			UpdateMapElement(updateState.TileCoordinate, updateState.NewState);
		}

		private void CleanMapState(MapCreatedEvent _)
		{
			uint mapSize = _mainConfig.GameParameters.MapSize;

			for (int x = 0; x < mapSize; x++)
			{
				for (int y = 0; y < mapSize; y++)
				{
					TileCoordinate coordinate = new TileCoordinate{ X = x, Y = y };
					UpdateMapElement(coordinate, TileOccupation.None);
				}
			}
		}

		private void UpdateMapElement(TileCoordinate coordinate, TileOccupation newOccupation)
		{
			_map[coordinate.X, coordinate.Y] = newOccupation;
			_tileUpdateEvent.UpdateValue(coordinate, newOccupation);
			_eventBus.Publish(_tileUpdateEvent);
		}
	}
}