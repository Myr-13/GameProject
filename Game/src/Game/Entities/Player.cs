using Game.Engine;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Game.Game.Entities;

public class Player : Entity
{
	private readonly RectangleShape _shape;
	
	public Player(World world, Vector position) : base(world, position, new Vector(32, 32))
	{
		_shape = new RectangleShape(Size);
		_shape.Position = position;
	}
	
	public override void Update(float deltaTime)
	{
		Velocity.Y += 1F;
		
		if (Input.IsKeyPressing(Keyboard.Key.D))
			Velocity.X = 30F;
		if (Input.IsKeyPressing(Keyboard.Key.A))
			Velocity.X = -30F;

		if (Input.IsKeyPressed(Keyboard.Key.Space))
			Velocity.Y = -32F;
		
		Console.WriteLine(Velocity);
		
		Vector nextPosition = Position + Velocity * deltaTime + new Vector(0, Size.Y);
		Vector checkPosition = Position + new Vector(Size.X / 2F, Size.Y);
		World.IntersectFloor(checkPosition, nextPosition, out checkPosition);
		Position = checkPosition - new Vector(Size.X / 2F, Size.Y);
		
		_shape.Position = Position;
	}

	public override void Draw(RenderWindow window)
	{
		window.Draw(_shape);
	}
}