using SFML.Graphics;
using SFML.System;

namespace Game;

public class App
{
	private static void OnClose(object? sender, EventArgs args)
	{
		if(sender == null)
			return;
		
		RenderWindow window = (RenderWindow)sender;
		window.Close();
	}
	
	public static void Main()
	{
		RenderWindow window = new(new(640, 420), "Game");
		
		Entity entity = new();
		entity.SetColor();
		entity.Position = new(0, 0);
		
		window.Closed += OnClose;
		
		while (window.IsOpen)
		{
			window.DispatchEvents();
			
			window.Clear();
			entity.Draw(window);
			window.Display();
		}
	}
}
