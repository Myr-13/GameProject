using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Game.Engine;

public sealed class Window
{
	public RenderWindow NativeWindow = new(0);
	private readonly Clock _clock = new();
	private Time _deltaTime;
	private readonly float _frameTime = 1f / 60;
	
	public delegate void WindowUpdateHandler(float deltaTime);
	public event WindowUpdateHandler OnUpdate;
	public delegate void WindowRenderHandler();
	public event WindowRenderHandler OnRender;

	public void Create(VideoMode mode, string title)
	{
		NativeWindow = new(mode, title);
	}
	
	public void Open()
	{
		while (NativeWindow.IsOpen)
		{
			NativeWindow.DispatchEvents();
			
			_deltaTime = _clock.Restart();
			if (_deltaTime.AsSeconds() < _frameTime)
				Thread.Sleep((int)((_frameTime - _deltaTime.AsSeconds()) * 1000));
			
			OnUpdate(_deltaTime.AsSeconds());
			
			NativeWindow.Clear();
			OnRender();
			NativeWindow.Display();
		}
	}
}
