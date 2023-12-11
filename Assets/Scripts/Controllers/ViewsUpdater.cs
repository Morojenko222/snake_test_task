using System.Collections.Generic;
using Events;
using State;
using Views;

namespace Controllers
{
	public class ViewsUpdater
	{
		private EventBus _eventBus;
		private Dictionary<TileCoordinate, MapTileView> _viewsMap;
		private IOccupationDisplayingStrategy _displayingStrategy;

		public ViewsUpdater(EventBus eventBus, Dictionary<TileCoordinate, MapTileView> viewsMap)
		{
			_eventBus = eventBus;
			_viewsMap = viewsMap;
			
			Initialize();
		}

		private void Initialize()
		{
			_displayingStrategy = new ColoringOccupiedTilesStrategy(_viewsMap);
			_eventBus.Subscribe<MapStateUpdatedEvent>(OnMapStateUpdate);
		}

		private void OnMapStateUpdate(MapStateUpdatedEvent evt)
		{
			_displayingStrategy.UpdateOccupationVisual(evt.Coordinate, evt.Occupation);
		}
	}
}