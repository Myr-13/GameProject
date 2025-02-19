using Game.Engine;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Game.Game.Entities;

public class Player : Entity
{
	private readonly RectangleShape _shape;
	
	public Player(Vector2f position) : base(position, new Vector2f(32, 32))
	{
		Physics = true;
		
		_shape = new RectangleShape(Size);
		_shape.Position = position;
	}
	
	public override void Update(float deltaTime)
	{
		_shape.Position = Position;
		
		if (Input.IsKeyPressing(Keyboard.Key.Space))
			Console.Out.WriteLine($"{Input.IsKeyPressed(Keyboard.Key.Space)}");
	}

	public override void Draw(RenderWindow window)
	{
		window.Draw(_shape);
	}
}