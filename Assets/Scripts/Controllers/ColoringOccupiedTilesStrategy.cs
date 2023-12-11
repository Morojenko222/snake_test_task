using System.Collections.Generic;
using State;
using UnityEngine;
using Views;

namespace Controllers
{
	public class ColoringOccupiedTilesStrategy : IOccupationDisplayingStrategy
	{
		private Dictionary<TileCoordinate, MapTileView> _viewsMap;

		public ColoringOccupiedTilesStrategy(Dictionary<TileCoordinate, MapTileView> viewsMap)
		{
			_viewsMap = viewsMap;
		}

		public void UpdateOccupationVisual(TileCoordinate tileCoordinate, TileOccupation occupation)
		{
			Color currentColor = _viewsMap[tileCoordinate].Image.color;
			Color requiredColor;
			
			requiredColor = occupation switch
			{
				TileOccupation.SnakeTile => Color.red,
				TileOccupation.FoodTile => Color.green,
				_ => Color.white
			};

			if (currentColor != requiredColor)
			{
				_viewsMap[tileCoordinate].Image.color = requiredColor;
			}
		}
	}
}