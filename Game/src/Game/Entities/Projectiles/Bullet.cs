using Game.Engine;
using SFML.Graphics;

namespace Game.Game.Entities.Projectiles;

public class Bullet : Entity
{
	private readonly RectangleShape _shape;
	
	public Bullet(World world, Vector position) : base(world, position, new Vector(8, 8))
	{
		_shape = new RectangleShape(Size);
		_shape.Position = position;
		_shape.FillColor = Color.Yellow;
	}
	
	public override void Update(float deltaTime)
	{
		
	}

	public override void Draw()
	{
		_shape.Position = Position - Size / 2F;
		World.Graphics.NativeWindow.Draw(_shape);
	}
}
