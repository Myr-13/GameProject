using Game.Engine;
using SFML.Graphics;

namespace Game.Game;

public abstract class Entity(World world, Vector position, Vector size)
{
	protected World World = world;
	
	public int Health;
	public int MaxHealth;
	public Vector Position = position;
	public Vector Velocity = new();
	public Vector Size = size;

	public Entity(World world) : this(world, new(0, 0), new(0, 0))
	{
	}
	
	public abstract void Update(float deltaTime);
	public abstract void Draw(RenderWindow window);
}
