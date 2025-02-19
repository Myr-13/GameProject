using Game.Engine;
using Game.Game.Entities;
using Game.Game;
using SFML.Graphics;

namespace Game;

public class App
{
	private World _world = new();
	private Window _window = new();
	
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
		_world.Draw(_window.NativeWindow);
	}
	
	public void Run()
	{
		_window.Create(new(640, 420), "Game");
		
		_window.NativeWindow.Closed += OnClose;
		_window.NativeWindow.KeyPressed += Input.OnKeyPressed;
		_window.NativeWindow.KeyReleased += Input.OnKeyReleased;
		_window.OnUpdate += OnUpdate;
		_window.OnRender += OnRender;
		
		Player entity = new(new(0, 0));
		_world.AddEntity(entity);
		
		_window.Open();
	}
}