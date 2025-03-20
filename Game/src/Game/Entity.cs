using Game.Engine;
using SFML.Graphics;

namespace Game.Game;

public abstract class Entity
{
	protected World World;
	
	public int Health;
	public int MaxHealth;
	public Vector Position;
	public Vector Velocity = new();
	public Vector Size;
	public bool MarkedForDeletion = false;

	public Entity(World world, Vector position, Vector size)
	{
		World = world;
		Position = position;
		Size = size;
		
		Health = MaxHealth = 100;
	}

	public Entity(World world) : this(world, new(0, 0), new(0, 0))
	{
	}
	
	public abstract void Update(float deltaTime);
	public abstract void Draw();
}
