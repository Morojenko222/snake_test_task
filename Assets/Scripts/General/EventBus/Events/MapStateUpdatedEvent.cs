using State;

namespace Events
{
	public class MapStateUpdatedEvent : IGameEvent
	{
		public TileCoordinate Coordinate { get; private set; }
		public TileOccupation Occupation { get; private set; }

		public MapStateUpdatedEvent(TileCoordinate coordinate, TileOccupation occupation)
		{
			UpdateValue(coordinate, occupation);
		}

		public void UpdateValue(TileCoordinate coordinate, TileOccupation occupation)
		{
			Coordinate = coordinate;
			Occupation = occupation;
		}
	}
}