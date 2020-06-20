using System.Runtime.CompilerServices;

namespace System
{
	internal static class MathF
	{
		public const float PI = 3.14159274f;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Abs(float x)
		{
			return Math.Abs(x);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Acos(float x)
		{
			return (float)Math.Acos(x);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Cos(float x)
		{
			return (float)Math.Cos(x);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float IEEERemainder(float x, float y)
		{
			return (float)Math.IEEERemainder(x, y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Pow(float x, float y)
		{
			return (float)Math.Pow(x, y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Sin(float x)
		{
			return (float)Math.Sin(x);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Sqrt(float x)
		{
			return (float)Math.Sqrt(x);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Tan(float x)
		{
			return (float)Math.Tan(x);
		}
	}
}
