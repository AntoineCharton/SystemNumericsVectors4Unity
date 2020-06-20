using System.Globalization;
using System.Numerics.Hashing;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Numerics
{
	/// <summary>Represents a vector with four single-precision floating-point values.</summary>
	public struct Vector4 : IEquatable<Vector4>, IFormattable
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

		/// <summary>The W component of the vector.</summary>
		/// <returns></returns>
		public float W;

		/// <summary>Gets a vector whose 4 elements are equal to zero.</summary>
		/// <returns>A vector whose four elements are equal to zero (that is, it returns the vector (0,0,0,0).</returns>
		public static Vector4 Zero => default(Vector4);

		/// <summary>Gets a vector whose 4 elements are equal to one.</summary>
		/// <returns>Returns <see cref="System.Numerics.Vector4"></see>.</returns>
		public static Vector4 One => new Vector4(1f, 1f, 1f, 1f);

		/// <summary>Gets the vector (1,0,0,0).</summary>
		/// <returns>The vector (1,0,0,0).</returns>
		public static Vector4 UnitX => new Vector4(1f, 0f, 0f, 0f);

		/// <summary>Gets the vector (0,1,0,0).</summary>
		/// <returns>The vector (0,1,0,0)..</returns>
		public static Vector4 UnitY => new Vector4(0f, 1f, 0f, 0f);

		/// <summary>Gets a vector whose 4 elements are equal to zero.</summary>
		/// <returns>The vector (0,0,1,0).</returns>
		public static Vector4 UnitZ => new Vector4(0f, 0f, 1f, 0f);

		/// <summary>Gets the vector (0,0,0,1).</summary>
		/// <returns>The vector (0,0,0,1).</returns>
		public static Vector4 UnitW => new Vector4(0f, 0f, 0f, 1f);

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>The hash code.</returns>
		public override int GetHashCode()
		{
			int hashCode = X.GetHashCode();
			hashCode = System.Numerics.Hashing.HashHelpers.Combine(hashCode, Y.GetHashCode());
			hashCode = System.Numerics.Hashing.HashHelpers.Combine(hashCode, Z.GetHashCode());
			return System.Numerics.Hashing.HashHelpers.Combine(hashCode, W.GetHashCode());
		}

		/// <summary>Returns a value that indicates whether this instance and a specified object are equal.</summary>
		/// <param name="obj">The object to compare with the current instance.</param>
		/// <returns>true if the current instance and <paramref name="obj">obj</paramref> are equal; otherwise, false. If <paramref name="obj">obj</paramref> is null, the method returns false.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object obj)
		{
			if (!(obj is Vector4))
			{
				return false;
			}
			return Equals((Vector4)obj);
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
			stringBuilder.Append(X.ToString(format, formatProvider));
			stringBuilder.Append(numberGroupSeparator);
			stringBuilder.Append(' ');
			stringBuilder.Append(Y.ToString(format, formatProvider));
			stringBuilder.Append(numberGroupSeparator);
			stringBuilder.Append(' ');
			stringBuilder.Append(Z.ToString(format, formatProvider));
			stringBuilder.Append(numberGroupSeparator);
			stringBuilder.Append(' ');
			stringBuilder.Append(W.ToString(format, formatProvider));
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
			float x2 = X * X + Y * Y + Z * Z + W * W;
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
			return X * X + Y * Y + Z * Z + W * W;
		}

		/// <summary>Computes the Euclidean distance between the two given points.</summary>
		/// <param name="value1">The first point.</param>
		/// <param name="value2">The second point.</param>
		/// <returns>The distance.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Distance(Vector4 value1, Vector4 value2)
		{
			if (Vector.IsHardwareAccelerated)
			{
				Vector4 vector = value1 - value2;
				float x = Dot(vector, vector);
				return MathF.Sqrt(x);
			}
			float num = value1.X - value2.X;
			float num2 = value1.Y - value2.Y;
			float num3 = value1.Z - value2.Z;
			float num4 = value1.W - value2.W;
			float x2 = num * num + num2 * num2 + num3 * num3 + num4 * num4;
			return MathF.Sqrt(x2);
		}

		/// <summary>Returns the Euclidean distance squared between two specified points.</summary>
		/// <param name="value1">The first point.</param>
		/// <param name="value2">The second point.</param>
		/// <returns>The distance squared.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float DistanceSquared(Vector4 value1, Vector4 value2)
		{
			if (Vector.IsHardwareAccelerated)
			{
				Vector4 vector = value1 - value2;
				return Dot(vector, vector);
			}
			float num = value1.X - value2.X;
			float num2 = value1.Y - value2.Y;
			float num3 = value1.Z - value2.Z;
			float num4 = value1.W - value2.W;
			return num * num + num2 * num2 + num3 * num3 + num4 * num4;
		}

		/// <summary>Returns a vector with the same direction as the specified vector, but with a length of one.</summary>
		/// <param name="vector">The vector to normalize.</param>
		/// <returns>The normalized vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Normalize(Vector4 vector)
		{
			if (Vector.IsHardwareAccelerated)
			{
				float value = vector.Length();
				return vector / value;
			}
			float x = vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z + vector.W * vector.W;
			float num = 1f / MathF.Sqrt(x);
			return new Vector4(vector.X * num, vector.Y * num, vector.Z * num, vector.W * num);
		}

		/// <summary>Restricts a vector between a minimum and a maximum value.</summary>
		/// <param name="value1">The vector to restrict.</param>
		/// <param name="min">The minimum value.</param>
		/// <param name="max">The maximum value.</param>
		/// <returns>The restricted vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Clamp(Vector4 value1, Vector4 min, Vector4 max)
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
			float w = value1.W;
			w = ((w > max.W) ? max.W : w);
			w = ((w < min.W) ? min.W : w);
			return new Vector4(x, y, z, w);
		}

		/// <summary>Performs a linear interpolation between two vectors based on the given weighting.</summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <param name="amount">A value between 0 and 1 that indicates the weight of value2.</param>
		/// <returns>The interpolated vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Lerp(Vector4 value1, Vector4 value2, float amount)
		{
			return new Vector4(value1.X + (value2.X - value1.X) * amount, value1.Y + (value2.Y - value1.Y) * amount, value1.Z + (value2.Z - value1.Z) * amount, value1.W + (value2.W - value1.W) * amount);
		}

		/// <summary>Transforms a two-dimensional vector by a specified 4x4 matrix.</summary>
		/// <param name="position">The vector to transform.</param>
		/// <param name="matrix">The transformation matrix.</param>
		/// <returns>The transformed vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Transform(Vector2 position, Matrix4x4 matrix)
		{
			return new Vector4(position.X * matrix.M11 + position.Y * matrix.M21 + matrix.M41, position.X * matrix.M12 + position.Y * matrix.M22 + matrix.M42, position.X * matrix.M13 + position.Y * matrix.M23 + matrix.M43, position.X * matrix.M14 + position.Y * matrix.M24 + matrix.M44);
		}

		/// <summary>Transforms a three-dimensional vector by a specified 4x4 matrix.</summary>
		/// <param name="position">The vector to transform.</param>
		/// <param name="matrix">The transformation matrix.</param>
		/// <returns>The transformed vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Transform(Vector3 position, Matrix4x4 matrix)
		{
			return new Vector4(position.X * matrix.M11 + position.Y * matrix.M21 + position.Z * matrix.M31 + matrix.M41, position.X * matrix.M12 + position.Y * matrix.M22 + position.Z * matrix.M32 + matrix.M42, position.X * matrix.M13 + position.Y * matrix.M23 + position.Z * matrix.M33 + matrix.M43, position.X * matrix.M14 + position.Y * matrix.M24 + position.Z * matrix.M34 + matrix.M44);
		}

		/// <summary>Transforms a four-dimensional vector by a specified 4x4 matrix.</summary>
		/// <param name="vector">The vector to transform.</param>
		/// <param name="matrix">The transformation matrix.</param>
		/// <returns>The transformed vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Transform(Vector4 vector, Matrix4x4 matrix)
		{
			return new Vector4(vector.X * matrix.M11 + vector.Y * matrix.M21 + vector.Z * matrix.M31 + vector.W * matrix.M41, vector.X * matrix.M12 + vector.Y * matrix.M22 + vector.Z * matrix.M32 + vector.W * matrix.M42, vector.X * matrix.M13 + vector.Y * matrix.M23 + vector.Z * matrix.M33 + vector.W * matrix.M43, vector.X * matrix.M14 + vector.Y * matrix.M24 + vector.Z * matrix.M34 + vector.W * matrix.M44);
		}

		/// <summary>Transforms a two-dimensional vector by the specified Quaternion rotation value.</summary>
		/// <param name="value">The vector to rotate.</param>
		/// <param name="rotation">The rotation to apply.</param>
		/// <returns>The transformed vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Transform(Vector2 value, Quaternion rotation)
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
			return new Vector4(value.X * (1f - num10 - num12) + value.Y * (num8 - num6), value.X * (num8 + num6) + value.Y * (1f - num7 - num12), value.X * (num9 - num5) + value.Y * (num11 + num4), 1f);
		}

		/// <summary>Transforms a three-dimensional vector by the specified Quaternion rotation value.</summary>
		/// <param name="value">The vector to rotate.</param>
		/// <param name="rotation">The rotation to apply.</param>
		/// <returns>The transformed vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Transform(Vector3 value, Quaternion rotation)
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
			return new Vector4(value.X * (1f - num10 - num12) + value.Y * (num8 - num6) + value.Z * (num9 + num5), value.X * (num8 + num6) + value.Y * (1f - num7 - num12) + value.Z * (num11 - num4), value.X * (num9 - num5) + value.Y * (num11 + num4) + value.Z * (1f - num7 - num10), 1f);
		}

		/// <summary>Transforms a four-dimensional vector by the specified Quaternion rotation value.</summary>
		/// <param name="value">The vector to rotate.</param>
		/// <param name="rotation">The rotation to apply.</param>
		/// <returns>The transformed vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Transform(Vector4 value, Quaternion rotation)
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
			return new Vector4(value.X * (1f - num10 - num12) + value.Y * (num8 - num6) + value.Z * (num9 + num5), value.X * (num8 + num6) + value.Y * (1f - num7 - num12) + value.Z * (num11 - num4), value.X * (num9 - num5) + value.Y * (num11 + num4) + value.Z * (1f - num7 - num10), value.W);
		}

		/// <summary>Adds two vectors together.</summary>
		/// <param name="left">The first vector to add.</param>
		/// <param name="right">The second vector to add.</param>
		/// <returns>The summed vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Add(Vector4 left, Vector4 right)
		{
			return left + right;
		}

		/// <summary>Subtracts the second vector from the first.</summary>
		/// <param name="left">The first vector.</param>
		/// <param name="right">The second vector.</param>
		/// <returns>The difference vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Subtract(Vector4 left, Vector4 right)
		{
			return left - right;
		}

		/// <summary>Multiplies two vectors together.</summary>
		/// <param name="left">The first vector.</param>
		/// <param name="right">The second vector.</param>
		/// <returns>The product vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Multiply(Vector4 left, Vector4 right)
		{
			return left * right;
		}

		/// <summary>Multiplies a vector by a specified scalar.</summary>
		/// <param name="left">The vector to multiply.</param>
		/// <param name="right">The scalar value.</param>
		/// <returns>The scaled vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Multiply(Vector4 left, float right)
		{
			return left * new Vector4(right, right, right, right);
		}

		/// <summary>Multiplies a scalar value by a specified vector.</summary>
		/// <param name="left">The scaled value.</param>
		/// <param name="right">The vector.</param>
		/// <returns>The scaled vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Multiply(float left, Vector4 right)
		{
			return new Vector4(left, left, left, left) * right;
		}

		/// <summary>Divides the first vector by the second.</summary>
		/// <param name="left">The first vector.</param>
		/// <param name="right">The second vector.</param>
		/// <returns>The vector resulting from the division.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Divide(Vector4 left, Vector4 right)
		{
			return left / right;
		}

		/// <summary>Divides the specified vector by a specified scalar value.</summary>
		/// <param name="left">The vector.</param>
		/// <param name="divisor">The scalar value.</param>
		/// <returns>The vector that results from the division.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Divide(Vector4 left, float divisor)
		{
			return left / divisor;
		}

		/// <summary>Negates a specified vector.</summary>
		/// <param name="value">The vector to negate.</param>
		/// <returns>The negated vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 Negate(Vector4 value)
		{
			return -value;
		}

		/// <summary>Creates a new <see cref="T:System.Numerics.Vector4"></see> object whose four elements have the same value.</summary>
		/// <param name="value">The value to assign to all four elements.</param>
		[Intrinsic]
		public Vector4(float value)
			: this(value, value, value, value)
		{
		}

		/// <summary>Creates a vector whose elements have the specified values.</summary>
		/// <param name="x">The value to assign to the <see cref="F:System.Numerics.Vector4.X"></see> field.</param>
		/// <param name="y">The value to assign to the <see cref="F:System.Numerics.Vector4.Y"></see> field.</param>
		/// <param name="z">The value to assign to the <see cref="F:System.Numerics.Vector4.Z"></see> field.</param>
		/// <param name="w">The value to assign to the <see cref="F:System.Numerics.Vector4.W"></see> field.</param>
		[Intrinsic]
		public Vector4(float x, float y, float z, float w)
		{
			W = w;
			X = x;
			Y = y;
			Z = z;
		}

		/// <summary>Creates a   new <see cref="T:System.Numerics.Vector4"></see> object from the specified <see cref="T:System.Numerics.Vector2"></see> object and a Z and a W component.</summary>
		/// <param name="value">The vector to use for the X and Y components.</param>
		/// <param name="z">The Z component.</param>
		/// <param name="w">The W component.</param>
		public Vector4(Vector2 value, float z, float w)
		{
			X = value.X;
			Y = value.Y;
			Z = z;
			W = w;
		}

		/// <summary>Constructs a new <see cref="T:System.Numerics.Vector4"></see> object from the specified <see cref="T:System.Numerics.Vector3"></see> object and a W component.</summary>
		/// <param name="value">The vector to use for the X, Y, and Z components.</param>
		/// <param name="w">The W component.</param>
		public Vector4(Vector3 value, float w)
		{
			X = value.X;
			Y = value.Y;
			Z = value.Z;
			W = w;
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
			if (array.Length - index < 4)
			{
				throw new ArgumentException(SR.Format(SR.Arg_ElementsInSourceIsGreaterThanDestination, index));
			}
			array[index] = X;
			array[index + 1] = Y;
			array[index + 2] = Z;
			array[index + 3] = W;
		}

		/// <summary>Returns a value that indicates whether this instance and another vector are equal.</summary>
		/// <param name="other">The other vector.</param>
		/// <returns>true if the two vectors are equal; otherwise, false.</returns>
		[Intrinsic]
		public bool Equals(Vector4 other)
		{
			if (X == other.X && Y == other.Y && Z == other.Z)
			{
				return W == other.W;
			}
			return false;
		}

		/// <summary>Returns the dot product of two vectors.</summary>
		/// <param name="vector1">The first vector.</param>
		/// <param name="vector2">The second vector.</param>
		/// <returns>The dot product.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[Intrinsic]
		public static float Dot(Vector4 vector1, Vector4 vector2)
		{
			return vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z + vector1.W * vector2.W;
		}

		/// <summary>Returns a vector whose elements are the minimum of each of the pairs of elements in two specified vectors.</summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The minimized vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[Intrinsic]
		public static Vector4 Min(Vector4 value1, Vector4 value2)
		{
			return new Vector4((value1.X < value2.X) ? value1.X : value2.X, (value1.Y < value2.Y) ? value1.Y : value2.Y, (value1.Z < value2.Z) ? value1.Z : value2.Z, (value1.W < value2.W) ? value1.W : value2.W);
		}

		/// <summary>Returns a vector whose elements are the maximum of each of the pairs of elements in two specified vectors.</summary>
		/// <param name="value1">The first vector.</param>
		/// <param name="value2">The second vector.</param>
		/// <returns>The maximized vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[Intrinsic]
		public static Vector4 Max(Vector4 value1, Vector4 value2)
		{
			return new Vector4((value1.X > value2.X) ? value1.X : value2.X, (value1.Y > value2.Y) ? value1.Y : value2.Y, (value1.Z > value2.Z) ? value1.Z : value2.Z, (value1.W > value2.W) ? value1.W : value2.W);
		}

		/// <summary>Returns a vector whose elements are the absolute values of each of the specified vector&amp;#39;s elements.</summary>
		/// <param name="value">A vector.</param>
		/// <returns>The absolute value vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[Intrinsic]
		public static Vector4 Abs(Vector4 value)
		{
			return new Vector4(MathF.Abs(value.X), MathF.Abs(value.Y), MathF.Abs(value.Z), MathF.Abs(value.W));
		}

		/// <summary>Returns a vector whose elements are the square root of each of a specified vector&amp;#39;s elements.</summary>
		/// <param name="value">A vector.</param>
		/// <returns>The square root vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[Intrinsic]
		public static Vector4 SquareRoot(Vector4 value)
		{
			return new Vector4(MathF.Sqrt(value.X), MathF.Sqrt(value.Y), MathF.Sqrt(value.Z), MathF.Sqrt(value.W));
		}

		/// <summary>Adds two vectors together.</summary>
		/// <param name="left">The first vector to add.</param>
		/// <param name="right">The second vector to add.</param>
		/// <returns>The summed vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[Intrinsic]
		public static Vector4 operator +(Vector4 left, Vector4 right)
		{
			return new Vector4(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
		}

		/// <summary>Subtracts the second vector from the first.</summary>
		/// <param name="left">The first vector.</param>
		/// <param name="right">The second vector.</param>
		/// <returns>The vector that results from subtracting <paramref name="right">right</paramref> from <paramref name="left">left</paramref>.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[Intrinsic]
		public static Vector4 operator -(Vector4 left, Vector4 right)
		{
			return new Vector4(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);
		}

		/// <summary>Multiplies two vectors together.</summary>
		/// <param name="left">The first vector.</param>
		/// <param name="right">The second vector.</param>
		/// <returns>The product vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[Intrinsic]
		public static Vector4 operator *(Vector4 left, Vector4 right)
		{
			return new Vector4(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);
		}

		/// <summary>Multiples the specified vector by the specified scalar value.</summary>
		/// <param name="left">The vector.</param>
		/// <param name="right">The scalar value.</param>
		/// <returns>The scaled vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[Intrinsic]
		public static Vector4 operator *(Vector4 left, float right)
		{
			return left * new Vector4(right);
		}

		/// <summary>Multiples the scalar value by the specified vector.</summary>
		/// <param name="left">The vector.</param>
		/// <param name="right">The scalar value.</param>
		/// <returns>The scaled vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[Intrinsic]
		public static Vector4 operator *(float left, Vector4 right)
		{
			return new Vector4(left) * right;
		}

		/// <summary>Divides the first vector by the second.</summary>
		/// <param name="left">The first vector.</param>
		/// <param name="right">The second vector.</param>
		/// <returns>The vector that results from dividing <paramref name="left">left</paramref> by <paramref name="right">right</paramref>.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[Intrinsic]
		public static Vector4 operator /(Vector4 left, Vector4 right)
		{
			return new Vector4(left.X / right.X, left.Y / right.Y, left.Z / right.Z, left.W / right.W);
		}

		/// <summary>Divides the specified vector by a specified scalar value.</summary>
		/// <param name="value1">The vector.</param>
		/// <param name="value2">The scalar value.</param>
		/// <returns>The result of the division.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator /(Vector4 value1, float value2)
		{
			return value1 / new Vector4(value2);
		}

		/// <summary>Negates the specified vector.</summary>
		/// <param name="value">The vector to negate.</param>
		/// <returns>The negated vector.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4 operator -(Vector4 value)
		{
			return Zero - value;
		}

		/// <summary>Returns a value that indicates whether each pair of elements in two specified vectors is equal.</summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if <paramref name="left">left</paramref> and <paramref name="right">right</paramref> are equal; otherwise, false.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[Intrinsic]
		public static bool operator ==(Vector4 left, Vector4 right)
		{
			return left.Equals(right);
		}

		/// <summary>Returns a value that indicates whether two specified vectors are not equal.</summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>true if <paramref name="left">left</paramref> and <paramref name="right">right</paramref> are not equal; otherwise, false.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Vector4 left, Vector4 right)
		{
			return !(left == right);
		}
	}
}
