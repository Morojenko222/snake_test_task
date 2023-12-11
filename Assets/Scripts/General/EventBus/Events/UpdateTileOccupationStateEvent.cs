using State;

namespace Events
{
	public class UpdateTileOccupationStateEvent : IGameEvent
	{
		public TileCoordinate TileCoordinate;
		public TileOccupation NewState;

		public UpdateTileOccupationStateEvent(TileCoordinate tileCoordinate, TileOccupation newState)
		{
			TileCoordinate = tileCoordinate;
			NewState = newState;
		}
	}
}