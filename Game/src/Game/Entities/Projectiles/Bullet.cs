using Game.Engine;
using SFML.Graphics;

namespace Game.Game.Entities.Projectiles;

public class Bullet : Entity
{
	private readonly RectangleShape _shape;
	
	public Bullet(World world, Vector position, Vector direction) : base(world, position, new Vector(8, 8))
	{
		_shape = new RectangleShape(Size);
		_shape.Position = position;
		_shape.FillColor = Color.Yellow;
		
		Velocity = direction * 5F;
	}
	
	public override void Update(float deltaTime)
	{
		Position += Velocity;

		if (World.TestBox(Position, Size))
		{
			MarkedForDeletion = true;
			return;
		}

		Enemy? enemy = (Enemy?)World.GetFirstEntityInRadius(Position, Size.X / 2F, typeof(Enemy));
		if (enemy != null)
		{
			MarkedForDeletion = true;
			enemy.Health -= 20;
		}
	}

	public override void Draw()
	{
		_shape.Position = Position - Size / 2F;
		World.Graphics.NativeWindow.Draw(_shape);
	}
}
