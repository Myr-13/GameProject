using Game.Engine;

namespace Game.Game;

public class Camera
{
	public Vector Position = new();
	public Vector TargetPosition = new();
	private Graphics _graphics;

	public void Init(Graphics graphics)
	{
		_graphics = graphics;
	}

	public void Render()
	{
		Position = Vector.Lerp(Position, TargetPosition, 0.1f);
		
		_graphics.View.Center = Position;
	}
}
