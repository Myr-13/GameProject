using Game.Engine;
using Game.Game.Entities;
using Game.Game;
using SFML.Graphics;

namespace Game;

public class App
{
	private World _world = new();
	private Graphics _graphics = new();
	
	private void OnClose(object? sender, EventArgs args)
	{
		if(sender == null)
			return;
		
		RenderWindow window = (RenderWindow)sender;
		window.Close();
	}

	private void OnUpdate(float deltaTime)
	{
		_world.Update(deltaTime);
		Input.OnUpdate();
	}

	private void OnRender()
	{
		_world.Draw();
	}
	
	public void Run()
	{
		_graphics.Create(new(640, 420), "Game");
		_world.Init(_graphics);
		
		_graphics.NativeWindow.Closed += OnClose;
		_graphics.NativeWindow.KeyPressed += Input.OnKeyPressed;
		_graphics.NativeWindow.KeyReleased += Input.OnKeyReleased;
		_graphics.OnUpdate += OnUpdate;
		_graphics.OnRender += OnRender;
		
		Player entity = new(_world, new(0, 0));
		_world.AddEntity(entity);
		
		_graphics.Open();
	}
}