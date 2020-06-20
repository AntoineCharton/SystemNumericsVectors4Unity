using System.Globalization;
using System.Numerics.Hashing;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Numerics
{
	/// <summary>Represents a vector with three  single-precision floating-point values.</summary>
	public struct Vector3 : IEquatable<Vector3>, IFormattable
	{
		/// <summary>The X component of the vector.</summary>
		/// <returns></returns>
		public float X;

		/// <summary>The Y component of the vector.</summary>
		/// <returns></returns>
		public float Y;

		/// <summary>The Z component of the vector.</summary>
		/// <returns></returns>
		public float Z;

		/// <summary>Gets a vector whose 3 elements are equal to zero.</summary>
		/// <returns>A vector whose three elements are equal to zero (that is, it returns the vector (0,0,0).</returns>
		public static Vector3 Zero => default(Vector3);

		/// <summary>Gets a vector whose 3 elements are equal to one.</summary>
		/// <returns>A vector whose three elements are equal to one (that is, it returns the vector (1,1,1).</returns>
		public static Vector3 One => new Vector3(1f, 1f, 1f);

		/// <summary>Gets the vector (1,0,0).</summary>
		/// <returns>The vector (1,0,0).</returns>
		public static Vector3 UnitX => new Vector3(1f, 0f, 0f);

		/// <summary>Gets the vector (0,1,0).</summary>
		/// <returns>The vector (0,1,0)..</returns>
		public static Vector3 UnitY => new Vector3(0f, 1f, 0f);

		/// <summary>Gets the vector (0,0,1).</summary>
		/// <returns>The vector (0,0,1).</returns>
		public static Vector3 UnitZ => new Vector3(0f, 0f, 1f);

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>The hash code.</returns>
		public override int GetHashCode()
		{
			int hashCode = X.GetHashCode();
			hashCode = System.Numerics.Hashing.HashHelpers.Combine(hashCode, Y.GetHashCode());
			return System.Numerics.Hashing.HashHelpers.Combine(hashCode, Z.GetHashCode());
		}

		/// <summary>Returns a value that indicates whether this instance and a specified object are equal.</summary>
		/// <param name="obj">The object to compare with the current instance.</param>
		/// <returns>true if the current instance and <paramref name="obj">obj</paramref> are equal; otherwise, false. If <paramref name="obj">obj</paramref> is null, the method returns false.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object obj)
		{
			if (!(obj is Vector3))
			{
				return false;
			}
			return Equals((Vector3)obj);
		}

		/// <summary>Returns the string representation of the current instance using default formatting.</summary>
		/// <returns>The string representation of the current instance.</returns>
		public override string ToString()
		{
			return ToString("G", CultureInfo.CurrentCulture);
		}

		/// <summary>Returns the string representation of the current instance using the specified format string to format individual elements.</summary>
		/// <param name="format">A  or  that defines the format of individual elements.</param>
		/// <returns>The string representation of the current instance.</returns>
		public string ToString(string format)
		{
			return ToString(format, CultureInfo.CurrentCulture);
		}

		/// <summary>Returns the string representation of the current instance using the specified format string to format individual elements and the specified format provider to define culture-specific formatting.</summary>
		/// <param name="format">A  or  that defines the format of individual elements.</param>
		/// <param name="formatProvider">A format provider that supplies culture-specific formatting information.</param>
		/// <returns>The string representation of the current instance.</returns>
		public string ToString(string format, IFormatProvider formatProvider)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string numberGroupSeparator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;
			stringBuilder.Append('<');
			stringBuilder.Append(((IFormattable)X).ToString(format, formatProvider));
			stringBuilder.Append(numberGroupSeparator);
			stringBuilder.Append(' ');
			stringBuilder.Append(((IFormattable)Y).ToString(format, formatProvider));
			stringBuilder.Append(numberGroupSeparator);
			stringBuilder.Append(' ');
			stringBuilder.Append(((IFormattable)Z).ToString(format, formatProvider));
			stringBuilder.Append('>');
			return stringBuilder.ToString();
		}

		/// <summary>Returns the length of this vector object.</summary>
		/// <returns>The vector&amp;#39;s length.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float Length()
		{
			if (Vector.IsHardwareAccelerated)
			{
				float x = Dot(this, this);
				return MathF.Sqrt(x);
			}
			float x2 = X * X + Y * Y + Z * Z;
			return MathF.Sqrt(x2);
		}

		/// <summary>Returns the length of the vector squared.</summary>
		/// <returns>The vector&amp;#39;s length squared.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float LengthSquared()
		{
			if (Vector.IsHardwareAccelerated)
			{
				return Dot(this, this);
			}
			return X * X + Y * Y + Z * Z;
		}

		/// <summary>Computes the Euclidean distance between the two given points.</summary>
		/// <param name="value1">The first point.</param>
		/// <param name="value2">The second point.</param>
		/// <returns>The distance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Distance(Vector3 value1, Vector3 value2)
		{
			if (Vector.IsHardwareAccelerated)
			{
				Vector3 vector = value1 - value2;
				float x = Dot(vector, vector);
				return MathF.Sqrt(x);
			}
			float num = value1.X - value2.X;
			float num2 = value1.Y - value2.Y;
			float num3 = value1.Z - value2.Z;
			float x2 = num * num + num2 * num2 + num3 * num3;
			return MathF.Sqrt(x2);
		}

		/// <summary>Returns the Euclidean distance squared between two specified points.</summary>
		/// <param name="value1">The first point.</param>
		/// <param name="value2">The second point.</param>
		/// <returns>The distance squared.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float DistanceSquared(Vector3 value1, Vector3 value2)
		{
			if (Vector.IsHardwareAccelerated)
			{
				Vector3 vector = value1 - value2;
				return Dot(vector, vector);
			}
			float num = value1.X - value2.X;
			float num2 = value1.Y - value2.Y;
			float num3 = value1.Z - value2.Z;
			return num * num + num2 * num2 + num3 * num3;
		}

		/// <summary>Returns a vector with the same direction as the specified vector, but with a length of one.</summary>
		/// <param name="value">The vector to normalize.</param>
		/// <returns>The normalized vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Normalize(Vector3 value)
		{
			if (Vector.IsHardwareAccelerated)
			{
				float value2 = value.Length();
				return value / value2;
			}
			float x = value.X * value.X + value.Y * value.Y + value.Z * value.Z;
			float num = MathF.Sqrt(x);
			return new Vector3(value.X / num, value.Y / num, value.Z / num);
		}

		/// <summary>Computes the cross product of two vectors.</summary>
		/// <param name="vector1">The first vector.</param>
		/// <param name="vector2">The second vector.</param>
		/// <returns>The cross product.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Cross(Vector3 vector1, Vector3 vector2)
		{
			return new Vector3(vector1.Y * vector2.Z - vector1.Z * vector2.Y, vector1.Z * vector2.X - vector1.X * vector2.Z, vector1.X * vector2.Y - vector1.Y * vector2.X);
		}

		/// <summary>Returns the reflection of a vector off a surface that has the specified normal.</summary>
		/// <param name="vector">The source vector.</param>
		/// <param name="normal">The normal of the surface being reflected off.</param>
		/// <returns>The reflected vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Reflect(Vector3 vector, Vector3 normal)
		{
			if (Vector.IsHardwareAccelerated)
			{
				float right = Dot(vector, normal);
				Vector3 right2 = normal * right * 2f;
				return vector - right2;
			}
			float num = vector.X * normal.X + vector.Y * normal.Y + vector.Z * normal.Z;
			float num2 = normal.X * num * 2f;
			float num3 = normal.Y * num * 2f;
			float num4 = normal.Z * num * 2f;
			return new Vector3(vector.X - num2, vector.Y - num3, vector.Z - num4);
		}

		/// <summary>Restricts a vector between a minimum and a maximum value.</summary>
		/// <param name="value1">The vector to restrict.</param>
		/// <param name="min">The minimum value.</param>
		/// <param name="max">The maximum value.</param>
		/// <returns>The restricted vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Clamp(Vector3 value1, Vector3 min, Vector3 max)
		{
			float x = value1.X;
			x = ((x > max.X) ? max.X : x);
			x = ((x < min.X) ? min.X : x);
			float y = value1.Y;
			y = ((y > max.Y) ? max.Y : y);
			y = ((y < min.Y) ? min.Y : y);
			float z = value1.Z;
			z = ((z > max.Z) ? max.Z : z);
			z = ((z < min.Z) ? min.Z : z);
			return new Vector3(x, y, z);
		}

		/// <summary>Performs a linear interpolation between two vectors based on the given weighting.</summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <param name="amount">A value between 0 and 1 that indicates the weight of value2.</param>
		/// <returns>The interpolated vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Lerp(Vector3 value1, Vector3 value2, float amount)
		{
			if (Vector.IsHardwareAccelerated)
			{
				Vector3 left = value1 * (1f - amount);
				Vector3 right = value2 * amount;
				return left + right;
			}
			return new Vector3(value1.X + (value2.X - value1.X) * amount, value1.Y + (value2.Y - value1.Y) * amount, value1.Z + (value2.Z - value1.Z) * amount);
		}

		/// <summary>Transforms a vector by a specified 4x4 matrix.</summary>
		/// <param name="position">The vector to transform.</param>
		/// <param name="matrix">The transformation matrix.</param>
		/// <returns>The transformed vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Transform(Vector3 position, Matrix4x4 matrix)
		{
			return new Vector3(position.X * matrix.M11 + position.Y * matrix.M21 + position.Z * matrix.M31 + matrix.M41, position.X * matrix.M12 + position.Y * matrix.M22 + position.Z * matrix.M32 + matrix.M42, position.X * matrix.M13 + position.Y * matrix.M23 + position.Z * matrix.M33 + matrix.M43);
		}

		/// <summary>Transforms a vector normal by the given 4x4 matrix.</summary>
		/// <param name="normal">The source vector.</param>
		/// <param name="matrix">The matrix.</param>
		/// <returns>The transformed vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 TransformNormal(Vector3 normal, Matrix4x4 matrix)
		{
			return new Vector3(normal.X * matrix.M11 + normal.Y * matrix.M21 + normal.Z * matrix.M31, normal.X * matrix.M12 + normal.Y * matrix.M22 + normal.Z * matrix.M32, normal.X * matrix.M13 + normal.Y * matrix.M23 + normal.Z * matrix.M33);
		}

		/// <summary>Transforms a vector by the specified Quaternion rotation value.</summary>
		/// <param name="value">The vector to rotate.</param>
		/// <param name="rotation">The rotation to apply.</param>
		/// <returns>The transformed vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Transform(Vector3 value, Quaternion rotation)
		{
			float num = rotation.X + rotation.X;
			float num2 = rotation.Y + rotation.Y;
			float num3 = rotation.Z + rotation.Z;
			float num4 = rotation.W * num;
			float num5 = rotation.W * num2;
			float num6 = rotation.W * num3;
			float num7 = rotation.X * num;
			float num8 = rotation.X * num2;
			float num9 = rotation.X * num3;
			float num10 = rotation.Y * num2;
			float num11 = rotation.Y * num3;
			float num12 = rotation.Z * num3;
			return new Vector3(value.X * (1f - num10 - num12) + value.Y * (num8 - num6) + value.Z * (num9 + num5), value.X * (num8 + num6) + value.Y * (1f - num7 - num12) + value.Z * (num11 - num4), value.X * (num9 - num5) + value.Y * (num11 + num4) + value.Z * (1f - num7 - num10));
		}

		/// <summary>Adds two vectors together.</summary>
		/// <param name="left">The first vector to add.</param>
		/// <param name="right">The second vector to add.</param>
		/// <returns>The summed vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Add(Vector3 left, Vector3 right)
		{
			return left + right;
		}

		/// <summary>Subtracts the second vector from the first.</summary>
		/// <param name="left">The first vector.</param>
		/// <param name="right">The second vector.</param>
		/// <returns>The difference vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Subtract(Vector3 left, Vector3 right)
		{
			return left - right;
		}

		/// <summary>Multiplies two vectors together.</summary>
		/// <param name="left">The first vector.</param>
		/// <param name="right">The second vector.</param>
		/// <returns>The product vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Multiply(Vector3 left, Vector3 right)
		{
			return left * right;
		}

		/// <summary>Multiplies a vector by a specified scalar.</summary>
		/// <param name="left">The vector to multiply.</param>
		/// <param name="right">The scalar value.</param>
		/// <returns>The scaled vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Multiply(Vector3 left, float right)
		{
			return left * right;
		}

		/// <summary>Multiplies a scalar value by a specified vector.</summary>
		/// <param name="left">The scaled value.</param>
		/// <param name="right">The vector.</param>
		/// <returns>The scaled vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Multiply(float left, Vector3 right)
		{
			return left * right;
		}

		/// <summary>Divides the first vector by the second.</summary>
		/// <param name="left">The first vector.</param>
		/// <param name="right">The second vector.</param>
		/// <returns>The vector resulting from the division.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Divide(Vector3 left, Vector3 right)
		{
			return left / right;
		}

		/// <summary>Divides the specified vector by a specified scalar value.</summary>
		/// <param name="left">The vector.</param>
		/// <param name="divisor">The scalar value.</param>
		/// <returns>The vector that results from the division.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Divide(Vector3 left, float divisor)
		{
			return left / divisor;
		}

		/// <summary>Negates a specified vector.</summary>
		/// <param name="value">The vector to negate.</param>
		/// <returns>The negated vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 Negate(Vector3 value)
		{
			return -value;
		}

		/// <summary>Creates a new <see cref="T:System.Numerics.Vector3"></see> object whose three elements have the same value.</summary>
		/// <param name="value">The value to assign to all three elements.</param>
		[Intrinsic]
		public Vector3(float value)
			: this(value, value, value)
		{
		}

		/// <summary>Creates a   new <see cref="T:System.Numerics.Vector3"></see> object from the specified <see cref="T:System.Numerics.Vector2"></see> object and the specified value.</summary>
		/// <param name="value">The vector with two elements.</param>
		/// <param name="z">The additional value to assign to the <see cref="F:System.Numerics.Vector3.Z"></see> field.</param>
		public Vector3(Vector2 value, float z)
			: this(value.X, value.Y, z)
		{
		}

		/// <summary>Creates a vector whose elements have the specified values.</summary>
		/// <param name="x">The value to assign to the <see cref="F:System.Numerics.Vector3.X"></see> field.</param>
		/// <param name="y">The value to assign to the <see cref="F:System.Numerics.Vector3.Y"></see> field.</param>
		/// <param name="z">The value to assign to the <see cref="F:System.Numerics.Vector3.Z"></see> field.</param>
		[Intrinsic]
		public Vector3(float x, float y, float z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		/// <summary>Copies the elements of the vector to a specified array.</summary>
		/// <param name="array">The destination array.</param>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="array">array</paramref> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in the current instance is greater than in the array.</exception>
		/// <exception cref="T:System.RankException"><paramref name="array">array</paramref> is multidimensional.</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void CopyTo(float[] array)
		{
			CopyTo(array, 0);
		}

		/// <summary>Copies the elements of the vector to a specified array starting at a specified index position.</summary>
		/// <param name="array">The destination array.</param>
		/// <param name="index">The index at which to copy the first element of the vector.</param>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="array">array</paramref> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in the current instance is greater than in the array.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index">index</paramref> is less than zero.  
		///  -or-  
		///  <paramref name="index">index</paramref> is greater than or equal to the array length.</exception>
		/// <exception cref="T:System.RankException"><paramref name="array">array</paramref> is multidimensional.</exception>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[Intrinsic]
		public void CopyTo(float[] array, int index)
		{
			if (array == null)
			{
				throw new NullReferenceException(SR.Arg_NullArgumentNullRef);
			}
			if (index < 0 || index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index", SR.Format(SR.Arg_ArgumentOutOfRangeException, index));
			}
			if (array.Length - index < 3)
			{
				throw new ArgumentException(SR.Format(SR.Arg_ElementsInSourceIsGreaterThanDestination, index));
			}
			array[index] = X;
			array[index + 1] = Y;
			array[index + 2] = Z;
		}

		/// <summary>Returns a value that indicates whether this instance and another vector are equal.</summary>
		/// <param name="other">The other vector.</param>
		/// <returns>true if the two vectors are equal; otherwise, false.</returns>
		[Intrinsic]
		public bool Equals(Vector3 other)
		{
			if (X == other.X && Y == other.Y)
			{
				return Z == other.Z;
			}
			return false;
		}

		/// <summary>Returns the dot product of two vectors.</summary>
		/// <param name="vector1">The first vector.</param>
		/// <param name="vector2">The second vector.</param>
		/// <returns>The dot product.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[Intrinsic]
		public static float Dot(Vector3 vector1, Vector3 vector2)
		{
			return vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z;
		}

		/// <summary>Returns a vector whose elements are the minimum of each of the pairs of elements in two specified vectors.</summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The minimized vector.</returns>
		[Intrinsic]
		public static Vector3 Min(Vector3 value1, Vector3 value2)
		{
			return new Vector3((value1.X < value2.X) ? value1.X : value2.X, (value1.Y < value2.Y) ? value1.Y : value2.Y, (value1.Z < value2.Z) ? value1.Z : value2.Z);
		}

		/// <summary>Returns a vector whose elements are the maximum of each of the pairs of elements in two specified vectors.</summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The maximized vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[Intrinsic]
		public static Vector3 Max(Vector3 value1, Vector3 value2)
		{
			return new Vector3((value1.X > value2.X) ? value1.X : value2.X, (value1.Y > value2.Y) ? value1.Y : value2.Y, (value1.Z > value2.Z) ? value1.Z : value2.Z);
		}

		/// <summary>Returns a vector whose elements are the absolute values of each of the specified vector&amp;#39;s elements.</summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[Intrinsic]
		public static Vector3 Abs(Vector3 value)
		{
			return new Vector3(MathF.Abs(value.X), MathF.Abs(value.Y), MathF.Abs(value.Z));
		}

		/// <summary>Returns a vector whose elements are the square root of each of a specified vector&amp;#39;s elements.</summary>
		/// <param name="value">A vector.</param>
		/// <returns>The square root vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[Intrinsic]
		public static Vector3 SquareRoot(Vector3 value)
		{
			return new Vector3(MathF.Sqrt(value.X), MathF.Sqrt(value.Y), MathF.Sqrt(value.Z));
		}

		/// <summary>Adds two vectors together.</summary>
		/// <param name="left">The first vector to add.</param>
		/// <param name="right">The second vector to add.</param>
		/// <returns>The summed vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[Intrinsic]
		public static Vector3 operator +(Vector3 left, Vector3 right)
		{
			return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
		}

		/// <summary>Subtracts the second vector from the first.</summary>
		/// <param name="left">The first vector.</param>
		/// <param name="right">The second vector.</param>
		/// <returns>The vector that results from subtracting <paramref name="right">right</paramref> from <paramref name="left">left</paramref>.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[Intrinsic]
		public static Vector3 operator -(Vector3 left, Vector3 right)
		{
			return new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
		}

		/// <summary>Multiplies two vectors together.</summary>
		/// <param name="left">The first vector.</param>
		/// <param name="right">The second vector.</param>
		/// <returns>The product vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[Intrinsic]
		public static Vector3 operator *(Vector3 left, Vector3 right)
		{
			return new Vector3(left.X * right.X, left.Y * right.Y, left.Z * right.Z);
		}

		/// <summary>Multiples the specified vector by the specified scalar value.</summary>
		/// <param name="left">The vector.</param>
		/// <param name="right">The scalar value.</param>
		/// <returns>The scaled vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[Intrinsic]
		public static Vector3 operator *(Vector3 left, float right)
		{
			return left * new Vector3(right);
		}

		/// <summary>Multiples the scalar value by the specified vector.</summary>
		/// <param name="left">The vector.</param>
		/// <param name="right">The scalar value.</param>
		/// <returns>The scaled vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[Intrinsic]
		public static Vector3 operator *(float left, Vector3 right)
		{
			return new Vector3(left) * right;
		}

		/// <summary>Divides the first vector by the second.</summary>
		/// <param name="left">The first vector.</param>
		/// <param name="right">The second vector.</param>
		/// <returns>The vector that results from dividing <paramref name="left">left</paramref> by <paramref name="right">right</paramref>.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[Intrinsic]
		public static Vector3 operator /(Vector3 left, Vector3 right)
		{
			return new Vector3(left.X / right.X, left.Y / right.Y, left.Z / right.Z);
		}

		/// <summary>Divides the specified vector by a specified scalar value.</summary>
		/// <param name="value1">The vector.</param>
		/// <param name="value2">The scalar value.</param>
		/// <returns>The result of the division.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator /(Vector3 value1, float value2)
		{
			return value1 / new Vector3(value2);
		}

		/// <summary>Negates the specified vector.</summary>
		/// <param name="value">The vector to negate.</param>
		/// <returns>The negated vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3 operator -(Vector3 value)
		{
			return Zero - value;
		}

		/// <summary>Returns a value that indicates whether each pair of elements in two specified vectors is equal.</summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if <paramref name="left">left</paramref> and <paramref name="right">right</paramref> are equal; otherwise, false.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[Intrinsic]
		public static bool operator ==(Vector3 left, Vector3 right)
		{
			if (left.X == right.X && left.Y == right.Y)
			{
				return left.Z == right.Z;
			}
			return false;
		}

		/// <summary>Returns a value that indicates whether two specified vectors are not equal.</summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if <paramref name="left">left</paramref> and <paramref name="right">right</paramref> are not equal; otherwise, false.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Vector3 left, Vector3 right)
		{
			if (left.X == right.X && left.Y == right.Y)
			{
				return left.Z != right.Z;
			}
			return true;
		}
	}
}
