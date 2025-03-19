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
		World.Graphics.NativeWindow.Draw(_shape);
		World.Graphics.Camera.TargetPosition = Position;
		World.Graphics.Camera.Render();
	}
}