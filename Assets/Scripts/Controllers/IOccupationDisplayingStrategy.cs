using State;

namespace Controllers
{
	public interface IOccupationDisplayingStrategy
	{
		void UpdateOccupationVisual (TileCoordinate tileCoordinate, TileOccupation occupation);
	}
}