using System.Diagnostics;
using System.Reflection;
using Game.Engine;
using Game.Game.Entities;
using Game.Game;
using Game.Game.Components;
using SFML.Graphics;

namespace Game;

public class App
{
	private readonly List<Component> _components = new();
	public readonly World World = new();
	public readonly Graphics Graphics = new();
	
	// Components
	public ParticleManager ParticleManager;
	public Camera Camera;

	private void OnClose(object? sender, EventArgs args)
	{
		if(sender == null)
			return;
		
		RenderWindow window = (RenderWindow)sender;
		window.Close();
	}

	private void OnUpdate(float deltaTime)
	{
		World.Update(deltaTime);
		Input.OnUpdate();

		foreach (Component component in _components)
		{
			component.OnUpdate(deltaTime);
		}
	}

	private void OnRender()
	{
		World.Draw();

		foreach (Component component in _components)
		{
			component.OnDraw();
		}
	}

	private void InitComponents()
	{
		var allComponents = Assembly.GetEntryAssembly()!.GetTypes()
			.Where(t => t.IsSubclassOf(typeof(Component)) && t != typeof(Component));

		Stopwatch sw = new();
		
		foreach (var componentType in allComponents)
		{
			sw.Restart();
			
			dynamic component = Activator.CreateInstance(componentType, this)!;
			component.OnInit();
			_components.Add(component);
			GetType().GetFields().FirstOrDefault(p => p.FieldType == componentType)?.SetValue(this, component);
			
			sw.Stop();
			Console.WriteLine($"Loaded component `{componentType.Name}` in {sw.ElapsedMilliseconds} ms");
		}
	}
	
	public void Run()
	{
		// Init base systems
		Graphics.Create(new(640, 420), "Game");
		World.Init(Graphics);
		Input.Graphics = Graphics;
		
		// Init handlers
		Graphics.NativeWindow.Closed += OnClose;
		Graphics.NativeWindow.KeyPressed += Input.OnKeyPressed;
		Graphics.NativeWindow.KeyReleased += Input.OnKeyReleased;
		Graphics.OnUpdate += OnUpdate;
		Graphics.OnRender += OnRender;
		
		// Test
		Player player = new(this, new(128, 0));
		World.AddEntity(player);

		Enemy enemy = new(this, new(64, 0));
		World.AddEntity(enemy);
		
		InitComponents();
					
		Graphics.Open();
	}
}
