using System.Collections.Generic;
using Config;
using Views;

namespace Controllers
{
	public class MapLifetimeController
	{
		private Dictionary<TileCoordinate, MapTileView> _viewsMap;
		
		private EventBus _eventBus;
		private MainConfig _mainConfig;
		private SceneView _sceneView;

		private MapCreator _mapCreator;
		private ViewsUpdater _viewsUpdater;
		
		public MapLifetimeController(EventBus eventBus, MainConfig mainConfig, SceneView sceneView)
		{
			_eventBus = eventBus;
			_mainConfig = mainConfig;
			_sceneView = sceneView;

			Initialize();
		}

		private void Initialize()
		{
			_viewsMap = new Dictionary<TileCoordinate, MapTileView>();
			_mapCreator = new MapCreator(_eventBus, _mainConfig, _sceneView, _viewsMap);
			_viewsUpdater = new ViewsUpdater(_eventBus, _viewsMap);
			
			_eventBus.Subscribe<StartNewGameEvent>(OnStartNewGameEvent);
		}

		private void OnStartNewGameEvent(StartNewGameEvent _)
		{
			_mapCreator.DestroyMap();
			_mapCreator.CreateMap();
		}
	}
}