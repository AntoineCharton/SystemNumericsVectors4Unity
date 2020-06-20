using System.Globalization;

namespace System.Numerics
{
	/// <summary>Represents a 3x2 matrix.</summary>
	public struct Matrix3x2 : IEquatable<Matrix3x2>
	{
		/// <summary>The first element of the first row.</summary>
		/// <returns></returns>
		public float M11;

		/// <summary>The second element of the first row.</summary>
		/// <returns></returns>
		public float M12;

		/// <summary>The first element of the second row.</summary>
		/// <returns></returns>
		public float M21;

		/// <summary>The second element of the second row.</summary>
		/// <returns></returns>
		public float M22;

		/// <summary>The first element of the third row.</summary>
		/// <returns></returns>
		public float M31;

		/// <summary>The second element of the third row.</summary>
		/// <returns></returns>
		public float M32;

		private static readonly Matrix3x2 _identity = new Matrix3x2(1f, 0f, 0f, 1f, 0f, 0f);

		/// <summary>Gets the multiplicative identity matrix.</summary>
		/// <returns>The multiplicative identify matrix.</returns>
		public static Matrix3x2 Identity => _identity;

		/// <summary>Indicates whether the current matrix is the identity matrix.</summary>
		/// <returns>true if the current matrix is the identity matrix; otherwise, false.</returns>
		public bool IsIdentity
		{
			get
			{
				if (M11 == 1f && M22 == 1f && M12 == 0f && M21 == 0f && M31 == 0f)
				{
					return M32 == 0f;
				}
				return false;
			}
		}

		/// <summary>Gets or sets the translation component of this matrix.</summary>
		/// <returns>The translation component of the current instance.</returns>
		public Vector2 Translation
		{
			get
			{
				return new Vector2(M31, M32);
			}
			set
			{
				M31 = value.X;
				M32 = value.Y;
			}
		}

		/// <summary>Creates a 3x2 matrix from the specified components.</summary>
		/// <param name="m11">The value to assign to the first element in the first row.</param>
		/// <param name="m12">The value to assign to the second element in the first row.</param>
		/// <param name="m21">The value to assign to the first element in the second row.</param>
		/// <param name="m22">The value to assign to the second element in the second row.</param>
		/// <param name="m31">The value to assign to the first element in the third row.</param>
		/// <param name="m32">The value to assign to the second element in the third row.</param>
		public Matrix3x2(float m11, float m12, float m21, float m22, float m31, float m32)
		{
			M11 = m11;
			M12 = m12;
			M21 = m21;
			M22 = m22;
			M31 = m31;
			M32 = m32;
		}

		/// <summary>Creates a translation matrix from the specified 2-dimensional vector.</summary>
		/// <param name="position">The translation position.</param>
		/// <returns>The translation matrix.</returns>
		public static Matrix3x2 CreateTranslation(Vector2 position)
		{
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = 1f;
			result.M12 = 0f;
			result.M21 = 0f;
			result.M22 = 1f;
			result.M31 = position.X;
			result.M32 = position.Y;
			return result;
		}

		/// <summary>Creates a translation matrix from the specified X and Y components.</summary>
		/// <param name="xPosition">The X position.</param>
		/// <param name="yPosition">The Y position.</param>
		/// <returns>The translation matrix.</returns>
		public static Matrix3x2 CreateTranslation(float xPosition, float yPosition)
		{
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = 1f;
			result.M12 = 0f;
			result.M21 = 0f;
			result.M22 = 1f;
			result.M31 = xPosition;
			result.M32 = yPosition;
			return result;
		}

		/// <summary>Creates a scaling matrix from the specified X and Y components.</summary>
		/// <param name="xScale">The value to scale by on the X axis.</param>
		/// <param name="yScale">The value to scale by on the Y axis.</param>
		/// <returns>The scaling matrix.</returns>
		public static Matrix3x2 CreateScale(float xScale, float yScale)
		{
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = xScale;
			result.M12 = 0f;
			result.M21 = 0f;
			result.M22 = yScale;
			result.M31 = 0f;
			result.M32 = 0f;
			return result;
		}

		/// <summary>Creates a scaling matrix that is offset by a given center point.</summary>
		/// <param name="xScale">The value to scale by on the X axis.</param>
		/// <param name="yScale">The value to scale by on the Y axis.</param>
		/// <param name="centerPoint">The center point.</param>
		/// <returns>The scaling matrix.</returns>
		public static Matrix3x2 CreateScale(float xScale, float yScale, Vector2 centerPoint)
		{
			float m = centerPoint.X * (1f - xScale);
			float m2 = centerPoint.Y * (1f - yScale);
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = xScale;
			result.M12 = 0f;
			result.M21 = 0f;
			result.M22 = yScale;
			result.M31 = m;
			result.M32 = m2;
			return result;
		}

		/// <summary>Creates a scaling matrix from the specified vector scale.</summary>
		/// <param name="scales">The scale to use.</param>
		/// <returns>The scaling matrix.</returns>
		public static Matrix3x2 CreateScale(Vector2 scales)
		{
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = scales.X;
			result.M12 = 0f;
			result.M21 = 0f;
			result.M22 = scales.Y;
			result.M31 = 0f;
			result.M32 = 0f;
			return result;
		}

		/// <summary>Creates a scaling matrix from the specified vector scale with an offset from the specified center point.</summary>
		/// <param name="scales">The scale to use.</param>
		/// <param name="centerPoint">The center offset.</param>
		/// <returns>The scaling matrix.</returns>
		public static Matrix3x2 CreateScale(Vector2 scales, Vector2 centerPoint)
		{
			float m = centerPoint.X * (1f - scales.X);
			float m2 = centerPoint.Y * (1f - scales.Y);
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = scales.X;
			result.M12 = 0f;
			result.M21 = 0f;
			result.M22 = scales.Y;
			result.M31 = m;
			result.M32 = m2;
			return result;
		}

		/// <summary>Creates a scaling matrix that scales uniformly with the given scale.</summary>
		/// <param name="scale">The uniform scale to use.</param>
		/// <returns>The scaling matrix.</returns>
		public static Matrix3x2 CreateScale(float scale)
		{
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = scale;
			result.M12 = 0f;
			result.M21 = 0f;
			result.M22 = scale;
			result.M31 = 0f;
			result.M32 = 0f;
			return result;
		}

		/// <summary>Creates a scaling matrix that scales uniformly with the specified scale with an offset from the specified center.</summary>
		/// <param name="scale">The uniform scale to use.</param>
		/// <param name="centerPoint">The center offset.</param>
		/// <returns>The scaling matrix.</returns>
		public static Matrix3x2 CreateScale(float scale, Vector2 centerPoint)
		{
			float m = centerPoint.X * (1f - scale);
			float m2 = centerPoint.Y * (1f - scale);
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = scale;
			result.M12 = 0f;
			result.M21 = 0f;
			result.M22 = scale;
			result.M31 = m;
			result.M32 = m2;
			return result;
		}

		/// <summary>Creates a skew matrix from the specified angles in radians.</summary>
		/// <param name="radiansX">The X angle, in radians.</param>
		/// <param name="radiansY">The Y angle, in radians.</param>
		/// <returns>The skew matrix.</returns>
		public static Matrix3x2 CreateSkew(float radiansX, float radiansY)
		{
			float m = MathF.Tan(radiansX);
			float m2 = MathF.Tan(radiansY);
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = 1f;
			result.M12 = m2;
			result.M21 = m;
			result.M22 = 1f;
			result.M31 = 0f;
			result.M32 = 0f;
			return result;
		}

		/// <summary>Creates a skew matrix from the specified angles in radians and a center point.</summary>
		/// <param name="radiansX">The X angle, in radians.</param>
		/// <param name="radiansY">The Y angle, in radians.</param>
		/// <param name="centerPoint">The center point.</param>
		/// <returns>The skew matrix.</returns>
		public static Matrix3x2 CreateSkew(float radiansX, float radiansY, Vector2 centerPoint)
		{
			float num = MathF.Tan(radiansX);
			float num2 = MathF.Tan(radiansY);
			float m = (0f - centerPoint.Y) * num;
			float m2 = (0f - centerPoint.X) * num2;
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = 1f;
			result.M12 = num2;
			result.M21 = num;
			result.M22 = 1f;
			result.M31 = m;
			result.M32 = m2;
			return result;
		}

		/// <summary>Creates a rotation matrix using the given rotation in radians.</summary>
		/// <param name="radians">The amount of rotation, in radians.</param>
		/// <returns>The rotation matrix.</returns>
		public static Matrix3x2 CreateRotation(float radians)
		{
			radians = MathF.IEEERemainder(radians, (float)Math.PI * 2f);
			float num;
			float num2;
			if (radians > -1.74532943E-05f && radians < 1.74532943E-05f)
			{
				num = 1f;
				num2 = 0f;
			}
			else if (radians > 1.570779f && radians < 1.57081378f)
			{
				num = 0f;
				num2 = 1f;
			}
			else if (radians < -3.14157534f || radians > 3.14157534f)
			{
				num = -1f;
				num2 = 0f;
			}
			else if (radians > -1.57081378f && radians < -1.570779f)
			{
				num = 0f;
				num2 = -1f;
			}
			else
			{
				num = MathF.Cos(radians);
				num2 = MathF.Sin(radians);
			}
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = num;
			result.M12 = num2;
			result.M21 = 0f - num2;
			result.M22 = num;
			result.M31 = 0f;
			result.M32 = 0f;
			return result;
		}

		/// <summary>Creates a rotation matrix using the specified rotation in radians and a center point.</summary>
		/// <param name="radians">The amount of rotation, in radians.</param>
		/// <param name="centerPoint">The center point.</param>
		/// <returns>The rotation matrix.</returns>
		public static Matrix3x2 CreateRotation(float radians, Vector2 centerPoint)
		{
			radians = MathF.IEEERemainder(radians, (float)Math.PI * 2f);
			float num;
			float num2;
			if (radians > -1.74532943E-05f && radians < 1.74532943E-05f)
			{
				num = 1f;
				num2 = 0f;
			}
			else if (radians > 1.570779f && radians < 1.57081378f)
			{
				num = 0f;
				num2 = 1f;
			}
			else if (radians < -3.14157534f || radians > 3.14157534f)
			{
				num = -1f;
				num2 = 0f;
			}
			else if (radians > -1.57081378f && radians < -1.570779f)
			{
				num = 0f;
				num2 = -1f;
			}
			else
			{
				num = MathF.Cos(radians);
				num2 = MathF.Sin(radians);
			}
			float m = centerPoint.X * (1f - num) + centerPoint.Y * num2;
			float m2 = centerPoint.Y * (1f - num) - centerPoint.X * num2;
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = num;
			result.M12 = num2;
			result.M21 = 0f - num2;
			result.M22 = num;
			result.M31 = m;
			result.M32 = m2;
			return result;
		}

		/// <summary>Calculates the determinant for this matrix.</summary>
		/// <returns>The determinant.</returns>
		public float GetDeterminant()
		{
			return M11 * M22 - M21 * M12;
		}

		/// <summary>Inverts the specified matrix. The return value indicates whether the operation succeeded.</summary>
		/// <param name="matrix">The matrix to invert.</param>
		/// <param name="result">When this method returns, contains the inverted matrix if the operation succeeded.</param>
		/// <returns>true if <paramref name="matrix">matrix</paramref> was converted successfully; otherwise,  false.</returns>
		public static bool Invert(Matrix3x2 matrix, out Matrix3x2 result)
		{
			float num = matrix.M11 * matrix.M22 - matrix.M21 * matrix.M12;
			if (MathF.Abs(num) < float.Epsilon)
			{
				result = new Matrix3x2(float.NaN, float.NaN, float.NaN, float.NaN, float.NaN, float.NaN);
				return false;
			}
			float num2 = 1f / num;
			result.M11 = matrix.M22 * num2;
			result.M12 = (0f - matrix.M12) * num2;
			result.M21 = (0f - matrix.M21) * num2;
			result.M22 = matrix.M11 * num2;
			result.M31 = (matrix.M21 * matrix.M32 - matrix.M31 * matrix.M22) * num2;
			result.M32 = (matrix.M31 * matrix.M12 - matrix.M11 * matrix.M32) * num2;
			return true;
		}

		/// <summary>Performs a linear interpolation from one matrix to a second matrix based on a value that specifies the weighting of the second matrix.</summary>
		/// <param name="matrix1">The first matrix.</param>
		/// <param name="matrix2">The second matrix.</param>
		/// <param name="amount">The relative weighting of matrix2.</param>
		/// <returns>The interpolated matrix.</returns>
		public static Matrix3x2 Lerp(Matrix3x2 matrix1, Matrix3x2 matrix2, float amount)
		{
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = matrix1.M11 + (matrix2.M11 - matrix1.M11) * amount;
			result.M12 = matrix1.M12 + (matrix2.M12 - matrix1.M12) * amount;
			result.M21 = matrix1.M21 + (matrix2.M21 - matrix1.M21) * amount;
			result.M22 = matrix1.M22 + (matrix2.M22 - matrix1.M22) * amount;
			result.M31 = matrix1.M31 + (matrix2.M31 - matrix1.M31) * amount;
			result.M32 = matrix1.M32 + (matrix2.M32 - matrix1.M32) * amount;
			return result;
		}

		/// <summary>Negates the specified matrix by multiplying all its values by -1.</summary>
		/// <param name="value">The matrix to negate.</param>
		/// <returns>The negated matrix.</returns>
		public static Matrix3x2 Negate(Matrix3x2 value)
		{
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = 0f - value.M11;
			result.M12 = 0f - value.M12;
			result.M21 = 0f - value.M21;
			result.M22 = 0f - value.M22;
			result.M31 = 0f - value.M31;
			result.M32 = 0f - value.M32;
			return result;
		}

		/// <summary>Adds each element in one matrix with its corresponding element in a second matrix.</summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The matrix that contains the summed values of <paramref name="value1">value1</paramref> and <paramref name="value2">value2</paramref>.</returns>
		public static Matrix3x2 Add(Matrix3x2 value1, Matrix3x2 value2)
		{
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = value1.M11 + value2.M11;
			result.M12 = value1.M12 + value2.M12;
			result.M21 = value1.M21 + value2.M21;
			result.M22 = value1.M22 + value2.M22;
			result.M31 = value1.M31 + value2.M31;
			result.M32 = value1.M32 + value2.M32;
			return result;
		}

		/// <summary>Subtracts each element in a second matrix from its corresponding element in a first matrix.</summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The matrix containing the values that result from subtracting each element in <paramref name="value2">value2</paramref> from its corresponding element in <paramref name="value1">value1</paramref>.</returns>
		public static Matrix3x2 Subtract(Matrix3x2 value1, Matrix3x2 value2)
		{
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = value1.M11 - value2.M11;
			result.M12 = value1.M12 - value2.M12;
			result.M21 = value1.M21 - value2.M21;
			result.M22 = value1.M22 - value2.M22;
			result.M31 = value1.M31 - value2.M31;
			result.M32 = value1.M32 - value2.M32;
			return result;
		}

		/// <summary>Returns the matrix that results from multiplying two matrices together.</summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The product matrix.</returns>
		public static Matrix3x2 Multiply(Matrix3x2 value1, Matrix3x2 value2)
		{
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = value1.M11 * value2.M11 + value1.M12 * value2.M21;
			result.M12 = value1.M11 * value2.M12 + value1.M12 * value2.M22;
			result.M21 = value1.M21 * value2.M11 + value1.M22 * value2.M21;
			result.M22 = value1.M21 * value2.M12 + value1.M22 * value2.M22;
			result.M31 = value1.M31 * value2.M11 + value1.M32 * value2.M21 + value2.M31;
			result.M32 = value1.M31 * value2.M12 + value1.M32 * value2.M22 + value2.M32;
			return result;
		}

		/// <summary>Returns the matrix that results from scaling all the elements of a specified matrix by a scalar factor.</summary>
		/// <param name="value1">The matrix to scale.</param>
		/// <param name="value2">The scaling value to use.</param>
		/// <returns>The scaled matrix.</returns>
		public static Matrix3x2 Multiply(Matrix3x2 value1, float value2)
		{
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = value1.M11 * value2;
			result.M12 = value1.M12 * value2;
			result.M21 = value1.M21 * value2;
			result.M22 = value1.M22 * value2;
			result.M31 = value1.M31 * value2;
			result.M32 = value1.M32 * value2;
			return result;
		}

		/// <summary>Negates the specified matrix by multiplying all its values by -1.</summary>
		/// <param name="value">The matrix to negate.</param>
		/// <returns>The negated matrix.</returns>
		public static Matrix3x2 operator -(Matrix3x2 value)
		{
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = 0f - value.M11;
			result.M12 = 0f - value.M12;
			result.M21 = 0f - value.M21;
			result.M22 = 0f - value.M22;
			result.M31 = 0f - value.M31;
			result.M32 = 0f - value.M32;
			return result;
		}

		/// <summary>Adds each element in one matrix with its corresponding element in a second matrix.</summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The matrix that contains the summed values.</returns>
		public static Matrix3x2 operator +(Matrix3x2 value1, Matrix3x2 value2)
		{
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = value1.M11 + value2.M11;
			result.M12 = value1.M12 + value2.M12;
			result.M21 = value1.M21 + value2.M21;
			result.M22 = value1.M22 + value2.M22;
			result.M31 = value1.M31 + value2.M31;
			result.M32 = value1.M32 + value2.M32;
			return result;
		}

		/// <summary>Subtracts each element in a second matrix from its corresponding element in a first matrix.</summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The matrix containing the values that result from subtracting each element in <paramref name="value2">value2</paramref> from its corresponding element in <paramref name="value1">value1</paramref>.</returns>
		public static Matrix3x2 operator -(Matrix3x2 value1, Matrix3x2 value2)
		{
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = value1.M11 - value2.M11;
			result.M12 = value1.M12 - value2.M12;
			result.M21 = value1.M21 - value2.M21;
			result.M22 = value1.M22 - value2.M22;
			result.M31 = value1.M31 - value2.M31;
			result.M32 = value1.M32 - value2.M32;
			return result;
		}

		/// <summary>Returns the matrix that results from multiplying two matrices together.</summary>
		/// <param name="value1">The first matrix.</param>
		/// <param name="value2">The second matrix.</param>
		/// <returns>The product matrix.</returns>
		public static Matrix3x2 operator *(Matrix3x2 value1, Matrix3x2 value2)
		{
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = value1.M11 * value2.M11 + value1.M12 * value2.M21;
			result.M12 = value1.M11 * value2.M12 + value1.M12 * value2.M22;
			result.M21 = value1.M21 * value2.M11 + value1.M22 * value2.M21;
			result.M22 = value1.M21 * value2.M12 + value1.M22 * value2.M22;
			result.M31 = value1.M31 * value2.M11 + value1.M32 * value2.M21 + value2.M31;
			result.M32 = value1.M31 * value2.M12 + value1.M32 * value2.M22 + value2.M32;
			return result;
		}

		/// <summary>Returns the matrix that results from scaling all the elements of a specified matrix by a scalar factor.</summary>
		/// <param name="value1">The matrix to scale.</param>
		/// <param name="value2">The scaling value to use.</param>
		/// <returns>The scaled matrix.</returns>
		public static Matrix3x2 operator *(Matrix3x2 value1, float value2)
		{
			Matrix3x2 result = default(Matrix3x2);
			result.M11 = value1.M11 * value2;
			result.M12 = value1.M12 * value2;
			result.M21 = value1.M21 * value2;
			result.M22 = value1.M22 * value2;
			result.M31 = value1.M31 * value2;
			result.M32 = value1.M32 * value2;
			return result;
		}

		/// <summary>Returns a value that indicates whether the specified matrices are equal.</summary>
		/// <param name="value1">The first matrix to compare.</param>
		/// <param name="value2">The second matrix to compare.</param>
		/// <returns>true if <paramref name="value1">value1</paramref> and <paramref name="value2">value2</paramref> are equal; otherwise, false.</returns>
		public static bool operator ==(Matrix3x2 value1, Matrix3x2 value2)
		{
			if (value1.M11 == value2.M11 && value1.M22 == value2.M22 && value1.M12 == value2.M12 && value1.M21 == value2.M21 && value1.M31 == value2.M31)
			{
				return value1.M32 == value2.M32;
			}
			return false;
		}

		/// <summary>Returns a value that indicates whether the specified matrices are not equal.</summary>
		/// <param name="value1">The first matrix to compare.</param>
		/// <param name="value2">The second matrix to compare.</param>
		/// <returns>true if <paramref name="value1">value1</paramref> and <paramref name="value2">value2</paramref> are not equal; otherwise, false.</returns>
		public static bool operator !=(Matrix3x2 value1, Matrix3x2 value2)
		{
			if (value1.M11 == value2.M11 && value1.M12 == value2.M12 && value1.M21 == value2.M21 && value1.M22 == value2.M22 && value1.M31 == value2.M31)
			{
				return value1.M32 != value2.M32;
			}
			return true;
		}

		/// <summary>Returns a value that indicates whether this instance and another 3x2 matrix are equal.</summary>
		/// <param name="other">The other matrix.</param>
		/// <returns>true if the two matrices are equal; otherwise, false.</returns>
		public bool Equals(Matrix3x2 other)
		{
			if (M11 == other.M11 && M22 == other.M22 && M12 == other.M12 && M21 == other.M21 && M31 == other.M31)
			{
				return M32 == other.M32;
			}
			return false;
		}

		/// <summary>Returns a value that indicates whether this instance and a specified object are equal.</summary>
		/// <param name="obj">The object to compare with the current instance.</param>
		/// <returns>true if the current instance and <paramref name="obj">obj</paramref> are equal; otherwise, false. If <paramref name="obj">obj</paramref> is null, the method returns false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Matrix3x2)
			{
				return Equals((Matrix3x2)obj);
			}
			return false;
		}

		/// <summary>Returns a string that represents this matrix.</summary>
		/// <returns>The string representation of this matrix.</returns>
		public override string ToString()
		{
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			return string.Format(currentCulture, "{{ {{M11:{0} M12:{1}}} {{M21:{2} M22:{3}}} {{M31:{4} M32:{5}}} }}", M11.ToString(currentCulture), M12.ToString(currentCulture), M21.ToString(currentCulture), M22.ToString(currentCulture), M31.ToString(currentCulture), M32.ToString(currentCulture));
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>The hash code.</returns>
		public override int GetHashCode()
		{
			return M11.GetHashCode() + M12.GetHashCode() + M21.GetHashCode() + M22.GetHashCode() + M31.GetHashCode() + M32.GetHashCode();
		}
	}
}
