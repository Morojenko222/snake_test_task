using UnityEngine;

namespace Config
{
	[CreateAssetMenu(fileName = "MainConfig", menuName = "Configs/MainConfig")]
	public class MainConfig : ScriptableObject
	{
		public GameParameters GameParameters => _gameParameters;
		public ViewPrefabs ViewPrefabs => _viewPrefabs;

		[SerializeField] private GameParameters _gameParameters;
		[SerializeField] private ViewPrefabs _viewPrefabs;
	}
}