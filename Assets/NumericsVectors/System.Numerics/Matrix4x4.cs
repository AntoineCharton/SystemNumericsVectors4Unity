using System.Globalization;

namespace System.Numerics
{
	/// <summary>Represents a 4x4 matrix.</summary>
	public struct Matrix4x4 : IEquatable<Matrix4x4>
	{
		private struct CanonicalBasis
		{
			public Vector3 Row0;

			public Vector3 Row1;

			public Vector3 Row2;
		}

		private struct VectorBasis
		{
			public unsafe Vector3* Element0;

			public unsafe Vector3* Element1;

			public unsafe Vector3* Element2;
		}

		/// <summary>The first element of the first row.</summary>
		/// <returns></returns>
		public float M11;

		/// <summary>The second element of the first row.</summary>
		/// <returns></returns>
		public float M12;

		/// <summary>The third element of the first row.</summary>
		/// <returns></returns>
		public float M13;

		/// <summary>The fourth element of the first row.</summary>
		/// <returns></returns>
		public float M14;

		/// <summary>The first element of the second row.</summary>
		/// <returns></returns>
		public float M21;

		/// <summary>The second element of the second row.</summary>
		/// <returns></returns>
		public float M22;

		/// <summary>The third element of the second row.</summary>
		/// <returns></returns>
		public float M23;

		/// <summary>The fourth element of the second row.</summary>
		/// <returns></returns>
		public float M24;

		/// <summary>The first element of the third row.</summary>
		/// <returns></returns>
		public float M31;

		/// <summary>The second element of the third row.</summary>
		/// <returns></returns>
		public float M32;

		/// <summary>The third element of the third row.</summary>
		/// <returns></returns>
		public float M33;

		/// <summary>The fourth element of the third row.</summary>
		/// <returns></returns>
		public float M34;

		/// <summary>The first element of the fourth row.</summary>
		/// <returns></returns>
		public float M41;

		/// <summary>The second element of the fourth row.</summary>
		/// <returns></returns>
		public float M42;

		/// <summary>The third element of the fourth row.</summary>
		/// <returns></returns>
		public float M43;

		/// <summary>The fourth element of the fourth row.</summary>
		/// <returns></returns>
		public float M44;

		private static readonly Matrix4x4 _identity = new Matrix4x4(1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f);

		/// <summary>Gets the multiplicative identity matrix.</summary>
		/// <returns>Gets the multiplicative identity matrix.</returns>
		public static Matrix4x4 Identity => _identity;

		/// <summary>Indicates whether the current matrix is the identity matrix.</summary>
		/// <returns>true if the current matrix is the identity matrix; otherwise, false.</returns>
		public bool IsIdentity
		{
			get
			{
				if (M11 == 1f && M22 == 1f && M33 == 1f && M44 == 1f && M12 == 0f && M13 == 0f && M14 == 0f && M21 == 0f && M23 == 0f && M24 == 0f && M31 == 0f && M32 == 0f && M34 == 0f && M41 == 0f && M42 == 0f)
				{
					return M43 == 0f;
				}
				return false;
			}
		}

		/// <summary>Gets or sets the translation component of this matrix.</summary>
		/// <returns>The translation component of the current instance.</returns>
		public Vector3 Translation
		{
			get
			{
				return new Vector3(M41, M42, M43);
			}
			set
			{
				M41 = value.X;
				M42 = value.Y;
				M43 = value.Z;
			}
		}

		/// <summary>Creates a 4x4 matrix from the specified components.</summary>
		/// <param name="m11">The value to assign to the first element in the first row.</param>
		/// <param name="m12">The value to assign to the second element in the first row.</param>
		/// <param name="m13">The value to assign to the third element in the first row.</param>
		/// <param name="m14">The value to assign to the fourth element in the first row.</param>
		/// <param name="m21">The value to assign to the first element in the second row.</param>
		/// <param name="m22">The value to assign to the second element in the second row.</param>
		/// <param name="m23">The value to assign to the third element in the second row.</param>
		/// <param name="m24">The value to assign to the third element in the second row.</param>
		/// <param name="m31">The value to assign to the first element in the third row.</param>
		/// <param name="m32">The value to assign to the second element in the third row.</param>
		/// <param name="m33">The value to assign to the third element in the third row.</param>
		/// <param name="m34">The value to assign to the fourth element in the third row.</param>
		/// <param name="m41">The value to assign to the first element in the fourth row.</param>
		/// <param name="m42">The value to assign to the second element in the fourth row.</param>
		/// <param name="m43">The value to assign to the third element in the fourth row.</param>
		/// <param name="m44">The value to assign to the fourth element in the fourth row.</param>
		public Matrix4x4(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
		{
			M11 = m11;
			M12 = m12;
			M13 = m13;
			M14 = m14;
			M21 = m21;
			M22 = m22;
			M23 = m23;
			M24 = m24;
			M31 = m31;
			M32 = m32;
			M33 = m33;
			M34 = m34;
			M41 = m41;
			M42 = m42;
			M43 = m43;
			M44 = m44;
		}

		/// <summary>Creates a <see cref="T:System.Numerics.Matrix4x4"></see> object from a specified <see cref="T:System.Numerics.Matrix3x2"></see> object.</summary>
		/// <param name="value">A 3x2 matrix.</param>
		public Matrix4x4(Matrix3x2 value)
		{
			M11 = value.M11;
			M12 = value.M12;
			M13 = 0f;
			M14 = 0f;
			M21 = value.M21;
			M22 = value.M22;
			M23 = 0f;
			M24 = 0f;
			M31 = 0f;
			M32 = 0f;
			M33 = 1f;
			M34 = 0f;
			M41 = value.M31;
			M42 = value.M32;
			M43 = 0f;
			M44 = 1f;
		}

		/// <summary>Creates a spherical billboard that rotates around a specified object position.</summary>
		/// <param name="objectPosition">The position of the object that the billboard will rotate around.</param>
		/// <param name="cameraPosition">The position of the camera.</param>
		/// <param name="cameraUpVector">The up vector of the camera.</param>
		/// <param name="cameraForwardVector">The forward vector of the camera.</param>
		/// <returns>The created billboard.</returns>
		public static Matrix4x4 CreateBillboard(Vector3 objectPosition, Vector3 cameraPosition, Vector3 cameraUpVector, Vector3 cameraForwardVector)
		{
			Vector3 left = new Vector3(objectPosition.X - cameraPosition.X, objectPosition.Y - cameraPosition.Y, objectPosition.Z - cameraPosition.Z);
			float num = left.LengthSquared();
			left = ((!(num < 0.0001f)) ? Vector3.Multiply(left, 1f / MathF.Sqrt(num)) : (-cameraForwardVector));
			Vector3 vector = Vector3.Normalize(Vector3.Cross(cameraUpVector, left));
			Vector3 vector2 = Vector3.Cross(left, vector);
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = vector.X;
			result.M12 = vector.Y;
			result.M13 = vector.Z;
			result.M14 = 0f;
			result.M21 = vector2.X;
			result.M22 = vector2.Y;
			result.M23 = vector2.Z;
			result.M24 = 0f;
			result.M31 = left.X;
			result.M32 = left.Y;
			result.M33 = left.Z;
			result.M34 = 0f;
			result.M41 = objectPosition.X;
			result.M42 = objectPosition.Y;
			result.M43 = objectPosition.Z;
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a cylindrical billboard that rotates around a specified axis.</summary>
		/// <param name="objectPosition">The position of the object that the billboard will rotate around.</param>
		/// <param name="cameraPosition">The position of the camera.</param>
		/// <param name="rotateAxis">The axis to rotate the billboard around.</param>
		/// <param name="cameraForwardVector">The forward vector of the camera.</param>
		/// <param name="objectForwardVector">The forward vector of the object.</param>
		/// <returns>The billboard matrix.</returns>
		public static Matrix4x4 CreateConstrainedBillboard(Vector3 objectPosition, Vector3 cameraPosition, Vector3 rotateAxis, Vector3 cameraForwardVector, Vector3 objectForwardVector)
		{
			Vector3 left = new Vector3(objectPosition.X - cameraPosition.X, objectPosition.Y - cameraPosition.Y, objectPosition.Z - cameraPosition.Z);
			float num = left.LengthSquared();
			left = ((!(num < 0.0001f)) ? Vector3.Multiply(left, 1f / MathF.Sqrt(num)) : (-cameraForwardVector));
			Vector3 vector = rotateAxis;
			float x = Vector3.Dot(rotateAxis, left);
			Vector3 vector3;
			Vector3 vector2;
			if (MathF.Abs(x) > 0.998254657f)
			{
				vector2 = objectForwardVector;
				x = Vector3.Dot(rotateAxis, vector2);
				if (MathF.Abs(x) > 0.998254657f)
				{
					vector2 = ((MathF.Abs(rotateAxis.Z) > 0.998254657f) ? new Vector3(1f, 0f, 0f) : new Vector3(0f, 0f, -1f));
				}
				vector3 = Vector3.Normalize(Vector3.Cross(rotateAxis, vector2));
				vector2 = Vector3.Normalize(Vector3.Cross(vector3, rotateAxis));
			}
			else
			{
				vector3 = Vector3.Normalize(Vector3.Cross(rotateAxis, left));
				vector2 = Vector3.Normalize(Vector3.Cross(vector3, vector));
			}
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = vector3.X;
			result.M12 = vector3.Y;
			result.M13 = vector3.Z;
			result.M14 = 0f;
			result.M21 = vector.X;
			result.M22 = vector.Y;
			result.M23 = vector.Z;
			result.M24 = 0f;
			result.M31 = vector2.X;
			result.M32 = vector2.Y;
			result.M33 = vector2.Z;
			result.M34 = 0f;
			result.M41 = objectPosition.X;
			result.M42 = objectPosition.Y;
			result.M43 = objectPosition.Z;
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a translation matrix from the specified 3-dimensional vector.</summary>
		/// <param name="position">The amount to translate in each axis.</param>
		/// <returns>The translation matrix.</returns>
		public static Matrix4x4 CreateTranslation(Vector3 position)
		{
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = 1f;
			result.M12 = 0f;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = 1f;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f;
			result.M33 = 1f;
			result.M34 = 0f;
			result.M41 = position.X;
			result.M42 = position.Y;
			result.M43 = position.Z;
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a translation matrix from the specified X, Y, and Z components.</summary>
		/// <param name="xPosition">The amount to translate on the X axis.</param>
		/// <param name="yPosition">The amount to translate on the Y axis.</param>
		/// <param name="zPosition">The amount to translate on the Z axis.</param>
		/// <returns>The translation matrix.</returns>
		public static Matrix4x4 CreateTranslation(float xPosition, float yPosition, float zPosition)
		{
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = 1f;
			result.M12 = 0f;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = 1f;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f;
			result.M33 = 1f;
			result.M34 = 0f;
			result.M41 = xPosition;
			result.M42 = yPosition;
			result.M43 = zPosition;
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a scaling matrix from the specified X, Y, and Z components.</summary>
		/// <param name="xScale">The value to scale by on the X axis.</param>
		/// <param name="yScale">The value to scale by on the Y axis.</param>
		/// <param name="zScale">The value to scale by on the Z axis.</param>
		/// <returns>The scaling matrix.</returns>
		public static Matrix4x4 CreateScale(float xScale, float yScale, float zScale)
		{
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = xScale;
			result.M12 = 0f;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = yScale;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f;
			result.M33 = zScale;
			result.M34 = 0f;
			result.M41 = 0f;
			result.M42 = 0f;
			result.M43 = 0f;
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a scaling matrix that is offset by a given center point.</summary>
		/// <param name="xScale">The value to scale by on the X axis.</param>
		/// <param name="yScale">The value to scale by on the Y axis.</param>
		/// <param name="zScale">The value to scale by on the Z axis.</param>
		/// <param name="centerPoint">The center point.</param>
		/// <returns>The scaling matrix.</returns>
		public static Matrix4x4 CreateScale(float xScale, float yScale, float zScale, Vector3 centerPoint)
		{
			float m = centerPoint.X * (1f - xScale);
			float m2 = centerPoint.Y * (1f - yScale);
			float m3 = centerPoint.Z * (1f - zScale);
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = xScale;
			result.M12 = 0f;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = yScale;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f;
			result.M33 = zScale;
			result.M34 = 0f;
			result.M41 = m;
			result.M42 = m2;
			result.M43 = m3;
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a scaling matrix from the specified vector scale.</summary>
		/// <param name="scales">The scale to use.</param>
		/// <returns>The scaling matrix.</returns>
		public static Matrix4x4 CreateScale(Vector3 scales)
		{
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = scales.X;
			result.M12 = 0f;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = scales.Y;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f;
			result.M33 = scales.Z;
			result.M34 = 0f;
			result.M41 = 0f;
			result.M42 = 0f;
			result.M43 = 0f;
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a scaling matrix with a center point.</summary>
		/// <param name="scales">The vector that contains the amount to scale on each axis.</param>
		/// <param name="centerPoint">The center point.</param>
		/// <returns>The scaling matrix.</returns>
		public static Matrix4x4 CreateScale(Vector3 scales, Vector3 centerPoint)
		{
			float m = centerPoint.X * (1f - scales.X);
			float m2 = centerPoint.Y * (1f - scales.Y);
			float m3 = centerPoint.Z * (1f - scales.Z);
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = scales.X;
			result.M12 = 0f;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = scales.Y;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f;
			result.M33 = scales.Z;
			result.M34 = 0f;
			result.M41 = m;
			result.M42 = m2;
			result.M43 = m3;
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a uniform scaling matrix that scale equally on each axis.</summary>
		/// <param name="scale">The uniform scaling factor.</param>
		/// <returns>The scaling matrix.</returns>
		public static Matrix4x4 CreateScale(float scale)
		{
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = scale;
			result.M12 = 0f;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = scale;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f;
			result.M33 = scale;
			result.M34 = 0f;
			result.M41 = 0f;
			result.M42 = 0f;
			result.M43 = 0f;
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a uniform scaling matrix that scales equally on each axis with a center point.</summary>
		/// <param name="scale">The uniform scaling factor.</param>
		/// <param name="centerPoint">The center point.</param>
		/// <returns>The scaling matrix.</returns>
		public static Matrix4x4 CreateScale(float scale, Vector3 centerPoint)
		{
			float m = centerPoint.X * (1f - scale);
			float m2 = centerPoint.Y * (1f - scale);
			float m3 = centerPoint.Z * (1f - scale);
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = scale;
			result.M12 = 0f;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = scale;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f;
			result.M33 = scale;
			result.M34 = 0f;
			result.M41 = m;
			result.M42 = m2;
			result.M43 = m3;
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a matrix for rotating points around the X axis.</summary>
		/// <param name="radians">The amount, in radians, by which to rotate around the X axis.</param>
		/// <returns>The rotation matrix.</returns>
		public static Matrix4x4 CreateRotationX(float radians)
		{
			float num = MathF.Cos(radians);
			float num2 = MathF.Sin(radians);
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = 1f;
			result.M12 = 0f;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = num;
			result.M23 = num2;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f - num2;
			result.M33 = num;
			result.M34 = 0f;
			result.M41 = 0f;
			result.M42 = 0f;
			result.M43 = 0f;
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a matrix for rotating points around the X axis from a center point.</summary>
		/// <param name="radians">The amount, in radians, by which to rotate around the X axis.</param>
		/// <param name="centerPoint">The center point.</param>
		/// <returns>The rotation matrix.</returns>
		public static Matrix4x4 CreateRotationX(float radians, Vector3 centerPoint)
		{
			float num = MathF.Cos(radians);
			float num2 = MathF.Sin(radians);
			float m = centerPoint.Y * (1f - num) + centerPoint.Z * num2;
			float m2 = centerPoint.Z * (1f - num) - centerPoint.Y * num2;
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = 1f;
			result.M12 = 0f;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = num;
			result.M23 = num2;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f - num2;
			result.M33 = num;
			result.M34 = 0f;
			result.M41 = 0f;
			result.M42 = m;
			result.M43 = m2;
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a matrix for rotating points around the Y axis.</summary>
		/// <param name="radians">The amount, in radians, by which to rotate around the Y-axis.</param>
		/// <returns>The rotation matrix.</returns>
		public static Matrix4x4 CreateRotationY(float radians)
		{
			float num = MathF.Cos(radians);
			float num2 = MathF.Sin(radians);
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = num;
			result.M12 = 0f;
			result.M13 = 0f - num2;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = 1f;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = num2;
			result.M32 = 0f;
			result.M33 = num;
			result.M34 = 0f;
			result.M41 = 0f;
			result.M42 = 0f;
			result.M43 = 0f;
			result.M44 = 1f;
			return result;
		}

		/// <summary>The amount, in radians, by which to rotate around the Y axis from a center point.</summary>
		/// <param name="radians">The amount, in radians, by which to rotate around the Y-axis.</param>
		/// <param name="centerPoint">The center point.</param>
		/// <returns>The rotation matrix.</returns>
		public static Matrix4x4 CreateRotationY(float radians, Vector3 centerPoint)
		{
			float num = MathF.Cos(radians);
			float num2 = MathF.Sin(radians);
			float m = centerPoint.X * (1f - num) - centerPoint.Z * num2;
			float m2 = centerPoint.Z * (1f - num) + centerPoint.X * num2;
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = num;
			result.M12 = 0f;
			result.M13 = 0f - num2;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = 1f;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = num2;
			result.M32 = 0f;
			result.M33 = num;
			result.M34 = 0f;
			result.M41 = m;
			result.M42 = 0f;
			result.M43 = m2;
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a matrix for rotating points around the Z axis.</summary>
		/// <param name="radians">The amount, in radians, by which to rotate around the Z-axis.</param>
		/// <returns>The rotation matrix.</returns>
		public static Matrix4x4 CreateRotationZ(float radians)
		{
			float num = MathF.Cos(radians);
			float num2 = MathF.Sin(radians);
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = num;
			result.M12 = num2;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = 0f - num2;
			result.M22 = num;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f;
			result.M33 = 1f;
			result.M34 = 0f;
			result.M41 = 0f;
			result.M42 = 0f;
			result.M43 = 0f;
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a matrix for rotating points around the Z axis from a center point.</summary>
		/// <param name="radians">The amount, in radians, by which to rotate around the Z-axis.</param>
		/// <param name="centerPoint">The center point.</param>
		/// <returns>The rotation matrix.</returns>
		public static Matrix4x4 CreateRotationZ(float radians, Vector3 centerPoint)
		{
			float num = MathF.Cos(radians);
			float num2 = MathF.Sin(radians);
			float m = centerPoint.X * (1f - num) + centerPoint.Y * num2;
			float m2 = centerPoint.Y * (1f - num) - centerPoint.X * num2;
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = num;
			result.M12 = num2;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = 0f - num2;
			result.M22 = num;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f;
			result.M33 = 1f;
			result.M34 = 0f;
			result.M41 = m;
			result.M42 = m2;
			result.M43 = 0f;
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a matrix that rotates around an arbitrary vector.</summary>
		/// <param name="axis">The axis to rotate around.</param>
		/// <param name="angle">The angle to rotate around axis, in radians.</param>
		/// <returns>The rotation matrix.</returns>
		public static Matrix4x4 CreateFromAxisAngle(Vector3 axis, float angle)
		{
			float x = axis.X;
			float y = axis.Y;
			float z = axis.Z;
			float num = MathF.Sin(angle);
			float num2 = MathF.Cos(angle);
			float num3 = x * x;
			float num4 = y * y;
			float num5 = z * z;
			float num6 = x * y;
			float num7 = x * z;
			float num8 = y * z;
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = num3 + num2 * (1f - num3);
			result.M12 = num6 - num2 * num6 + num * z;
			result.M13 = num7 - num2 * num7 - num * y;
			result.M14 = 0f;
			result.M21 = num6 - num2 * num6 - num * z;
			result.M22 = num4 + num2 * (1f - num4);
			result.M23 = num8 - num2 * num8 + num * x;
			result.M24 = 0f;
			result.M31 = num7 - num2 * num7 + num * y;
			result.M32 = num8 - num2 * num8 - num * x;
			result.M33 = num5 + num2 * (1f - num5);
			result.M34 = 0f;
			result.M41 = 0f;
			result.M42 = 0f;
			result.M43 = 0f;
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a perspective projection matrix based on a field of view, aspect ratio, and near and far view plane distances.</summary>
		/// <param name="fieldOfView">The field of view in the y direction, in radians.</param>
		/// <param name="aspectRatio">The aspect ratio, defined as view space width divided by height.</param>
		/// <param name="nearPlaneDistance">The distance to the near view plane.</param>
		/// <param name="farPlaneDistance">The distance to the far view plane.</param>
		/// <returns>The perspective projection matrix.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="fieldOfView">fieldOfView</paramref> is less than or equal to zero.  
		///  -or-  
		///  <paramref name="fieldOfView">fieldOfView</paramref> is greater than or equal to <see cref="System.Math.PI"></see>.  
		///  <paramref name="nearPlaneDistance">nearPlaneDistance</paramref> is less than or equal to zero.  
		///  -or-  
		///  <paramref name="farPlaneDistance">farPlaneDistance</paramref> is less than or equal to zero.  
		///  -or-  
		///  <paramref name="nearPlaneDistance">nearPlaneDistance</paramref> is greater than or equal to <paramref name="farPlaneDistance">farPlaneDistance</paramref>.</exception>
		public static Matrix4x4 CreatePerspectiveFieldOfView(float fieldOfView, float aspectRatio, float nearPlaneDistance, float farPlaneDistance)
		{
			if (fieldOfView <= 0f || fieldOfView >= (float)Math.PI)
			{
				throw new ArgumentOutOfRangeException("fieldOfView");
			}
			if (nearPlaneDistance <= 0f)
			{
				throw new ArgumentOutOfRangeException("nearPlaneDistance");
			}
			if (farPlaneDistance <= 0f)
			{
				throw new ArgumentOutOfRangeException("farPlaneDistance");
			}
			if (nearPlaneDistance >= farPlaneDistance)
			{
				throw new ArgumentOutOfRangeException("nearPlaneDistance");
			}
			float num = 1f / MathF.Tan(fieldOfView * 0.5f);
			Matrix4x4 result = default(Matrix4x4);
			float num2 = result.M11 = num / aspectRatio;
			result.M12 = (result.M13 = (result.M14 = 0f));
			result.M22 = num;
			result.M21 = (result.M23 = (result.M24 = 0f));
			result.M31 = (result.M32 = 0f);
			float num3 = result.M33 = (float.IsPositiveInfinity(farPlaneDistance) ? (-1f) : (farPlaneDistance / (nearPlaneDistance - farPlaneDistance)));
			result.M34 = -1f;
			result.M41 = (result.M42 = (result.M44 = 0f));
			result.M43 = nearPlaneDistance * num3;
			return result;
		}

		/// <summary>Creates a perspective projection matrix from the given view volume dimensions.</summary>
		/// <param name="width">The width of the view volume at the near view plane.</param>
		/// <param name="height">The height of the view volume at the near view plane.</param>
		/// <param name="nearPlaneDistance">The distance to the near view plane.</param>
		/// <param name="farPlaneDistance">The distance to the far view plane.</param>
		/// <returns>The perspective projection matrix.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="nearPlaneDistance">nearPlaneDistance</paramref> is less than or equal to zero.  
		///  -or-  
		///  <paramref name="farPlaneDistance">farPlaneDistance</paramref> is less than or equal to zero.  
		///  -or-  
		///  <paramref name="nearPlaneDistance">nearPlaneDistance</paramref> is greater than or equal to <paramref name="farPlaneDistance">farPlaneDistance</paramref>.</exception>
		public static Matrix4x4 CreatePerspective(float width, float height, float nearPlaneDistance, float farPlaneDistance)
		{
			if (nearPlaneDistance <= 0f)
			{
				throw new ArgumentOutOfRangeException("nearPlaneDistance");
			}
			if (farPlaneDistance <= 0f)
			{
				throw new ArgumentOutOfRangeException("farPlaneDistance");
			}
			if (nearPlaneDistance >= farPlaneDistance)
			{
				throw new ArgumentOutOfRangeException("nearPlaneDistance");
			}
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = 2f * nearPlaneDistance / width;
			result.M12 = (result.M13 = (result.M14 = 0f));
			result.M22 = 2f * nearPlaneDistance / height;
			result.M21 = (result.M23 = (result.M24 = 0f));
			float num = result.M33 = (float.IsPositiveInfinity(farPlaneDistance) ? (-1f) : (farPlaneDistance / (nearPlaneDistance - farPlaneDistance)));
			result.M31 = (result.M32 = 0f);
			result.M34 = -1f;
			result.M41 = (result.M42 = (result.M44 = 0f));
			result.M43 = nearPlaneDistance * num;
			return result;
		}

		/// <summary>Creates a customized perspective projection matrix.</summary>
		/// <param name="left">The minimum x-value of the view volume at the near view plane.</param>
		/// <param name="right">The maximum x-value of the view volume at the near view plane.</param>
		/// <param name="bottom">The minimum y-value of the view volume at the near view plane.</param>
		/// <param name="top">The maximum y-value of the view volume at the near view plane.</param>
		/// <param name="nearPlaneDistance">The distance to the near view plane.</param>
		/// <param name="farPlaneDistance">The distance to the far view plane.</param>
		/// <returns>The perspective projection matrix.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="nearPlaneDistance">nearPlaneDistance</paramref> is less than or equal to zero.  
		///  -or-  
		///  <paramref name="farPlaneDistance">farPlaneDistance</paramref> is less than or equal to zero.  
		///  -or-  
		///  <paramref name="nearPlaneDistance">nearPlaneDistance</paramref> is greater than or equal to <paramref name="farPlaneDistance">farPlaneDistance</paramref>.</exception>
		public static Matrix4x4 CreatePerspectiveOffCenter(float left, float right, float bottom, float top, float nearPlaneDistance, float farPlaneDistance)
		{
			if (nearPlaneDistance <= 0f)
			{
				throw new ArgumentOutOfRangeException("nearPlaneDistance");
			}
			if (farPlaneDistance <= 0f)
			{
				throw new ArgumentOutOfRangeException("farPlaneDistance");
			}
			if (nearPlaneDistance >= farPlaneDistance)
			{
				throw new ArgumentOutOfRangeException("nearPlaneDistance");
			}
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = 2f * nearPlaneDistance / (right - left);
			result.M12 = (result.M13 = (result.M14 = 0f));
			result.M22 = 2f * nearPlaneDistance / (top - bottom);
			result.M21 = (result.M23 = (result.M24 = 0f));
			result.M31 = (left + right) / (right - left);
			result.M32 = (top + bottom) / (top - bottom);
			float num = result.M33 = (float.IsPositiveInfinity(farPlaneDistance) ? (-1f) : (farPlaneDistance / (nearPlaneDistance - farPlaneDistance)));
			result.M34 = -1f;
			result.M43 = nearPlaneDistance * num;
			result.M41 = (result.M42 = (result.M44 = 0f));
			return result;
		}

		/// <summary>Creates an orthographic perspective matrix from the given view volume dimensions.</summary>
		/// <param name="width">The width of the view volume.</param>
		/// <param name="height">The height of the view volume.</param>
		/// <param name="zNearPlane">The minimum Z-value of the view volume.</param>
		/// <param name="zFarPlane">The maximum Z-value of the view volume.</param>
		/// <returns>The orthographic projection matrix.</returns>
		public static Matrix4x4 CreateOrthographic(float width, float height, float zNearPlane, float zFarPlane)
		{
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = 2f / width;
			result.M12 = (result.M13 = (result.M14 = 0f));
			result.M22 = 2f / height;
			result.M21 = (result.M23 = (result.M24 = 0f));
			result.M33 = 1f / (zNearPlane - zFarPlane);
			result.M31 = (result.M32 = (result.M34 = 0f));
			result.M41 = (result.M42 = 0f);
			result.M43 = zNearPlane / (zNearPlane - zFarPlane);
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a customized orthographic projection matrix.</summary>
		/// <param name="left">The minimum X-value of the view volume.</param>
		/// <param name="right">The maximum X-value of the view volume.</param>
		/// <param name="bottom">The minimum Y-value of the view volume.</param>
		/// <param name="top">The maximum Y-value of the view volume.</param>
		/// <param name="zNearPlane">The minimum Z-value of the view volume.</param>
		/// <param name="zFarPlane">The maximum Z-value of the view volume.</param>
		/// <returns>The orthographic projection matrix.</returns>
		public static Matrix4x4 CreateOrthographicOffCenter(float left, float right, float bottom, float top, float zNearPlane, float zFarPlane)
		{
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = 2f / (right - left);
			result.M12 = (result.M13 = (result.M14 = 0f));
			result.M22 = 2f / (top - bottom);
			result.M21 = (result.M23 = (result.M24 = 0f));
			result.M33 = 1f / (zNearPlane - zFarPlane);
			result.M31 = (result.M32 = (result.M34 = 0f));
			result.M41 = (left + right) / (left - right);
			result.M42 = (top + bottom) / (bottom - top);
			result.M43 = zNearPlane / (zNearPlane - zFarPlane);
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a view matrix.</summary>
		/// <param name="cameraPosition">The position of the camera.</param>
		/// <param name="cameraTarget">The target towards which the camera is pointing.</param>
		/// <param name="cameraUpVector">The direction that is &amp;quot;up&amp;quot; from the camera&amp;#39;s point of view.</param>
		/// <returns>The view matrix.</returns>
		public static Matrix4x4 CreateLookAt(Vector3 cameraPosition, Vector3 cameraTarget, Vector3 cameraUpVector)
		{
			Vector3 vector = Vector3.Normalize(cameraPosition - cameraTarget);
			Vector3 vector2 = Vector3.Normalize(Vector3.Cross(cameraUpVector, vector));
			Vector3 vector3 = Vector3.Cross(vector, vector2);
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = vector2.X;
			result.M12 = vector3.X;
			result.M13 = vector.X;
			result.M14 = 0f;
			result.M21 = vector2.Y;
			result.M22 = vector3.Y;
			result.M23 = vector.Y;
			result.M24 = 0f;
			result.M31 = vector2.Z;
			result.M32 = vector3.Z;
			result.M33 = vector.Z;
			result.M34 = 0f;
			result.M41 = 0f - Vector3.Dot(vector2, cameraPosition);
			result.M42 = 0f - Vector3.Dot(vector3, cameraPosition);
			result.M43 = 0f - Vector3.Dot(vector, cameraPosition);
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a world matrix with the specified parameters.</summary>
		/// <param name="position">The position of the object.</param>
		/// <param name="forward">The forward direction of the object.</param>
		/// <param name="up">The upward direction of the object. Its value is usually [0, 1, 0].</param>
		/// <returns>The world matrix.</returns>
		public static Matrix4x4 CreateWorld(Vector3 position, Vector3 forward, Vector3 up)
		{
			Vector3 vector = Vector3.Normalize(-forward);
			Vector3 vector2 = Vector3.Normalize(Vector3.Cross(up, vector));
			Vector3 vector3 = Vector3.Cross(vector, vector2);
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = vector2.X;
			result.M12 = vector2.Y;
			result.M13 = vector2.Z;
			result.M14 = 0f;
			result.M21 = vector3.X;
			result.M22 = vector3.Y;
			result.M23 = vector3.Z;
			result.M24 = 0f;
			result.M31 = vector.X;
			result.M32 = vector.Y;
			result.M33 = vector.Z;
			result.M34 = 0f;
			result.M41 = position.X;
			result.M42 = position.Y;
			result.M43 = position.Z;
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a rotation matrix from the specified Quaternion rotation value.</summary>
		/// <param name="quaternion">The source Quaternion.</param>
		/// <returns>The rotation matrix.</returns>
		public static Matrix4x4 CreateFromQuaternion(Quaternion quaternion)
		{
			float num = quaternion.X * quaternion.X;
			float num2 = quaternion.Y * quaternion.Y;
			float num3 = quaternion.Z * quaternion.Z;
			float num4 = quaternion.X * quaternion.Y;
			float num5 = quaternion.Z * quaternion.W;
			float num6 = quaternion.Z * quaternion.X;
			float num7 = quaternion.Y * quaternion.W;
			float num8 = quaternion.Y * quaternion.Z;
			float num9 = quaternion.X * quaternion.W;
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = 1f - 2f * (num2 + num3);
			result.M12 = 2f * (num4 + num5);
			result.M13 = 2f * (num6 - num7);
			result.M14 = 0f;
			result.M21 = 2f * (num4 - num5);
			result.M22 = 1f - 2f * (num3 + num);
			result.M23 = 2f * (num8 + num9);
			result.M24 = 0f;
			result.M31 = 2f * (num6 + num7);
			result.M32 = 2f * (num8 - num9);
			result.M33 = 1f - 2f * (num2 + num);
			result.M34 = 0f;
			result.M41 = 0f;
			result.M42 = 0f;
			result.M43 = 0f;
			result.M44 = 1f;
			return result;
		}

		/// <summary>Creates a rotation matrix from the specified yaw, pitch, and roll.</summary>
		/// <param name="yaw">The angle of rotation, in radians, around the Y axis.</param>
		/// <param name="pitch">The angle of rotation, in radians, around the X axis.</param>
		/// <param name="roll">The angle of rotation, in radians, around the Z axis.</param>
		/// <returns>The rotation matrix.</returns>
		public static Matrix4x4 CreateFromYawPitchRoll(float yaw, float pitch, float roll)
		{
			Quaternion quaternion = Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll);
			return CreateFromQuaternion(quaternion);
		}

		/// <summary>Creates a matrix that flattens geometry into a specified plane as if casting a shadow from a specified light source.</summary>
		/// <param name="lightDirection">The direction from which the light that will cast the shadow is coming.</param>
		/// <param name="plane">The plane onto which the new matrix should flatten geometry so as to cast a shadow.</param>
		/// <returns>A new matrix that can be used to flatten geometry onto the specified plane from the specified direction.</returns>
		public static Matrix4x4 CreateShadow(Vector3 lightDirection, Plane plane)
		{
			Plane plane2 = Plane.Normalize(plane);
			float num = plane2.Normal.X * lightDirection.X + plane2.Normal.Y * lightDirection.Y + plane2.Normal.Z * lightDirection.Z;
			float num2 = 0f - plane2.Normal.X;
			float num3 = 0f - plane2.Normal.Y;
			float num4 = 0f - plane2.Normal.Z;
			float num5 = 0f - plane2.D;
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = num2 * lightDirection.X + num;
			result.M21 = num3 * lightDirection.X;
			result.M31 = num4 * lightDirection.X;
			result.M41 = num5 * lightDirection.X;
			result.M12 = num2 * lightDirection.Y;
			result.M22 = num3 * lightDirection.Y + num;
			result.M32 = num4 * lightDirection.Y;
			result.M42 = num5 * lightDirection.Y;
			result.M13 = num2 * lightDirection.Z;
			result.M23 = num3 * lightDirection.Z;
			result.M33 = num4 * lightDirection.Z + num;
			result.M43 = num5 * lightDirection.Z;
			result.M14 = 0f;
			result.M24 = 0f;
			result.M34 = 0f;
			result.M44 = num;
			return result;
		}

		/// <summary>Creates a matrix that reflects the coordinate system about a specified plane.</summary>
		/// <param name="value">The plane about which to create a reflection.</param>
		/// <returns>A new matrix expressing the reflection.</returns>
		public static Matrix4x4 CreateReflection(Plane value)
		{
			value = Plane.Normalize(value);
			float x = value.Normal.X;
			float y = value.Normal.Y;
			float z = value.Normal.Z;
			float num = -2f * x;
			float num2 = -2f * y;
			float num3 = -2f * z;
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = num * x + 1f;
			result.M12 = num2 * x;
			result.M13 = num3 * x;
			result.M14 = 0f;
			result.M21 = num * y;
			result.M22 = num2 * y + 1f;
			result.M23 = num3 * y;
			result.M24 = 0f;
			result.M31 = num * z;
			result.M32 = num2 * z;
			result.M33 = num3 * z + 1f;
			result.M34 = 0f;
			result.M41 = num * value.D;
			result.M42 = num2 * value.D;
			result.M43 = num3 * value.D;
			result.M44 = 1f;
			return result;
		}

		/// <summary>Calculates the determinant of the current 4x4 matrix.</summary>
		/// <returns>The determinant.</returns>
		public float GetDeterminant()
		{
			float m = M11;
			float m2 = M12;
			float m3 = M13;
			float m4 = M14;
			float m5 = M21;
			float m6 = M22;
			float m7 = M23;
			float m8 = M24;
			float m9 = M31;
			float m10 = M32;
			float m11 = M33;
			float m12 = M34;
			float m13 = M41;
			float m14 = M42;
			float m15 = M43;
			float m16 = M44;
			float num = m11 * m16 - m12 * m15;
			float num2 = m10 * m16 - m12 * m14;
			float num3 = m10 * m15 - m11 * m14;
			float num4 = m9 * m16 - m12 * m13;
			float num5 = m9 * m15 - m11 * m13;
			float num6 = m9 * m14 - m10 * m13;
			return m * (m6 * num - m7 * num2 + m8 * num3) - m2 * (m5 * num - m7 * num4 + m8 * num5) + m3 * (m5 * num2 - m6 * num4 + m8 * num6) - m4 * (m5 * num3 - m6 * num5 + m7 * num6);
		}

		/// <summary>Inverts the specified matrix. The return value indicates whether the operation succeeded.</summary>
		/// <param name="matrix">The matrix to invert.</param>
		/// <param name="result">When this method returns, contains the inverted matrix if the operation succeeded.</param>
		/// <returns>true if <paramref name="matrix">matrix</paramref> was converted successfully; otherwise,  false.</returns>
		public static bool Invert(Matrix4x4 matrix, out Matrix4x4 result)
		{
			float m = matrix.M11;
			float m2 = matrix.M12;
			float m3 = matrix.M13;
			float m4 = matrix.M14;
			float m5 = matrix.M21;
			float m6 = matrix.M22;
			float m7 = matrix.M23;
			float m8 = matrix.M24;
			float m9 = matrix.M31;
			float m10 = matrix.M32;
			float m11 = matrix.M33;
			float m12 = matrix.M34;
			float m13 = matrix.M41;
			float m14 = matrix.M42;
			float m15 = matrix.M43;
			float m16 = matrix.M44;
			float num = m11 * m16 - m12 * m15;
			float num2 = m10 * m16 - m12 * m14;
			float num3 = m10 * m15 - m11 * m14;
			float num4 = m9 * m16 - m12 * m13;
			float num5 = m9 * m15 - m11 * m13;
			float num6 = m9 * m14 - m10 * m13;
			float num7 = m6 * num - m7 * num2 + m8 * num3;
			float num8 = 0f - (m5 * num - m7 * num4 + m8 * num5);
			float num9 = m5 * num2 - m6 * num4 + m8 * num6;
			float num10 = 0f - (m5 * num3 - m6 * num5 + m7 * num6);
			float num11 = m * num7 + m2 * num8 + m3 * num9 + m4 * num10;
			if (MathF.Abs(num11) < float.Epsilon)
			{
				result = new Matrix4x4(float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN);
				return false;
			}
			float num12 = 1f / num11;
			result.M11 = num7 * num12;
			result.M21 = num8 * num12;
			result.M31 = num9 * num12;
			result.M41 = num10 * num12;
			result.M12 = (0f - (m2 * num - m3 * num2 + m4 * num3)) * num12;
			result.M22 = (m * num - m3 * num4 + m4 * num5) * num12;
			result.M32 = (0f - (m * num2 - m2 * num4 + m4 * num6)) * num12;
			result.M42 = (m * num3 - m2 * num5 + m3 * num6) * num12;
			float num13 = m7 * m16 - m8 * m15;
			float num14 = m6 * m16 - m8 * m14;
			float num15 = m6 * m15 - m7 * m14;
			float num16 = m5 * m16 - m8 * m13;
			float num17 = m5 * m15 - m7 * m13;
			float num18 = m5 * m14 - m6 * m13;
			result.M13 = (m2 * num13 - m3 * num14 + m4 * num15) * num12;
			result.M23 = (0f - (m * num13 - m3 * num16 + m4 * num17)) * num12;
			result.M33 = (m * num14 - m2 * num16 + m4 * num18) * num12;
			result.M43 = (0f - (m * num15 - m2 * num17 + m3 * num18)) * num12;
			float num19 = m7 * m12 - m8 * m11;
			float num20 = m6 * m12 - m8 * m10;
			float num21 = m6 * m11 - m7 * m10;
			float num22 = m5 * m12 - m8 * m9;
			float num23 = m5 * m11 - m7 * m9;
			float num24 = m5 * m10 - m6 * m9;
			result.M14 = (0f - (m2 * num19 - m3 * num20 + m4 * num21)) * num12;
			result.M24 = (m * num19 - m3 * num22 + m4 * num23) * num12;
			result.M34 = (0f - (m * num20 - m2 * num22 + m4 * num24)) * num12;
			result.M44 = (m * num21 - m2 * num23 + m3 * num24) * num12;
			return true;
		}

		/// <summary>Attempts to extract the scale, translation, and rotation components from the given scale, rotation, or translation matrix. The return value indicates whether the operation succeeded.</summary>
		/// <param name="matrix">The source matrix.</param>
		/// <param name="scale">When this method returns, contains the scaling component of the transformation matrix if the operation succeeded.</param>
		/// <param name="rotation">When this method returns, contains the rotation component of the transformation matrix if the operation succeeded.</param>
		/// <param name="translation">When the method returns, contains the translation component of the transformation matrix if the operation succeeded.</param>
		/// <returns>true if <paramref name="matrix">matrix</paramref> was decomposed successfully; otherwise,  false.</returns>
		public unsafe static bool Decompose(Matrix4x4 matrix, out Vector3 scale, out Quaternion rotation, out Vector3 translation)
		{
			bool result = true;
			fixed (Vector3* ptr = &scale)
			{
				float* ptr2 = (float*)ptr;
				VectorBasis vectorBasis = default(VectorBasis);
				Vector3** ptr3 = (Vector3**)(&vectorBasis);
				Matrix4x4 identity = Identity;
				CanonicalBasis canonicalBasis = default(CanonicalBasis);
				Vector3* ptr4 = &canonicalBasis.Row0;
				canonicalBasis.Row0 = new Vector3(1f, 0f, 0f);
				canonicalBasis.Row1 = new Vector3(0f, 1f, 0f);
				canonicalBasis.Row2 = new Vector3(0f, 0f, 1f);
				translation = new Vector3(matrix.M41, matrix.M42, matrix.M43);
				*ptr3 = (Vector3*)(&identity.M11);
				ptr3[1] = (Vector3*)(&identity.M21);
				ptr3[2] = (Vector3*)(&identity.M31);
				*(*ptr3) = new Vector3(matrix.M11, matrix.M12, matrix.M13);
				*ptr3[1] = new Vector3(matrix.M21, matrix.M22, matrix.M23);
				*ptr3[2] = new Vector3(matrix.M31, matrix.M32, matrix.M33);
				scale.X = (*ptr3)->Length();
				scale.Y = ptr3[1]->Length();
				scale.Z = ptr3[2]->Length();
				float num = *ptr2;
				float num2 = ptr2[1];
				float num3 = ptr2[2];
				uint num4;
				uint num5;
				uint num6;
				if (num < num2)
				{
					if (num2 < num3)
					{
						num4 = 2u;
						num5 = 1u;
						num6 = 0u;
					}
					else
					{
						num4 = 1u;
						if (num < num3)
						{
							num5 = 2u;
							num6 = 0u;
						}
						else
						{
							num5 = 0u;
							num6 = 2u;
						}
					}
				}
				else if (num < num3)
				{
					num4 = 2u;
					num5 = 0u;
					num6 = 1u;
				}
				else
				{
					num4 = 0u;
					if (num2 < num3)
					{
						num5 = 2u;
						num6 = 1u;
					}
					else
					{
						num5 = 1u;
						num6 = 2u;
					}
				}
				if (ptr2[num4] < 0.0001f)
				{
					*ptr3[num4] = ptr4[num4];
				}
				*ptr3[num4] = Vector3.Normalize(*ptr3[num4]);
				if (ptr2[num5] < 0.0001f)
				{
					float num7 = MathF.Abs(ptr3[num4]->X);
					float num8 = MathF.Abs(ptr3[num4]->Y);
					float num9 = MathF.Abs(ptr3[num4]->Z);
					uint num10 = (num7 < num8) ? ((!(num8 < num9)) ? ((!(num7 < num9)) ? 2u : 0u) : 0u) : ((num7 < num9) ? 1u : ((num8 < num9) ? 1u : 2u));
					*ptr3[num5] = Vector3.Cross(*ptr3[num4], ptr4[num10]);
				}
				*ptr3[num5] = Vector3.Normalize(*ptr3[num5]);
				if (ptr2[num6] < 0.0001f)
				{
					*ptr3[num6] = Vector3.Cross(*ptr3[num4], *ptr3[num5]);
				}
				*ptr3[num6] = Vector3.Normalize(*ptr3[num6]);
				float num11 = identity.GetDeterminant();
				if (num11 < 0f)
				{
					ptr2[num4] = 0f - ptr2[num4];
					*ptr3[num4] = -(*ptr3[num4]);
					num11 = 0f - num11;
				}
				num11 -= 1f;
				num11 *= num11;
				if (0.0001f < num11)
				{
					rotation = Quaternion.Identity;
					result = false;
				}
				else
				{
					rotation = Quaternion.CreateFromRotationMatrix(identity);
				}
			}
			return result;
		}

		/// <summary>Transforms the specified matrix by applying the specified Quaternion rotation.</summary>
		/// <param name="value">The matrix to transform.</param>
		/// <param name="rotation">The rotation t apply.</param>
		/// <returns>The transformed matrix.</returns>
		public static Matrix4x4 Transform(Matrix4x4 value, Quaternion rotation)
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
			float num13 = 1f - num10 - num12;
			float num14 = num8 - num6;
			float num15 = num9 + num5;
			float num16 = num8 + num6;
			float num17 = 1f - num7 - num12;
			float num18 = num11 - num4;
			float num19 = num9 - num5;
			float num20 = num11 + num4;
			float num21 = 1f - num7 - num10;
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = value.M11 * num13 + value.M12 * num14 + value.M13 * num15;
			result.M12 = value.M11 * num16 + value.M12 * num17 + value.M13 * num18;
			result.M13 = value.M11 * num19 + value.M12 * num20 + value.M13 * num21;
			result.M14 = value.M14;
			result.M21 = value.M21 * num13 + value.M22 * num14 + value.M23 * num15;
			result.M22 = value.M21 * num16 + value.M22 * num17 + value.M23 * num18;
			result.M23 = value.M21 * num19 + value.M22 * num20 + value.M23 * num21;
			result.M24 = value.M24;
			result.M31 = value.M31 * num13 + value.M32 * num14 + value.M33 * num15;
			result.M32 = value.M31 * num16 + value.M32 * num17 + value.M33 * num18;
			result.M33 = value.M31 * num19 + value.M32 * num20 + value.M33 * num21;
			result.M34 = value.M34;
			result.M41 = value.M41 * num13 + value.M42 * num14 + value.M43 * num15;
			result.M42 = value.M41 * num16 + value.M42 * num17 + value.M43 * num18;
			result.M43 = value.M41 * num19 + value.M42 * num20 + value.M43 * num21;
			result.M44 = value.M44;
			return result;
		}

		/// <summary>Transposes the rows and columns of a matrix.</summary>
		/// <param name="matrix">The matrix to transpose.</param>
		/// <returns>The transposed matrix.</returns>
		public static Matrix4x4 Transpose(Matrix4x4 matrix)
		{
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = matrix.M11;
			result.M12 = matrix.M21;
			result.M13 = matrix.M31;
			result.M14 = matrix.M41;
			result.M21 = matrix.M12;
			result.M22 = matrix.M22;
			result.M23 = matrix.M32;
			result.M24 = matrix.M42;
			result.M31 = matrix.M13;
			result.M32 = matrix.M23;
			result.M33 = matrix.M33;
			result.M34 = matrix.M43;
			result.M41 = matrix.M14;
			result.M42 = matrix.M24;
			result.M43 = matrix.M34;
			result.M44 = matrix.M44;
			return result;
		}

		/// <summary>Performs a linear interpolation from one matrix to a second matrix based on a value that specifies the weighting of the second matrix.</summary>
		/// <param name="matrix1">The first matrix.</param>
		/// <param name="matrix2">The second matrix.</param>
		/// <param name="amount">The relative weighting of matrix2.</param>
		/// <returns>The interpolated matrix.</returns>
		public static Matrix4x4 Lerp(Matrix4x4 matrix1, Matrix4x4 matrix2, float amount)
		{
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = matrix1.M11 + (matrix2.M11 - matrix1.M11) * amount;
			result.M12 = matrix1.M12 + (matrix2.M12 - matrix1.M12) * amount;
			result.M13 = matrix1.M13 + (matrix2.M13 - matrix1.M13) * amount;
			result.M14 = matrix1.M14 + (matrix2.M14 - matrix1.M14) * amount;
			result.M21 = matrix1.M21 + (matrix2.M21 - matrix1.M21) * amount;
			result.M22 = matrix1.M22 + (matrix2.M22 - matrix1.M22) * amount;
			result.M23 = matrix1.M23 + (matrix2.M23 - matrix1.M23) * amount;
			result.M24 = matrix1.M24 + (matrix2.M24 - matrix1.M24) * amount;
			result.M31 = matrix1.M31 + (matrix2.M31 - matrix1.M31) * amount;
			result.M32 = matrix1.M32 + (matrix2.M32 - matrix1.M32) * amount;
			result.M33 = matrix1.M33 + (matrix2.M33 - matrix1.M33) * amount;
			result.M34 = matrix1.M34 + (matrix2.M34 - matrix1.M34) * amount;
			result.M41 = matrix1.M41 + (matrix2.M41 - matrix1.M41) * amount;
			result.M42 = matrix1.M42 + (matrix2.M42 - matrix1.M42) * amount;
			result.M43 = matrix1.M43 + (matrix2.M43 - matrix1.M43) * amount;
			result.M44 = matrix1.M44 + (matrix2.M44 - matrix1.M44) * amount;
			return result;
		}

		/// <summary>Negates the specified matrix by multiplying all its values by -1.</summary>
		/// <param name="value">The matrix to negate.</param>
		/// <returns>The negated matrix.</returns>
		public static Matrix4x4 Negate(Matrix4x4 value)
		{
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = 0f - value.M11;
			result.M12 = 0f - value.M12;
			result.M13 = 0f - value.M13;
			result.M14 = 0f - value.M14;
			result.M21 = 0f - value.M21;
			result.M22 = 0f - value.M22;
			result.M23 = 0f - value.M23;
			result.M24 = 0f - value.M24;
			result.M31 = 0f - value.M31;
			result.M32 = 0f - value.M32;
			result.M33 = 0f - value.M33;
			result.M34 = 0f - value.M34;
			result.M41 = 0f - value.M41;
			result.M42 = 0f - value.M42;
			result.M43 = 0f - value.M43;
			result.M44 = 0f - value.M44;
			return result;
		}

		/// <summary>Adds each element in one matrix with its corresponding element in a second matrix.</summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The matrix that contains the summed values of <paramref name="value1">value1</paramref> and <paramref name="value2">value2</paramref>.</returns>
		public static Matrix4x4 Add(Matrix4x4 value1, Matrix4x4 value2)
		{
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = value1.M11 + value2.M11;
			result.M12 = value1.M12 + value2.M12;
			result.M13 = value1.M13 + value2.M13;
			result.M14 = value1.M14 + value2.M14;
			result.M21 = value1.M21 + value2.M21;
			result.M22 = value1.M22 + value2.M22;
			result.M23 = value1.M23 + value2.M23;
			result.M24 = value1.M24 + value2.M24;
			result.M31 = value1.M31 + value2.M31;
			result.M32 = value1.M32 + value2.M32;
			result.M33 = value1.M33 + value2.M33;
			result.M34 = value1.M34 + value2.M34;
			result.M41 = value1.M41 + value2.M41;
			result.M42 = value1.M42 + value2.M42;
			result.M43 = value1.M43 + value2.M43;
			result.M44 = value1.M44 + value2.M44;
			return result;
		}

		/// <summary>Subtracts each element in a second matrix from its corresponding element in a first matrix.</summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The matrix containing the values that result from subtracting each element in <paramref name="value2">value2</paramref> from its corresponding element in <paramref name="value1">value1</paramref>.</returns>
		public static Matrix4x4 Subtract(Matrix4x4 value1, Matrix4x4 value2)
		{
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = value1.M11 - value2.M11;
			result.M12 = value1.M12 - value2.M12;
			result.M13 = value1.M13 - value2.M13;
			result.M14 = value1.M14 - value2.M14;
			result.M21 = value1.M21 - value2.M21;
			result.M22 = value1.M22 - value2.M22;
			result.M23 = value1.M23 - value2.M23;
			result.M24 = value1.M24 - value2.M24;
			result.M31 = value1.M31 - value2.M31;
			result.M32 = value1.M32 - value2.M32;
			result.M33 = value1.M33 - value2.M33;
			result.M34 = value1.M34 - value2.M34;
			result.M41 = value1.M41 - value2.M41;
			result.M42 = value1.M42 - value2.M42;
			result.M43 = value1.M43 - value2.M43;
			result.M44 = value1.M44 - value2.M44;
			return result;
		}

		/// <summary>Returns the matrix that results from multiplying two matrices together.</summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The product matrix.</returns>
		public static Matrix4x4 Multiply(Matrix4x4 value1, Matrix4x4 value2)
		{
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = value1.M11 * value2.M11 + value1.M12 * value2.M21 + value1.M13 * value2.M31 + value1.M14 * value2.M41;
			result.M12 = value1.M11 * value2.M12 + value1.M12 * value2.M22 + value1.M13 * value2.M32 + value1.M14 * value2.M42;
			result.M13 = value1.M11 * value2.M13 + value1.M12 * value2.M23 + value1.M13 * value2.M33 + value1.M14 * value2.M43;
			result.M14 = value1.M11 * value2.M14 + value1.M12 * value2.M24 + value1.M13 * value2.M34 + value1.M14 * value2.M44;
			result.M21 = value1.M21 * value2.M11 + value1.M22 * value2.M21 + value1.M23 * value2.M31 + value1.M24 * value2.M41;
			result.M22 = value1.M21 * value2.M12 + value1.M22 * value2.M22 + value1.M23 * value2.M32 + value1.M24 * value2.M42;
			result.M23 = value1.M21 * value2.M13 + value1.M22 * value2.M23 + value1.M23 * value2.M33 + value1.M24 * value2.M43;
			result.M24 = value1.M21 * value2.M14 + value1.M22 * value2.M24 + value1.M23 * value2.M34 + value1.M24 * value2.M44;
			result.M31 = value1.M31 * value2.M11 + value1.M32 * value2.M21 + value1.M33 * value2.M31 + value1.M34 * value2.M41;
			result.M32 = value1.M31 * value2.M12 + value1.M32 * value2.M22 + value1.M33 * value2.M32 + value1.M34 * value2.M42;
			result.M33 = value1.M31 * value2.M13 + value1.M32 * value2.M23 + value1.M33 * value2.M33 + value1.M34 * value2.M43;
			result.M34 = value1.M31 * value2.M14 + value1.M32 * value2.M24 + value1.M33 * value2.M34 + value1.M34 * value2.M44;
			result.M41 = value1.M41 * value2.M11 + value1.M42 * value2.M21 + value1.M43 * value2.M31 + value1.M44 * value2.M41;
			result.M42 = value1.M41 * value2.M12 + value1.M42 * value2.M22 + value1.M43 * value2.M32 + value1.M44 * value2.M42;
			result.M43 = value1.M41 * value2.M13 + value1.M42 * value2.M23 + value1.M43 * value2.M33 + value1.M44 * value2.M43;
			result.M44 = value1.M41 * value2.M14 + value1.M42 * value2.M24 + value1.M43 * value2.M34 + value1.M44 * value2.M44;
			return result;
		}

		/// <summary>Returns the matrix that results from scaling all the elements of a specified matrix by a scalar factor.</summary>
		/// <param name="value1">The matrix to scale.</param>
		/// <param name="value2">The scaling value to use.</param>
		/// <returns>The scaled matrix.</returns>
		public static Matrix4x4 Multiply(Matrix4x4 value1, float value2)
		{
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = value1.M11 * value2;
			result.M12 = value1.M12 * value2;
			result.M13 = value1.M13 * value2;
			result.M14 = value1.M14 * value2;
			result.M21 = value1.M21 * value2;
			result.M22 = value1.M22 * value2;
			result.M23 = value1.M23 * value2;
			result.M24 = value1.M24 * value2;
			result.M31 = value1.M31 * value2;
			result.M32 = value1.M32 * value2;
			result.M33 = value1.M33 * value2;
			result.M34 = value1.M34 * value2;
			result.M41 = value1.M41 * value2;
			result.M42 = value1.M42 * value2;
			result.M43 = value1.M43 * value2;
			result.M44 = value1.M44 * value2;
			return result;
		}

		/// <summary>Negates the specified matrix by multiplying all its values by -1.</summary>
		/// <param name="value">The matrix to negate.</param>
		/// <returns>The negated matrix.</returns>
		public static Matrix4x4 operator -(Matrix4x4 value)
		{
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = 0f - value.M11;
			result.M12 = 0f - value.M12;
			result.M13 = 0f - value.M13;
			result.M14 = 0f - value.M14;
			result.M21 = 0f - value.M21;
			result.M22 = 0f - value.M22;
			result.M23 = 0f - value.M23;
			result.M24 = 0f - value.M24;
			result.M31 = 0f - value.M31;
			result.M32 = 0f - value.M32;
			result.M33 = 0f - value.M33;
			result.M34 = 0f - value.M34;
			result.M41 = 0f - value.M41;
			result.M42 = 0f - value.M42;
			result.M43 = 0f - value.M43;
			result.M44 = 0f - value.M44;
			return result;
		}

		/// <summary>Adds each element in one matrix with its corresponding element in a second matrix.</summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The matrix that contains the summed values.</returns>
		public static Matrix4x4 operator +(Matrix4x4 value1, Matrix4x4 value2)
		{
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = value1.M11 + value2.M11;
			result.M12 = value1.M12 + value2.M12;
			result.M13 = value1.M13 + value2.M13;
			result.M14 = value1.M14 + value2.M14;
			result.M21 = value1.M21 + value2.M21;
			result.M22 = value1.M22 + value2.M22;
			result.M23 = value1.M23 + value2.M23;
			result.M24 = value1.M24 + value2.M24;
			result.M31 = value1.M31 + value2.M31;
			result.M32 = value1.M32 + value2.M32;
			result.M33 = value1.M33 + value2.M33;
			result.M34 = value1.M34 + value2.M34;
			result.M41 = value1.M41 + value2.M41;
			result.M42 = value1.M42 + value2.M42;
			result.M43 = value1.M43 + value2.M43;
			result.M44 = value1.M44 + value2.M44;
			return result;
		}

		/// <summary>Subtracts each element in a second matrix from its corresponding element in a first matrix.</summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The matrix containing the values that result from subtracting each element in <paramref name="value2">value2</paramref> from its corresponding element in <paramref name="value1">value1</paramref>.</returns>
		public static Matrix4x4 operator -(Matrix4x4 value1, Matrix4x4 value2)
		{
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = value1.M11 - value2.M11;
			result.M12 = value1.M12 - value2.M12;
			result.M13 = value1.M13 - value2.M13;
			result.M14 = value1.M14 - value2.M14;
			result.M21 = value1.M21 - value2.M21;
			result.M22 = value1.M22 - value2.M22;
			result.M23 = value1.M23 - value2.M23;
			result.M24 = value1.M24 - value2.M24;
			result.M31 = value1.M31 - value2.M31;
			result.M32 = value1.M32 - value2.M32;
			result.M33 = value1.M33 - value2.M33;
			result.M34 = value1.M34 - value2.M34;
			result.M41 = value1.M41 - value2.M41;
			result.M42 = value1.M42 - value2.M42;
			result.M43 = value1.M43 - value2.M43;
			result.M44 = value1.M44 - value2.M44;
			return result;
		}

		/// <summary>Returns the matrix that results from multiplying two matrices together.</summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The product matrix.</returns>
		public static Matrix4x4 operator *(Matrix4x4 value1, Matrix4x4 value2)
		{
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = value1.M11 * value2.M11 + value1.M12 * value2.M21 + value1.M13 * value2.M31 + value1.M14 * value2.M41;
			result.M12 = value1.M11 * value2.M12 + value1.M12 * value2.M22 + value1.M13 * value2.M32 + value1.M14 * value2.M42;
			result.M13 = value1.M11 * value2.M13 + value1.M12 * value2.M23 + value1.M13 * value2.M33 + value1.M14 * value2.M43;
			result.M14 = value1.M11 * value2.M14 + value1.M12 * value2.M24 + value1.M13 * value2.M34 + value1.M14 * value2.M44;
			result.M21 = value1.M21 * value2.M11 + value1.M22 * value2.M21 + value1.M23 * value2.M31 + value1.M24 * value2.M41;
			result.M22 = value1.M21 * value2.M12 + value1.M22 * value2.M22 + value1.M23 * value2.M32 + value1.M24 * value2.M42;
			result.M23 = value1.M21 * value2.M13 + value1.M22 * value2.M23 + value1.M23 * value2.M33 + value1.M24 * value2.M43;
			result.M24 = value1.M21 * value2.M14 + value1.M22 * value2.M24 + value1.M23 * value2.M34 + value1.M24 * value2.M44;
			result.M31 = value1.M31 * value2.M11 + value1.M32 * value2.M21 + value1.M33 * value2.M31 + value1.M34 * value2.M41;
			result.M32 = value1.M31 * value2.M12 + value1.M32 * value2.M22 + value1.M33 * value2.M32 + value1.M34 * value2.M42;
			result.M33 = value1.M31 * value2.M13 + value1.M32 * value2.M23 + value1.M33 * value2.M33 + value1.M34 * value2.M43;
			result.M34 = value1.M31 * value2.M14 + value1.M32 * value2.M24 + value1.M33 * value2.M34 + value1.M34 * value2.M44;
			result.M41 = value1.M41 * value2.M11 + value1.M42 * value2.M21 + value1.M43 * value2.M31 + value1.M44 * value2.M41;
			result.M42 = value1.M41 * value2.M12 + value1.M42 * value2.M22 + value1.M43 * value2.M32 + value1.M44 * value2.M42;
			result.M43 = value1.M41 * value2.M13 + value1.M42 * value2.M23 + value1.M43 * value2.M33 + value1.M44 * value2.M43;
			result.M44 = value1.M41 * value2.M14 + value1.M42 * value2.M24 + value1.M43 * value2.M34 + value1.M44 * value2.M44;
			return result;
		}

		/// <summary>Returns the matrix that results from scaling all the elements of a specified matrix by a scalar factor.</summary>
		/// <param name="value1">The matrix to scale.</param>
		/// <param name="value2">The scaling value to use.</param>
		/// <returns>The scaled matrix.</returns>
		public static Matrix4x4 operator *(Matrix4x4 value1, float value2)
		{
			Matrix4x4 result = default(Matrix4x4);
			result.M11 = value1.M11 * value2;
			result.M12 = value1.M12 * value2;
			result.M13 = value1.M13 * value2;
			result.M14 = value1.M14 * value2;
			result.M21 = value1.M21 * value2;
			result.M22 = value1.M22 * value2;
			result.M23 = value1.M23 * value2;
			result.M24 = value1.M24 * value2;
			result.M31 = value1.M31 * value2;
			result.M32 = value1.M32 * value2;
			result.M33 = value1.M33 * value2;
			result.M34 = value1.M34 * value2;
			result.M41 = value1.M41 * value2;
			result.M42 = value1.M42 * value2;
			result.M43 = value1.M43 * value2;
			result.M44 = value1.M44 * value2;
			return result;
		}

		/// <summary>Returns a value that indicates whether the specified matrices are equal.</summary>
		/// <param name="value1">The first matrix to compare.</param>
		/// <param name="value2">The second matrix to care</param>
		/// <returns>true if <paramref name="value1">value1</paramref> and <paramref name="value2">value2</paramref> are equal; otherwise, false.</returns>
		public static bool operator ==(Matrix4x4 value1, Matrix4x4 value2)
		{
			if (value1.M11 == value2.M11 && value1.M22 == value2.M22 && value1.M33 == value2.M33 && value1.M44 == value2.M44 && value1.M12 == value2.M12 && value1.M13 == value2.M13 && value1.M14 == value2.M14 && value1.M21 == value2.M21 && value1.M23 == value2.M23 && value1.M24 == value2.M24 && value1.M31 == value2.M31 && value1.M32 == value2.M32 && value1.M34 == value2.M34 && value1.M41 == value2.M41 && value1.M42 == value2.M42)
			{
				return value1.M43 == value2.M43;
			}
			return false;
		}

		/// <summary>Returns a value that indicates whether the specified matrices are not equal.</summary>
		/// <param name="value1">The first matrix to compare.</param>
		/// <param name="value2">The second matrix to compare.</param>
		/// <returns>true if <paramref name="value1">value1</paramref> and <paramref name="value2">value2</paramref> are not equal; otherwise, false.</returns>
		public static bool operator !=(Matrix4x4 value1, Matrix4x4 value2)
		{
			if (value1.M11 == value2.M11 && value1.M12 == value2.M12 && value1.M13 == value2.M13 && value1.M14 == value2.M14 && value1.M21 == value2.M21 && value1.M22 == value2.M22 && value1.M23 == value2.M23 && value1.M24 == value2.M24 && value1.M31 == value2.M31 && value1.M32 == value2.M32 && value1.M33 == value2.M33 && value1.M34 == value2.M34 && value1.M41 == value2.M41 && value1.M42 == value2.M42 && value1.M43 == value2.M43)
			{
				return value1.M44 != value2.M44;
			}
			return true;
		}

		/// <summary>Returns a value that indicates whether this instance and another 4x4 matrix are equal.</summary>
		/// <param name="other">The other matrix.</param>
		/// <returns>true if the two matrices are equal; otherwise, false.</returns>
		public bool Equals(Matrix4x4 other)
		{
			if (M11 == other.M11 && M22 == other.M22 && M33 == other.M33 && M44 == other.M44 && M12 == other.M12 && M13 == other.M13 && M14 == other.M14 && M21 == other.M21 && M23 == other.M23 && M24 == other.M24 && M31 == other.M31 && M32 == other.M32 && M34 == other.M34 && M41 == other.M41 && M42 == other.M42)
			{
				return M43 == other.M43;
			}
			return false;
		}

		/// <summary>Returns a value that indicates whether this instance and a specified object are equal.</summary>
		/// <param name="obj">The object to compare with the current instance.</param>
		/// <returns>true if the current instance and <paramref name="obj">obj</paramref> are equal; otherwise, false. If <paramref name="obj">obj</paramref> is null, the method returns false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Matrix4x4)
			{
				return Equals((Matrix4x4)obj);
			}
			return false;
		}

		/// <summary>Returns a string that represents this matrix.</summary>
		/// <returns>The string representation of this matrix.</returns>
		public override string ToString()
		{
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			return string.Format(currentCulture, "{{ {{M11:{0} M12:{1} M13:{2} M14:{3}}} {{M21:{4} M22:{5} M23:{6} M24:{7}}} {{M31:{8} M32:{9} M33:{10} M34:{11}}} {{M41:{12} M42:{13} M43:{14} M44:{15}}} }}", M11.ToString(currentCulture), M12.ToString(currentCulture), M13.ToString(currentCulture), M14.ToString(currentCulture), M21.ToString(currentCulture), M22.ToString(currentCulture), M23.ToString(currentCulture), M24.ToString(currentCulture), M31.ToString(currentCulture), M32.ToString(currentCulture), M33.ToString(currentCulture), M34.ToString(currentCulture), M41.ToString(currentCulture), M42.ToString(currentCulture), M43.ToString(currentCulture), M44.ToString(currentCulture));
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>The hash code.</returns>
		public override int GetHashCode()
		{
			return M11.GetHashCode() + M12.GetHashCode() + M13.GetHashCode() + M14.GetHashCode() + M21.GetHashCode() + M22.GetHashCode() + M23.GetHashCode() + M24.GetHashCode() + M31.GetHashCode() + M32.GetHashCode() + M33.GetHashCode() + M34.GetHashCode() + M41.GetHashCode() + M42.GetHashCode() + M43.GetHashCode() + M44.GetHashCode();
		}
	}
}
