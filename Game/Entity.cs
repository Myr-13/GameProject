using SFML.Graphics;
using SFML.System;

namespace Game;

public class Entity
{
	public int Health;
	public RectangleShape Shape = new();

	public Vector2f Position { get => Shape.Position; set => Shape.Position = value; }

	public void SetColor()
	{
		Shape.FillColor = Color.White;
	}

	public void Draw(RenderWindow window)
	{
		window.Draw(Shape);
	}
}
