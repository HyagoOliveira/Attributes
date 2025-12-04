using UnityEngine;

namespace ActionCode.Attributes
{
    /// <summary>
    /// Extension class for Vectors 2
    /// </summary>
    public static class Vector2Extension
    {
        /// <summary>
        /// Returns a random integer value between the interval X - Y. Both X and Y are inclusive.
        /// </summary>
        /// <param name="interval"></param>
        /// <returns></returns>
    	public static int GetRandomInt(this Vector2 interval) => Random.Range((int)interval.x, (int)interval.y + 1);

        /// <summary>
        /// Returns a random value between the interval X - Y. Both X and Y are inclusive.
        /// </summary>
        /// <param name="interval"></param>
        /// <returns></returns>
    	public static float GetRandom(this Vector2 interval) => Random.Range(interval.x, interval.y);
    }
}