using Game.Engine;

namespace Game.Game;

public class Component(App app)
{
	protected readonly App App = app;
	protected readonly World World = app.World;
	protected readonly Graphics Graphics = app.Graphics;
	
	public virtual void OnInit() {}
	public virtual void OnUpdate(float deltaTime) {}
	public virtual void OnDraw() {}
}
