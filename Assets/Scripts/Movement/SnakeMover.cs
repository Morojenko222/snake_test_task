using System.Collections.Generic;
using Config;
using Events;
using State;

namespace Movement
{
	public class SnakeMover
	{
		private MainConfig _mainConfig;
		private EventBus _eventBus;
		private GameState _gameState;

		private LinkedList<TileCoordinate> _snakeCoordinates;
		private MovementDirection _currentDirection;
		private MovementDirection _nextDirection;
		private bool _requireToAddKnot;

		private UpdateTileOccupationStateEvent _cachedUpdateTileEvent;
		
		public SnakeMover(MainConfig mainConfig, EventBus eventBus, GameState gameState)
		{
			_mainConfig = mainConfig;
			_eventBus = eventBus;
			_gameState = gameState;
			
			Initialize();
		}

		private void Initialize()
		{
			_snakeCoordinates = new LinkedList<TileCoordinate>();
			_cachedUpdateTileEvent = new UpdateTileOccupationStateEvent(default, default);
			
			_eventBus.Subscribe<UpdateDirectionCommandEvent> (OnDirectionChange);
			_eventBus.Subscribe<FirstSnakeKnotSpawnedEvent> (OnFirstKnotSpawn);
			_eventBus.Subscribe<GameTickEvent> (OnTick);
			_eventBus.Subscribe<CleaningBeforeNewGameEvent> (Cleaning);
		}

		private void OnDirectionChange(UpdateDirectionCommandEvent evt)
		{
			if (IsDirectionChangeAllowed(evt.Direction))
			{
				_nextDirection = evt.Direction;
			}
		}

		private void OnFirstKnotSpawn(FirstSnakeKnotSpawnedEvent evt)
		{
			_snakeCoordinates.AddFirst(evt.TileCoordinate);
		}

		private void OnTick(GameTickEvent evt)
		{
			UpdateDirectionToNext();
			AttemptToMove();
		}
		
		private void AttemptToMove()
		{
			if (_currentDirection != MovementDirection.None)
			{
				TileCoordinate nextCoordinate = GetNextCoordinate();

				if (IsNextCellFree(nextCoordinate))
				{
					bool isFoodCell = IsFoodCell(nextCoordinate);
					MoveSnake();

					if (isFoodCell)
					{
						_eventBus.Publish(new AteFoodEvent());
						_requireToAddKnot = true;
					}
				}
				else
				{
					_eventBus.Publish(new SnakeCrashEvent());
				}
			}
		}

		private TileCoordinate GetNextCoordinate()
		{
			int xCoord = _snakeCoordinates.First.Value.X;
			int yCoord = _snakeCoordinates.First.Value.Y;

			(int x, int y) delta = GetCoordinateDelta();

			xCoord += delta.x;
			yCoord += delta.y;

			return new TileCoordinate() { X = xCoord, Y = yCoord };
		}

		private bool IsNextCellFree(TileCoordinate nextCoordinate)
		{
			bool result = !IsEdgeCell(nextCoordinate)
			              && !IsOccupiedCell(nextCoordinate);

			return result;
		}

		private (int x, int y) GetCoordinateDelta()
		{
			if (_currentDirection == MovementDirection.Right)
				return (1,0);
			if (_currentDirection == MovementDirection.Left)
				return (-1, 0);
			if (_currentDirection == MovementDirection.Up)
				return (0, 1);
			if (_currentDirection == MovementDirection.Down)
				return (0,-1);

			return default;
		}

		private bool IsEdgeCell(TileCoordinate coordinate)
		{
			uint mapSize = _mainConfig.GameParameters.MapSize;

			if (coordinate.X < 0 || coordinate.Y < 0)
				return true;

			if (coordinate.X >= mapSize || coordinate.Y >= mapSize)
				return true;

			return false;
		}

		private bool IsOccupiedCell(TileCoordinate coordinate)
		{
			TileOccupation occupation = _gameState.GetTileOccupationState(coordinate.X, coordinate.Y);
			return occupation == TileOccupation.SnakeTile;
		}

		private bool IsFoodCell(TileCoordinate coordinate)
		{
			TileOccupation occupation = _gameState.GetTileOccupationState(coordinate.X, coordinate.Y);
			return occupation == TileOccupation.FoodTile;
		}

		private void MoveSnake()
		{
			TileCoordinate nextCoordinate = GetNextCoordinate();
			_snakeCoordinates.AddFirst(nextCoordinate);
			
			SetupUpdateTileEvent(nextCoordinate, TileOccupation.SnakeTile);
			_eventBus.Publish(_cachedUpdateTileEvent);
			
			if (!_requireToAddKnot)
			{
				TileCoordinate lastKnotCoordinate = _snakeCoordinates.Last.Value;
				SetupUpdateTileEvent(lastKnotCoordinate, TileOccupation.None);
				_eventBus.Publish(_cachedUpdateTileEvent);
				
				_snakeCoordinates.RemoveLast();
			}
			else
			{
				_requireToAddKnot = false;
			}
		}

		private bool IsDirectionChangeAllowed(MovementDirection newDirection)
		{
			if (_currentDirection == newDirection)
			{
				return false;
			}

			return newDirection != GetOppositeDirection(_currentDirection);
		}

		private MovementDirection GetOppositeDirection(MovementDirection inputDirection)
		{
			MovementDirection oppositeDir;
			
			switch (inputDirection)
			{
				case MovementDirection.Up:
					oppositeDir = MovementDirection.Down;
					break;
				case MovementDirection.Down:
					oppositeDir = MovementDirection.Up;
					break;
				case MovementDirection.Left:
					oppositeDir = MovementDirection.Right;
					break;
				case MovementDirection.Right:
					oppositeDir = MovementDirection.Left;
					break;
				default:
					oppositeDir = MovementDirection.None;
					break;
			}

			return oppositeDir;
		}

		private void UpdateDirectionToNext()
		{
			if (_nextDirection != MovementDirection.None)
			{
				_currentDirection = _nextDirection;
				_nextDirection = MovementDirection.None;
			}
		}

		private void SetupUpdateTileEvent(TileCoordinate coordinate, TileOccupation occupation)
		{
			_cachedUpdateTileEvent.TileCoordinate = coordinate;
			_cachedUpdateTileEvent.NewState = occupation;
		}

		private void Cleaning(CleaningBeforeNewGameEvent _)
		{
			_snakeCoordinates.Clear();
			_currentDirection = MovementDirection.None;
			_nextDirection = MovementDirection.None;
			_requireToAddKnot = false;
		}
	}
}