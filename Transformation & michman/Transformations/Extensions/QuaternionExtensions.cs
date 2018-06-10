using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Opdracht4_Collision
{
    public static class QuanternionExtensions
    {
        /// <summary> Returns the euler angle of this rotation quaternion in roll,pitch and yaw.</summary>
        /// <param name="quat">rotation</param>
        /// <returns>rotation vector</returns>
        public static Vector3 ToEulerAngle(this Quaternion quat)
        {
            //roll (X)
            double sinr = 2 * (quat.W * quat.X + quat.Y * quat.Z);
            double cosr = 1 - 2 * (quat.X * quat.X + quat.Y * quat.Y);

            double roll = Math.Atan2(sinr, cosr);


            //pitch (Y)
            double pitch;
            double sinp = 2 * (quat.W * quat.Y - quat.Z * quat.X);
            if (sinp > 1)
                sinp = 1;
            else if (sinp < -1)
                sinp = -1;

            pitch = Math.Asin(sinp);


            //yaw (Z)
            double siny = 2 * (quat.W * quat.Z + quat.X * quat.Y);
            double cosy = 1 - 2 * (quat.Y * quat.Y + quat.Z * quat.Z);

            double yaw = Math.Atan2(siny, cosy);

            return new Vector3((float)roll, (float)pitch, (float)yaw);
        }

        /// <summary> Returns the euler angle of this rotation quaternion in roll,pitch and yaw.</summary>
        /// <param name="quat">rotation</param>
        /// <param name="roll">X-axis rotation</param>
        /// <param name="pitch">Y-axis rotation</param>
        /// <param name="yaw">Z-axis rotation</param>
        /// <returns>rotation vector</returns>
        public static Vector3 ToEulerAngle(this Quaternion quat, ref double roll, ref double pitch, ref double yaw)
        {
            //roll (X)
            double sinr = 2 * (quat.W * quat.X + quat.Y * quat.Z);
            double cosr = 1 - 2 * (quat.X * quat.X + quat.Y * quat.Y);

            roll = Math.Atan2(sinr, cosr);


            //pitch (Y)
            double sinp = 2 * (quat.W * quat.Y - quat.Z * quat.X);
            if (sinp > 1)
                sinp = 1;
            else if (sinp < -1)
                sinp = -1;

            pitch = Math.Asin(sinp);


            //yaw (Z)
            double siny = 2 * (quat.W * quat.Z + quat.X * quat.Y);
            double cosy = 1 - 2 * (quat.Y * quat.Y + quat.Z * quat.Z);

            yaw = Math.Atan2(siny, cosy);

            return new Vector3((float)roll, (float)pitch, (float)yaw);
        }

        public static Quaternion QuaternionFromMatrix(Matrix m)
        {
            
        }

        /// <summary>creates a look rotation quaternion based on direction is looked at</summary>
        /// <param name="forwards">the looking direction</param>
        /// <param name="upwards">defines the "up" direction</param>
        /// <returns>rotation quaternion looking in the given direction</returns>
        /// <remarks>Based on: https://github.com/Unity-Technologies/Unity.Mathematics/blob/5d2fa6687d008f9f538df553d9168bb6873197d7/src/Unity.Mathematics/mathquat.cs#L166 </remarks>
        public static Quaternion LookRotation(this Quaternion quat,Vector3 forwards,Vector3? upwards = null)
        {
            float[][] matrix3x3 = new float[][] { new float[3], new float[3], new float[3] };

            //create the 3x3 matrix values
            Vector3 vectorRow1 = Vector3.Normalize(forwards);
            Vector3 vectorRow2 = Vector3.Cross(upwards ?? Vector3.Up,vectorRow1);
            Vector3 vectorRow3 = Vector3.Cross(vectorRow1, vectorRow2);

            //fill in the matrix
            matrix3x3[0][0] = vectorRow1.X;
            matrix3x3[0][1] = vectorRow1.Y;
            matrix3x3[0][2] = vectorRow1.Z;
            matrix3x3[1][0] = vectorRow2.X;
            matrix3x3[1][1] = vectorRow2.Y;
            matrix3x3[1][2] = vectorRow2.Z;
            matrix3x3[2][0] = vectorRow3.X;
            matrix3x3[2][1] = vectorRow3.Y;
            matrix3x3[2][2] = vectorRow3.Z;

            float diagonalSum = matrix3x3[0][0] + matrix3x3[1][1] + matrix3x3[2][2];

            if(diagonalSum > 0.0)
            {
                //todo: find good names
                float foo = (float)Math.Sqrt(diagonalSum + 1);
                float bar = 0.5f / foo;

                return new Quaternion(
                    (matrix3x3[1][2] - matrix3x3[2][1]) * bar,
                    (matrix3x3[2][0] - matrix3x3[0][2]) * bar,
                    (matrix3x3[0][1]-matrix3x3[1][0]) * bar,
                    foo * 0.5f
                    );
            }

            // is 00 the biggest diagonal?
            if((matrix3x3[0][0] >= matrix3x3[1][1]) && (matrix3x3[0][0] >= matrix3x3[2][2]))
            {
                //todo: find good names
                float foo = (float)Math.Sqrt((1 + matrix3x3[0][0]) - matrix3x3[1][1] - matrix3x3[2][2]);
                float bar = 0.5f / foo;

                return new Quaternion(
                    0.5f * foo,
                    (matrix3x3[0][1] + matrix3x3[1][0]) * bar,
                    (matrix3x3[0][2] + matrix3x3[2][0]) * bar,
                    (matrix3x3[1][2] -matrix3x3[2][1]) *bar
                    );
            }

            // is 11 or 22 the biggest diagonal?
            if(matrix3x3[1][1] > matrix3x3[2][2])
            {
                float foo = (float)Math.Sqrt((1 + matrix3x3[1][1]) - matrix3x3[0][0] - matrix3x3[2][2]);
                float bar = 0.5f / foo;

                return new Quaternion(
                    (matrix3x3[0][0] + matrix3x3[0][0]) * bar;
                    );
            }

            // 22 is the biggest diagonal
            return new Quaternion();
        }

        public static Matrix GetRotationMatrix(this Quaternion quat)
        {
            return Matrix.ro
        }
    }
}
