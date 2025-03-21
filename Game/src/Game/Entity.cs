using Game.Engine;
using SFML.Graphics;

namespace Game.Game;

public class Entity
{
	protected readonly App App;
	protected readonly World World;
	protected readonly Graphics Graphics;
	
	public int Health;
	public int MaxHealth;
	public Vector Position;
	public Vector Velocity = new();
	public Vector Size;
	public bool MarkedForDeletion = false;

	public Entity(App app, Vector position, Vector size)
	{
		App = app;
		World = app.World;
		Graphics = app.Graphics;
		Position = position;
		Size = size;
		
		Health = MaxHealth = 100;
	}

	public Entity(App app) : this(app, new(0, 0), new(0, 0))
	{
	}
	
	public virtual void OnUpdate(float deltaTime) {}
	public virtual void OnDraw() {}
	public virtual void OnDestroy() {}
}
