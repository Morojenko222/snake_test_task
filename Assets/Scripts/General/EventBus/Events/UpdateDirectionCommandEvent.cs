using Movement;

namespace Events
{
	public class UpdateDirectionCommandEvent : IGameEvent
	{
		public readonly MovementDirection Direction;

		public UpdateDirectionCommandEvent(MovementDirection direction)
		{
			Direction = direction;
		}
	}
}