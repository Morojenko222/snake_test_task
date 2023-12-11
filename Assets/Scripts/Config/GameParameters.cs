using UnityEngine;

namespace Config
{
	[CreateAssetMenu(fileName = "GameParameters", menuName = "Configs/GameParameters")]
	public class GameParameters : ScriptableObject
	{
		public uint MapSize => _mapSize;
		public float MapTileSize => _mapTileSize;
		public float MapTilePositionDelta => _mapTilePositionDelta;
		public float TickSeconds => _tickSeconds;

		[SerializeField] private uint _mapSize = 5;
		[SerializeField] private float _mapTileSize = 100;
		[SerializeField] private float _mapTilePositionDelta = 10;
		[SerializeField] private float _tickSeconds = 0.3f;
	}
}