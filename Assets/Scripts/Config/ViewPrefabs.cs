using UnityEngine;
using Views;

namespace Config
{
	In
	public class ViewPrefabs : ScriptableObject
	{
		public MapTileView MapTileView => _mapTileView;

		[SerializeField] private MapTileView _mapTileView;
	}
}