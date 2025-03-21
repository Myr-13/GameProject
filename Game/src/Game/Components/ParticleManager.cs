using SFML.Graphics;
using SFML.System;

namespace Game.Game.Gfx.Particles;

public sealed class ParticleManager(App app) : Component(app)
{
	private readonly List<Particle> _particles = new();
	
	public override void OnInit()
	{
		
	}

	public override void OnDraw()
	{
		foreach (Particle particle in _particles.ToList())
		{
			particle.Velocity.Y += particle.Gravity;
			if (particle.Collision)
				World.MoveBox(ref particle.Position, ref particle.Velocity, particle.Size);
			else
				particle.Position += particle.Velocity;
			
			RectangleShape shape = new(new Vector2f(4, 4));
			shape.Position = particle.Position;
			shape.FillColor = particle.Color;
			Graphics.NativeWindow.Draw(shape);
			
			if (particle.Lifetime-- <= 0)
				_particles.Remove(particle);
		}
	}

	public void AddParticle(Particle particle)
	{
		_particles.Add(particle);
	}
}
