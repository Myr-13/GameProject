using Game.Engine;
using Game.Game.Gfx.Particles;
using SFML.Graphics;

namespace Game.Game.Entities;

public class Enemy : Entity
{
	private readonly RectangleShape _shape;
	private readonly Healbar _healbar;
	
	public Enemy(App app, Vector position) : base(app, position, new Vector(32, 32))
	{
		_shape = new RectangleShape(Size);
		_shape.Position = position;
		_shape.FillColor = Color.Red;
		_healbar = new();
	}
	
	public override void OnUpdate(float deltaTime)
	{
		Velocity.Y += 0.75F;
		World.MoveBox(ref Position, ref Velocity, Size);
	}

	public override void OnDraw()
	{
		_shape.Position = Position - Size / 2F;
		World.Graphics.NativeWindow.Draw(_shape);
		_healbar.Draw(World.Graphics, Position - new Vector(0, Size.Y + 10F), Health, MaxHealth);
	}

	public override void OnDestroy()
	{
		Random random = new();

		for (int i = 0; i < 10; i++)
		{
			Particle particle = new() {
				Position = Position,
				Size = new Vector(4, 4),
				Velocity = new(random.NextDouble() * 6F - 3F, -random.NextDouble() * 5F - 5F),
				Gravity = 0.75F,
				Color = Color.Red,
				Collision = true,
				Lifetime = 60
			};
			
			App.ParticleManager.AddParticle(particle);
		}
	}
}
