using Game.Engine;
using SFML.Graphics;
using SFML.System;

namespace Game.Game;

public class Healbar
{
	private readonly RectangleShape _shape;
	private readonly Vector _size = new(30, 5);

	public Healbar()
	{
		_shape = new RectangleShape(_size);
		_shape.FillColor = Color.White;
	}

	public void Draw(Graphics graphics, Vector position, int health, int maxHealth)
	{
		Vector newSize = _size.Clone();
		newSize.X = (double)health / maxHealth * _size.X;
		
		_shape.Size = newSize;
		_shape.Position = position - _size / 2F;
		graphics.NativeWindow.Draw(_shape);
	}
}
