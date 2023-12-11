namespace Events
{
	public class FirstSnakeKnotSpawnedEvent : IGameEvent
	{
		public readonly TileCoordinate TileCoordinate;

		public FirstSnakeKnotSpawnedEvent(TileCoordinate tileCoordinate)
		{
			TileCoordinate = tileCoordinate;
		}
	}
}