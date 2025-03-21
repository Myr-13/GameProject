using Game.Engine;
using SFML.Graphics;

namespace Game.Game.Gfx.Particles;

public class Particle
{
	public Vector Position;
	public Vector Size;
	public Vector Velocity;
	public float Gravity;
	public bool Collision;
	public Color Color;
	public int Lifetime;
}
