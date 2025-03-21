using System.Diagnostics;
using Game.Engine;
using SFML.Graphics;
using DotTiled;
using DotTiled.Serialization;

namespace Game.Game;

public sealed class World
{
	public Graphics Graphics;
	public List<Entity> Entities = new();
	public byte[] Collision = [];
	public int MapWidth;
	public int MapHeight;

	public void Init(Graphics graphics)
	{
		Graphics = graphics;
		
		LoadMap("../../../../Tiled/unnamed.tmx");
	}

	public void LoadMap(string filePath)
	{
		Stopwatch sw = new Stopwatch();
		sw.Start();
		
		var loader = Loader.Default();
		var map = loader.LoadMap(filePath);
		
		MapWidth = (int)map.Width;
		MapHeight = (int)map.Height;

		Collision = new byte[MapWidth * MapHeight];
		
		foreach (BaseLayer layer in map.Layers)
		{
			if (layer.Name != "Collision")
				continue;
			
			if (layer is TileLayer tileLayer)
			{
				for (int x = 0; x < MapWidth; x++)
				{
					for (int y = 0; y < MapHeight; y++)
					{
						int id = y * MapWidth + x;
						Collision[id] = (byte)tileLayer.Data.Value.GlobalTileIDs.Value[id];
					}
				}
			}
		}
		
		sw.Stop();
		Console.WriteLine($"Loaded map in {sw.ElapsedMilliseconds} ms");
	}

	public void Draw()
	{
		foreach (var entity in Entities)
			entity.OnDraw();

		for (int x = 0; x < MapWidth; x++)
		{
			for (int y = 0; y < MapHeight; y++)
			{
				if (Collision[y * MapWidth + x] == 0)
					continue;
				
				RectangleShape shape = new RectangleShape();
				shape.Position = new(x * 32, y * 32);
				shape.Size = new(32, 32);
				shape.FillColor = SFML.Graphics.Color.White;

				Graphics.NativeWindow.Draw(shape);
			}
		}
	}

	public void Update(float deltaTime)
	{
		foreach (Entity entity in Entities.ToList())
		{
			entity.OnUpdate(deltaTime);

			if (entity.MarkedForDeletion || entity.Health <= 0)
			{
				entity.OnDestroy();
				Entities.Remove(entity);
			}
		}
	}
	
	public void AddEntity(Entity entity)
	{
		Entities.Add(entity);
	}

	public Entity? GetFirstEntityInRadius(Vector position, double radius, Type type)
	{
		foreach (var entity in Entities)
		{
			if (entity.GetType() != type)
				continue;
			
			double dist = entity.Position.Distance(position);
			if (dist < radius + entity.Size.X / 2F)
				return entity;
		}
		
		return null;
	}

	public int IntersectLine(Vector positionIn, Vector positionOut, out Vector intersectPosition)
	{
		double len = positionIn.Length();

		for (int i = 0; i < (int)len; i++)
		{
			double amount = i / len;
			Vector checkPosition = Vector.Lerp(positionIn, positionOut, amount);

			byte tile = GetTile(checkPosition);
			if (tile != 0)
			{
				intersectPosition = checkPosition;
				return tile;
			}
		}

		intersectPosition = positionOut;

		return 0;
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
