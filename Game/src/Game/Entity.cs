using System.Numerics;
using SFML.Graphics;
using SFML.System;

namespace Game.Game;

public abstract class Entity
{
	public int Health;
	public int MaxHealth;
	public bool Physics;
	public Vector2f Position;
	public Vector2f Velocity;
	public Vector2f Size;

	public Entity() { }

	public Entity(Vector2f position, Vector2f size)
	{
		Position = position;
		Size = size;
	}
	
	public abstract void Update(float deltaTime);
	public abstract void Draw(RenderWindow window);
}
