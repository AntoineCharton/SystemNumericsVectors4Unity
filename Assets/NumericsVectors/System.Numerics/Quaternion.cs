using System.Globalization;

namespace System.Numerics
{
	/// <summary>Represents a vector that is used to encode three-dimensional physical rotations.</summary>
	public struct Quaternion : IEquatable<Quaternion>
	{
		/// <summary>The X value of the vector component of the quaternion.</summary>
		/// <returns></returns>
		public float X;

		/// <summary>The Y value of the vector component of the quaternion.</summary>
		/// <returns></returns>
		public float Y;

		/// <summary>The Z value of the vector component of the quaternion.</summary>
		/// <returns></returns>
		public float Z;

		/// <summary>The rotation component of the quaternion.</summary>
		/// <returns></returns>
		public float W;

		/// <summary>Gets a quaternion that represents no rotation.</summary>
		/// <returns>A quaternion whose values are (0, 0, 0, 1).</returns>
		public static Quaternion Identity => new Quaternion(0f, 0f, 0f, 1f);

		/// <summary>Gets a value that indicates whether the current instance is the identity quaternion.</summary>
		/// <returns>true if the current instance is the identity quaternion; otherwise, false.</returns>
		public bool IsIdentity
		{
			get
			{
				if (X == 0f && Y == 0f && Z == 0f)
				{
					return W == 1f;
				}
				return false;
			}
		}

		/// <summary>Constructs a quaternion from the specified components.</summary>
		/// <param name="x">The value to assign to the X component of the quaternion.</param>
		/// <param name="y">The value to assign to the Y component of the quaternion.</param>
		/// <param name="z">The value to assign to the Z component of the quaternion.</param>
		/// <param name="w">The value to assign to the W component of the quaternion.</param>
		public Quaternion(float x, float y, float z, float w)
		{
			X = x;
			Y = y;
			Z = z;
			W = w;
		}

		/// <summary>Creates a quaternion from the specified vector and rotation parts.</summary>
		/// <param name="vectorPart">The vector part of the quaternion.</param>
		/// <param name="scalarPart">The rotation part of the quaternion.</param>
		public Quaternion(Vector3 vectorPart, float scalarPart)
		{
			X = vectorPart.X;
			Y = vectorPart.Y;
			Z = vectorPart.Z;
			W = scalarPart;
		}

		/// <summary>Calculates the length of the quaternion.</summary>
		/// <returns>The computed length of the quaternion.</returns>
		public float Length()
		{
			float x = X * X + Y * Y + Z * Z + W * W;
			return MathF.Sqrt(x);
		}

		/// <summary>Calculates the squared length of the quaternion.</summary>
		/// <returns>The length squared of the quaternion.</returns>
		public float LengthSquared()
		{
			return X * X + Y * Y + Z * Z + W * W;
		}

		/// <summary>Divides each component of a specified <see cref="T:System.Numerics.Quaternion"></see> by its length.</summary>
		/// <param name="value">The quaternion to normalize.</param>
		/// <returns>The normalized quaternion.</returns>
		public static Quaternion Normalize(Quaternion value)
		{
			float x = value.X * value.X + value.Y * value.Y + value.Z * value.Z + value.W * value.W;
			float num = 1f / MathF.Sqrt(x);
			Quaternion result = default(Quaternion);
			result.X = value.X * num;
			result.Y = value.Y * num;
			result.Z = value.Z * num;
			result.W = value.W * num;
			return result;
		}

		/// <summary>Returns the conjugate of a specified quaternion.</summary>
		/// <param name="value">The quaternion.</param>
		/// <returns>A new quaternion that is the conjugate of value.</returns>
		public static Quaternion Conjugate(Quaternion value)
		{
			Quaternion result = default(Quaternion);
			result.X = 0f - value.X;
			result.Y = 0f - value.Y;
			result.Z = 0f - value.Z;
			result.W = value.W;
			return result;
		}

		/// <summary>Returns the inverse of a quaternion.</summary>
		/// <param name="value">The quaternion.</param>
		/// <returns>The inverted quaternion.</returns>
		public static Quaternion Inverse(Quaternion value)
		{
			float num = value.X * value.X + value.Y * value.Y + value.Z * value.Z + value.W * value.W;
			float num2 = 1f / num;
			Quaternion result = default(Quaternion);
			result.X = (0f - value.X) * num2;
			result.Y = (0f - value.Y) * num2;
			result.Z = (0f - value.Z) * num2;
			result.W = value.W * num2;
			return result;
		}

		/// <summary>Creates a quaternion from a vector and an angle to rotate about the vector.</summary>
		/// <param name="axis">The vector to rotate around.</param>
		/// <param name="angle">The angle, in radians, to rotate around the vector.</param>
		/// <returns>The newly created quaternion.</returns>
		public static Quaternion CreateFromAxisAngle(Vector3 axis, float angle)
		{
			float x = angle * 0.5f;
			float num = MathF.Sin(x);
			float w = MathF.Cos(x);
			Quaternion result = default(Quaternion);
			result.X = axis.X * num;
			result.Y = axis.Y * num;
			result.Z = axis.Z * num;
			result.W = w;
			return result;
		}

		/// <summary>Creates a new quaternion from the given yaw, pitch, and roll.</summary>
		/// <param name="yaw">The yaw angle, in radians, around the Y axis.</param>
		/// <param name="pitch">The pitch angle, in radians, around the X axis.</param>
		/// <param name="roll">The roll angle, in radians, around the Z axis.</param>
		/// <returns>The resulting quaternion.</returns>
		public static Quaternion CreateFromYawPitchRoll(float yaw, float pitch, float roll)
		{
			float x = roll * 0.5f;
			float num = MathF.Sin(x);
			float num2 = MathF.Cos(x);
			float x2 = pitch * 0.5f;
			float num3 = MathF.Sin(x2);
			float num4 = MathF.Cos(x2);
			float x3 = yaw * 0.5f;
			float num5 = MathF.Sin(x3);
			float num6 = MathF.Cos(x3);
			Quaternion result = default(Quaternion);
			result.X = num6 * num3 * num2 + num5 * num4 * num;
			result.Y = num5 * num4 * num2 - num6 * num3 * num;
			result.Z = num6 * num4 * num - num5 * num3 * num2;
			result.W = num6 * num4 * num2 + num5 * num3 * num;
			return result;
		}

		/// <summary>Creates a quaternion from the specified rotation matrix.</summary>
		/// <param name="matrix">The rotation matrix.</param>
		/// <returns>The newly created quaternion.</returns>
		public static Quaternion CreateFromRotationMatrix(Matrix4x4 matrix)
		{
			float num = matrix.M11 + matrix.M22 + matrix.M33;
			Quaternion result = default(Quaternion);
			if (num > 0f)
			{
				float num2 = MathF.Sqrt(num + 1f);
				result.W = num2 * 0.5f;
				num2 = 0.5f / num2;
				result.X = (matrix.M23 - matrix.M32) * num2;
				result.Y = (matrix.M31 - matrix.M13) * num2;
				result.Z = (matrix.M12 - matrix.M21) * num2;
			}
			else if (matrix.M11 >= matrix.M22 && matrix.M11 >= matrix.M33)
			{
				float num3 = MathF.Sqrt(1f + matrix.M11 - matrix.M22 - matrix.M33);
				float num4 = 0.5f / num3;
				result.X = 0.5f * num3;
				result.Y = (matrix.M12 + matrix.M21) * num4;
				result.Z = (matrix.M13 + matrix.M31) * num4;
				result.W = (matrix.M23 - matrix.M32) * num4;
			}
			else if (matrix.M22 > matrix.M33)
			{
				float num5 = MathF.Sqrt(1f + matrix.M22 - matrix.M11 - matrix.M33);
				float num6 = 0.5f / num5;
				result.X = (matrix.M21 + matrix.M12) * num6;
				result.Y = 0.5f * num5;
				result.Z = (matrix.M32 + matrix.M23) * num6;
				result.W = (matrix.M31 - matrix.M13) * num6;
			}
			else
			{
				float num7 = MathF.Sqrt(1f + matrix.M33 - matrix.M11 - matrix.M22);
				float num8 = 0.5f / num7;
				result.X = (matrix.M31 + matrix.M13) * num8;
				result.Y = (matrix.M32 + matrix.M23) * num8;
				result.Z = 0.5f * num7;
				result.W = (matrix.M12 - matrix.M21) * num8;
			}
			return result;
		}

		/// <summary>Calculates the dot product of two quaternions.</summary>
		/// <param name="quaternion1">The first quaternion.</param>
		/// <param name="quaternion2">The second quaternion.</param>
		/// <returns>The dot product.</returns>
		public static float Dot(Quaternion quaternion1, Quaternion quaternion2)
		{
			return quaternion1.X * quaternion2.X + quaternion1.Y * quaternion2.Y + quaternion1.Z * quaternion2.Z + quaternion1.W * quaternion2.W;
		}

		/// <summary>Interpolates between two quaternions, using spherical linear interpolation.</summary>
		/// <param name="quaternion1">The first quaternion.</param>
		/// <param name="quaternion2">The second quaternion.</param>
		/// <param name="amount">The relative weight of the second quaternion in the interpolation.</param>
		/// <returns>The interpolated quaternion.</returns>
		public static Quaternion Slerp(Quaternion quaternion1, Quaternion quaternion2, float amount)
		{
			float num = quaternion1.X * quaternion2.X + quaternion1.Y * quaternion2.Y + quaternion1.Z * quaternion2.Z + quaternion1.W * quaternion2.W;
			bool flag = false;
			if (num < 0f)
			{
				flag = true;
				num = 0f - num;
			}
			float num2;
			float num3;
			if (num > 0.999999f)
			{
				num2 = 1f - amount;
				num3 = (flag ? (0f - amount) : amount);
			}
			else
			{
				float num4 = MathF.Acos(num);
				float num5 = 1f / MathF.Sin(num4);
				num2 = MathF.Sin((1f - amount) * num4) * num5;
				num3 = (flag ? ((0f - MathF.Sin(amount * num4)) * num5) : (MathF.Sin(amount * num4) * num5));
			}
			Quaternion result = default(Quaternion);
			result.X = num2 * quaternion1.X + num3 * quaternion2.X;
			result.Y = num2 * quaternion1.Y + num3 * quaternion2.Y;
			result.Z = num2 * quaternion1.Z + num3 * quaternion2.Z;
			result.W = num2 * quaternion1.W + num3 * quaternion2.W;
			return result;
		}

		/// <summary>Performs a linear interpolation between two quaternions based on a value that specifies the weighting of the second quaternion.</summary>
		/// <param name="quaternion1">The first quaternion.</param>
		/// <param name="quaternion2">The second quaternion.</param>
		/// <param name="amount">The relative weight of quaternion2 in the interpolation.</param>
		/// <returns>The interpolated quaternion.</returns>
		public static Quaternion Lerp(Quaternion quaternion1, Quaternion quaternion2, float amount)
		{
			float num = 1f - amount;
			Quaternion result = default(Quaternion);
			float num2 = quaternion1.X * quaternion2.X + quaternion1.Y * quaternion2.Y + quaternion1.Z * quaternion2.Z + quaternion1.W * quaternion2.W;
			if (num2 >= 0f)
			{
				result.X = num * quaternion1.X + amount * quaternion2.X;
				result.Y = num * quaternion1.Y + amount * quaternion2.Y;
				result.Z = num * quaternion1.Z + amount * quaternion2.Z;
				result.W = num * quaternion1.W + amount * quaternion2.W;
			}
			else
			{
				result.X = num * quaternion1.X - amount * quaternion2.X;
				result.Y = num * quaternion1.Y - amount * quaternion2.Y;
				result.Z = num * quaternion1.Z - amount * quaternion2.Z;
				result.W = num * quaternion1.W - amount * quaternion2.W;
			}
			float x = result.X * result.X + result.Y * result.Y + result.Z * result.Z + result.W * result.W;
			float num3 = 1f / MathF.Sqrt(x);
			result.X *= num3;
			result.Y *= num3;
			result.Z *= num3;
			result.W *= num3;
			return result;
		}

		/// <summary>Concatenates two quaternions.</summary>
		/// <param name="value1">The first quaternion rotation in the series.</param>
		/// <param name="value2">The second quaternion rotation in the series.</param>
		/// <returns>A new quaternion representing the concatenation of the <paramref name="value1">value1</paramref> rotation followed by the <paramref name="value2">value2</paramref> rotation.</returns>
		public static Quaternion Concatenate(Quaternion value1, Quaternion value2)
		{
			float x = value2.X;
			float y = value2.Y;
			float z = value2.Z;
			float w = value2.W;
			float x2 = value1.X;
			float y2 = value1.Y;
			float z2 = value1.Z;
			float w2 = value1.W;
			float num = y * z2 - z * y2;
			float num2 = z * x2 - x * z2;
			float num3 = x * y2 - y * x2;
			float num4 = x * x2 + y * y2 + z * z2;
			Quaternion result = default(Quaternion);
			result.X = x * w2 + x2 * w + num;
			result.Y = y * w2 + y2 * w + num2;
			result.Z = z * w2 + z2 * w + num3;
			result.W = w * w2 - num4;
			return result;
		}

		/// <summary>Reverses the sign of each component of the quaternion.</summary>
		/// <param name="value">The quaternion to negate.</param>
		/// <returns>The negated quaternion.</returns>
		public static Quaternion Negate(Quaternion value)
		{
			Quaternion result = default(Quaternion);
			result.X = 0f - value.X;
			result.Y = 0f - value.Y;
			result.Z = 0f - value.Z;
			result.W = 0f - value.W;
			return result;
		}

		/// <summary>Adds each element in one quaternion with its corresponding element in a second quaternion.</summary>
		/// <param name="value1">The first quaternion.</param>
		/// <param name="value2">The second quaternion.</param>
		/// <returns>The quaternion that contains the summed values of <paramref name="value1">value1</paramref> and <paramref name="value2">value2</paramref>.</returns>
		public static Quaternion Add(Quaternion value1, Quaternion value2)
		{
			Quaternion result = default(Quaternion);
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
			result.Z = value1.Z + value2.Z;
			result.W = value1.W + value2.W;
			return result;
		}

		/// <summary>Subtracts each element in a second quaternion from its corresponding element in a first quaternion.</summary>
		/// <param name="value1">The first quaternion.</param>
		/// <param name="value2">The second quaternion.</param>
		/// <returns>The quaternion containing the values that result from subtracting each element in <paramref name="value2">value2</paramref> from its corresponding element in <paramref name="value1">value1</paramref>.</returns>
		public static Quaternion Subtract(Quaternion value1, Quaternion value2)
		{
			Quaternion result = default(Quaternion);
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
			result.Z = value1.Z - value2.Z;
			result.W = value1.W - value2.W;
			return result;
		}

		/// <summary>Returns the quaternion that results from multiplying two quaternions together.</summary>
		/// <param name="value1">The first quaternion.</param>
		/// <param name="value2">The second quaternion.</param>
		/// <returns>The product quaternion.</returns>
		public static Quaternion Multiply(Quaternion value1, Quaternion value2)
		{
			float x = value1.X;
			float y = value1.Y;
			float z = value1.Z;
			float w = value1.W;
			float x2 = value2.X;
			float y2 = value2.Y;
			float z2 = value2.Z;
			float w2 = value2.W;
			float num = y * z2 - z * y2;
			float num2 = z * x2 - x * z2;
			float num3 = x * y2 - y * x2;
			float num4 = x * x2 + y * y2 + z * z2;
			Quaternion result = default(Quaternion);
			result.X = x * w2 + x2 * w + num;
			result.Y = y * w2 + y2 * w + num2;
			result.Z = z * w2 + z2 * w + num3;
			result.W = w * w2 - num4;
			return result;
		}

		/// <summary>Returns the quaternion that results from scaling all the components of a specified quaternion by a scalar factor.</summary>
		/// <param name="value1">The source quaternion.</param>
		/// <param name="value2">The scalar value.</param>
		/// <returns>The scaled quaternion.</returns>
		public static Quaternion Multiply(Quaternion value1, float value2)
		{
			Quaternion result = default(Quaternion);
			result.X = value1.X * value2;
			result.Y = value1.Y * value2;
			result.Z = value1.Z * value2;
			result.W = value1.W * value2;
			return result;
		}

		/// <summary>Divides one quaternion by a second quaternion.</summary>
		/// <param name="value1">The dividend.</param>
		/// <param name="value2">The divisor.</param>
		/// <returns>The quaternion that results from dividing <paramref name="value1">value1</paramref> by <paramref name="value2">value2</paramref>.</returns>
		public static Quaternion Divide(Quaternion value1, Quaternion value2)
		{
			float x = value1.X;
			float y = value1.Y;
			float z = value1.Z;
			float w = value1.W;
			float num = value2.X * value2.X + value2.Y * value2.Y + value2.Z * value2.Z + value2.W * value2.W;
			float num2 = 1f / num;
			float num3 = (0f - value2.X) * num2;
			float num4 = (0f - value2.Y) * num2;
			float num5 = (0f - value2.Z) * num2;
			float num6 = value2.W * num2;
			float num7 = y * num5 - z * num4;
			float num8 = z * num3 - x * num5;
			float num9 = x * num4 - y * num3;
			float num10 = x * num3 + y * num4 + z * num5;
			Quaternion result = default(Quaternion);
			result.X = x * num6 + num3 * w + num7;
			result.Y = y * num6 + num4 * w + num8;
			result.Z = z * num6 + num5 * w + num9;
			result.W = w * num6 - num10;
			return result;
		}

		/// <summary>Reverses the sign of each component of the quaternion.</summary>
		/// <param name="value">The quaternion to negate.</param>
		/// <returns>The negated quaternion.</returns>
		public static Quaternion operator -(Quaternion value)
		{
			Quaternion result = default(Quaternion);
			result.X = 0f - value.X;
			result.Y = 0f - value.Y;
			result.Z = 0f - value.Z;
			result.W = 0f - value.W;
			return result;
		}

		/// <summary>Adds each element in one quaternion with its corresponding element in a second quaternion.</summary>
		/// <param name="value1">The first quaternion.</param>
		/// <param name="value2">The second quaternion.</param>
		/// <returns>The quaternion that contains the summed values of <paramref name="value1">value1</paramref> and <paramref name="value2">value2</paramref>.</returns>
		public static Quaternion operator +(Quaternion value1, Quaternion value2)
		{
			Quaternion result = default(Quaternion);
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
			result.Z = value1.Z + value2.Z;
			result.W = value1.W + value2.W;
			return result;
		}

		/// <summary>Subtracts each element in a second quaternion from its corresponding element in a first quaternion.</summary>
		/// <param name="value1">The first quaternion.</param>
		/// <param name="value2">The second quaternion.</param>
		/// <returns>The quaternion containing the values that result from subtracting each element in <paramref name="value2">value2</paramref> from its corresponding element in <paramref name="value1">value1</paramref>.</returns>
		public static Quaternion operator -(Quaternion value1, Quaternion value2)
		{
			Quaternion result = default(Quaternion);
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
			result.Z = value1.Z - value2.Z;
			result.W = value1.W - value2.W;
			return result;
		}

		/// <summary>Returns the quaternion that results from multiplying two quaternions together.</summary>
		/// <param name="value1">The first quaternion.</param>
		/// <param name="value2">The second quaternion.</param>
		/// <returns>The product quaternion.</returns>
		public static Quaternion operator *(Quaternion value1, Quaternion value2)
		{
			float x = value1.X;
			float y = value1.Y;
			float z = value1.Z;
			float w = value1.W;
			float x2 = value2.X;
			float y2 = value2.Y;
			float z2 = value2.Z;
			float w2 = value2.W;
			float num = y * z2 - z * y2;
			float num2 = z * x2 - x * z2;
			float num3 = x * y2 - y * x2;
			float num4 = x * x2 + y * y2 + z * z2;
			Quaternion result = default(Quaternion);
			result.X = x * w2 + x2 * w + num;
			result.Y = y * w2 + y2 * w + num2;
			result.Z = z * w2 + z2 * w + num3;
			result.W = w * w2 - num4;
			return result;
		}

		/// <summary>Returns the quaternion that results from scaling all the components of a specified quaternion by a scalar factor.</summary>
		/// <param name="value1">The source quaternion.</param>
		/// <param name="value2">The scalar value.</param>
		/// <returns>The scaled quaternion.</returns>
		public static Quaternion operator *(Quaternion value1, float value2)
		{
			Quaternion result = default(Quaternion);
			result.X = value1.X * value2;
			result.Y = value1.Y * value2;
			result.Z = value1.Z * value2;
			result.W = value1.W * value2;
			return result;
		}

		/// <summary>Divides one quaternion by a second quaternion.</summary>
		/// <param name="value1">The dividend.</param>
		/// <param name="value2">The divisor.</param>
		/// <returns>The quaternion that results from dividing <paramref name="value1">value1</paramref> by <paramref name="value2">value2</paramref>.</returns>
		public static Quaternion operator /(Quaternion value1, Quaternion value2)
		{
			float x = value1.X;
			float y = value1.Y;
			float z = value1.Z;
			float w = value1.W;
			float num = value2.X * value2.X + value2.Y * value2.Y + value2.Z * value2.Z + value2.W * value2.W;
			float num2 = 1f / num;
			float num3 = (0f - value2.X) * num2;
			float num4 = (0f - value2.Y) * num2;
			float num5 = (0f - value2.Z) * num2;
			float num6 = value2.W * num2;
			float num7 = y * num5 - z * num4;
			float num8 = z * num3 - x * num5;
			float num9 = x * num4 - y * num3;
			float num10 = x * num3 + y * num4 + z * num5;
			Quaternion result = default(Quaternion);
			result.X = x * num6 + num3 * w + num7;
			result.Y = y * num6 + num4 * w + num8;
			result.Z = z * num6 + num5 * w + num9;
			result.W = w * num6 - num10;
			return result;
		}

		/// <summary>Returns a value that indicates whether two quaternions are equal.</summary>
		/// <param name="value1">The first quaternion to compare.</param>
		/// <param name="value2">The second quaternion to compare.</param>
		/// <returns>true if the two quaternions are equal; otherwise, false.</returns>
		public static bool operator ==(Quaternion value1, Quaternion value2)
		{
			if (value1.X == value2.X && value1.Y == value2.Y && value1.Z == value2.Z)
			{
				return value1.W == value2.W;
			}
			return false;
		}

		/// <summary>Returns a value that indicates whether two quaternions are not equal.</summary>
		/// <param name="value1">The first quaternion to compare.</param>
		/// <param name="value2">The second quaternion to compare.</param>
		/// <returns>true if <paramref name="value1">value1</paramref> and <paramref name="value2">value2</paramref> are not equal; otherwise, false.</returns>
		public static bool operator !=(Quaternion value1, Quaternion value2)
		{
			if (value1.X == value2.X && value1.Y == value2.Y && value1.Z == value2.Z)
			{
				return value1.W != value2.W;
			}
			return true;
		}

		/// <summary>Returns a value that indicates whether this instance and another quaternion are equal.</summary>
		/// <param name="other">The other quaternion.</param>
		/// <returns>true if the two quaternions are equal; otherwise, false.</returns>
		public bool Equals(Quaternion other)
		{
			if (X == other.X && Y == other.Y && Z == other.Z)
			{
				return W == other.W;
			}
			return false;
		}

		/// <summary>Returns a value that indicates whether this instance and a specified object are equal.</summary>
		/// <param name="obj">The object to compare with the current instance.</param>
		/// <returns>true if the current instance and <paramref name="obj">obj</paramref> are equal; otherwise, false. If <paramref name="obj">obj</paramref> is null, the method returns false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Quaternion)
			{
				return Equals((Quaternion)obj);
			}
			return false;
		}

		/// <summary>Returns a string that represents this quaternion.</summary>
		/// <returns>The string representation of this quaternion.</returns>
		public override string ToString()
		{
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			return string.Format(currentCulture, "{{X:{0} Y:{1} Z:{2} W:{3}}}", X.ToString(currentCulture), Y.ToString(currentCulture), Z.ToString(currentCulture), W.ToString(currentCulture));
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>The hash code.</returns>
		public override int GetHashCode()
		{
			return X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode() + W.GetHashCode();
		}
	}
}
