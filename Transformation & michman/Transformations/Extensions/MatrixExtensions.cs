using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opdracht6_Transformations.Extensions
{
    public static class MatrixExtensions
    {
        #region GetColumn
        /// <summary> returns a vector4 representation of the selected column from the given matrix.</summary>
        /// <param name="m">The given matrix.</param>
        /// <param name="column">the 0 based index of the column</param>
        /// <returns>vector4 representation of the selected column.</returns>
        public static Vector4 GetColumn(this Matrix m, byte column)
        {
            switch (column)
            {
                case 0: return new Vector4(m.M11, m.M21, m.M31, m.M41);
                case 1: return new Vector4(m.M12, m.M22, m.M32, m.M42);
                case 2: return new Vector4(m.M13, m.M23, m.M33, m.M43);
                case 3: return new Vector4(m.M14, m.M24, m.M34, m.M44);

                default: throw new Exception("column out of range.");
            }
        }

        /// <summary> returns a vector4 representation of the selected column from the given matrix.</summary>
        /// <param name="m">The given matrix.</param>
        /// <param name="column">the 0 based index of the column</param>
        /// <returns>vector4 representation of the selected column.</returns>
        public static Vector4 GetColumn(this Matrix m, sbyte column)
        {
            switch (column)
            {
                case 0: return new Vector4(m.M11, m.M21, m.M31, m.M41);
                case 1: return new Vector4(m.M12, m.M22, m.M32, m.M42);
                case 2: return new Vector4(m.M13, m.M23, m.M33, m.M43);
                case 3: return new Vector4(m.M14, m.M24, m.M34, m.M44);

                default: throw new Exception("column out of range.");
            }
        }

        /// <summary> returns a vector4 representation of the selected column from the given matrix.</summary>
        /// <param name="m">The given matrix.</param>
        /// <param name="column">the 0 based index of the column</param>
        /// <returns>vector4 representation of the selected column.</returns>
        public static Vector4 GetColumn(this Matrix m, short column)
        {
            switch (column)
            {
                case 0: return new Vector4(m.M11, m.M21, m.M31, m.M41);
                case 1: return new Vector4(m.M12, m.M22, m.M32, m.M42);
                case 2: return new Vector4(m.M13, m.M23, m.M33, m.M43);
                case 3: return new Vector4(m.M14, m.M24, m.M34, m.M44);

                default: throw new Exception("column out of range.");
            }
        }

        /// <summary> returns a vector4 representation of the selected column from the given matrix.</summary>
        /// <param name="m">The given matrix.</param>
        /// <param name="column">the 0 based index of the column</param>
        /// <returns>vector4 representation of the selected column.</returns>
        public static Vector4 GetColumn(this Matrix m, ushort column)
        {
            switch (column)
            {
                case 0: return new Vector4(m.M11, m.M21, m.M31, m.M41);
                case 1: return new Vector4(m.M12, m.M22, m.M32, m.M42);
                case 2: return new Vector4(m.M13, m.M23, m.M33, m.M43);
                case 3: return new Vector4(m.M14, m.M24, m.M34, m.M44);

                default: throw new Exception("column out of range.");
            }
        }

        /// <summary> returns a vector4 representation of the selected column from the given matrix.</summary>
        /// <param name="m">The given matrix.</param>
        /// <param name="column">the 0 based index of the column</param>
        /// <returns>vector4 representation of the selected column.</returns>
        public static Vector4 GetColumn(this Matrix m, int column)
        {
            switch (column)
            {
                case 0: return new Vector4(m.M11, m.M21, m.M31, m.M41);
                case 1: return new Vector4(m.M12, m.M22, m.M32, m.M42);
                case 2: return new Vector4(m.M13, m.M23, m.M33, m.M43);
                case 3: return new Vector4(m.M14, m.M24, m.M34, m.M44);

                default: throw new Exception("column out of range.");
            }
        }

        /// <summary> returns a vector4 representation of the selected column from the given matrix.</summary>
        /// <param name="m">The given matrix.</param>
        /// <param name="column">the 0 based index of the column</param>
        /// <returns>vector4 representation of the selected column.</returns>
        public static Vector4 GetColumn(this Matrix m, uint column)
        {
            switch (column)
            {
                case 0: return new Vector4(m.M11, m.M21, m.M31, m.M41);
                case 1: return new Vector4(m.M12, m.M22, m.M32, m.M42);
                case 2: return new Vector4(m.M13, m.M23, m.M33, m.M43);
                case 3: return new Vector4(m.M14, m.M24, m.M34, m.M44);

                default: throw new Exception("column out of range.");
            }
        }

        /// <summary> returns a vector4 representation of the selected column from the given matrix.</summary>
        /// <param name="m">The given matrix.</param>
        /// <param name="column">the 0 based index of the column</param>
        /// <returns>vector4 representation of the selected column.</returns>
        public static Vector4 GetColumn(this Matrix m, long column)
        {
            switch (column)
            {
                case 0: return new Vector4(m.M11, m.M21, m.M31, m.M41);
                case 1: return new Vector4(m.M12, m.M22, m.M32, m.M42);
                case 2: return new Vector4(m.M13, m.M23, m.M33, m.M43);
                case 3: return new Vector4(m.M14, m.M24, m.M34, m.M44);

                default: throw new Exception("column out of range.");
            }
        }

        /// <summary> returns a vector4 representation of the selected column from the given matrix.</summary>
        /// <param name="m">The given matrix.</param>
        /// <param name="column">the 0 based index of the column</param>
        /// <returns>vector4 representation of the selected column.</returns>
        public static Vector4 GetColumn(this Matrix m, ulong column)
        {
            switch (column)
            {
                case 0: return new Vector4(m.M11, m.M21, m.M31, m.M41);
                case 1: return new Vector4(m.M12, m.M22, m.M32, m.M42);
                case 2: return new Vector4(m.M13, m.M23, m.M33, m.M43);
                case 3: return new Vector4(m.M14, m.M24, m.M34, m.M44);

                default: throw new Exception("column out of range.");
            }
        }
        #endregion

        #region GetRow
        /// <summary> returns a vector4 representation of the selected row from the given matrix.</summary>
        /// <param name="m">The given matrix.</param>
        /// <param name="row">the 0 based index of the row</param>
        /// <returns>vector4 representation of the selected row.</returns>
        public static Vector4 GetRow(this Matrix m, byte row)
        {
            switch (row)
            {
                case 0: return new Vector4(m.M11, m.M12, m.M13, m.M14);
                case 1: return new Vector4(m.M21, m.M22, m.M23, m.M24);
                case 2: return new Vector4(m.M31, m.M32, m.M33, m.M34);
                case 3: return new Vector4(m.M41, m.M42, m.M43, m.M44);

                default: throw new Exception("row out of range.");
            }
        }

        /// <summary> returns a vector4 representation of the selected row from the given matrix.</summary>
        /// <param name="m">The given matrix.</param>
        /// <param name="row">the 0 based index of the row</param>
        /// <returns>vector4 representation of the selected row.</returns>
        public static Vector4 GetRow(this Matrix m, sbyte row)
        {
            switch (row)
            {
                case 0: return new Vector4(m.M11, m.M12, m.M13, m.M14);
                case 1: return new Vector4(m.M21, m.M22, m.M23, m.M24);
                case 2: return new Vector4(m.M31, m.M32, m.M33, m.M34);
                case 3: return new Vector4(m.M41, m.M42, m.M43, m.M44);

                default: throw new Exception("row out of range.");
            }
        }

        /// <summary> returns a vector4 representation of the selected row from the given matrix.</summary>
        /// <param name="m">The given matrix.</param>
        /// <param name="row">the 0 based index of the row</param>
        /// <returns>vector4 representation of the selected row.</returns>
        public static Vector4 GetRow(this Matrix m, short row)
        {
            switch (row)
            {
                case 0: return new Vector4(m.M11, m.M12, m.M13, m.M14);
                case 1: return new Vector4(m.M21, m.M22, m.M23, m.M24);
                case 2: return new Vector4(m.M31, m.M32, m.M33, m.M34);
                case 3: return new Vector4(m.M41, m.M42, m.M43, m.M44);

                default: throw new Exception("row out of range.");
            }
        }

        /// <summary> returns a vector4 representation of the selected row from the given matrix.</summary>
        /// <param name="m">The given matrix.</param>
        /// <param name="row">the 0 based index of the row</param>
        /// <returns>vector4 representation of the selected row.</returns>
        public static Vector4 GetRow(this Matrix m, ushort row)
        {
            switch (row)
            {
                case 0: return new Vector4(m.M11, m.M12, m.M13, m.M14);
                case 1: return new Vector4(m.M21, m.M22, m.M23, m.M24);
                case 2: return new Vector4(m.M31, m.M32, m.M33, m.M34);
                case 3: return new Vector4(m.M41, m.M42, m.M43, m.M44);

                default: throw new Exception("row out of range.");
            }
        }

        /// <summary> returns a vector4 representation of the selected row from the given matrix.</summary>
        /// <param name="m">The given matrix.</param>
        /// <param name="row">the 0 based index of the row</param>
        /// <returns>vector4 representation of the selected row.</returns>
        public static Vector4 GetRow(this Matrix m, int row)
        {
            switch (row)
            {
                case 0: return new Vector4(m.M11, m.M12, m.M13, m.M14);
                case 1: return new Vector4(m.M21, m.M22, m.M23, m.M24);
                case 2: return new Vector4(m.M31, m.M32, m.M33, m.M34);
                case 3: return new Vector4(m.M41, m.M42, m.M43, m.M44);

                default: throw new Exception("row out of range.");
            }
        }

        /// <summary> returns a vector4 representation of the selected row from the given matrix.</summary>
        /// <param name="m">The given matrix.</param>
        /// <param name="row">the 0 based index of the row</param>
        /// <returns>vector4 representation of the selected row.</returns>
        public static Vector4 GetRow(this Matrix m, uint row)
        {
            switch (row)
            {
                case 0: return new Vector4(m.M11, m.M12, m.M13, m.M14);
                case 1: return new Vector4(m.M21, m.M22, m.M23, m.M24);
                case 2: return new Vector4(m.M31, m.M32, m.M33, m.M34);
                case 3: return new Vector4(m.M41, m.M42, m.M43, m.M44);

                default: throw new Exception("row out of range.");
            }
        }

        /// <summary> returns a vector4 representation of the selected row from the given matrix.</summary>
        /// <param name="m">The given matrix.</param>
        /// <param name="row">the 0 based index of the row</param>
        /// <returns>vector4 representation of the selected row.</returns>
        public static Vector4 GetRow(this Matrix m, long row)
        {
            switch (row)
            {
                case 0: return new Vector4(m.M11, m.M12, m.M13, m.M14);
                case 1: return new Vector4(m.M21, m.M22, m.M23, m.M24);
                case 2: return new Vector4(m.M31, m.M32, m.M33, m.M34);
                case 3: return new Vector4(m.M41, m.M42, m.M43, m.M44);

                default: throw new Exception("row out of range.");
            }
        }

        /// <summary> returns a vector4 representation of the selected row from the given matrix.</summary>
        /// <param name="m">The given matrix.</param>
        /// <param name="row">the 0 based index of the row</param>
        /// <returns>vector4 representation of the selected row.</returns>
        public static Vector4 GetRow(this Matrix m, ulong row)
        {
            switch (row)
            {
                case 0: return new Vector4(m.M11, m.M12, m.M13, m.M14);
                case 1: return new Vector4(m.M21, m.M22, m.M23, m.M24);
                case 2: return new Vector4(m.M31, m.M32, m.M33, m.M34);
                case 3: return new Vector4(m.M41, m.M42, m.M43, m.M44);

                default: throw new Exception("row out of range.");
            }
        }
        #endregion
    }
}
