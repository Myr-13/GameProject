using SFML.Graphics;

namespace Game.Game;

public class World
{
	public List<Entity> Entities = new();

	public void Draw(RenderWindow window)
	{
		foreach (var entity in Entities)
			entity.Draw(window);
	}

	public void Update(float deltaTime)
	{
		foreach (var entity in Entities)
		{
			// Apply physics
			if (entity.Physics)
			{
				entity.Velocity.Y += 1f;
			}
			
			entity.Position += entity.Velocity * deltaTime;
			entity.Update(deltaTime);
		}
	}
	
	public void AddEntity(Entity entity)
	{
		Entities.Add(entity);
	}
}