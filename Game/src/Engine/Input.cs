using SFML.System;
using SFML.Window;

namespace Game.Engine;

public class Input
{
	public static Graphics Graphics;
	private static readonly bool[] Keys = new bool[(int)Keyboard.Key.KeyCount];
	private static bool[] _frameKeys = new bool[(int)Keyboard.Key.KeyCount];
	
	public static void OnKeyPressed(object? sender, EventArgs args)
	{
		KeyEventArgs? keyArgs = args as KeyEventArgs;
		if(sender == null || keyArgs == null)
			return;
		
		Keys[(int)keyArgs.Code] = true;
		_frameKeys[(int)keyArgs.Code] = true;
	}
	
	public static void OnKeyReleased(object? sender, EventArgs args)
	{
		KeyEventArgs? keyArgs = args as KeyEventArgs;
		if(sender == null || keyArgs == null)
			return;
		
		Keys[(int)keyArgs.Code] = false;
	}

	public static void OnUpdate()
	{
		_frameKeys = new bool[(int)Keyboard.Key.KeyCount];
	}
	
	public static bool IsKeyPressing(Keyboard.Key key) => Keys[(int)key];
	public static bool IsKeyPressed(Keyboard.Key key) => _frameKeys[(int)key];
	public static Vector MousePosition() => new(Graphics.NativeWindow.MapPixelToCoords(Mouse.GetPosition(Graphics.NativeWindow)));
}
