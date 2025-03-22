using Game.Engine;

namespace Game.Game.Components;

public sealed class Camera(App app) : Component(app)
{
	public Vector Position = new();
	public Vector TargetPosition = new();

	public override void OnDraw()
	{
		Position = Vector.Lerp(Position, TargetPosition, 0.1f);
		Graphics.View.Center = Position;
	}
}
