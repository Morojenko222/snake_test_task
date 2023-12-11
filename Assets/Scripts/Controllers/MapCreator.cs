using System.Collections.Generic;
using Config;
using Events;
using Factories;
using UnityEngine;
using Views;

namespace Controllers
{
	public class MapCreator
	{
		private EventBus _eventBus;
		private MainConfig _mainConfig;
		private SceneView _sceneView;

		private Dictionary<TileCoordinate, MapTileView> _viewsMap;
		private Pool<MapTileView> _tilesPool;
		private ObjectFactory<MapTileView> _mapTileFactory;

		public MapCreator(
			EventBus eventBus,
			MainConfig mainConfig,
			SceneView sceneView,
			Dictionary<TileCoordinate, MapTileView> viewsMap)
		{
			_eventBus = eventBus;
			_mainConfig = mainConfig;
			_sceneView = sceneView;
			_viewsMap = viewsMap;
			
			Initialize();
		}
		
		private void Initialize()
		{
			Transform mapCenter = _sceneView.MapCenterPoint;

			MapTileView mapTilePrefab = _mainConfig.ViewPrefabs.MapTileView;

			_mapTileFactory = new ObjectFactory<MapTileView>(mapTilePrefab);
			_tilesPool = new Pool<MapTileView>(() => _mapTileFactory.Create(Vector3.zero, mapCenter));
		}
		
		public void CreateMap()
		{
			uint mapSize = _mainConfig.GameParameters.MapSize;
			float tileSize = _mainConfig.GameParameters.MapTileSize;
			float delta = _mainConfig.GameParameters.MapTilePositionDelta;

			uint reminder = mapSize % 2;
			uint halfSize = (mapSize / 2);
			float totalFirstElemOffset;
			float sumTilesOffset = (halfSize * tileSize);

			if (reminder == 0)
			{
				float sumDeltasOffset = (halfSize - 1) * delta;
				float halfDelta = (delta / 2f);
				totalFirstElemOffset = -sumTilesOffset - sumDeltasOffset - halfDelta; 
			}
			else
			{
				float tileHalf = tileSize / 2f;
				float sumDeltasOffset = halfSize * delta;
				
				totalFirstElemOffset = -tileHalf - sumTilesOffset - sumDeltasOffset;
			}
			
			float startXpos = totalFirstElemOffset;
			float startYpos = totalFirstElemOffset;

			for (int x = 0; x < mapSize; x++)
			{
				for (int y = 0; y < mapSize; y++)
				{
					float xPos = startXpos + x * (tileSize + delta);
					float yPos = startYpos + y * (tileSize + delta);
					
					MapTileView tileView = _tilesPool.Get();
					tileView.RectTf.localPosition = new Vector3(xPos, yPos, 0f);
					tileView.RectTf.sizeDelta = new Vector2(tileSize, tileSize);

					TileCoordinate coordinate = new TileCoordinate() { X = x, Y = y };
					_viewsMap.Add(coordinate, tileView);
				}
			}
			
			_eventBus.Publish(new MapCreatedEvent());
		}

		public void DestroyMap()
		{
			foreach (MapTileView tileView in _viewsMap.Values)
			{
				_tilesPool.Release(tileView);
			}
			
			_viewsMap.Clear();
		}
	}
}