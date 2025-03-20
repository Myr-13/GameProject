using Game.Engine;
using SFML.Graphics;

namespace Game.Game.Entities;

public class Enemy : Entity
{
	private readonly RectangleShape _shape;
	
	public Enemy(World world, Vector position) : base(world, position, new Vector(32, 32))
	{
		_shape = new RectangleShape(Size);
		_shape.Position = position;
		_shape.FillColor = Color.Red;
	}
	
	public override void Update(float deltaTime)
	{
		Velocity.Y += 0.75F;
		World.MoveBox(ref Position, ref Velocity, Size);
	}

	public override void Draw()
	{
		_shape.Position = Position - Size / 2F;
		World.Graphics.NativeWindow.Draw(_shape);
	}
}
