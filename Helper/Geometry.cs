using System.Numerics;

namespace API.Helper
{
    public static class Geometry
    {
        /// <summary>
        /// Converts a Vector3 to a Vector2. Z is discarded.
        /// </summary>
        public static Vector2 ToVector2(this Vector3 vector)
        {
            return new Vector2(vector.X, vector.Y);
        }

        /// <summary>
        /// Converts a Vector2 to a Vector3. Default z is set to 0.
        /// </summary>
        public static Vector3 ToVector3(this Vector2 vector, float z = 0)
        {
            return new Vector3(vector.X, vector.Y, z);
        }
    }
}