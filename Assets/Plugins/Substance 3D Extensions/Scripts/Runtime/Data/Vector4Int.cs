using UnityEngine;
using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace SOS.SubstanceExtensions
{
	[System.Serializable]
    public struct Vector4Int : IEquatable<Vector4Int>, IFormattable
	{
		private static readonly Vector4Int zeroVector = new Vector4Int(0, 0, 0, 0);
		private static readonly Vector4Int oneVector = new Vector4Int(1, 1, 1, 1);

		/// <summary>
		/// X component of the vector.
		/// </summary>
		public int x;
        /// <summary>
        /// Y component of the vector.
        /// </summary>
        public int y;
        /// <summary>
        /// Z component of the vector.
        /// </summary>
        public int z;
        /// <summary>
        /// W component of the vector.
        /// </summary>
        public int w;

		public int this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				switch(index)
                {
					case 0: return x;
					case 1: return y;
					case 2: return z;
					case 3: return w;
                }

				throw new IndexOutOfRangeException("Invalid Vector4Int index!");
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				switch(index)
				{
					case 0:
						x = value;
						break;
					case 1:
						y = value;
						break;
					case 2:
						z = value;
						break;
					case 3:
						w = value;
						break;
					default:
						throw new IndexOutOfRangeException("Invalid Vector4Int index!");
				}
			}
		}

		/// <summary>
		/// Shorthand for writing Vector4Int(0, 0, 0, 0).
		/// </summary>
		public static Vector4Int zero
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get { return zeroVector; }
		}

		/// <summary>
		/// Shorthand for writing Vector4Int(1, 1, 1, 1).
		/// </summary>
		public static Vector4Int one
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get { return oneVector; }
		}

		/// <summary>
		/// Returns the length of this vector (Read Only).
		/// </summary>
		public float magnitude
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get { return (float)Math.Sqrt(x * x + y * y + z * z + w * w); }
		}

		/// <summary>
		/// Returns the squared length of this vector (Read Only).
		/// </summary>
		public float sqrMagnitude
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get { return x * x + y * y + z * z + w * w; }
        }

        /// <summary>
        /// Creates a new vector with the given int array.
        /// </summary>
        /// <param name="values">Array containing vector values.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector4Int(int[] values)
        {
			if(values.Length >= 4)
			{
                x = values[0];
                y = values[1];
                z = values[2];
                w = values[3];
				return;
            }

			switch(values.Length)
			{
				case 3:
					x = values[0];
                    y = values[1];
                    z = values[2];
					w = 0;
                    break;
				case 2:
                    x = values[0];
                    y = values[1];
					z = 0;
					w = 0;
                    break;
                case 1:
                    x = values[0];
                    y = 0;
                    z = 0;
                    w = 0;
                    break;
                default:
                    x = 0;
                    y = 0;
                    z = 0;
                    w = 0;
                    break;
            }
        }

        /// <summary>
        /// Creates a new vector with the given x, y, z, w components.
        /// </summary>
        /// <param name="x">X component of the vector.</param>
        /// <param name="y">Y component of the vector.</param>
        /// <param name="z">Z component of the vector.</param>
        /// <param name="w">W component of the vector.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector4Int(int x, int y, int z, int w)
        {
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		/// <summary>
		/// Creates a new vector with the given x, y, z components and sets w to 0.
		/// </summary>
		/// <param name="x">X component of the vector.</param>
		/// <param name="y">Y component of the vector.</param>
		/// <param name="z">Z component of the vector.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector4Int(int x, int y, int z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = 0;
		}

		/// <summary>
		/// Creates a new vector with the given x, y components and sets z, w to 0.
		/// </summary>
		/// <param name="x">X component of the vector.</param>
		/// <param name="y">Y component of the vector.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector4Int(int x, int y)
		{
			this.x = x;
			this.y = y;
			this.z = 0;
			this.w = 0;
		}

		/// <summary>
		/// Creates a new vector with the given x component. If floodFill is true, y, z, w will be set to match x, otherwise they will be 0.
		/// </summary>
		/// <param name="x">X component of the vector.</param>
		/// <param name="floodFill">If true, y, z, w will be set to match x, otherwise they will be 0.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector4Int(int x, bool floodFill=false)
		{
			this.x = x;

			if(floodFill)
			{
				y = x;
				z = x;
				w = x;
			}
			else
			{
				y = 0;
				z = 0;
				w = 0;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4Int Min(Vector4Int a, Vector4Int b)
		{
			return new Vector4Int(Mathf.Min(a.x, b.x), Mathf.Min(a.y, b.y), Mathf.Min(a.z, b.z), Mathf.Min(a.w, b.w));
		}


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4Int Max(Vector4Int a, Vector4Int b)
		{
			return new Vector4Int(Mathf.Max(a.x, b.x), Mathf.Max(a.y, b.y), Mathf.Max(a.z, b.z), Mathf.Max(a.w, b.w));
		}


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4Int Scale(Vector4Int a, Vector4Int b)
		{
			return new Vector4Int(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
		}

        /// <summary>
        /// Converts a Vector4 to a Vector4Int by doing a Floor to each value.
        /// </summary>
        /// <param name="v">Vector to perform a floor operation on.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4Int FloorToInt(Vector4 v)
		{
			return new Vector4Int(Mathf.FloorToInt(v.x), Mathf.FloorToInt(v.y), Mathf.FloorToInt(v.z), Mathf.FloorToInt(v.w));
		}

		/// <summary>
		/// Converts a Vector4 to a Vector4Int by doing a Ceiling to each value.
		/// </summary>
		/// <param name="v">Vector to perform a ceiling operation on.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4Int CeilToInt(Vector4 v)
		{
			return new Vector4Int(Mathf.CeilToInt(v.x), Mathf.CeilToInt(v.y), Mathf.CeilToInt(v.z), Mathf.CeilToInt(v.w));
		}

		/// <summary>
		/// Converts a Vector4 to a Vector4Int by doing a Round to each value.
		/// </summary>
		/// <param name="v">Vector to round.</param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4Int RoundToInt(Vector4 v)
		{
			return new Vector4Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), Mathf.RoundToInt(v.z), Mathf.RoundToInt(v.w));
		}


		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Set(int newX, int newY, int newZ, int newW)
        {
			x = newX;
			y = newY;
			z = newZ;
			w = newW;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Scale(Vector4Int scale)
        {
			x *= scale.x;
			y *= scale.y;
			z *= scale.z;
			w *= scale.w;
        }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Clamp(Vector4Int min, Vector4Int max)
		{
			x = Math.Max(min.x, x);
			x = Math.Min(max.x, x);
			y = Math.Max(min.y, y);
			y = Math.Min(max.y, y);
			z = Math.Max(min.z, z);
			z = Math.Min(max.z, z);
			w = Math.Max(min.w, w);
			w = Math.Min(max.w, w);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Vector4(Vector4Int v)
		{
			return new Vector4(v.x, v.y, v.z, v.w);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator Vector3Int(Vector4Int v)
		{
			return new Vector3Int(v.x, v.y, v.z);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator Vector2Int(Vector4Int v)
		{
			return new Vector2Int(v.x, v.y);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4Int operator +(Vector4Int a, Vector4Int b)
		{
			return new Vector4Int(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4Int operator -(Vector4Int a, Vector4Int b)
		{
			return new Vector4Int(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4Int operator *(Vector4Int a, Vector4Int b)
		{
			return new Vector4Int(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4Int operator -(Vector4Int a)
		{
			return new Vector4Int(-a.x, -a.y, -a.z, -a.w);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4Int operator *(Vector4Int a, int b)
		{
			return new Vector4Int(a.x * b, a.y * b, a.z * b, a.w * b);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4Int operator *(int a, Vector4Int b)
		{
			return new Vector4Int(a * b.x, a * b.y, a * b.z, a * b.w);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector4Int operator /(Vector4Int a, int b)
		{
			return new Vector4Int(a.x / b, a.y / b, a.z / b, a.x / b);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Vector4Int lhs, Vector4Int rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z && lhs.w == rhs.w;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Vector4Int lhs, Vector4Int rhs)
		{
			return !(lhs == rhs);
		}

		/// <summary>
		/// Returns true if the objects are equal.
		/// </summary>
		/// <param name="other"></param>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object other)
		{
			if(!(other is Vector4Int))
			{
				return false;
			}
			return Equals((Vector4Int)other);
		}

		/// <summary>
		/// Returns true if the given vector is exactly equal to this vector.
		/// </summary>
		/// <param name="other"></param>\
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(Vector4Int other)
		{
			return this == other;
		}

		/// <summary>
		/// Gets the hash code for the Vector4Int.
		/// </summary>
		/// <returns>
		/// The hash code of the Vector4Int.
		/// </returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return x.GetHashCode() ^ (y.GetHashCode() << 2) ^ (z.GetHashCode() >> 2) ^ (w.GetHashCode() >> 1);
		}

		/// <summary>
		/// Returns a formatted string for this vector.
		/// </summary>
		public override string ToString()
		{
			return ToString(null, null);
		}

		/// <summary>
		/// Returns a formatted string for this vector.
		/// </summary>
		/// <param name="format">A numeric format string.</param>
		public string ToString(string format)
		{
			return ToString(format, null);
		}

		/// <summary>
		/// Returns a formatted string for this vector.
		/// </summary>
		/// <param name="format">A numeric format string.</param>
		/// <param name="formatProvider">An object that specifies culture-specific formatting.</param>
		public string ToString(string format, IFormatProvider formatProvider)
		{
			if(formatProvider == null)
			{
				formatProvider = CultureInfo.InvariantCulture.NumberFormat;
			}

			return string.Format("({0}, {1}, {2}, {3})", x.ToString(format, formatProvider), y.ToString(format, formatProvider), z.ToString(format, formatProvider), w.ToString(format, formatProvider));
		}
	}
}