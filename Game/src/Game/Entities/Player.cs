using Game.Engine;
using Game.Game.Entities.Projectiles;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Game.Game.Entities;

public class Player : Entity
{
	private readonly RectangleShape _shape;
	private readonly RectangleShape[] _eyesShapes;
	private int _jumpsCount = 1;
	
	public Player(App app, Vector position) : base(app, position, new Vector(32, 32))
	{
		_shape = new RectangleShape(Size);
		_eyesShapes = [ new RectangleShape(new Vector2f(5, 10)), new RectangleShape(new Vector2f(5, 10)) ];
		_shape.Position = position;
		
		_eyesShapes[0].FillColor = Color.Black;
		_eyesShapes[1].FillColor = Color.Black;
	}
	
	public override void OnUpdate(float deltaTime)
	{
		Velocity.Y += 0.75F;
		
		if (Input.IsKeyPressing(Keyboard.Key.D))
			Velocity.X = 5F;
		if (Input.IsKeyPressing(Keyboard.Key.A))
			Velocity.X = -5F;

		bool grounded =
			World.IntersectLine(Position, Position + new Vector(Size.X / 2F, Size.Y / 2F + 4F), out Vector _) != 0 ||
			World.IntersectLine(Position, Position + new Vector(-Size.X / 2F, Size.Y / 2F + 4F), out Vector _) != 0;
		if (grounded)
		{
			_jumpsCount = 1;
			Velocity.X *= 0.73F;
		}
		else
		{
			Velocity.X *= 0.95F;
		}

		if (Input.IsKeyPressed(Keyboard.Key.Space) && _jumpsCount > 0)
		{
			Velocity.Y = -10F;
			if (!grounded)
				_jumpsCount--;
		}

		World.MoveBox(ref Position, ref Velocity, Size);

		// Shoot
		Vector direction = (Input.MousePosition() - Position).Normalize();
		if (Input.IsKeyPressed(Keyboard.Key.F))
		{
			Bullet bullet = new(App, Position, direction);
			World.AddEntity(bullet);
		}
	}

	public override void OnDraw()
	{
		Vector centerPosition = Position - new Vector(2.5F, 5F) + (Input.MousePosition() - Position).Normalize() * 5F;
		_eyesShapes[0].Position = centerPosition + new Vector(3.5F, 0F);
		_eyesShapes[1].Position = centerPosition - new Vector(3.5F, 0F);
		_shape.Position = Position - Size / 2F;
		
		World.Graphics.NativeWindow.Draw(_shape);
		World.Graphics.NativeWindow.Draw(_eyesShapes[0]);
		World.Graphics.NativeWindow.Draw(_eyesShapes[1]);
		App.Camera.TargetPosition = Position;
	}
}