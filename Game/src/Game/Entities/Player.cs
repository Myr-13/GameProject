using Game.Engine;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Game.Game.Entities;

public class Player : Entity
{
	private readonly RectangleShape _shape;
	private readonly RectangleShape[] _eyesShapes;
	
	public Player(World world, Vector position) : base(world, position, new Vector(32, 32))
	{
		_shape = new RectangleShape(Size);
		_eyesShapes = [ new RectangleShape(new Vector2f(5, 10)), new RectangleShape(new Vector2f(5, 10)) ];
		_shape.Position = position;
		
		_eyesShapes[0].FillColor = Color.Black;
		_eyesShapes[1].FillColor = Color.Black;
	}
	
	public override void Update(float deltaTime)
	{
		Velocity.Y += 0.75F;
		
		if (Input.IsKeyPressing(Keyboard.Key.D))
			Velocity.X = 5F;
		if (Input.IsKeyPressing(Keyboard.Key.A))
			Velocity.X = -5F;
		Velocity.X *= 0.83F;

		if (Input.IsKeyPressed(Keyboard.Key.Space))
			Velocity.Y = -10F;
		
		World.MoveBox(ref Position, ref Velocity, Size);
		
		_shape.Position = Position - Size / 2F;
	}

	public override void Draw()
	{
		_eyesShapes[0].Position = Position - new Vector(2.5F, 5F) + (Input.MousePosition() - Position).Normalize() * 5F;
		_eyesShapes[1].Position = Position - new Vector(2.5F, 5F) + (Input.MousePosition() - Position).Normalize() * 5F;
		
		World.Graphics.NativeWindow.Draw(_shape);
		World.Graphics.NativeWindow.Draw(_eyesShapes[0]);
		World.Graphics.NativeWindow.Draw(_eyesShapes[1]);
		World.Graphics.Camera.TargetPosition = Position;
		World.Graphics.Camera.Render();
	}
}