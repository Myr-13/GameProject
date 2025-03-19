using Game.Engine;
using SFML.Graphics;
using SFML.System;

namespace Game.Game;

public class World
{
	public Graphics Graphics;
	public List<Entity> Entities = new();
	public byte[] Collision = [];
	public int MapWidth;
	public int MapHeight;

	public void Init(Graphics graphics)
	{
		Graphics = graphics;
		
		MapWidth = 10;
		MapHeight = 10;

		Collision = new byte[MapWidth * MapHeight];
		
		for (int x = 0; x < MapWidth; x++)
		{
			for (int y = 0; y < MapHeight; y++)
			{
				if (y < 3)
					continue;
				
				Collision[y * MapWidth + x] = 1;
			}
		}
	}

	public void Draw()
	{
		foreach (var entity in Entities)
			entity.Draw();

		for (int x = 0; x < MapWidth; x++)
		{
			for (int y = 0; y < MapHeight; y++)
			{
				if (Collision[y * MapWidth + x] == 0)
					continue;
				
				RectangleShape shape = new RectangleShape();
				shape.Position = new(x * 32, y * 32);
				shape.Size = new(32, 32);
				shape.FillColor = Color.White;

				Graphics.NativeWindow.Draw(shape);
			}
		}
	}

	public void Update(float deltaTime)
	{
		foreach (var entity in Entities)
		{
			entity.Update(deltaTime);
		}
	}
	
	public void AddEntity(Entity entity)
	{
		Entities.Add(entity);
	}

	public void IntersectFloor(Vector positionIn, Vector positionOut, out Vector intersectPosition)
	{
		double len = positionIn.Length();

		for (int i = 0; i < (int)len; i++)
		{
			double amount = i / len;
			Vector checkPosition = Vector.Lerp(positionIn, positionOut, amount);

			if (GetTile(checkPosition) != 0)
			{
				intersectPosition = checkPosition;
				return;
			}
		}

		intersectPosition = positionOut;
	}
	
	public byte GetTile(Vector a)
	{
		return GetTile(a.X, a.Y);
	}

	public byte GetTile(double x, double y)
	{
		int x2 = Math.Clamp((int)Math.Floor(x / 32), 0, MapWidth - 1);
		int y2 = Math.Clamp((int)Math.Floor(y / 32), 0, MapHeight - 1);
		return Collision[y2 * MapWidth + x2];
	}

	public bool TestBox(Vector position, Vector size)
	{
		size *= 0.5f;
		if (GetTile(position.X - size.X, position.Y - size.Y) != 0) return true;
		if (GetTile(position.X + size.X, position.Y - size.Y) != 0) return true;
		if (GetTile(position.X - size.X, position.Y + size.Y) != 0) return true;
		if (GetTile(position.X + size.X, position.Y + size.Y) != 0) return true;
		return false;
	}

	public void MoveBox(ref Vector position, ref Vector velocity, Vector size)
	{
		double dist = velocity.Length();
		int max = (int)dist;

		if (dist < 0.0001)
			return;

		double fraction = 1f / (double)(max + 1);
		
		for (int i = 0; i <= max; i++)
		{
			Vector newPos = position + velocity * fraction;

			if (newPos == position)
				break;

			if (TestBox(newPos, size))
			{
				int hits = 0;
				
				if (TestBox(new Vector(position.X, newPos.Y), size))
				{
					newPos.Y = position.Y;
					velocity.Y = 0;
					hits++;
				}

				if (TestBox(new Vector(newPos.X, position.Y), size))
				{
					newPos.X = position.X;
					velocity.X = 0;
					hits++;
				}

				if (hits == 0)
				{
					newPos = position;
					velocity = new Vector(0, 0);
				}
			}
			
			position = newPos;
		}
	}
}