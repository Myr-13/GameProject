using SFML.System;

namespace Game.Engine;

public class Vector(double x, double y)
{
	public double X = x;
	public double Y = y;

	public Vector() : this(0, 0) { }

	public Vector(Vector2f a) : this(a.X, a.Y) { }
	public Vector(Vector2i a) : this(a.X, a.Y) { }

	public static Vector operator +(Vector a, Vector b) => new(a.X + b.X, a.Y + b.Y);
	public static Vector operator -(Vector a, Vector b) => new(a.X - b.X, a.Y - b.Y);
	public static Vector operator -(Vector a) => new(-a.X, -a.Y);
	public static Vector operator *(Vector a, Vector b) => new(a.X * b.X, a.Y * b.Y);
	public static Vector operator *(Vector a, double b) => new(a.X * b, a.Y * b);
	public static Vector operator /(Vector a, Vector b) => new(a.X / b.X, a.Y / b.Y);
	public static Vector operator /(Vector a, double b) => new(a.X / b, a.Y / b);
	public static implicit operator Vector2f(Vector a) => new((float)a.X, (float)a.Y);
	public static bool operator ==(Vector a, Vector b) => a.X == b.X && a.Y == b.Y;
	public static bool operator !=(Vector a, Vector b) => !(a == b);

	public double Dot(Vector a) => X * a.X + Y * a.Y;
	public double Length() => Math.Sqrt(Dot(this));

	public Vector Normalize()
	{
		double divisor = Length();
		if (divisor == 0)
			return new();
		double l = 1F / divisor;
		return new(X * l, Y * l);
	}

	public static Vector Lerp(Vector a, Vector b, double amount) => a + (b - a) * amount;

	public override string ToString() => $"Vector2({X}, {Y})";
}
